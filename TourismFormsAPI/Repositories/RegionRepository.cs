using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace TourismFormsAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly TourismContext _context;

        public RegionRepository(TourismContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }
    }
}
