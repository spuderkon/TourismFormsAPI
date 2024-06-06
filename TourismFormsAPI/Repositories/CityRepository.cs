using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO;

namespace TourismFormsAPI.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly TourismContext _context;

        public CityRepository(TourismContext context)
        {
            _context = context;
        }

        #region GET
        public async Task<List<City>> GetAllWithMunicipality()
        {
            return await LoadData(_context.Cities.Include(c => c.Municipality)).ToListAsync();
        }
        private IQueryable<City> LoadData(IQueryable<City> items)
        {
            return items
                .Select(x => new CityDTO(x)
                {
                    Municipality = new MunicipalityDTO(x.Municipality)
                    {
                        Region = new RegionDTO(x.Municipality.Region)
                    }
                });
        }
        #endregion
    }
}
