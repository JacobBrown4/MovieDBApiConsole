using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDBApi.Models
{
    public class SearchResult<T>
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("results")]
        public T[] Results { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }

    public class Credits<T,O>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cast")]
        public T[] Cast { get; set; }

        [JsonProperty("crew")]
        public O[] Crew { get; set; }
    }
}
