using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.Interfaces;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Abp.eCommerce.Services
{
    public class MpesaAppService : eCommerceAppService, IMpesaAppService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public MpesaAppService(
            IConfiguration config,
            IHttpClientFactory httpClientFactory
        )
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                $"{_config["Mpesa:ConsumerKey"]}:{_config["Mpesa:ConsumerSecret"]}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var response = await client.GetAsync($"{_config["Mpesa:BaseUrl"]}/oauth/v1/generate?grant_type=client_credentials");
            var content = await response.Content.ReadFromJsonAsync<JsonElement>();

            return content.GetProperty("access_token").GetString();
        }

        public async Task<string> InitiateSTKPushAsync(MpesaStkPushRequestDto input)
        {
            string token = await GetAccessTokenAsync();

            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    _config["Mpesa:Shortcode"] + _config["Mpesa:Passkey"] + timestamp
                )
            );

            var payload = new MpesaStkPushPayload
            {
                BusinessShortCode = _config["Mpesa:Shortcode"]?.To<long>() ?? 0,
                Password = password,
                Timestamp = timestamp,
                TransactionType = "CustomerPayBillOnline",
                Amount = input.Amount,
                PartyA = input.PhoneNumber,
                PartyB = _config["Mpesa:Shortcode"]?.To<long>() ?? 0,
                PhoneNumber = input.PhoneNumber,
                CallBackURL = _config["Mpesa:CallbackUrl"] ?? string.Empty,
                AccountReference = input.AccountReference,
                TransactionDesc = input.TransactionDescription
            };

            var client = new RestClient(_config["Mpesa:BaseUrl"] ?? string.Empty);
            var request = new RestRequest("mpesa/stkpush/v1/processrequest", Method.Post);
            var json = JsonConvert.SerializeObject(payload);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            return response.Content ?? string.Empty; 
        }
    }
}
