using Microsoft.AspNetCore.Mvc;

namespace vikiProject.Controllers
{
    [ApiController]
    [Route("/")]
    [Route("/home")]
    public class Home : Controller
    {
        // GET
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/Episodes")]
        public IActionResult Episodes()
        {
            return View();
        }
    }
}