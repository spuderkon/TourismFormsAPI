using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _iRegionRepository;

        public RegionController(IRegionRepository iRegionRepository)
        {
            _iRegionRepository = iRegionRepository;
        }

        [HttpGet("GetAll"), Authorize(Policy = "IsAdmin")]
        public async Task<ActionResult<IEnumerable<Region>>> GetAll()
        {
            return Ok(await _iRegionRepository.GetAll());
        }
    }
}
