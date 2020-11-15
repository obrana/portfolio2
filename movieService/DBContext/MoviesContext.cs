using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using movieService.Models;
using Npgsql;
using NpgsqlTypes;

namespace movieService.DBContext
{
    public class MoviesContext :DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DbSet<TitleBasics> titleBasics { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string database = HostDatabase.address;
            optionsBuilder
                .UseNpgsql(database)
                .UseLoggerFactory(loggerFactory);
        }
        public IList<StringSearch> Search(string keyword)
        {
            var searchResult = new List<StringSearch>();

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "string_search";
                command.Parameters.Add(new Npgsql.NpgsqlParameter("s", NpgsqlTypes.NpgsqlDbType.Varchar)
                    { Value = keyword });

                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Id = Convert.ToString(reader["id"]);
                        var Title = Convert.ToString(reader["title"]);

                        searchResult.Add(new StringSearch()
                        {
                           Id = Id, Title = Title
                            
                        });
                    }
                }

                return searchResult;
            }
        }
        public IList<BestMatch> BestMatch(string firstKeyword, string secondKeyword)
        {
            var searchResult = new List<BestMatch>();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "best_match";
                command.Parameters.Add(new NpgsqlParameter("w1", NpgsqlDbType.Varchar) {Value = firstKeyword});
                command.Parameters.Add(new NpgsqlParameter("w2", NpgsqlDbType.Varchar) {Value = secondKeyword});
                if(command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       var Id = Convert.ToString(reader["id"]);
                        var Title = Convert.ToString(reader["title"]);
                        int? Rank = Convert.ToInt32(reader["rank"]);
                        
                        searchResult.Add(new BestMatch()
                        {
                           Id = Id,
                                Rank = (int)Rank,
                            Title = Title
                        });
                    }
                }

                return searchResult;
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleBasics>().ToTable("title_basics");
            modelBuilder.Entity<TitleBasics>().Property(x => x.Id).HasColumnName("tconst").HasColumnType("varchar");
            modelBuilder.Entity<TitleBasics>().Property(x => x.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<TitleBasics>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<TitleBasics>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<TitleBasics>().Property(x => x.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<TitleBasics>().Property(x => x.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<TitleBasics>().Property(x => x.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<TitleBasics>().Property(x => x.RuntimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<TitleBasics>().Property(x => x.Genres).HasColumnName("genres");
        }

      
    }
}