using System.Text.Json;
using AuraPlatform.Configuration;
using AuraPlatform.Models;
using Microsoft.Extensions.Options;

namespace AuraPlatform.Services;

public class MetricsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<MetricsService> _logger;
    private readonly string? _prometheusUrl;

    public MetricsService(
        IHttpClientFactory httpClientFactory,
        IOptions<PrometheusSettings> settings,
        ILogger<MetricsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _prometheusUrl = settings.Value.Url?.TrimEnd('/');
    }

    public bool IsAvailable => !string.IsNullOrEmpty(_prometheusUrl);

    public async Task<ServiceMetrics?> GetServiceMetricsAsync(
        string serviceName, CancellationToken ct = default)
    {
        if (!IsAvailable)
        {
            _logger.LogDebug("Prometheus not configured, skipping metrics for {Service}", serviceName);
            return null;
        }

        try
        {
            var client = _httpClientFactory.CreateClient("Prometheus");

            var rpsQuery = $"rate(http_requests_total{{service=\"{serviceName}\"}}[5m])";
            var rps = await QueryPrometheusAsync(client, rpsQuery, ct);

            var errorQuery = $"rate(http_requests_total{{service=\"{serviceName}\",status=~\"5..\"}}[5m])";
            var errorRate = await QueryPrometheusAsync(client, errorQuery, ct);

            var latencyQuery = $"rate(http_request_duration_seconds_sum{{service=\"{serviceName}\"}}[5m]) / rate(http_request_duration_seconds_count{{service=\"{serviceName}\"}}[5m])";
            var avgLatency = await QueryPrometheusAsync(client, latencyQuery, ct);

            return new ServiceMetrics
            {
                ServiceName = serviceName,
                RequestsPerSecond = rps,
                ErrorRate = errorRate,
                AvgResponseTimeMs = avgLatency * 1000,
                Timestamp = DateTimeOffset.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch Prometheus metrics for {Service}", serviceName);
            return null;
        }
    }

    private async Task<double> QueryPrometheusAsync(
        HttpClient client, string query, CancellationToken ct)
    {
        var url = $"{_prometheusUrl}/api/v1/query?query={Uri.EscapeDataString(query)}";
        var response = await client.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        using var doc = await JsonDocument.ParseAsync(
            await response.Content.ReadAsStreamAsync(ct), cancellationToken: ct);

        var result = doc.RootElement
            .GetProperty("data")
            .GetProperty("result");

        if (result.GetArrayLength() == 0)
            return 0;

        var valueStr = result[0]
            .GetProperty("value")[1]
            .GetString();

        return double.TryParse(valueStr, out var value) ? value : 0;
    }
}
