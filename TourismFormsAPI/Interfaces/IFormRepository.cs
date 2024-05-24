using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IFormRepository
    {
        public List<Form> GetAll();
        public Form? GetById(int id);
        public Form Create(FormPost body);

    }
}
