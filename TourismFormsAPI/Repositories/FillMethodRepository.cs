using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace TourismFormsAPI.Repositories
{
    public class FillMethodRepository : IFillMethodRepository
    {
        private readonly TourismContext _context;

        public FillMethodRepository(TourismContext context)
        {
            _context = context;
        }

        #region

        public async Task<IEnumerable<FillMethod>> GetAll()
        {
            return await _context.FillMethods.ToListAsync();
        }
        #endregion
    }
}
