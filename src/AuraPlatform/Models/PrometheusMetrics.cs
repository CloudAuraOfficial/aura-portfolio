namespace AuraPlatform.Models;

public record ServiceMetrics
{
    public required string ServiceName { get; init; }
    public double RequestsPerSecond { get; init; }
    public double ErrorRate { get; init; }
    public double AvgResponseTimeMs { get; init; }
    public DateTimeOffset Timestamp { get; init; }
}
