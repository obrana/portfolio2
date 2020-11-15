using System.Collections.Generic;
using movieService.Models;
using System;
using System.Linq;
using movieService.DataServices.Interfaces;
using movieService.DBContext;


namespace movieService.DataServices
{
    public class TitleBasicsService : ITitleBasicsService
    {
        public IList<TitleBasics> GetTitleBasics(int page, int pagesize)
        {
            using var ctx = new MoviesContext();
            Console.WriteLine(ctx);
            return ctx.titleBasics
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }
    }
}