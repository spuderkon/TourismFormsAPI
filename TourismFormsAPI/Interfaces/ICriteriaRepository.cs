using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface ICriteriaRepository
    {
        public List<Criteria> GetAll();
        public List<Criteria> CreateArray(CriteriaPost[] body);
        public Task UpdateArray(CriteriaPut[] body);
    }
}
