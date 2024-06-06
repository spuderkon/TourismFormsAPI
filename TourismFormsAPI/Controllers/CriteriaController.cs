using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;
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

        [HttpPost("CreateArray"), Authorize(Policy = "isAdmin")]
        public IActionResult CreateArray([FromBody] CriteriaPost[] body)
        {
            try
            {
                return Ok(_iCriteriaRepository.CreateArray(body));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateArray"), Authorize(Policy = "isAdmin")]
        public async Task<IActionResult> UpdateArray([FromBody] CriteriaPut[] body)
        {
            try
            {
                await _iCriteriaRepository.UpdateArray(body);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
