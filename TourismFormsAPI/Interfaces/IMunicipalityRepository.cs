using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IMunicipalityRepository
    {
        public List<Municipality> GetAll();
        public Municipality? GetById(int id);
        public Task<Municipality> Create(MunicipalityPost body);
        public Task Update(MunicipalityPut body);
        public Task Delete(int id);
    }
}
