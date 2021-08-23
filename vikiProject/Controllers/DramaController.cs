using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vikiProject.Dto;

namespace vikiProject.Controllers
{
    [ApiController]
    [Route("/con")]
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
            const string drama = "playful kiss";//search value
            // var findDrama = await _addDramaService.GetDramaNameswithCodes(new StringDto(drama));
            //return view

//todo don't check every where true use void
// todo make new other name and add drama's other name list
            // var addDrama = await _kdramaMainService.AddDramaFromJObject(new StringIntegerDto(drama,findDrama[0].code));//code of selected one
            
            // if (!addDrama)
            // {
            //     return NotFound();
            // }

            var response = new JsonResult(new { name = "get"})
            {
                StatusCode = (int) HttpStatusCode.Created
            };
            return response;
        }

        [HttpGet]
        [Route("/Get2")]
        public async Task<IActionResult> Get2()
        {
            const string name = "Playful Kiss";
            const int no = 1;
            // var addDrama = await _kdramaMainService.AddDownloadLink(new StringIntegerDto(name, no));
            // if (!addDrama)
            // {
            //     return NotFound();
            // }

            var response = new JsonResult(new {name = "get2" })
            {
                StatusCode = (int) HttpStatusCode.Created,
                
            };
            return response;
        }
    }
}