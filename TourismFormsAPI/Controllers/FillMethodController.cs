using System.Collections;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FillMethodController : ControllerBase
    {
        private readonly IFillMethodRepository _iFillMethodRepository;

        public FillMethodController(IFillMethodRepository iFillMethodRepository)
        {
            _iFillMethodRepository = iFillMethodRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<FillMethod>>> GetAll()
        {
            return Ok(await _iFillMethodRepository.GetAll());
        }
    }
}
