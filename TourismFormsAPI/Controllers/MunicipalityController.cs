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
        [HttpGet("GetAll"), Authorize(Policy = "IsAdmin")]
        public ActionResult<IEnumerable<Municipality>> GetAll()
        {
            return Ok(_iMunicipalityRepository.GetAll());
        }

        [HttpGet("GetById/{id}"), Authorize(Policy = "IsAdmin")]
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
        [HttpPost("Create"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([FromBody] MunicipalityPost body)
        {
            return Ok(await _iMunicipalityRepository.Create(body));
        }
        #endregion

        #region PUT
        [HttpPut("Update"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Update([FromBody] MunicipalityPut body)
        {
            try
            {
                await _iMunicipalityRepository.Update(body);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete/{id}"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _iMunicipalityRepository.Delete(id);
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
