using ClosedXML.Excel;

using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using System.IO;
using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO;
using TourismFormsAPI.ModelsDTO.Requests;
using System.Collections.ObjectModel;
using System.Drawing;

namespace TourismFormsAPI.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly TourismContext _context;
        private readonly IFormRepository _iFormRepository;

        public SurveyRepository(TourismContext context, IFormRepository iFormRepository)
        {
            _context = context;
            _iFormRepository = iFormRepository;
        }

        #region GET
        public List<Survey> GetAll()
        {
            return LoadData(_context.Surveys).ToList();
        }
        public Survey? GetById(int id)
        {
            return _context.Surveys.FirstOrDefault(s => s.Id == id);
        }
        public Task<byte[]> GetExcel(int id)
        {
            try
            {
                var survey = _context.Surveys.Where(s => s.Id == id).Include(s => s.Answers).FirstOrDefault();
               
                if (survey is not null)
                {
                    survey.Form = _iFormRepository.GetById(survey.FormId)!;
                    var criterias = survey.Form.Criterias;
                    var answers = _context.Answers.Where(a => a.SurveyId == id).ToList();
                    int rowIndex = 5;
                    using (var workBook = new XLWorkbook())
                    {
                        var ws = workBook.Worksheets.Add(survey.Form.Name);
                        ws.Cell("A1").Value = "Дата формирования отчета:";
                        ws.Cell("B1").Value = DateTime.Now;
                        ws.Cell("A2").Value = "Город:";
                        ws.Cell("B2").Value = survey.CityName;
                        ws.Cell("A3").Value = "Муниципалитет:";
                        ws.Cell("B3").Value = survey.MunicipalityName;

                        ws.Cell("A4").Value = "№ п/п";
                        ws.Cell("A4").Style.Font.Bold = true;
                        ws.Cell("B4").Value = "Критерий";
                        ws.Cell("B4").Style.Font.Bold = true;
                        ws.Cell("C4").Value = "Показатель";
                        ws.Cell("C4").Style.Font.Bold = true;
                        ws.Cell("D4").Value = "Единицы измерения";
                        ws.Cell("D4").Style.Font.Bold = true;
                        ws.Cell("E4").Value = "Значение показателей (с перечислением объектов)";
                        ws.Cell("E4").Style.Font.Bold = true;
                        ws.Cell("F4").Value = "Период проставления данных";
                        ws.Cell("F4").Style.Font.Bold = true;
                        ws.Cell("G4").Value = "Оценка";
                        ws.Cell("G4").Style.Font.Bold = true;


                        foreach (var criteria in criterias)
                        {
                            foreach (var question in criteria.Questions)
                            {
                                var answer = answers.FirstOrDefault(a => a.QuestionId == question.Id);
                                ws.Cell($"A{rowIndex}").Value = criteria.Sequence;
                                ws.Cell($"B{rowIndex}").Value = criteria.Name;
                                ws.Cell($"C{rowIndex}").Value = question.Name;
                                ws.Cell($"D{rowIndex}").Value = question.FillMethod == null ? "" : question.FillMethod.Name;
                                ws.Cell($"E{rowIndex}").Value = answer == null ? "" : answer.Text;
                                ws.Cell($"F{rowIndex}").Value = question.Hint;
                                ws.Cell($"G{rowIndex}").Value = answer.Score;
                                rowIndex++;
                            }
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
        public List<Survey> GetMyAll(int municipalityId)
        {
            return LoadData(_context.Surveys.Where(s => s.MunicipalityId == municipalityId)).ToList();
        }
        public Form? GetMyById(int id, int municipalityId)
        {
            var survey = _context.Surveys.FirstOrDefault(s => s.Id == id && s.MunicipalityId == municipalityId);
            if (survey is not null)
                return _iFormRepository.GetById(survey.FormId);

            throw new Exception();
        }
        private IQueryable<Survey> LoadData(IQueryable<Survey> items)
        {
            return items
                .Select(x => new SurveyDTO(x)
                {
                    Form = new FormDTO(x.Form)
                });
        }
        #endregion

        #region POST
        public Task CreateArray(SurveyPost[] body)
        {
            try
            {
                foreach (var item in body)
                {
                    Survey itemToCreate = new Survey()
                    {
                        FormId = item.FormId,
                        AppointmentDate = DateTime.Now,
                        StartDate = item.StartDate,
                        CompletionDate = null,
                        EndDate = item.EndDate,
                        Completed = false,
                        MunicipalityName = _context.Municipalities.FirstOrDefault(m => m.Id == item.MunicipalityId).Name,
                        MunicipalityId = item.MunicipalityId,
                        CityName = _context.Cities.FirstOrDefault(c => c.Id == item.CityId).Name,
                        CityId = item.CityId,
                    };
                    _context.Surveys.Add(itemToCreate);
                }
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }
        #endregion

        #region PUT
        public Survey UpdateMy(int municipalityId, SurveyPut body)
        {
            var itemToUpdate = _context.Surveys.FirstOrDefault(s => s.Id == body.Id && s.MunicipalityId == municipalityId);
            if (itemToUpdate is not null)
            {
                itemToUpdate.FormId = body.FormId;
                itemToUpdate.AppointmentDate = body.AppointmentDate;
                itemToUpdate.StartDate = body.StartDate;
                itemToUpdate.CompletionDate = body.CompletionDate;
                itemToUpdate.EndDate = body.EndDate;
                itemToUpdate.MunicipalityName = body.MunicipalityName;
                itemToUpdate.MunicipalityId = body.MunicipalityId;
                itemToUpdate.CityName = body.CityName;
                itemToUpdate.CityId = body.CityId;

                _context.Surveys.Update(itemToUpdate);
                _context.SaveChanges();
                return itemToUpdate;
            }
            throw new Exception();
        }
        public async Task SubmitForEvaluation(int id)
        {
            try
            {
                var itemToUpdate = _context.Surveys.FirstOrDefaultAsync(s => s.Id == id).Result;
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Completed = true;
                    itemToUpdate.CompletionDate = DateTime.Now;
                    _context.Surveys.Update(itemToUpdate);
                    await _context.SaveChangesAsync();
                }
                else
                    throw new Exception("NotFound");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task Extend(int id, DateTime newCompletionDate)
        {
            try
            {
                var itemToUpdate = _context.Surveys.FirstOrDefault(s => s.Id == id);
                if (itemToUpdate is not null)
                {
                    itemToUpdate.CompletionDate = newCompletionDate;
                    _context.Surveys.Update(itemToUpdate);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                else
                    return Task.FromException(new Exception("NotFound"));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task Revision(SurveyRevisionPut body)
        {
            try
            {
                var itemToUpdate = _context.Surveys.FirstOrDefaultAsync(s => s.Id == body.Id).Result;
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Comment = body.Comment;
                    itemToUpdate.CompletionDate = body.CompletionDate;
                    itemToUpdate.Completed = false;
                    _context.Surveys.Update(itemToUpdate);
                    await _context.SaveChangesAsync();
                }
                else
                    throw new Exception("NotFound");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            var itemToDelete = _context.Surveys.FirstOrDefault(m => m.Id == id);
            if (itemToDelete is not null)
            {
                var answersToDelete = _context.Answers.Where(a => a.SurveyId == id).ToList();
                _context.Answers.RemoveRange(answersToDelete);
                _context.Surveys.Remove(itemToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }
        #endregion

    }
}
