using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Dtos.AiSearch
{
    public class QdrantSearchResponse
    {
        [JsonProperty("result")]
        public List<QdrantSearchResult>? Results { get; set; }
    }

    public class QdrantSearchResult
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }

        [JsonProperty("payload")]
        public QdrantPayload? Payload { get; set; }
    }

    public class QdrantPayload
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class SearchResult
    {
        public string? Name { get; set; }
        public float Score { get; set; }
    }
}
