using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class CityDTO : City
    {
        public CityDTO(City city)
        {
            Id = city.Id;
            Name = city.Name;
            MunicipalityId = city.MunicipalityId;
        }
    }
}
