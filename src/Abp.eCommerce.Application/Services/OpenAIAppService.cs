using Abp.eCommerce.Dtos.OpenAI;
using Abp.eCommerce.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using static MongoDB.Driver.WriteConcern;

namespace Abp.eCommerce.Services
{
    public class OpenAIAppService : eCommerceAppService, IOpenAIAppService
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region CTOR
        public OpenAIAppService(
            IConfiguration configuration
        ) 
        {
            _configuration = configuration;
        }
        #endregion 

        public async Task<string> GetSuggestedProductsAsync(string prompt)
        {
            try
            {
                var apiKey = _configuration["OpenAI:ApiKey"];
                var model = _configuration["OpenAI:Model"];

                var client = new RestClient(_configuration["OpenAI:BaseUrl"] ?? string.Empty);
                var request = new RestRequest("v1/chat/completions", Method.Post);

                request.AddHeader("Authorization", $"Bearer {apiKey}");
                request.AddHeader("Content-Type", "application/json");

                var body = new
                {
                    model,
                    messages = new[]
                    {
                        new { role = "system", content = "You're an AI that recommends similar products based on category and tags." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.7
                };

                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                    throw new Exception($"OpenAI API failed: {response.StatusCode} - {response.Content}");
                }

                var doc = JsonDocument.Parse(response.Content ?? string.Empty);
                return doc?.RootElement
                          .GetProperty("choices")[0]
                          .GetProperty("message")
                          .GetProperty("content")
                          .GetString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<ProductAttributeDto> ExtractProductAttributesAsync(string productName)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            var model = _configuration["OpenAI:Model"];

            var client = new RestClient(_configuration["OpenAI:BaseUrl"] ?? string.Empty);
            var request = new RestRequest("v1/chat/completions", Method.Post);

            request.AddHeader("Authorization", $"Bearer {apiKey}");
            request.AddHeader("Content-Type", "application/json");

            #region Prompt
            var prompt =
                "Extract product attributes from the product name below and return them in strict JSON format:\n\n" +
                $"Product Name: \"{productName}\"\n\n" +
                "Return only JSON with the following fields:\n" +
                "{\n" +
                "  \"brand\": string,\n" +
                "  \"color\": string,\n" +
                "  \"size\": string,\n" +
                "  \"material\": string,\n" +
                "  \"type\": string\n" +
                "}\n\n" +
                "Use null for any unknown values. Do not include any explanation or extra text.\n" +
                "Output format example:\n" +
                "{\n" +
                "  \"brand\": \"HP\",\n" +
                "  \"color\": \"black\",\n" +
                "  \"size\": null,\n" +
                "  \"material\": null,\n" +
                "  \"type\": \"flash drive\"\n" +
                "}";
            #endregion 

            request.AddJsonBody(new
            {
                model = _configuration["OpenAI:Model"],
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful product attribute extractor." },
                    new { role = "user", content = prompt }
                },
                response_format = new { type = "json_object" },
                temperature = 0.1 
            });

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new BusinessException($"OpenAI request failed: {response.Content}");
            }

            dynamic result = JsonConvert.DeserializeObject(response.Content)!;
            var jsonResponse = result.choices[0].message.content.ToString();

            try
            {
                return JsonConvert.DeserializeObject<ProductAttributeDto>(jsonResponse) ?? new ProductAttributeDto();
            }
            catch
            {
                return new ProductAttributeDto
                {
                    Brand = null,
                    Color = null,
                    Size = null
                };
            }
        }

        
    }
}
