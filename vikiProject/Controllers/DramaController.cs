using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vikiProject.Dto;

namespace vikiProject.Controllers
{
    [ApiController]
    [Route("/")]
    [Route("/drama")]
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
        [Route("/home")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/addDrama")] //todo add session
        public async Task<IActionResult> AddDrama(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                HttpContext.Session.SetString("search", searchText);
                List<(int, string)> dramaNames =
                    await _addDramaService.GetDramaNameswithCodes(new StringDto(searchText));
                return View(dramaNames);
            }


            return Redirect("/");
        }

        [HttpGet]
        [Route("/{id}/episodes")]
        public async Task<IActionResult> Episodes([FromRoute] int id, string drama)
        {
            if (HttpContext.Session.IsAvailable)
            {
                var (dramaName, nOfEpis) =
                    await _kdramaMainService.AddDramaFromJObject(new StringIntegerDto(drama, id));

                HttpContext.Session.SetString("dramaName", dramaName);
                HttpContext.Session.SetInt32("dId", id);
                return View(nOfEpis);
            }

            return Redirect("/");
        }

        [HttpGet]
        [Route("/{dId}/episode/{eId}")] // todo security
        public async Task<IActionResult> Episode([FromRoute] int dId, [FromRoute] int eId)
        {
            if (HttpContext.Session.IsAvailable)
            {
                var episodeNo = await _kdramaMainService.AddDownloadLink(new TwointDto(dId, eId));
                return View(episodeNo);
            }

            return Redirect("/");
        }

//         [HttpGet]
//         public async Task<IActionResult> Get()
//         {
//             const string drama = "playful kiss";//search value
//             // var findDrama = await _addDramaService.GetDramaNameswithCodes(new StringDto(drama));
//             //return view
//
// //todo don't check every where true use void
// // todo make new other name and add drama's other name list
//             //code of selected one
//             
//             // if (!addDrama)
//             // {
//             //     return NotFound();
//             // }
//
//             var response = new JsonResult(new { name = "get"})
//             {
//                 StatusCode = (int) HttpStatusCode.Created
//             };
//             return response;
//         }

        // [HttpGet]
        // [Route("/Get2")]
        // public async Task<IActionResult> Get2()
        // {
        //     const string name = "Playful Kiss";
        //     const int no = 1;

        //     // if (!addDrama)
        //     // {
        //     //     return NotFound();
        //     // }
        //
        //     var response = new JsonResult(new {name = "get2" })
        //     {
        //         StatusCode = (int) HttpStatusCode.Created,
        //         
        //     };
        //     return response;
        // }
    }
}