using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly TourismContext _context;

        public AnswerRepository(TourismContext context)
        {
            _context = context;
        }

        public Task SaveMyAll(AnswerPost[] body)
        {
            try
            {
                foreach (var item in body)
                {
                    var itemToCreate = new Answer()
                    {
                        SurveyId = item.SurveyId,
                        Text = item.Text,
                        Score = 5,
                        QuestionId = item.QuestionId,
                    };
                    _context.Add(itemToCreate);
                }
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
