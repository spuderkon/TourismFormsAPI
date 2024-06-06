using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _iCityRepository;

        public CityController(ICityRepository iCityRepository)
        {
            _iCityRepository = iCityRepository;
        }

        [HttpGet("GetAllWithMunicipality")]
        public async Task<ActionResult<List<City>>> GetAllWithMunicipality()
        {
            return Ok(await _iCityRepository.GetAllWithMunicipality());
        }
    }
}
