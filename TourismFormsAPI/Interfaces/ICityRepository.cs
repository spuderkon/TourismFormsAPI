using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface ICityRepository
    {
        public Task<List<City>> GetAllWithMunicipality();
    }
}
