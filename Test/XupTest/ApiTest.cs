using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using XupApi.Controllers;
using XupApi.Models;
using XupApi.Services;

namespace XupTest
{
    [TestClass]
    public class ApiTest
    {

       Mock<ICheckRegisterService> objICheckRegisterService;
        Mock<IMapper> objMapper;
        public ApiTest()
        {
            // Arrange
            objICheckRegisterService = new Mock<ICheckRegisterService>();
            objMapper = new Mock<IMapper>();            
        }
        [TestMethod]
        public void ViewAllCheck_Test()
        {
            var controller = new XupCheckController(objICheckRegisterService.Object, objMapper.Object);

            // Act
            var result = controller.ViewAllCheck();
            var resultAsAnonymous = new OkObjectResult(new { messsage="some obj"});

            // Assert
            var okresult = result.Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(okresult.StatusCode, resultAsAnonymous.StatusCode);
        }

        [TestMethod]
        public void GetCheckByFilter_Test()
        {
            var controller = new XupCheckController(objICheckRegisterService.Object, objMapper.Object);
          
            //Act
            var result = controller.GetCheckByFilter("xyz",2);
            var resultAsAnonymous = new OkObjectResult(new { messsage = "some obj" });

            // Assert
            var okresult = result.Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(okresult.StatusCode, resultAsAnonymous.StatusCode);
        }

        [TestMethod]
        public void GetCheckByFilter_Test_WhenFrequencyNotin_Range()
        {
            var controller = new XupCheckController(objICheckRegisterService.Object, objMapper.Object);

            var result = controller.GetCheckByFilter("xyz", -1);
            var resultAsAnonymous = new BadRequestObjectResult(new { messsage = "Frequency should be 2 characters int format and range between 1 to 59" });

            // Assert
            var badresult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(badresult.StatusCode, resultAsAnonymous.StatusCode);
        }
    }
}
