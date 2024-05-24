using System.IdentityModel.Tokens.Jwt;

using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

using TourismFormsAPI.Controllers;
using TourismFormsAPI.Interfaces;

namespace Tests
{
    public class AuthControllerTests
    {
        private readonly IAuthRepository _iAuthRepository;

        public AuthControllerTests()
        {
            _iAuthRepository = A.Fake<IAuthRepository>();
        }

        [Fact]
        public async Task AuthController_Authorize_ReturnsSuccess()
        {
            int expectedId = 1; 
            string login = "Kungur";
            string password = "123";
            string dummmyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJpc0FkbWluIjoiVHJ1ZSIsIm5iZiI6MTcxNjMwNTY2OCwiZXhwIjoxNzE2MzE3OTQwLCJpYXQiOjE3MTYzMDU2NjgsImlzcyI6IlRvdXJpc21Gb3JtcyIsImF1ZCI6IlRvdXJpc21Gb3Jtc0FwcCJ9.FOpWImGg91FkdG4OFb_kzJdVHco807PAEpZwOwDTAZE";

            A.CallTo(() => _iAuthRepository.Authorize(login, password)).Returns(Task.FromResult(dummmyToken));

            var controller = new AuthController(_iAuthRepository);

            var actionResult = await controller.Authorize(login, password);

            var result = actionResult.Result as OkObjectResult;
            
            string returnToken = result.Value.ToString();
            JObject obj = JObject.Parse(returnToken);

            // Получение значения токена из объекта JSON
            string jwtToken = (string)obj["Token"];

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            Assert.Equal(expectedId, Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "id").Value));
        }
    }
}