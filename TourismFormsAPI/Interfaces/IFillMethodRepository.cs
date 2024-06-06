using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface IFillMethodRepository
    {
        public Task<IEnumerable<FillMethod>> GetAll();
        public Task Create(FillMethodPost body);
        public Task Update(FillMethod body);
        public Task Delete(int id);
    }
}
