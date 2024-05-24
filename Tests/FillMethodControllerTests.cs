using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using TourismFormsAPI.Controllers;
using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;

namespace Tests
{
    public class FillMethodControllerTests
    {
        private IFillMethodRepository _iFillMethodRepository;
        private FillMethodController _fillMethodController;

        public FillMethodControllerTests()
        {
            _iFillMethodRepository = A.Fake<IFillMethodRepository>();
            _fillMethodController = new FillMethodController(_iFillMethodRepository);
        }

        [Fact]
        public async Task FillMethodController_GetAll_ReturnsSuccess()
        {
            int count = 3;
            var fillMethods = A.Fake<IEnumerable<FillMethod>>();

            A.CallTo(() =>  _iFillMethodRepository.GetAll()).Returns(fillMethods);

            var actionResult = await _fillMethodController.GetAll();

            var result = actionResult.Result as OkObjectResult;
            var returnFillMethods = result.Value as IEnumerable<FillMethod>;

            Assert.Equal(fillMethods, returnFillMethods);
        }
    }
}
