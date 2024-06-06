using System.IO;

using ClosedXML.Excel;

using DocumentFormat.OpenXml.Spreadsheet;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Serilog;

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
        [HttpGet("GetAll"), Authorize(Policy = "IsAdmin")]
        public ActionResult<IEnumerable<Survey>> GetAll()
        {
            var result = _iSurveyRepository.GetAll();
            Log.Information("User with id {@id} got {@result}", int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value), result);
            return Ok(result);
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
                var result = _iSurveyRepository.GetMyAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value));
                Log.Information("User with id {@id} got {@result}", int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value), result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("GetMyById/{id}"), Authorize]
        public ActionResult<Form> GetMyById(int id)
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
        [HttpGet("GetExcel/{id}")]
        public ActionResult GetExcel(int id)
        {
            try
            {

                var result = _iSurveyRepository.GetExcel(id).Result;
      
                return File(
                        result,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"survey{id}.xlsx");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region POST
        [HttpPost("CreateArray")]
        public IActionResult CreateArray([FromBody] SurveyPost[] body)
        {
            try
            {
                _iSurveyRepository.CreateArray(body);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region PUT
        [HttpPut("UpdateMy"), Authorize]
        public ActionResult<Survey> UpdateMy([FromBody] SurveyPut body)
        {
            try
            {
                return Ok(_iSurveyRepository.UpdateMy(int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value), body));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut("Extend/{id}/{completionDate}"), Authorize(Policy = "IsAdmin")]
        public async Task<ActionResult<Survey>> Extend(int id, DateTime newCompletionDate)
        {
            try
            {
                await _iSurveyRepository.Extend(id, newCompletionDate);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut("Revision"), Authorize(Policy = "IsAdmin")]
        public async Task<ActionResult> Revision([FromBody] SurveyRevisionPut body)
        {
            try
            {
                await _iSurveyRepository.Revision(body);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut("SubmitForEvaluation/{id}"), Authorize]
        public async Task<ActionResult> SubmitForEvaluation(int id)
        {
            try
            {
                await _iSurveyRepository.SubmitForEvaluation(id);
                return Ok();
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
