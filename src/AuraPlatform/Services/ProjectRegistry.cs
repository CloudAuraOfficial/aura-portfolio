using AuraPlatform.Configuration;
using AuraPlatform.Models;
using Microsoft.Extensions.Options;

namespace AuraPlatform.Services;

public class ProjectRegistry
{
    private readonly List<ProjectInfo> _projects;

    public ProjectRegistry(IOptions<HealthCheckSettings> healthSettings)
    {
        var settings = healthSettings.Value;

        _projects = new List<ProjectInfo>
        {
            new()
            {
                Id = "P1",
                Name = "cloudaura-rag",
                Description = "RAG-powered documentation assistant",
                Subdomain = "ragdocs",
                Stack = "Python / FastAPI",
                HealthUrl = settings.P1HealthUrl,
                Port = 8001
            },
            new()
            {
                Id = "P2",
                Name = "cloudaura-slm",
                Description = "Small Language Model showcase",
                Subdomain = "localllm",
                Stack = "Static HTML",
                Port = 0
            },
            new()
            {
                Id = "P3",
                Name = "cloudaura-observe",
                Description = "Observability stack (Grafana + Prometheus)",
                Subdomain = "observe",
                Stack = "Grafana + Prometheus",
                HealthUrl = settings.P3HealthUrl,
                Port = 3000
            },
            new()
            {
                Id = "P4",
                Name = "cloudaura-finetune",
                Description = "Fine-tuning pipeline showcase",
                Subdomain = "finetune",
                Stack = "Static HTML",
                Port = 0
            },
            new()
            {
                Id = "P5",
                Name = "cloudaura-voice",
                Description = "AI Voice Agent for customer support",
                Subdomain = "voice",
                Stack = "Python / FastAPI",
                HealthUrl = settings.P5HealthUrl,
                Port = 8005
            },
            new()
            {
                Id = "P6",
                Name = "aura-platform",
                Description = "Management portal & dashboard",
                Subdomain = "aura",
                Stack = ".NET 8 / Blazor Server",
                Port = 8006
            }
        };
    }

    public IReadOnlyList<ProjectInfo> GetAll() => _projects.AsReadOnly();

    public ProjectInfo? GetById(string id) =>
        _projects.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
}
