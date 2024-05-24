using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class MeasureDTO : Municipality
    {
        public MeasureDTO(Municipality municipality)
        {
            Id = municipality.Id;
            Name = municipality.Name;
        }
    }
}
