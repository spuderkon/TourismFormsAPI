using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IQuestionRepository
    {
        public List<Question> GetAll();
        public Question GetById(int id);
        public Task CreateArray(QuestionPost[] body);
        public Task UpdateArray(QuestionPut[] body);
    }
}
