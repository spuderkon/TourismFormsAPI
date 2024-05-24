using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Newtonsoft.Json;

using TourismFormsAPI.Interfaces;

namespace TourismFormsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _iAuthRepository;

        public AuthController(IAuthRepository iAuthRepository)
        {
            _iAuthRepository = iAuthRepository;
        }

        #region GET
        [HttpGet("IsAuth"), Authorize]
        public IActionResult IsAuth()
        {
            return Ok();
        }
        #endregion

        #region POST
        [HttpPost("Authorize")]
        public async Task<ActionResult<string>> Authorize(string login, string password)
        {
            try
            {
                var token = await _iAuthRepository.Authorize(login, password);
                var result = new
                {
                    Token = token
                };
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
