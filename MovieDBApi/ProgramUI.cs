using MovieDBApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieDBApi
{
    public class ProgramUI
    {
        private readonly TMDBService _tmdb;
        public ProgramUI()
        {
            _tmdb = new TMDBService();
        }
        public void Run()
        {
            Menu();
        }

        private void Menu()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Find a movie by Id\n" +
                    "2. Search a movie\n" +
                    "3. Find a person by Id\n" +
                    "E. to exit");


                switch (Console.ReadLine().ToLower())
                {

                    case "1":
                        FindMovieById();
                        break;
                    case "2":
                        SeachMovie();
                        break;
                    case "3":
                        FindPersonById();
                        break;
                    case "e":
                        run = false;
                        break;
                }
            }
        }

        private void FindPersonById()
        {
            Console.Clear();
            Console.WriteLine($"What's the id?");
            Console.Write("Id: ");
            var result = _tmdb.GetPersonById(SafeIntReturn()).Result;
            if (result != null)
            {
                DisplayPersonFull(result);
            }
            else
            {
                PPL("No person found", 12);
                AnyKey();
            }
        }

        private void DisplayPersonFull(Person result)
        {
            PropertyInfo[] properties = typeof(Person).GetProperties();
            // pC means property color
            int pC = 2;
            Console.Clear();
            PP("Name: ", pC);
            Console.WriteLine($"{result.Name}");
            foreach (PropertyInfo property in properties)
            {
                // Ignore list, if the property matches these don't bother printing.
                if ((new string[] { "Name", "ProfilePath" }.Contains(property.Name))) { }
                else
                {
                    PP($"{property.Name}: ", pC);
                    if (property.Name == "Gender")
                    {
                        var gender = result.Gender;
                        if (gender == 1)
                            Console.WriteLine("Female");
                        else if (gender == 2)
                            Console.WriteLine("Male");
                        else if (gender == 3)
                            Console.WriteLine("Non-Binary");
                        else
                            Console.WriteLine("Unknown");
                    }
                    else if (property.Name == "AlsoKnownAs")
                    {
                        Console.WriteLine();
                        foreach (string name in result.AlsoKnownAs)
                        {
                            Console.WriteLine($"\t{name}");
                        }
                    }
                    else if (property.Name == "MovieCredits")
                    {

                    }
                    else
                    {
                        Console.Write(property.GetValue(result));
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine("Press ? to view film credits\nanykey to exit");
            var key = Console.ReadKey();
            if (key.KeyChar == '?' || key.KeyChar == '/')
            {
                Console.Clear();
                Console.WriteLine();
                foreach (FilmCredit credit in result.MovieCredits.Cast.OrderBy(x => x.ReleaseDate))
                {
                    PP($"{credit.Id,-8}", 2);
                    PPL($"{credit.Title,50}\t({credit.ReleaseDate.Year})", 14);
                }
                AnyKey();
            }
        }

        private void FindMovieById()
        {
            Console.Clear();
            Console.WriteLine($"What's the id?");
            Console.Write("Id: ");
            var result = _tmdb.GetMovieByIdAsync(SafeIntReturn()).Result;
            if (result != null)
            {
                DisplayMovieFull(result);
            }
            else
            {
                PPL("No movie found", 12);
                AnyKey();
            }
        }

        private void GetCreditsFromMovie(long id)
        {
            Console.WriteLine("Press ? for credits\nAnykey to exit");
            var key = Console.ReadKey();
            if (key.KeyChar == '?' || key.KeyChar == '/')
            {

                Console.Clear();
                var result = _tmdb.GetCreditsByMovieId(id).Result;
                PPL("Cast: ", 2);
                foreach (Cast cast in result.Cast)
                {
                    DisplayCastItem(cast);
                }
                AnyKey();
            }
        }

        private void SeachMovie()
        {
            Console.Clear();
            Console.WriteLine($"What are you looking for?");
            Console.Write("Search: ");
            string search = Console.ReadLine();
            SearchAgain(search, 1);
        }

        private void SearchAgain(string search, int page)
        {
            Console.Clear();
            Console.WriteLine("Seaching..");
            var result = _tmdb.GetMovieSearchAsync(search, page).Result;
            Console.Clear();
            if (result != default)
            {
                PP("Search: ", 2);
                Console.WriteLine(search);
                DisplaySearch(result);

                List<int> ids = new List<int>();
                foreach (Movie movie in result.Results)
                {
                    ids.Add((int)movie.Id);
                }


                if (result.TotalPages > 1)
                {
                    Console.WriteLine("\n");
                    if (result.Page == 1)
                    {
                        Console.WriteLine("More results found\npress > to go to the next page\npress ? to search an id\nanykey to exit");
                        var key = Console.ReadKey();
                        if (key.KeyChar == '.' || key.KeyChar == '>')
                        {
                            SearchAgain(search, (int)++result.Page);
                        }
                        else if (key.KeyChar == '/' || key.KeyChar == '?')
                        {
                            DisplayMovieFull(_tmdb.GetMovieByIdAsync(ids[SafeIndexReturn()]).Result);

                        }
                    }
                    else if (result.Page > 1 && result.Page < result.TotalPages)
                    {
                        Console.WriteLine("More results found\npress > to go to the next page\npress < to go to the previous page\npress ? to search an id\nanykey to exit");
                        var key = Console.ReadKey();
                        if (key.KeyChar == '.' || key.KeyChar == '>')
                        {
                            SearchAgain(search, (int)++result.Page);
                        }
                        else if (key.KeyChar == ',' || key.KeyChar == '<')
                        {
                            SearchAgain(search, (int)--result.Page);
                        }
                        else if (key.KeyChar == '/' || key.KeyChar == '?')
                        {
                            DisplayMovieFull(_tmdb.GetMovieByIdAsync(ids[SafeIndexReturn()]).Result);

                        }
                    }
                    else
                    {
                        Console.WriteLine("More results found\npress < to go to the previous page\npress ? to search an id\nanykey to exit");
                        var key = Console.ReadKey();
                        if (key.KeyChar == ',' || key.KeyChar == '<')
                        {
                            SearchAgain(search, (int)--result.Page);
                        }
                        else if (key.KeyChar == '/' || key.KeyChar == '?')
                        {
                            DisplayMovieFull(_tmdb.GetMovieByIdAsync(ids[SafeIndexReturn()]).Result);

                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n\npress ? to search an id\npress any key to exit");
                    var key = Console.ReadKey();
                    if (key.KeyChar == '/' || key.KeyChar == '?')
                    {
                        DisplayMovieFull(_tmdb.GetMovieByIdAsync(ids[SafeIndexReturn()]).Result);

                    }
                }

            }
        }

        private void DisplaySearch(SearchResult<Movie> result)
        {
            PP("Results: ", 2);
            Console.WriteLine(result.TotalResults);
            PP("Total Pages: ", 2);
            Console.WriteLine(result.TotalPages);
            PP("Page: ", 12);
            Console.WriteLine(result.Page);
            Console.WriteLine("\n");
            int index = 1;
            Console.WriteLine("Index\tId\tTitle\t(Year)");
            foreach (Movie movie in result.Results)
            {
                PP($"{ index,-5}", 2);
                if (movie != null)
                {
                    DisplayMovieItem(movie);
                }
                index++;
            }
        }

        private void DisplayMovieFull(Movie result)
        {
            PropertyInfo[] properties = typeof(Movie).GetProperties();
            // pC means property color
            int pC = 2;
            Console.Clear();
            PP("Title: ", pC);
            Console.WriteLine($"{result.Title}");
            PP("Tagline: ", pC);
            Console.WriteLine($"{result.Tagline}");
            PP("Overview: ", pC);
            Console.WriteLine($"{result.Overview}");
            foreach (PropertyInfo property in properties)
            {
                // Ignore list, if the property matches these don't bother printing.
                if ((new string[] { "Homepage", "PosterPath", "Title", "Tagline", "Overview", "BackdropPath" }.Contains(property.Name))) { }
                else
                {
                    PP($"{property.Name}: ", pC);
                    if (property.Name == "BelongsToCollection")
                    {
                        if (result.BelongsToCollection != null)
                        {
                            Console.Write(result.BelongsToCollection.Name);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No collection");
                        }
                    }
                    else if (property.Name == "Budget")
                    {
                        Console.Write(result.Budget.ToString("C"));
                        Console.WriteLine();
                    }
                    else if (property.Name == "Revenue")
                    {
                        Console.Write(result.Revenue.ToString("C"));
                        Console.WriteLine();
                    }
                    else if (property.Name == "Genres")
                    {
                        Console.WriteLine();
                        foreach (Genre genre in result.Genres)
                        {
                            Console.WriteLine($"\t{genre.Name}");
                        }
                    }
                    else if (property.Name == "ProductionCompanies")
                    {
                        Console.WriteLine();
                        foreach (ProductionCompany company in result.ProductionCompanies)
                        {
                            Console.WriteLine($"\t{company.Name}");
                        }
                    }
                    else if (property.Name == "ProductionCountries")
                    {
                        Console.WriteLine();
                        foreach (ProductionCountry country in result.ProductionCountries)
                        {
                            Console.WriteLine($"\t{country.Name}");
                        }
                    }
                    else if (property.Name == "SpokenLanguages")
                    {
                        Console.WriteLine();
                        foreach (SpokenLanguage language in result.SpokenLanguages)
                        {
                            Console.WriteLine($"\t{language.EnglishName}");
                        }
                    }
                    else
                    {
                        Console.Write(property.GetValue(result));
                        Console.WriteLine();
                    }
                }
            }
            GetCreditsFromMovie(result.Id);
        }

        private void DisplayMovieItem(Movie movie)
        {
            if (movie.ReleaseDate != null)
            {
                Console.WriteLine("{0,-6} {1} ({2})", movie.Id, movie.Title, movie.ReleaseDate.Value.Year);
            }
            else
                Console.WriteLine("{0,-6} {1} (No Date)", movie.Id, movie.Title);
        }

        private void DisplayCastItem(Cast cast)
        {
            PP($"{cast.Id,-8}", 2);
            PPL($"{cast.Name,30}\t{cast.Character,5}", 14);
        }

        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private int SafeIntReturn()
        {
            var input = Console.ReadLine();
            Console.WriteLine("Requesting..");
            Int32.TryParse(input, out int result);
            return result;
        }

        private int SafeIndexReturn()
        {
            Console.WriteLine("Enter the index number of the movie you wish to find");
            var input = Console.ReadLine();
            Console.WriteLine("Requesting..");
            Int32.TryParse(input, out int result);
            return --result;
        }

        //Pretty Print
        private void PP(string statement, int color)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write(statement);
            Console.ResetColor();
        }
        private void PPL(string statement, int color)
        {
            PP(statement, color);
            Console.WriteLine();
        }
        //Black   0
        //DarkBlue    1
        //DarkGreen   2
        //DarkCyan    3
        //DarkRed 4
        //DarkMagenta 5
        //DarkYellow  6
        //Gray    7
        //DarkGray    8
        //Blue    9
        //Green   10
        //Cyan    11
        //Red 12
        //Magenta 13
        //Yellow  14
        //White   15
    }
}
