using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace vikiProject.Controllers
{
    [ApiController]
    [Route("/drama")]
    public class DramaController : Controller
    {
        private readonly KdramaMainService _kdramaMainService;

        public DramaController(KdramaMainService kdramaMainService)
        {
            _kdramaMainService = kdramaMainService;
        }
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var repository =await _kdramaMainService.AddDrama(name);
            if (!repository)
            {
                return NotFound();
            }

            var response = new JsonResult(new {})
            {
                StatusCode = (int) HttpStatusCode.Created
            };
            return response;

        }
    }
}