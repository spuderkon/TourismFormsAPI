using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface IQuestionRepository
    {
        public List<Question> GetAll();
        public Question GetById(int id);
    }
}
