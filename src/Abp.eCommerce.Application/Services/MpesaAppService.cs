using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.Enums;
using Abp.eCommerce.Interfaces;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Interfaces;
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
using Volo.Abp;
using ZstdSharp.Unsafe;

namespace Abp.eCommerce.Services
{
    public class MpesaAppService : eCommerceAppService, IMpesaAppService
    {
        #region Fields
        private readonly IConfiguration _config;
        private readonly IMpesaTransactionAppService _mpesaTransactionAppService;
        #endregion

        #region CTOR
        public MpesaAppService(
            IConfiguration config,
            IMpesaTransactionAppService mpesaTransactionAppService
        )
        {
            _config = config;
            _mpesaTransactionAppService = mpesaTransactionAppService;
        }
        #endregion 

        public async Task<string> GetAccessTokenAsync()
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                $"{_config["Mpesa:ConsumerKey"]}:{_config["Mpesa:ConsumerSecret"]}"));

            var client = new RestClient(_config["Mpesa:BaseUrl"] ?? string.Empty);

            var request = new RestRequest("oauth/v1/generate?grant_type=client_credentials", Method.Get);
            request.AddHeader("Authorization", $"Basic {credentials}");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            {
                throw new Exception($"Failed to get access token: {response.StatusCode} - {response.Content}");
            }

            using var document = JsonDocument.Parse(response.Content);
            var root = document.RootElement;

            if (root.TryGetProperty("access_token", out var tokenElement))
            {
                return tokenElement.GetString() ?? string.Empty;
            }

            throw new Exception("access_token not found in the response.");
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

            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(response.Content))
            {
                var responseDto = JsonConvert.DeserializeObject<MpesaStkPushResponse>(response.Content);

                if (responseDto is not null && responseDto.ResponseCode == 0)
                {
                    var transaction = new CreateUpdateMpesaTransactionDto
                    {
                        PaymentTransactionId = input.PaymentTransactionId,
                        MerchantRequestId = responseDto.MerchantRequestId,
                        CheckoutRequestId = responseDto.CheckoutRequestId,
                        ResponseCode = responseDto.ResponseCode,
                        ResponseDecription = responseDto.ResponseDecription,
                        CustomerMessage = responseDto.CustomerMessage,
                        Status = MpesaTransactionStatusEnum.Sent
                    };

                    await _mpesaTransactionAppService.CreateAsync(transaction);
                }
                else
                {
                    // Save as failed or log a problem if ResponseCode is not success (usually "0")
                    await _mpesaTransactionAppService.CreateAsync(new CreateUpdateMpesaTransactionDto
                    {
                        PaymentTransactionId = input.PaymentTransactionId,
                        ResponseCode = responseDto?.ResponseCode,
                        ResponseDecription = responseDto?.ResponseDecription ?? "Invalid Response",
                        Status = MpesaTransactionStatusEnum.Failed
                    });
                }
            }
            else
            {
                // Log or persist failure
                await _mpesaTransactionAppService.CreateAsync(new CreateUpdateMpesaTransactionDto
                {
                    PaymentTransactionId = input.PaymentTransactionId,
                    ResponseDecription = $"HTTP Error: {(int)response.StatusCode} - {response.Content}",
                    Status = MpesaTransactionStatusEnum.Error
                });
            }

            return response.Content ?? string.Empty; 
        }
    }
}
