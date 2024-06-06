using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IMeasureRepository
    {
        public Task<List<Measure>> GetAll();
        public Task<Measure> Create(MeasurePost body);
        public Task Update(MeasurePut body);
        public Task Delete(int id);
    }
}
