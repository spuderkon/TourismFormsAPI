using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface IRegionRepository
    {
        public Task<IEnumerable<Region>> GetAll();
    }
}
