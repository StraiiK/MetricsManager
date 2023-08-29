using System.Net.Http;
using System.Text.Json;
using System;
using MetricsManagerUi.Responses;
using MetricsManagerUi.Requests;
using Microsoft.Extensions.Logging;

namespace MetricsManagerUi.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;
        private readonly JsonSerializerOptions _options;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger, JsonSerializerOptions options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options;
        }

        public AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromTime = Uri.EscapeDataString(request.FromTime.ToString("o"));
            var toTime = Uri.EscapeDataString(request.ToTime.ToString("o"));
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
            $"{request.AgentAddress}/api/metrics/cpu/getbyperiod?fromTime={fromTime}&toTime={toTime}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var result = JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream, _options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        //public AllDotNetMetricsApiResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        //{
        //    var fromTime = Uri.EscapeDataString(request.FromTime.ToString("o"));
        //    var toTime = Uri.EscapeDataString(request.ToTime.ToString("o"));
        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //    $"{request.AgentAddress}/api/metrics/dotnet/getbyperiod?fromTime={fromTime}&toTime={toTime}");

        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var result = JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream, _options).Result;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}

        //public AllNetworkMetricsApiResponse GetNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        //{
        //    var fromTime = Uri.EscapeDataString(request.FromTime.ToString("o"));
        //    var toTime = Uri.EscapeDataString(request.ToTime.ToString("o"));
        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //    $"{request.AgentAddress}/api/metrics/network/getbyperiod?fromTime={fromTime}&toTime={toTime}");

        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var result = JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream, _options).Result;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}

        //public AllRamMetricsApiResponse GetRamMetrics(GetAllRamMetricsApiRequest request)
        //{
        //    var fromTime = Uri.EscapeDataString(request.FromTime.ToString("o"));
        //    var toTime = Uri.EscapeDataString(request.ToTime.ToString("o"));
        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //    $"{request.AgentAddress}/api/metrics/ram/getbyperiod?fromTime={fromTime}&toTime={toTime}");

        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var result = JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream, _options).Result;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}

        //public AllRomMetricsApiResponse GetRomMetrics(GetAllRomMetricsApiRequest request)
        //{
        //    var fromTime = Uri.EscapeDataString(request.FromTime.ToString("o"));
        //    var toTime = Uri.EscapeDataString(request.ToTime.ToString("o"));
        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //    $"{request.AgentAddress}/api/metrics/rom/getbyperiod?fromTime={fromTime}&toTime={toTime}");

        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var result = JsonSerializer.DeserializeAsync<AllRomMetricsApiResponse>(responseStream, _options).Result;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}
    }
}