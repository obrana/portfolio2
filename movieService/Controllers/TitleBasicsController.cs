using Microsoft.AspNetCore.Mvc;
using System;
using movieService.DataServices;
using movieService.DataServices.Interfaces;


namespace movieService.Controllers 
{
    [ApiController]
    [Route("api/namebasics")]
    public class TitleBasicsController : ControllerBase
    {
         ITitleBasicsService ds;

         public TitleBasicsController(ITitleBasicsService data)
         {
             ds = data;
         }

         [HttpGet]
         public IActionResult GetTitleBasics(int page, int pagesize)
         {
             var titlebasics = ds.GetTitleBasics(page, pagesize);
             return Ok(titlebasics.ToJson());
         }
    }

   
}