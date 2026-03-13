namespace AuraPlatform.Models;

public record ProjectInfo
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Subdomain { get; init; }
    public required string Stack { get; init; }
    public string? HealthUrl { get; init; }
    public int Port { get; init; }

    // Detail page content
    public string Problem { get; init; } = string.Empty;
    public string Solution { get; init; } = string.Empty;
    public List<string> CoreFeatures { get; init; } = [];
    public List<string> MarketSignals { get; init; } = [];
    public List<BuildMilestone> BuildProgress { get; init; } = [];
    public List<TechStackItem> TechStack { get; init; } = [];
    public string TargetUser { get; init; } = string.Empty;
    public string? LiveUrl { get; init; }
    public string? GithubUrl { get; init; }
}

public record BuildMilestone
{
    public required string Label { get; init; }
    public bool Completed { get; init; }
}

public record TechStackItem
{
    public required string Category { get; init; }
    public required string Technology { get; init; }
    public string? Note { get; init; }
}
