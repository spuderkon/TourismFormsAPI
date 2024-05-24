using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Interfaces.Services;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _iAnswerRepository;
        private readonly IEmailSenderService _iEmailSenderService;

        public AnswerController(IAnswerRepository iAnswerRepository, IEmailSenderService iEmailSenderService)
        {
            _iAnswerRepository = iAnswerRepository;
            _iEmailSenderService = iEmailSenderService;
        }

        #region POST
        [HttpPost("SaveMyAll"), Authorize]
        public async Task<IActionResult> SaveMyAll([FromBody] AnswerPost[] body)
        {
            try
            {
                await _iAnswerRepository.SaveMyAll(body);

                await _iEmailSenderService.SendEmailAsync(Convert.ToString(HttpContext.User.Claims.First(x => x.Type == "email").Value), "Вам новая анкета", "Вам новая акента");
                
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
