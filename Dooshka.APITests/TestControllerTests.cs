using Dooshka.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Dooshka.APITests
{
    public class TestControllerTests
    {

        [Fact]
        public void Get_Should_ReturnOk()
        {
            var controller = new TestController();
            var result = controller.Get();
        
            Assert.NotNull(result);
        }
    }
}