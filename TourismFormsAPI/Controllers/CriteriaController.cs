using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Tools;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CriteriaController : ControllerBase
    {
        private readonly ICriteriaRepository _iCriteriaRepository;

        public CriteriaController(ICriteriaRepository iCriteriaRepository)
        {
            _iCriteriaRepository = iCriteriaRepository;
        }

        
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_iCriteriaRepository.GetAll());
        }
    }
}
