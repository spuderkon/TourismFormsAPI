using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface IFillMethodRepository
    {
        public Task<IEnumerable<FillMethod>> GetAll();
    }
}
