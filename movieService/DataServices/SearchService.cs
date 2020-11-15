using System.Collections;
using movieService.DataServices.Interfaces;
using movieService.Models;
using System.Collections.Generic;
using movieService.DBContext;

namespace movieService.DataServices
{
    public class SearchService : ISearchService
    {
        public IList<StringSearch> Search(string keyword)
        {
            using var ctx = new MoviesContext();
            return ctx.Search(keyword);
        }

        public IList<BestMatch> BestMatch(string firstKeyword, string secondKeyword)
        {
            using var ctx = new MoviesContext();
            return ctx.BestMatch(firstKeyword, secondKeyword);
        }

       
    }
}