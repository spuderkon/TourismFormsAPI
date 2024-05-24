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
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _iFormRepository;

        public FormController(IFormRepository iFormRepository)
        {
            _iFormRepository = iFormRepository;
        }

        #region GET

        [HttpGet("GetAll"), Authorize(Policy = "isAdmin")]
        public ActionResult<IEnumerable<Form>> GetAll() 
        {
            return (_iFormRepository.GetAll());
        }

        [HttpGet("GetById/{id}"), Authorize(Policy = "isAdmin")]
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
        [HttpPost("Create"), Authorize(Policy = "isAdmin")]
        public IActionResult Create([FromBody] FormPost body)
        {
            return Ok(_iFormRepository.Create(body));
        }

        #endregion
    }
}
