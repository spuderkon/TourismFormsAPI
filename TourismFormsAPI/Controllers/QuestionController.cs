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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _iQuestionRepository;

        public QuestionController(IQuestionRepository iQuestionRepository)
        {
            _iQuestionRepository = iQuestionRepository;
        }

        [HttpPost("CreateArray"), Authorize]
        public async Task<IActionResult> CreateArray([FromBody] QuestionPost[] body)
        {
            try
            {
                await _iQuestionRepository.CreateArray(body);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("UpdateArray"), Authorize]
        public async Task<IActionResult> UpdateArray([FromBody] QuestionPut[] body)
        {
            try
            {
                await _iQuestionRepository.UpdateArray(body);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
