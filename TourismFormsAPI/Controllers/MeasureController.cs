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
        [HttpGet("GetAll"), Authorize(Policy = "isAdmin")]
        public ActionResult<IEnumerable<Measure>> GetAll()
        {
            return Ok(_iMeasureRepository.GetAll());
        }
        #endregion
        #region POST
        [HttpPost("Create/{name}"), Authorize(Policy = "isAdmin")]
        public IActionResult Create(string name)
        {
            try
            {
                return Ok(_iMeasureRepository.Create(name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT

        #endregion

        #region DELETE
        [HttpDelete("Delete/{id}"), Authorize(Policy = "isAdmin")]
        public ActionResult Delete(int id)
        {
            try
            {
                _iMeasureRepository.Delete(id);
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
