using System.Data;
using System.Linq;

using ClosedXML.Excel;

using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO;
using TourismFormsAPI.ModelsDTO.Requests;
using TourismFormsAPI.Services;

namespace TourismFormsAPI.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly TourismContext _context;

        public FormRepository(TourismContext context)
        {
            _context = context;
        }

        #region GET

        public List<Form> GetAll()
        {
            return _context.Forms.ToList();
        }

        public Form? GetById(int id)
        {
            try
            {
                Form form = _context.Forms.FirstOrDefault(form => form.Id == id);
                if (form is not null)
                {
                    form = _context.Forms
                                .Where(form => form.Id == id)
                                .Select(form => new Form
                                {
                                    Id = form.Id,
                                    Name = form.Name,
                                    CreationDate = form.CreationDate,
                                    ModifiedDate = form.ModifiedDate,
                                    Surveys = new List<Survey>(),
                                    Criterias = form.Criterias.Select(criteria => new Criteria
                                    {
                                        Id = criteria.Id,
                                        Name = criteria.Name,
                                        FormId = criteria.FormId,
                                        Sequence = criteria.Sequence,
                                        Questions = criteria.Questions.Where(q => q.CriteriaId == criteria.Id).Select(question => new Question
                                        {
                                            Id = question.Id,
                                            Name = question.Name,
                                            Hint = question.Hint,
                                            CriteriaId = question.CriteriaId,
                                            Sequence = question.Sequence,
                                            Formula = question.Formula,
                                            MeasureId = question.MeasureId,
                                            Hidden = question.Hidden,
                                            FillMethodId = question.FillMethodId,
                                            FillMethod = question.FillMethod != null ? new FillMethod
                                            {
                                                Id = question.FillMethod.Id,
                                                Name = question.FillMethod.Name,
                                                Hint = question.FillMethod.Hint
                                            } : null,
                                            Measure = question.Measure != null ? new Measure
                                            {
                                                Id = question.Measure.Id,
                                                Name = question.Measure.Name
                                            } : null,
                                            Answers = question.Answers.Where(a => a.QuestionId == question.Id).ToList(),
                                        }).ToList()
                                    }).ToList()
                                })
                                .FirstOrDefault()!;
                    return form;
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Form? GetById(int id, int surveyId)
        {
            try
            {
                Form form = _context.Forms.FirstOrDefault(form => form.Id == id);
                if (form is not null)
                {
                    form = _context.Forms
                                .Where(form => form.Id == id)
                                .Select(form => new Form
                                {
                                    Id = form.Id,
                                    Name = form.Name,
                                    CreationDate = form.CreationDate,
                                    ModifiedDate = form.ModifiedDate,
                                    Surveys = new List<Survey>(),
                                    Criterias = form.Criterias.OrderBy(c => c.Sequence).Select(criteria => new Criteria
                                    {
                                        Id = criteria.Id,
                                        Name = criteria.Name,
                                        FormId = criteria.FormId,
                                        Sequence = criteria.Sequence,
                                        Questions = criteria.Questions.Where(q => q.CriteriaId == criteria.Id).OrderBy(q => q.Sequence).Select(question => new Question
                                        {
                                            Id = question.Id,
                                            Name = question.Name,
                                            Hint = question.Hint,
                                            CriteriaId = question.CriteriaId,
                                            Sequence = question.Sequence,
                                            Formula = question.Formula,
                                            MeasureId = question.MeasureId,
                                            Hidden = question.Hidden,
                                            FillMethodId = question.FillMethodId,
                                            FillMethod = question.FillMethod != null ? new FillMethod
                                            {
                                                Id = question.FillMethod.Id,
                                                Name = question.FillMethod.Name,
                                                Hint = question.FillMethod.Hint
                                            } : null,
                                            Measure = question.Measure != null ? new Measure
                                            {
                                                Id = question.Measure.Id,
                                                Name = question.Measure.Name
                                            } : null,
                                            Answers = question.Answers.Where(a => a.QuestionId == question.Id && a.SurveyId == surveyId).ToList(),
                                        }).ToList()
                                    }).ToList()
                                })
                                .FirstOrDefault()!;
                    return form;
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Task<byte[]> GetExcel(int id)
        {
            try
            {
                var form = GetById(id);
                var surveys = _context.Surveys.Where(s => s.FormId == id).Include(s => s.Answers).Include(s => s.City).Include(s => s.Form).ToList();
                if (surveys is not null && form is not null)
                {
                    int columnIndexForGroup = 0;
                    int rowIndexForGroup = 3;
                    List<double> sums = new List<double>();
                    using (var workBook = new XLWorkbook())
                    {
                        var ws = workBook.Worksheets.Add($"{form.Name}");

                        ws.Cell("A1").Value = "Дата формирования отчета:";
                        ws.Cell("B1").Value = DateTime.Now;
                        ws.Cell("A2").Value = "Город";

                        int rowIndex = 3;
                        int columnIndex = 1;
                        foreach (var survey in surveys)
                        {
                            ws.Cell(rowIndex++, columnIndex).Value = survey.CityName;
                        }

                        rowIndex = 2;
                        columnIndex = 2;
                        foreach (var criteria in form.Criterias)
                        {
                            foreach (var question in criteria.Questions)
                            {
                                ws.Cell(rowIndex, columnIndex++).Value = $"{criteria.Sequence}.{question.Sequence}";
                            }
                        }
                        ws.Cell(rowIndex, columnIndex++).Value = "Оценка";
                        ws.Cell(rowIndex, columnIndex++).Value = "Группа";

                        
                        rowIndex = 3;
                        columnIndex = 2;
                        double sum = 0;
                        foreach(var survey in surveys)
                        {
                            sum = 0;
                            foreach (var criteria in form.Criterias )
                            {
                                foreach (var question in criteria.Questions)
                                {
                                    var answer = survey.Answers.FirstOrDefault(a => a.SurveyId == survey.Id && a.QuestionId == question.Id);
                                    if (answer is not null)
                                    {
                                        ws.Cell(rowIndex, columnIndex++).Value = answer.Score;
                                        if(answer.Score is not null)
                                            sum += (double)answer.Score;
                                    }

                                }
                            }
                            var answers = survey.Answers.Where(a => a.SurveyId == survey.Id).ToList();
                            
                            ws.Cell(rowIndex, columnIndex++).Value = sum;
                            sums.Add(sum);
                            columnIndexForGroup = columnIndex;
                            columnIndex = 2;
                            rowIndex++;
                        }

                        KMeans kmeans = new KMeans(sums);
                        for (int i = 0; i < surveys.Count; i++ )
                        {
                            var q = sums[i];
                            ws.Cell(rowIndexForGroup++, columnIndexForGroup).Value = kmeans.FindClusterIndex(q);
                        }

                        using (var stream = new MemoryStream())
                        {
                            workBook.SaveAs(stream);
                            var content = stream.ToArray();

                            return Task.FromResult(content);
                        }
                    }
                }
                else
                    throw new Exception("NotFound");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IQueryable<Form> LoadData(IQueryable<Form> items)
        {
            return items
                .Select(x => new FormDTO(x)
                {
                  
                });
        }
        #endregion

        #region POST
        public Form Create(FormPost body)
        {
            try
            {
                var itemToCreate = new Form()
                {
                    Name = body.Name,
                    CreationDate = DateTime.Now
                };
                _context.Forms.Add(itemToCreate);
                _context.SaveChanges();
                return itemToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UPDATE
        public Task Update(FormPut body)
        {
            try
            {
                var itemToUpdate = _context.Forms.FirstOrDefault(x => x.Id == body.Id);
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Name = body.Name;
                    itemToUpdate.ModifiedDate = DateTime.Now;
                    _context.Forms.Update(itemToUpdate);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
