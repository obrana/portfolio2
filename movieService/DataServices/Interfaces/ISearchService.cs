using System.Collections;
using System.Collections.Generic;
using movieService.Models;

namespace movieService.DataServices.Interfaces
{
    public interface ISearchService
    {
        IList<StringSearch> Search(string keyword);
        IList<BestMatch> BestMatch(string firstKeyword, string secondKeyword);
    }
}