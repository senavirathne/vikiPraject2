using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vikiProject.Dto;
using vikiProject.Models;

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
        public async Task<IActionResult> AddDrama(string searchText) //search drama
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                
                var list = _kdramaMainService.SearchDramaName(new StringDto(searchText)).Result;
                if (list != null)// search from db
                {
                    HttpContext.Session.SetInt32("searchFromDb", 1);
                    return View(list.List); 
                }

                HttpContext.Session.SetString("search", searchText);//to send db othernames 
                var dramaNames =
                    await _addDramaService.GetDramaNameswithCodes(new StringDto(searchText)); //from google
                // HttpContext.Session.SetInt32("searchFromDb", 0);
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
                var searchFromDb = HttpContext.Session.GetInt32("searchFromDb");
                if (searchFromDb ==1)
                {
                 var x = await _kdramaMainService.GetNoOfEpisodes(new IntegerDto(id));
                 HttpContext.Session.SetString("dramaName", drama);
                 HttpContext.Session.SetInt32("dId", id);
                 return View(x.Number);
                }
                var (dramaName, nOfEpis) =
                    await _kdramaMainService.AddDramaFromJObject(new StringIntegerDto(drama, id));

                // if (!string.IsNullOrEmpty(dramaName))
                {
                    HttpContext.Session.SetString("dramaName", dramaName);
                    HttpContext.Session.SetInt32("dId", id);
                    return View(nOfEpis);
                }
            }

            return Redirect("/");
        }

        [HttpGet]
        [Route("/{dId}/episode/{eId}")] // todo security
        public async Task<IActionResult> Episode([FromRoute] int dId, [FromRoute] int eId)
        {
            if (HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.SetInt32("eId", eId);
                var episodeNo = await _kdramaMainService.AddDownloadLink(new TwointDto(dId, eId));
                return View(episodeNo);
            }

            return Redirect("/");
        }

        [HttpPost]
        [Route("/drama")]
        public async Task<IActionResult> EpisodePost()
        {
            if (HttpContext.Session.IsAvailable)
            {
                var req = HttpContext.Request.Form;
                if (req["submit"].ToString().Equals("Generate"))
                {
                    var dId = HttpContext.Session.GetInt32("dId").GetValueOrDefault();
                    var eId = HttpContext.Session.GetInt32("eId").GetValueOrDefault();

                    var quality = (Quality) int.Parse(req["quality"]);
                    var links =await _kdramaMainService.GetEpisodeDownloadlinks(new GetDownloadLinkDto(dId, eId, quality));

                    return View(links);
                }
            }
            return Redirect("/");
        }

//         
// //todo don't check every where true use void
// // todo make new other name and add drama's other name list
//            
    }
}