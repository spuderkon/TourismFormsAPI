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
    public class MeasureController : ControllerBase
    {
        private readonly IMeasureRepository _iMeasureRepository;

        public MeasureController(IMeasureRepository iMeasureRepository)
        {
            _iMeasureRepository = iMeasureRepository;
        }

        #region GET
        [HttpGet("GetAll"), Authorize(Policy = "IsAdmin")]
        public async Task<ActionResult<IEnumerable<Measure>>> GetAll()
        {
            return Ok(await _iMeasureRepository.GetAll());
        }
        #endregion

        #region POST
        [HttpPost("Create"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([FromBody] MeasurePost body)
        {
            try
            {
                return Ok(await _iMeasureRepository.Create(body));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut("Update"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Update([FromBody] MeasurePut body)
        {
            try
            {
                await _iMeasureRepository.Update(body);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete/{id}"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _iMeasureRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion
    }
}
