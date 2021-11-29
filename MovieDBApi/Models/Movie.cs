using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDBApi.Models
{
    public partial class Movie
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("belongs_to_collection")]
        public BelongsToCollection BelongsToCollection { get; set; }

        [JsonProperty("budget")]
        public long Budget { get; set; }

        [JsonProperty("genres")]
        public Genre[] Genres { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("production_companies")]
        public ProductionCompany[] ProductionCompanies { get; set; }

        [JsonProperty("production_countries")]
        public ProductionCountry[] ProductionCountries { get; set; }

        [JsonProperty("release_date")]
        public DateTimeOffset? ReleaseDate { get; set; }

        [JsonProperty("revenue")]
        public long Revenue { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("spoken_languages")]
        public SpokenLanguage[] SpokenLanguages { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }
    }

    public partial class BelongsToCollection
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
    }

    public partial class Genre
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class ProductionCompany
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public partial class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string Iso3166_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class SpokenLanguage
    {
        [JsonProperty("english_name")]
        public string EnglishName { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public partial class Cast
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("cast_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? CastId { get; set; }

        [JsonProperty("character", NullValueHandling = NullValueHandling.Ignore)]
        public string Character { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public long? Order { get; set; }

        [JsonProperty("department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department { get; set; }

        [JsonProperty("job", NullValueHandling = NullValueHandling.Ignore)]
        public string Job { get; set; }
    }

    public partial class Crew
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
    }

    public partial class Person
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("also_known_as")]
        public string[] AlsoKnownAs { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("birthday")]
        public DateTimeOffset Birthday { get; set; }

        [JsonProperty("deathday")]
        public object Deathday { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("homepage")]
        public object Homepage { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
        [JsonProperty("movie_credits")]
        public MovieCredit MovieCredits { get; set; }
    }

    public partial class MovieCredit
    {
        [JsonProperty("cast")]
        public FilmCredit[] Cast { get; set; }

        [JsonProperty("crew")]
        public FilmCredit[] Crew { get; set; }
    }

    public partial class FilmCredit
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("genre_ids")]
        public long[] GenreIds { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("release_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("character", NullValueHandling = NullValueHandling.Ignore)]
        public string Character { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public long? Order { get; set; }

        [JsonProperty("department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department { get; set; }

        [JsonProperty("job", NullValueHandling = NullValueHandling.Ignore)]
        public string Job { get; set; }
    }
}



