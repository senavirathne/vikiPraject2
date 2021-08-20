using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vikiProject.Dto;

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
            var findDrama = await _addDramaService.GetDramaNameswithCodes(new StringDto(drama));
          // await  _addDramaService.GetDramaDetailsAsJObject(new StringDto(findDrama[0].code));
           // var x = Stopwatch.StartNew();
            var addDrama = await _kdramaMainService.AddDramaFromJObject(new StringDto(findDrama[0].code));
            // Console.WriteLine("fuck xxx" + x.ElapsedMilliseconds);
            
            // if (!addDrama)
            // {
            //     return NotFound();
            // }

            var response = new JsonResult(new { })
            {
                StatusCode = (int) HttpStatusCode.Created
            };
            return response;
        }

        [HttpGet]
        [Route("/2")]
        public async Task<IActionResult> Get2()
        {
            const string name = "Playful Kiss";
            const int no = 1;
            var addDrama = await _kdramaMainService.AddDownloadLink(new StringIntegerDto(name, no));
            if (!addDrama)
            {
                return NotFound();
            }

            var response = new JsonResult(new { })
            {
                StatusCode = (int) HttpStatusCode.Created
            };
            return response;
        }
    }
}