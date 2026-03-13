namespace AuraPlatform.Configuration;

public class HealthCheckSettings
{
    public const string SectionName = "HealthCheck";

    public string? P1HealthUrl { get; set; }
    public string? P2HealthUrl { get; set; }
    public string? P3HealthUrl { get; set; }
    public string? P4HealthUrl { get; set; }
    public string? P5HealthUrl { get; set; }
    public string? P6HealthUrl { get; set; }
    public int PollIntervalSeconds { get; set; } = 30;
}
