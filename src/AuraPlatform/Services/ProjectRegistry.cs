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
                Description = "Production RAG with hybrid retrieval (BM25 + vector), cross-encoder reranking, and citation enforcement",
                Subdomain = "ragdocs",
                Stack = "Python / FastAPI",
                HealthUrl = settings.P1HealthUrl,
                Port = 8001
            },
            new()
            {
                Id = "P2",
                Name = "cloudaura-slm",
                Description = "Local SLM benchmarking — run and compare models offline with Ollama",
                Subdomain = "localllm",
                Stack = "Python / FastAPI + Ollama",
                HealthUrl = settings.P2HealthUrl,
                Port = 8002
            },
            new()
            {
                Id = "P3",
                Name = "cloudaura-observe",
                Description = "Observability stack — Grafana dashboards, Prometheus metrics, Langfuse LLM tracing",
                Subdomain = "observe",
                Stack = "Grafana + Prometheus",
                HealthUrl = settings.P3HealthUrl,
                Port = 3000
            },
            new()
            {
                Id = "P4",
                Name = "cloudaura-finetune",
                Description = "Fine-tuning pipeline with LoRA/QLoRA + DPO for JSON extraction",
                Subdomain = "finetune",
                Stack = "Static (nginx:alpine) + Colab pipeline",
                HealthUrl = settings.P4HealthUrl,
                Port = 8004
            },
            new()
            {
                Id = "P5",
                Name = "cloudaura-voice",
                Description = "AI voice agent — handles inbound calls with STT, LLM, and TTS via LiveKit SIP",
                Subdomain = "voice",
                Stack = "Python / FastAPI",
                HealthUrl = settings.P5HealthUrl,
                Port = 8005
            },
            new()
            {
                Id = "P6",
                Name = "aura-platform",
                Description = "Multi-tenant infrastructure orchestration platform with RBAC, deployment pipelines, and FIFO queue",
                Subdomain = "platform",
                Stack = "C# / .NET 8 — ASP.NET Core Web API",
                HealthUrl = settings.P6HealthUrl,
                Port = 8006
            }
        };
    }

    public IReadOnlyList<ProjectInfo> GetAll() => _projects.AsReadOnly();

    public ProjectInfo? GetById(string id) =>
        _projects.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
}
