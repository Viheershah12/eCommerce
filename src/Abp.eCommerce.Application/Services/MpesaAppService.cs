using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.Enums;
using Abp.eCommerce.HangfireJobArgs;
using Abp.eCommerce.Hubs;
using Abp.eCommerce.Interfaces;
using Amazon.Runtime.Internal.Util;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Interfaces;
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
using System.Transactions;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.Local;

namespace Abp.eCommerce.Services
{
    public class MpesaAppService : eCommerceAppService, IMpesaAppService
    {
        #region Fields
        private readonly IConfiguration _config;
        private readonly IMpesaTransactionAppService _mpesaTransactionAppService;
        private readonly ILogger<MpesaAppService> _logger;
        private readonly IPaymentTransactionAppService _paymentTransactionAppService;
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly IHubContext<TransactionHub> _hubContext;
        private readonly IBackgroundJobManager _backgroundJobManager;
        //private readonly IBackgroundJobClient _backgroundJobClient;
        #endregion

        #region CTOR
        public MpesaAppService(
            IConfiguration config,
            IMpesaTransactionAppService mpesaTransactionAppService,
            ILogger<MpesaAppService> logger,
            IPaymentTransactionAppService paymentTransactionAppService,
            IOrderTransactionAppService orderTransactionAppService,
            IHubContext<TransactionHub> hubContext,
            IBackgroundJobManager backgroundJobManager
            //IBackgroundJobClient backgroundJobClient
        )
        {
            _config = config;
            _mpesaTransactionAppService = mpesaTransactionAppService;
            _logger = logger;
            _paymentTransactionAppService = paymentTransactionAppService;
            _orderTransactionAppService = orderTransactionAppService;
            _hubContext = hubContext;
            _backgroundJobManager = backgroundJobManager;
            //_backgroundJobClient = backgroundJobClient;
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
                    await _backgroundJobManager.EnqueueAsync(new MpesaTransactionCheckArgs
                    {
                        PaymentTransactionId = transaction.PaymentTransactionId
                    }, BackgroundJobPriority.High, delay: TimeSpan.FromSeconds(30));
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

        public async Task CheckTransactionAsync(Guid transactionId)
        {
            try
            {
                string token = await GetAccessTokenAsync();
                var tx = await _mpesaTransactionAppService.GetByTransactionIdAysnc(transactionId);

                var client = new RestClient(_config["Mpesa:BaseUrl"] ?? string.Empty);
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var password = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(_config["Mpesa:Shortcode"] + _config["Mpesa:Passkey"] + timestamp)
                );

                var queryPayload = new MpesaStkPushQueryRequest
                {
                    BusinessShortCode = _config["Mpesa:Shortcode"]?.To<long>() ?? 0,
                    Password = password,
                    Timestamp = timestamp,
                    CheckoutRequestID = tx.CheckoutRequestId
                };

                var request = new RestRequest("mpesa/stkpushquery/v1/query", Method.Post);
                var json = JsonConvert.SerializeObject(queryPayload);

                request.AddHeader("Authorization", $"Bearer {token}");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(response.Content))
                {
                    var result = JsonConvert.DeserializeObject<MpesaStkPushQueryResponse>(response.Content);

                    if (result != null && result.ResponseCode == 0)
                    {
                        var paymentTransaction = await _paymentTransactionAppService.GetAsync(tx.PaymentTransactionId);
                        var order = await _orderTransactionAppService.GetAsync(paymentTransaction.OrderId);

                        switch (result.ResultCode)
                        {
                            case 0:
                                tx.Status = MpesaTransactionStatusEnum.Confirmed;
                                paymentTransaction.Status = PaymentTransactionStatus.Completed;
                                order.PaymentStatus = PaymentStatus.Paid;
                                order.Status = OrderStatus.Processing;

                                order.PaymentMethod = PaymentMethodEnum.MpesaStk;
                                order.PaymentMethodSystemName = PaymentMethodEnum.MpesaStk.ToString();

                                order.PaidDate = DateTime.Now;
                                break;
                            case 1:
                                tx.Status = MpesaTransactionStatusEnum.Failed;
                                paymentTransaction.Status = PaymentTransactionStatus.Failed;
                                order.PaymentStatus = PaymentStatus.UnPaid;
                                order.Status = OrderStatus.Pending;
                                break;
                            case 1032:
                                tx.Status = MpesaTransactionStatusEnum.Cancelled;
                                paymentTransaction.Status = PaymentTransactionStatus.Cancelled;
                                order.PaymentStatus = PaymentStatus.UnPaid;
                                order.Status = OrderStatus.Pending;
                                break;
                            case 1037:
                                tx.Status = MpesaTransactionStatusEnum.Timeout;
                                paymentTransaction.Status = PaymentTransactionStatus.Cancelled;
                                order.PaymentStatus = PaymentStatus.UnPaid;
                                order.Status = OrderStatus.Pending;
                                break;
                            case 2001:
                                tx.Status = MpesaTransactionStatusEnum.Error;
                                paymentTransaction.Status = PaymentTransactionStatus.Failed;
                                order.PaymentStatus = PaymentStatus.UnPaid;
                                order.Status = OrderStatus.Pending;
                                break;
                            default:
                                tx.Status = MpesaTransactionStatusEnum.Failed;
                                paymentTransaction.Status = PaymentTransactionStatus.Failed;
                                order.PaymentStatus = PaymentStatus.UnPaid;
                                order.Status = OrderStatus.Pending;
                                break;
                        }

                        tx.ConfirmedOn = DateTime.Now;
                        tx.ResultCode = result.ResultCode;
                        tx.ResultDesc = result.ResultDesc;

                        await _mpesaTransactionAppService.UpdateAsync(tx);
                        await _paymentTransactionAppService.UpdateAsync(paymentTransaction);
                        await _orderTransactionAppService.UpdateAsync(order);

                        await _hubContext.Clients
                            .User(order.CustomerId.ToString())
                            .SendAsync("ReceiveTransactionStatus", tx.Status);
                    }
                    else
                    {
                        // log failure or retry later
                        _logger.Log(LogLevel.Warning, response.Content);
                    }
                }
                else
                {
                    // log HTTP error
                    _logger.Log(LogLevel.Error, response.Content);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task CheckTransactionStatusAsync()
        {
            try
            {
                string token = await GetAccessTokenAsync();

                // Get all pending transactions
                var pendingTransactions = (await _mpesaTransactionAppService.GetListAsync(new GetMpesaTransactionListDto { MaxResultCount = 1000 })).Items.ToList();
                pendingTransactions = pendingTransactions.Where(x => x.Status == MpesaTransactionStatusEnum.Sent).ToList();

                var client = new RestClient(_config["Mpesa:BaseUrl"] ?? string.Empty);
                foreach (var tx in pendingTransactions)
                {
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var password = Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(_config["Mpesa:Shortcode"] + _config["Mpesa:Passkey"] + timestamp)
                    );

                    var queryPayload = new MpesaStkPushQueryRequest
                    {
                        BusinessShortCode = _config["Mpesa:Shortcode"]?.To<long>() ?? 0,
                        Password = password,
                        Timestamp = timestamp,
                        CheckoutRequestID = tx.CheckoutRequestId
                    };

                    var request = new RestRequest("mpesa/stkpushquery/v1/query", Method.Post);
                    var json = JsonConvert.SerializeObject(queryPayload);

                    request.AddHeader("Authorization", $"Bearer {token}");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", json, ParameterType.RequestBody);

                    var response = await client.ExecuteAsync(request);

                    if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(response.Content))
                    {
                        var result = JsonConvert.DeserializeObject<MpesaStkPushQueryResponse>(response.Content);

                        if (result != null && result.ResponseCode == 0)
                        {
                            var paymentTransaction = await _paymentTransactionAppService.GetAsync(tx.PaymentTransactionId);
                            var order = await _orderTransactionAppService.GetAsync(paymentTransaction.OrderId);

                            switch (result.ResultCode)
                            {
                                case 0:
                                    tx.Status = MpesaTransactionStatusEnum.Confirmed;
                                    paymentTransaction.Status = PaymentTransactionStatus.Completed;
                                    order.PaymentStatus = PaymentStatus.Paid;
                                    order.Status = OrderStatus.Processing;

                                    order.PaymentMethod = PaymentMethodEnum.MpesaStk;
                                    order.PaymentMethodSystemName = PaymentMethodEnum.MpesaStk.ToString();

                                    order.PaidDate = DateTime.Now;
                                    break;
                                case 1:
                                    tx.Status = MpesaTransactionStatusEnum.Failed;
                                    paymentTransaction.Status = PaymentTransactionStatus.Failed;
                                    order.PaymentStatus = PaymentStatus.UnPaid;
                                    order.Status = OrderStatus.Pending;
                                    break;
                                case 1032:
                                    tx.Status = MpesaTransactionStatusEnum.Cancelled;
                                    paymentTransaction.Status = PaymentTransactionStatus.Cancelled;
                                    order.PaymentStatus = PaymentStatus.UnPaid;
                                    order.Status = OrderStatus.Pending;
                                    break;
                                case 1037:
                                    tx.Status = MpesaTransactionStatusEnum.Timeout;
                                    paymentTransaction.Status = PaymentTransactionStatus.Cancelled;
                                    order.PaymentStatus = PaymentStatus.UnPaid;
                                    order.Status = OrderStatus.Pending;
                                    break;
                                case 2001:
                                    tx.Status = MpesaTransactionStatusEnum.Error;
                                    paymentTransaction.Status = PaymentTransactionStatus.Failed;
                                    order.PaymentStatus = PaymentStatus.UnPaid;
                                    order.Status = OrderStatus.Pending;
                                    break;
                                default:
                                    tx.Status = MpesaTransactionStatusEnum.Failed;
                                    paymentTransaction.Status = PaymentTransactionStatus.Failed;
                                    order.PaymentStatus = PaymentStatus.UnPaid;
                                    order.Status = OrderStatus.Pending;
                                    break;
                            }

                            tx.ConfirmedOn = DateTime.Now;
                            tx.ResultCode = result.ResultCode;
                            tx.ResultDesc = result.ResultDesc;

                            var dto = ObjectMapper.Map<MpesaTransactionDto, CreateUpdateMpesaTransactionDto>(tx);

                            await _mpesaTransactionAppService.UpdateAsync(dto);
                            await _paymentTransactionAppService.UpdateAsync(paymentTransaction);
                            await _orderTransactionAppService.UpdateAsync(order);

                            await _hubContext.Clients
                                .User(order.CustomerId.ToString())
                                .SendAsync("ReceiveTransactionStatus", tx.Status);
                        }
                        else
                        {
                            // log failure or retry later
                            _logger.Log(LogLevel.Warning, response.Content);
                        }
                    }
                    else
                    {
                        // log HTTP error
                        _logger.Log(LogLevel.Error, response.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }            
        }
    }
}
