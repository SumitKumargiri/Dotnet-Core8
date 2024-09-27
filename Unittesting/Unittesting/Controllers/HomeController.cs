using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Unittesting.Controllers
{
    public class HomeController : Controller
    {
        [Route("api/[controller]")]
        public string Index()
        {
            return "I am in home";
        }

      
    }
}
