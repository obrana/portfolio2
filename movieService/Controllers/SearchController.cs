using Microsoft.AspNetCore.Mvc;
using movieService.DataServices.Interfaces;

namespace movieService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
         ISearchService ds;

         public SearchController(ISearchService data)
         {
             ds = data;
             
         }

         [HttpGet("{keyword}")]
         public IActionResult string_search(string keyword)
         {
             var data = ds.Search(keyword);
             return Ok(data);
         }

         [HttpGet("{firstKeyword}/{secondKeyword}")]
         public IActionResult best_match(string firstKeyword, string secondKeyword)
         {
             var data = ds.BestMatch(firstKeyword, secondKeyword);
             return Ok(data);
         }
    }
}