using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unittesting.Controllers;

namespace utestpro.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void HomeController_Index_ValidResult()
        {
            HomeController controller = new HomeController();
            string expectedResult = "I am in home";

            string result = controller.Index();

            Assert.Equal(expectedResult, result);
        }
    }
}
