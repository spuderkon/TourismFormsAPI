using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly TourismContext _context;

        public QuestionRepository(TourismContext context)
        {
            _context = context;
        }

        public List<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task CreateArray(QuestionPost[] body)
        {
            try
            {
                foreach (var item in body)
                {
                    var itemToCreate = new Question()
                    {
                        Name = item.Name,
                        Hint = item.Hint,
                        CriteriaId = item.CriteriaId,
                        Sequence = item.Sequence,
                        Formula = item.Formula,
                        MeasureId = item.MeasureId,
                        Hidden = item.Hidden,
                        FillMethodId = item.FillMethodId,
                        
                    };
                    _context.Questions.Add(itemToCreate);
                }
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region UPDATE
        public Task UpdateArray(QuestionPut[] body)
        {
            try
            {
                foreach (var item in body)
                {
                    var itemToUpdate = _context.Questions.FirstOrDefault(q => q.Id == item.Id);
                    if (itemToUpdate is not null)
                    {
                        itemToUpdate.Name = item.Name;
                        itemToUpdate.Hint = item.Hint;
                        itemToUpdate.CriteriaId = item.CriteriaId;
                        itemToUpdate.Sequence = item.Sequence;
                        itemToUpdate.Formula = item.Formula;
                        itemToUpdate.MeasureId = item.MeasureId;
                        itemToUpdate.Hidden = item.Hidden;
                        itemToUpdate.FillMethodId = item.FillMethodId;
                        _context.Questions.Update(itemToUpdate);
                    }
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
    }
}
