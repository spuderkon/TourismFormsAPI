using System.Linq;

using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO;
using TourismFormsAPI.ModelsDTO.Requests;

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
            return _context.Forms.Include(x => x.Criterias).ToList();
        }

        public Form? GetById(int id)
        {
            try
            {
                Form form = _context.Forms.Where(form => form.Id == id).FirstOrDefault();
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
                                            Answers = new List<Answer>(),
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
                                            } : null
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
                    CreationDate = body.CreationDate
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
    }
}
