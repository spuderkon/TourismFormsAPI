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
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyRepository _iSurveyRepository;

        public SurveyController(ISurveyRepository iSurveyRepository)
        {
            _iSurveyRepository = iSurveyRepository;
        }

        #region GET
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Survey>> GetAll()
        {
            return Ok(_iSurveyRepository.GetAll());
        }
        [HttpGet("GetById/{id}"), Authorize]
        public ActionResult<IEnumerable<Survey>> GetById(int id)
        {
            try
            {
                return Ok(_iSurveyRepository.GetAll());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("GetMyAll"), Authorize]
        public ActionResult<IEnumerable<Survey>> GetMyAll()
        {
            try
            {
                return Ok(_iSurveyRepository.GetMyAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value)));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("GetMyById/{id}"), Authorize]
        public ActionResult<Survey> GetMyById(int id)
        {
            try
            {
                return Ok(_iSurveyRepository.GetMyById(id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value)));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("GetExcel/{id}"), Authorize]
        public ActionResult<Survey> GetExcel(int id)
        {
            try
            {
                return Ok(_iSurveyRepository.GetExcel(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region POST
        [HttpPost("Create")]
        public IActionResult Create([FromBody] SurveyPost body)
        {
            try
            {
                return Ok(_iSurveyRepository.Create(body));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region PUT
        [HttpPut("UpdateMy/{id}"), Authorize]
        public ActionResult<Survey> UpdateMy(int id, [FromBody] SurveyPut body)
        {
            try
            {
                return Ok(_iSurveyRepository.UpdateMy(id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value), body));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete/{id}"), Authorize(Policy = "isAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _iSurveyRepository.Delete(id);
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
