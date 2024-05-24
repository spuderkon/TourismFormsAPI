using TourismFormsAPI.Models;

namespace TourismFormsAPI.Interfaces
{
    public interface IAuthRepository
    {
        public Task<string?> Authorize(string login, string password);
        public Task<Municipality?> ValidMunicipality(string login, string password);
    }
}
