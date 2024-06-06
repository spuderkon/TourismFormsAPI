using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IFormRepository
    {
        public List<Form> GetAll();
        public Form? GetById(int id);
        public Form? GetById(int id, int surveyId);
        public Task<byte[]> GetExcel(int id);
        public Form Create(FormPost body);
        public Task Update(FormPut body);
    }
}
