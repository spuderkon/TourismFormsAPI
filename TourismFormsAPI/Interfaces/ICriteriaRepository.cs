using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface ICriteriaRepository
    {
        public List<Criteria> GetAll();
    }
}
