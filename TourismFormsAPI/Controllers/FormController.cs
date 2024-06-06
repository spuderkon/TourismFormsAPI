using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _iFormRepository;

        public FormController(IFormRepository iFormRepository)
        {
            _iFormRepository = iFormRepository;
        }

        #region GET

        [HttpGet("GetAll"), Authorize(Policy = "IsAdmin")]
        public ActionResult<IEnumerable<Form>> GetAll() 
        {
            return (_iFormRepository.GetAll());
        }
        [HttpGet("GetExcel/{id}")]
        public ActionResult GetExcel(int id)
        {
            return File(
                        _iFormRepository.GetExcel(id).Result,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"form{id}.xlsx");
        }
        [HttpGet("GetById/{id}"), Authorize(Policy = "IsAdmin")]
        public ActionResult<Form?> GetById(int id)
        {
            try
            {
                return Ok(_iFormRepository.GetById(id));
            }
            catch
            {
                return NotFound();
            }
        }
        #endregion

        #region POST
        [HttpPost("Create"), Authorize(Policy = "IsAdmin")]
        public IActionResult Create([FromBody] FormPost body)
        {
            return Ok(_iFormRepository.Create(body));
        }
        //[HttpPost("CreateFullForm"), Authorize(Policy = "isAdmin")]
        //public IActionResult CreateFullForm([FromBody] FormPost body)
        //{
        //    return Ok(_iFormRepository.Create(body));
        //}

        #endregion

        #region PUT
        [HttpPut("Update"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Update([FromBody] FormPut body)
        {
            try
            {
                await _iFormRepository.Update(body);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
