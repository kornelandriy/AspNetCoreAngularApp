using Microsoft.AspNetCore.Mvc;
using Serilog;
using TestService.Models;

namespace TestService.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [Route("[action]")]
        public ApiData Get()
        {
            Log.Error("some error");
            return new ApiData
            {
                Message = "Some text"
            };
        }
    }
}