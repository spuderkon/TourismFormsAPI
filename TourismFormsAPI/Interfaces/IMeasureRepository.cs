using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IMeasureRepository
    {
        public List<Measure> GetAll();
        public Measure Create(string name);
        public void Delete(int id);
    }
}
