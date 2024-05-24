using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class RegionDTO : Region
    {
        public RegionDTO(Region region) 
        { 
            Id = region.Id;
            Name = region.Name;
        }
    }
}
