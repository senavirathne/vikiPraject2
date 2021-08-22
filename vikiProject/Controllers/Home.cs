using System.Collections.Generic;
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
        [HttpGet]
        [Route("{searchTerm}")]
        public IActionResult AddDrama(string searchTerm = "")
        {
            List<drmaName> drmaNames;
            if (!string.IsNullOrEmpty(searchTerm) )
            {
                // drmaNames = //from db .tolist
                
                
            }

            // Redirect(Index());
             return View(drmaNames);
        }
    }
}