namespace AuraPlatform.Models;

public record ProjectStatus
{
    public required ProjectInfo Project { get; init; }
    public HealthState Health { get; init; } = HealthState.Unknown;
    public DateTimeOffset LastChecked { get; init; } = DateTimeOffset.MinValue;
    public int? ResponseTimeMs { get; init; }
    public string? ErrorMessage { get; init; }
}

public enum HealthState
{
    Healthy,
    Degraded,
    Unhealthy,
    Unknown
}
