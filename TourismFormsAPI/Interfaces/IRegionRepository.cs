using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface IRegionRepository
    {
        public List<Region> GetAll();
        public Region GetById(int id);
    }
}
