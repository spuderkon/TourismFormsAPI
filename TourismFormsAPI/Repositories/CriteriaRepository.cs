using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace TourismFormsAPI.Repositories
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private readonly TourismContext _context;

        public CriteriaRepository(TourismContext context)
        {
            _context = context;
        }

        public List<Criteria> GetAll()
        {
            return _context.Criteria.ToList();
        }
    }
}
