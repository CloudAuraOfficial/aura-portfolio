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
}
