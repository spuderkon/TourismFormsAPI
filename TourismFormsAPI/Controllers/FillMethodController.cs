using System.Collections;

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
    public class FillMethodController : ControllerBase
    {
        private readonly IFillMethodRepository _iFillMethodRepository;

        public FillMethodController(IFillMethodRepository iFillMethodRepository)
        {
            _iFillMethodRepository = iFillMethodRepository;
        }
        #region GET
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<FillMethod>>> GetAll()
        {
            return Ok(await _iFillMethodRepository.GetAll());
        }
        #endregion

        #region POST
        [HttpPost("Create"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([FromBody] FillMethodPost body)
        {
            await _iFillMethodRepository.Create(body);
            return Ok();
        }
        #endregion

        #region PUT
        [HttpPut("Update"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Update([FromBody] FillMethod body)
        {
            await _iFillMethodRepository.Update(body);
            return Ok();
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete/{id}"), Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _iFillMethodRepository.Delete(id);
            return Ok();
        }
        #endregion
    }
}
