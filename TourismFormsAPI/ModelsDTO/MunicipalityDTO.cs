using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class MunicipalityDTO : Municipality
    {
        public MunicipalityDTO(Municipality municipality) 
        { 
            Id = municipality.Id;
            Name = municipality.Name;
            RegionId = municipality.RegionId;
            Login = null;
            Password = null;
            Email = municipality.Email;
            IsAdmin = municipality.IsAdmin;
        }
    }
}
