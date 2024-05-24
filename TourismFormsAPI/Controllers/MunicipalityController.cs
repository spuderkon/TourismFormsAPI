using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityRepository _iMunicipalityRepository;

        public MunicipalityController(IMunicipalityRepository iMunicipalityRepository)
        {
            _iMunicipalityRepository = iMunicipalityRepository;
        }

        #region GET
        [HttpGet("GetAll"), Authorize(Policy = "isAdmin")]
        public ActionResult<IEnumerable<Municipality>> GetAll()
        {
            return Ok(_iMunicipalityRepository.GetAll());
        }

        [HttpGet("GetById/{id}"), Authorize(Policy = "isAdmin")]
        public ActionResult<IEnumerable<Municipality>> GetById(int id)
        {
            try
            {
                return Ok(_iMunicipalityRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region POST
        [HttpPost("Create"), Authorize(Policy = "isAdmin")]
        public IActionResult Create([FromBody] MunicipalityPost body)
        {
            return Ok(_iMunicipalityRepository.Create(body));
        }
        #endregion

        #region PUT
        [HttpPut("Update/{id}"), Authorize(Policy = "isAdmin")]
        public IActionResult Update(int id,[FromBody] MunicipalityUpdate body)
        {
            try
            {
                return Ok(_iMunicipalityRepository.Update(id, body));
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete/{id}"), Authorize(Policy = "isAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _iMunicipalityRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
