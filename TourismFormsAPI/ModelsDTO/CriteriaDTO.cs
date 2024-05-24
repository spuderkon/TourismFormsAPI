using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class CriteriaDTO : Criteria
    {
        public CriteriaDTO(Criteria criteria) 
        { 
            Id = criteria.Id;
            Name = criteria.Name;
            FormId = criteria.FormId;
            Sequence = criteria.Sequence;
        }
    }
}
