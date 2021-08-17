using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace vikiProject.Controllers
{
    [ApiController]
    [Route("/")]
    public class DramaController : Controller
    {
        private readonly KdramaMainService _kdramaMainService;
        private readonly AddDramaService _addDramaService;

        public DramaController(KdramaMainService kdramaMainService, AddDramaService addDramaService)
        {
            _kdramaMainService = kdramaMainService;
            _addDramaService = addDramaService;
        }
        [HttpGet]
        
        public async Task<IActionResult> Get()
        {
            const string drama = "playful kiss";
            var findDrama = await _addDramaService.GetDramaNameswithCodes(drama);
            var addDrama =await _kdramaMainService.AddDrama(findDrama[0].code);
            if (!addDrama)
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