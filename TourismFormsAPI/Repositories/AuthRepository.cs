using System.Security.Claims;

using TourismFormsAPI.Tools;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TourismFormsAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TourismContext _context;
        private readonly IConfiguration _iConfiguration;

        public AuthRepository(TourismContext context, IConfiguration iConfiguration)
        {
            _context = context;
            _iConfiguration = iConfiguration;
        }

        public async Task<string?> Authorize(string login, string password)
        {
            var municipality = await ValidMunicipality(login, password);
            if (municipality is not null)
            {
                var identity = new ClaimsIdentity(new[] {
                        new Claim("id",municipality.Id.ToString()),
                        new Claim(ClaimTypes.Email, " "),
                        new Claim("isAdmin", municipality.IsAdmin.ToString()),
                });

                return JwtTools.GenerateJwtToken(identity, _iConfiguration["JwtSettings:Key"]!, _iConfiguration["JwtSettings:Issuer"]!, _iConfiguration["JwtSettings:Audience"]!);
            }
            throw new Exception();
        }

        public async Task<Municipality?> ValidMunicipality(string login, string password)
        {
            var municipality = await _context.Municipalities.FirstAsync(x => x.Login == login);
            if (municipality is not null)
            {
                if (municipality.Password == RegistrationTools.GetPasswordSha256(password))
                {
                    return municipality;
                }
                return null;
            }
            else
                return null;
        }
    }
}
