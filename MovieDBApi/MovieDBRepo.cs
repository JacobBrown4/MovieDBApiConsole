using MovieDBApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieDBApi
{
    public class TMDBService
    {
        private readonly HttpClient _client = new HttpClient();
        private string _urlBase;
        private string _key;
        private string _language;
        public TMDBService()
        {
            _urlBase = "https://api.themoviedb.org/3/";
            _key = "?api_key=a56a3771c3ace95d4a806ca4988f139d";
            _language = "&language=en-US";
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{ _urlBase}movie/{id}{_key}{_language}");
            if (response.IsSuccessStatusCode)
            {
                Movie movie = await response.Content.ReadAsAsync<Movie>();
                return movie;
            }
            return default;
        }

        public async Task<SearchResult<Movie>> GetMovieSearchAsync(string query, int page)
        {
            // https://api.themoviedb.org/3/search/movie?api_key=a56a3771c3ace95d4a806ca4988f139d&language=en-US&query=star%20wars&page=1&include_adult=false
            List<char> queryPieces = new List<char>();
            foreach (char letter in query)
            {
                // Need to replace spaces with %20
                if (letter == ' ')
                {
                    queryPieces.Add('%');
                    queryPieces.Add('2');
                    queryPieces.Add('0');
                }
                else
                    queryPieces.Add(letter);
            }
            string convertedQuery = new string(queryPieces.ToArray());
            HttpResponseMessage response = await _client.GetAsync(_urlBase + "search/movie" + _key + _language + $"&query={convertedQuery}" + $"&page={page}" + "&include_adult=false");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<Movie>>();
            }

            return default;
        }

        public async Task<Credits<Cast,Crew>> GetCreditsByMovieId(long id)
        {

            // https://api.themoviedb.org/3/movie/11/credits?api_key=a56a3771c3ace95d4a806ca4988f139d&language=en-US
            HttpResponseMessage response = await _client.GetAsync(_urlBase + "/movie/"+ id + "/credits" + _key + _language);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Credits<Cast,Crew>>();
            }

            return default;
        }

        public async Task<Person> GetPersonById(long id)
        {
            //https://api.themoviedb.org/3/person/6384?api_key=a56a3771c3ace95d4a806ca4988f139d&language=en-US
            HttpResponseMessage response = await _client.GetAsync($"{ _urlBase}person/{id}{_key}{_language}&append_to_response=movie_credits");
            if (response.IsSuccessStatusCode)
            {
                Person person = await response.Content.ReadAsAsync<Person>();
                return person;
            }
            return default;
        }
    }
}
