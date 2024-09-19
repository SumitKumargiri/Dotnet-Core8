using Dllprocess.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Dllprocess.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ICrud _crudService;

        public CrudController(ICrud crudService)
        {
            _crudService = crudService;
        }

        [HttpGet("getdata")]
        public async Task<IActionResult> Getdata()
        {
            var result = await _crudService.GetAsync();
            return Ok(result);
        }
    }
}
