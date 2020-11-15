using System.Collections.Generic;
using movieService.Models;

namespace movieService.DataServices.Interfaces
{
    public interface ITitleBasicsService
    {
        IList<TitleBasics> GetTitleBasics (int page, int pagesize);
    }
}