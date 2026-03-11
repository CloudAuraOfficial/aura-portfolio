using System.Collections.Concurrent;
using System.Diagnostics;
using AuraPlatform.Configuration;
using AuraPlatform.Models;
using Microsoft.Extensions.Options;

namespace AuraPlatform.Services;

public class ProjectStatusService : BackgroundService
{
    private readonly ProjectRegistry _registry;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ProjectStatusService> _logger;
    private readonly int _pollIntervalSeconds;
    private readonly ConcurrentDictionary<string, ProjectStatus> _statuses = new();

    public event Action? OnStatusChanged;

    public ProjectStatusService(
        ProjectRegistry registry,
        IHttpClientFactory httpClientFactory,
        IOptions<HealthCheckSettings> settings,
        ILogger<ProjectStatusService> logger)
    {
        _registry = registry;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _pollIntervalSeconds = settings.Value.PollIntervalSeconds;

        foreach (var project in _registry.GetAll())
        {
            _statuses[project.Id] = new ProjectStatus
            {
                Project = project,
                Health = project.HealthUrl is null ? HealthState.Unknown : HealthState.Unknown
            };
        }
    }

    public IReadOnlyDictionary<string, ProjectStatus> GetAllStatuses() =>
        _statuses.AsReadOnly();

    public ProjectStatus? GetStatus(string projectId) =>
        _statuses.GetValueOrDefault(projectId);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Health polling started with {IntervalSeconds}s interval",
            _pollIntervalSeconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            var projects = _registry.GetAll()
                .Where(p => p.HealthUrl is not null);

            var tasks = projects.Select(p => PollHealthAsync(p, stoppingToken));
            await Task.WhenAll(tasks);

            OnStatusChanged?.Invoke();

            await Task.Delay(TimeSpan.FromSeconds(_pollIntervalSeconds), stoppingToken);
        }
    }

    private async Task PollHealthAsync(ProjectInfo project, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var client = _httpClientFactory.CreateClient("HealthCheck");
            var response = await client.GetAsync(project.HealthUrl, ct);
            sw.Stop();

            var health = response.IsSuccessStatusCode
                ? HealthState.Healthy
                : HealthState.Degraded;

            _statuses[project.Id] = new ProjectStatus
            {
                Project = project,
                Health = health,
                LastChecked = DateTimeOffset.UtcNow,
                ResponseTimeMs = (int)sw.ElapsedMilliseconds
            };

            _logger.LogDebug("Health check {ProjectId}: {Status} ({ResponseMs}ms)",
                project.Id, health, sw.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            sw.Stop();
            _statuses[project.Id] = new ProjectStatus
            {
                Project = project,
                Health = HealthState.Unhealthy,
                LastChecked = DateTimeOffset.UtcNow,
                ResponseTimeMs = (int)sw.ElapsedMilliseconds,
                ErrorMessage = ex.Message
            };

            _logger.LogWarning(ex, "Health check failed for {ProjectId}", project.Id);
        }
    }
}
