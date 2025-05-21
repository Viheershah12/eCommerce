using Abp.eCommerce.Dtos.AiSearch;
using Abp.eCommerce.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Product.Dtos.Product;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.eCommerce.Services
{
    public class AiSearchAppService : eCommerceAppService, IAiSearchAppService
    {
        private readonly RestClient _qdrantClient;
        private readonly RestClient _openAiClient;
        private readonly IConfiguration _config;

        private readonly string _collectionName;
        private readonly string _qdrantApiKey;
        private readonly string _openAiApiKey;

        public AiSearchAppService(IConfiguration config)
        {
            _config = config;

            _collectionName = _config["Qdrant:CollectionName"] ?? string.Empty;
            _qdrantApiKey = _config["Qdrant:ApiKey"] ?? string.Empty;
            _openAiApiKey = _config["OpenAI:ApiKey"] ?? string.Empty;

            _qdrantClient = new RestClient(_config["Qdrant:ClusterUrl"] ?? string.Empty);
            _qdrantClient.AddDefaultHeader("api-key", _qdrantApiKey);

            _openAiClient = new RestClient("https://api.openai.com/v1");
            _openAiClient.AddDefaultHeader("Authorization", $"Bearer {_openAiApiKey}");
        }

        public async Task CreateCollectionAsync()
        {
            var request = new RestRequest($"/collections/{_collectionName}", Method.Put);
            request.AddJsonBody(new
            {
                vectors = new
                {
                    size = 1536,
                    distance = "Cosine",
                    on_disk = true // Better for production
                },
                optimizers_config = new
                {
                    indexing_threshold = 0,
                    memmap_threshold = 1000
                }
            });

            var response = await _qdrantClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new BusinessException($"Qdrant Collection Creation Failed: {response.Content}");
        }

        public async Task CreatePayloadIndexAsync(string fieldName, string fieldSchema)
        {
            var request = new RestRequest($"/collections/{_collectionName}/index", Method.Put);
            request.AddJsonBody(new
            {
                field_name = fieldName,
                field_schema = fieldSchema // Required type
            });

            var response = await _qdrantClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new BusinessException($"Failed to create payload index on '{fieldName}': {response.Content}");
        }

        public async Task AddDataPointAsync(string id, string content, Dictionary<string, object> payload)
        {
            var embedding = await GetEmbeddingAsync(content);

            var request = new RestRequest($"/collections/{_collectionName}/points?wait=true", Method.Put);
            request.AddJsonBody(new
            {
                points = new[]
                {
                    new
                    {
                        id,
                        vector = embedding,
                        payload
                    }
                }
            });

            var response = await _qdrantClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new BusinessException($"Failed to add point: {response.Content}");
        }

        public async Task<List<ProductResultDto>> ProductSearchAsync(ProductSearchDto dto)
        {
            var embedding = await GetEmbeddingAsync(dto.Query);

            var request = new RestRequest($"/collections/{_collectionName}/points/search", Method.Post);
            request.AddJsonBody(new
            {
                vector = embedding,
                limit = 5,
                with_payload = true,
                with_vectors = false, // We don't need the vectors in response
                score_threshold = 0.5, // Add a minimum similarity threshold
                exact = true, // Force exact search (not approximate)
                dto.Filter
            });

            var response = await _qdrantClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new BusinessException($"Search failed: {response.Content}");

            // More robust parsing
            try
            {
                var result = JsonConvert.DeserializeObject<QdrantSearchResponse>(response.Content ?? "{}");
                return result?.Results?
                    .OrderByDescending(r => r.Score)
                    .Select(r => new ProductResultDto
                    {
                        Id = r.Id,
                        Name = r.Payload?.Name ?? string.Empty
                    })
                    .ToList() ?? [];
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Failed to parse search results: {ex.Message}");
            }
        }

        private async Task<List<float>> GetEmbeddingAsync(string text)
        {
            var request = new RestRequest("embeddings", Method.Post);
            request.AddJsonBody(new
            {
                model = "text-embedding-3-small",
                input = text
            });

            var response = await _openAiClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new BusinessException($"Embedding error: {response.Content}");

            dynamic? result = JsonConvert.DeserializeObject(response.Content ?? string.Empty) ?? null;
            return result?.data[0].embedding.ToObject<List<float>>() ?? new List<float>();
        }
    }
}
