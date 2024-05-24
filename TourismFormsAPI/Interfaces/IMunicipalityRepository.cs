using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IMunicipalityRepository
    {
        public List<Municipality> GetAll();
        public Municipality? GetById(int id);
        public Municipality Create(MunicipalityPost body);
        public Municipality Update(int id, MunicipalityUpdate body);
        public void Delete(int id);
    }
}
