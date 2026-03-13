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
                Port = 8001,
                Problem = "Teams drown in documentation sprawl. Traditional keyword search returns irrelevant results because it cannot understand intent, synonyms, or context — leading engineers to re-ask the same questions or read entire documents manually.",
                Solution = "A hybrid retrieval-augmented generation system that fuses keyword precision (BM25) with semantic understanding (vector search), applies cross-encoder reranking for high-precision results, and generates cited answers grounded in retrieved context using a local LLM.",
                CoreFeatures =
                [
                    "Hybrid retrieval combining BM25 keyword matching with ChromaDB vector search",
                    "Reciprocal Rank Fusion to merge dual retrieval signals",
                    "Cross-encoder reranking with ms-marco-MiniLM for precision",
                    "Citation-enforced answer generation — every claim traces to a source chunk",
                    "Fully local inference via Ollama (phi3:mini) — no data leaves the server",
                    "Recursive character text splitting with 512-token overlapping chunks",
                    "Ragas evaluation pipeline for faithfulness, relevancy, and context precision"
                ],
                MarketSignals =
                [
                    "RAG market projected to reach $40B+ by 2030 as enterprises adopt AI-powered knowledge retrieval",
                    "Hybrid search (keyword + vector) outperforms either method alone by 15–30% on retrieval benchmarks",
                    "Cross-encoder reranking is emerging as a standard post-retrieval step in production RAG pipelines",
                    "Growing demand for on-premises / local-first AI solutions due to data privacy regulations"
                ],
                BuildProgress =
                [
                    new() { Label = "Document ingestion and chunking pipeline", Completed = true },
                    new() { Label = "BM25 + Vector hybrid retrieval with RRF", Completed = true },
                    new() { Label = "Cross-encoder reranking integration", Completed = true },
                    new() { Label = "Citation-enforced generation with Ollama", Completed = true },
                    new() { Label = "FastAPI endpoints with structured error handling", Completed = true },
                    new() { Label = "Ragas evaluation pipeline", Completed = true },
                    new() { Label = "Frontend dashboard", Completed = true },
                    new() { Label = "Docker Compose deployment with health checks", Completed = true },
                    new() { Label = "Prometheus metrics instrumentation", Completed = true }
                ],
                TechStack =
                [
                    new() { Category = "Framework", Technology = "FastAPI", Note = "Async" },
                    new() { Category = "Embeddings", Technology = "all-MiniLM-L6-v2", Note = "Local" },
                    new() { Category = "Vector Store", Technology = "ChromaDB", Note = "Persistent" },
                    new() { Category = "Keyword Search", Technology = "BM25Okapi", Note = "In-Memory" },
                    new() { Category = "Reranker", Technology = "ms-marco-MiniLM-L-6-v2", Note = "Cross-Encoder" },
                    new() { Category = "LLM", Technology = "Ollama / phi3:mini", Note = "Local" },
                    new() { Category = "Evaluation", Technology = "Ragas", Note = "Automated" },
                    new() { Category = "Metrics", Technology = "Prometheus", Note = "FastAPI Instrumentator" }
                ],
                TargetUser = "Engineering teams and DevOps orgs with large documentation sets (runbooks, wikis, internal knowledge bases) who need instant, cited answers without sending data to external APIs.",
                LiveUrl = "https://ragdocs.cloudaura.cloud",
                GithubUrl = "https://github.com/CloudAuraOfficial/cloudaura-rag"
            },
            new()
            {
                Id = "P2",
                Name = "cloudaura-slm",
                Description = "Local SLM benchmarking — run and compare models offline with Ollama",
                Subdomain = "localllm",
                Stack = "Python / FastAPI + Ollama",
                HealthUrl = settings.P2HealthUrl,
                Port = 8002,
                Problem = "Choosing the right small language model for a specific use case is guesswork. Teams waste days testing models one at a time with ad-hoc prompts, lacking standardized benchmarks for throughput, latency, and quality on their own hardware.",
                Solution = "A benchmarking dashboard that runs standardized tests across multiple local SLMs via Ollama, measuring tokens-per-second, time-to-first-token, latency, and output quality — all on your own hardware with no cloud dependency.",
                CoreFeatures =
                [
                    "Multi-model benchmarking with standardized test prompts",
                    "Tokens-per-second, time-to-first-token, and end-to-end latency measurement",
                    "Side-by-side model comparison with bar charts and data tables",
                    "Interactive chat interface to test models qualitatively",
                    "Hardware-aware benchmarks that reflect real performance on your server",
                    "Ollama integration for seamless model management",
                    "Historical benchmark results with timestamps"
                ],
                MarketSignals =
                [
                    "Small Language Models (SLMs) are the fastest-growing segment — phi3, gemma, qwen all released in 2024",
                    "Edge AI and on-device inference market growing 25%+ YoY as companies avoid cloud API costs",
                    "MLOps teams need reproducible benchmarking to justify model selection decisions",
                    "Ollama downloads exceeded 10M+ as local inference becomes mainstream"
                ],
                BuildProgress =
                [
                    new() { Label = "Ollama integration and model management", Completed = true },
                    new() { Label = "Benchmark execution engine", Completed = true },
                    new() { Label = "Multi-model comparison API", Completed = true },
                    new() { Label = "Interactive chat interface", Completed = true },
                    new() { Label = "Frontend dashboard with charts", Completed = true },
                    new() { Label = "Docker Compose deployment", Completed = true },
                    new() { Label = "Prometheus metrics instrumentation", Completed = true }
                ],
                TechStack =
                [
                    new() { Category = "Framework", Technology = "FastAPI", Note = "Async" },
                    new() { Category = "LLM Runtime", Technology = "Ollama", Note = "Local" },
                    new() { Category = "Models", Technology = "phi3, gemma, qwen", Note = "Configurable" },
                    new() { Category = "Metrics", Technology = "Prometheus", Note = "FastAPI Instrumentator" },
                    new() { Category = "Container", Technology = "Docker Compose", Note = "Multi-service" }
                ],
                TargetUser = "ML engineers, DevOps teams, and AI startups evaluating which small language model to deploy on-premises or at the edge, who need reproducible performance data without cloud dependencies.",
                LiveUrl = "https://localllm.cloudaura.cloud",
                GithubUrl = "https://github.com/CloudAuraOfficial/cloudaura-slm"
            },
            new()
            {
                Id = "P3",
                Name = "cloudaura-observe",
                Description = "Observability stack — Grafana dashboards, Prometheus metrics, Langfuse LLM tracing",
                Subdomain = "observe",
                Stack = "Grafana + Prometheus + Langfuse",
                HealthUrl = settings.P3HealthUrl,
                Port = 3000,
                Problem = "Running multiple AI services without observability is flying blind. When latency spikes, errors surface, or a model starts producing poor results, teams have no centralized way to see what's happening across the stack.",
                Solution = "A unified observability platform that aggregates metrics from all portfolio services via Prometheus, visualizes them through pre-configured Grafana dashboards, monitors host health with Node Exporter, and traces LLM calls with Langfuse.",
                CoreFeatures =
                [
                    "Pre-provisioned Grafana dashboards for portfolio-wide service monitoring",
                    "Prometheus scraping all FastAPI and .NET services via /metrics endpoints",
                    "Node Exporter for host CPU, memory, disk, and network metrics",
                    "Langfuse for LLM-specific observability — trace RAG queries end-to-end",
                    "Alert rules for service down, high error rate, high latency, CPU/memory/disk thresholds",
                    "Auto-provisioned datasources and dashboards — zero manual setup"
                ],
                MarketSignals =
                [
                    "Observability market projected at $62B by 2030 — critical for AI workloads",
                    "LLM observability is an emerging category — Langfuse, LangSmith, and Helicone all raised Series A/B in 2024",
                    "Prometheus + Grafana remains the most adopted open-source monitoring stack globally",
                    "AI/ML teams increasingly need tracing beyond traditional APM to debug model quality issues"
                ],
                BuildProgress =
                [
                    new() { Label = "Prometheus configuration with multi-service scraping", Completed = true },
                    new() { Label = "Grafana with auto-provisioned datasources", Completed = true },
                    new() { Label = "Portfolio Overview dashboard", Completed = true },
                    new() { Label = "Host Metrics dashboard (Node Exporter)", Completed = true },
                    new() { Label = "Alert rules (6 rules)", Completed = true },
                    new() { Label = "Langfuse deployment for LLM tracing", Completed = true },
                    new() { Label = "Docker Compose with host networking for Prometheus", Completed = true },
                    new() { Label = "Nginx + SSL for observe and langfuse subdomains", Completed = true }
                ],
                TechStack =
                [
                    new() { Category = "Metrics", Technology = "Prometheus", Note = "Host network mode" },
                    new() { Category = "Visualization", Technology = "Grafana", Note = "Auto-provisioned" },
                    new() { Category = "Host Metrics", Technology = "Node Exporter", Note = "CPU/Memory/Disk" },
                    new() { Category = "LLM Tracing", Technology = "Langfuse", Note = "Self-hosted" },
                    new() { Category = "Database", Technology = "PostgreSQL 16", Note = "Langfuse backend" },
                    new() { Category = "Container", Technology = "Docker Compose", Note = "5 services" }
                ],
                TargetUser = "Platform engineers and SREs managing AI/ML services in production who need a single pane of glass for both traditional infrastructure metrics and LLM-specific observability.",
                LiveUrl = "https://observe.cloudaura.cloud",
                GithubUrl = "https://github.com/CloudAuraOfficial/cloudaura-observe"
            },
            new()
            {
                Id = "P4",
                Name = "cloudaura-finetune",
                Description = "Fine-tuning pipeline with LoRA/QLoRA + DPO for JSON extraction",
                Subdomain = "finetune",
                Stack = "Static (nginx:alpine) + Colab pipeline",
                HealthUrl = settings.P4HealthUrl,
                Port = 8004,
                Problem = "Off-the-shelf LLMs produce unreliable structured output. When you need consistent JSON extraction from unstructured text, generic models hallucinate fields, break schemas, and require extensive prompt engineering that doesn't generalize.",
                Solution = "A documented fine-tuning pipeline using LoRA/QLoRA for parameter-efficient training and DPO for alignment, targeting reliable JSON extraction. The static site documents the methodology, results, and provides reproducible Colab notebooks.",
                CoreFeatures =
                [
                    "LoRA/QLoRA parameter-efficient fine-tuning for structured output",
                    "Direct Preference Optimization (DPO) for alignment on extraction quality",
                    "Reproducible Google Colab notebooks for training and evaluation",
                    "Before/after benchmark comparisons showing improvement over base models",
                    "Detailed methodology documentation with pipeline visualization",
                    "JSON schema enforcement and validation metrics"
                ],
                MarketSignals =
                [
                    "Fine-tuning demand surged 400%+ in 2024 as enterprises move beyond prompt engineering",
                    "LoRA/QLoRA reduced fine-tuning compute costs by 10–100x, making it accessible to small teams",
                    "DPO is replacing RLHF as the preferred alignment technique — simpler and more stable",
                    "Structured output (JSON mode) is the #1 requested enterprise LLM feature"
                ],
                BuildProgress =
                [
                    new() { Label = "Training data curation and preprocessing", Completed = true },
                    new() { Label = "LoRA/QLoRA fine-tuning pipeline", Completed = true },
                    new() { Label = "DPO alignment training", Completed = true },
                    new() { Label = "Evaluation benchmarks and comparison charts", Completed = true },
                    new() { Label = "Documentation site with methodology", Completed = true },
                    new() { Label = "Google Colab notebooks", Completed = true },
                    new() { Label = "Docker deployment (nginx:alpine)", Completed = true }
                ],
                TechStack =
                [
                    new() { Category = "Training", Technology = "Hugging Face Transformers + PEFT", Note = "LoRA/QLoRA" },
                    new() { Category = "Alignment", Technology = "TRL (DPO Trainer)", Note = "Direct Preference Optimization" },
                    new() { Category = "Compute", Technology = "Google Colab", Note = "T4/A100 GPU" },
                    new() { Category = "Evaluation", Technology = "Custom benchmarks", Note = "Schema accuracy" },
                    new() { Category = "Hosting", Technology = "nginx:alpine", Note = "Static site" }
                ],
                TargetUser = "ML engineers and data scientists who need reliable structured output from LLMs and want a proven, reproducible fine-tuning pipeline they can adapt to their own extraction tasks.",
                LiveUrl = "https://finetune.cloudaura.cloud",
                GithubUrl = "https://github.com/CloudAuraOfficial/cloudaura-finetune"
            },
            new()
            {
                Id = "P5",
                Name = "cloudaura-voice",
                Description = "AI voice agent — handles inbound calls with STT, LLM, and TTS via LiveKit SIP",
                Subdomain = "voice",
                Stack = "Python / FastAPI + LiveKit",
                HealthUrl = settings.P5HealthUrl,
                Port = 8005,
                Problem = "Building a production voice AI agent requires stitching together SIP trunking, speech-to-text, LLM reasoning, and text-to-speech into a low-latency pipeline — a complex integration that most teams abandon or outsource at high cost.",
                Solution = "An end-to-end AI voice agent that accepts inbound phone calls via LiveKit SIP trunk, transcribes speech in real-time, processes it through an LLM for intelligent responses, and speaks back using neural TTS — with full call logging to Airtable.",
                CoreFeatures =
                [
                    "Inbound call handling via LiveKit SIP trunk integration",
                    "Real-time speech-to-text transcription",
                    "LLM-powered conversational reasoning with context management",
                    "Neural text-to-speech with configurable providers (OpenAI / ElevenLabs)",
                    "Call logging with transcripts, duration, and metadata to Airtable",
                    "Configurable voice personas and system prompts",
                    "Sub-second response latency for natural conversation flow"
                ],
                MarketSignals =
                [
                    "Voice AI agent market projected to reach $47B by 2030 — replacing IVR and call centers",
                    "LiveKit raised $22M Series A for real-time communication infrastructure",
                    "Enterprise demand for AI phone agents grew 300%+ in 2024 (customer support, scheduling, intake)",
                    "OpenAI and ElevenLabs TTS quality now indistinguishable from human speech"
                ],
                BuildProgress =
                [
                    new() { Label = "LiveKit SIP trunk configuration", Completed = true },
                    new() { Label = "Speech-to-text pipeline", Completed = true },
                    new() { Label = "LLM integration for conversation", Completed = true },
                    new() { Label = "Text-to-speech with provider abstraction", Completed = true },
                    new() { Label = "Call logging to Airtable", Completed = true },
                    new() { Label = "FastAPI control plane and dashboard", Completed = true },
                    new() { Label = "Docker Compose deployment", Completed = true },
                    new() { Label = "Prometheus metrics instrumentation", Completed = true }
                ],
                TechStack =
                [
                    new() { Category = "Framework", Technology = "FastAPI", Note = "Async" },
                    new() { Category = "Real-time Comms", Technology = "LiveKit Agents SDK", Note = "SIP Trunk" },
                    new() { Category = "STT", Technology = "Deepgram / Whisper", Note = "Real-time" },
                    new() { Category = "LLM", Technology = "OpenAI GPT-4", Note = "Conversational" },
                    new() { Category = "TTS", Technology = "OpenAI / ElevenLabs", Note = "Configurable" },
                    new() { Category = "Logging", Technology = "Airtable", Note = "Call records" },
                    new() { Category = "Metrics", Technology = "Prometheus", Note = "FastAPI Instrumentator" }
                ],
                TargetUser = "Startups and businesses that want to automate inbound phone calls — customer support, appointment scheduling, intake forms — with an AI agent that sounds natural and logs every interaction.",
                LiveUrl = "https://voice.cloudaura.cloud",
                GithubUrl = "https://github.com/CloudAuraOfficial/cloudaura-voice"
            },
            new()
            {
                Id = "P6",
                Name = "aura-platform",
                Description = "Multi-tenant infrastructure orchestration platform with RBAC, deployment pipelines, and FIFO queue",
                Subdomain = "platform",
                Stack = "C# / .NET 8 — ASP.NET Core Web API",
                HealthUrl = settings.P6HealthUrl,
                Port = 8006,
                Problem = "Small teams running multi-service architectures on VPS or bare metal lack a lightweight orchestration layer. Kubernetes is overkill, bash scripts are fragile, and there's no self-hosted platform that combines deployment pipelines, RBAC, and audit logging without enterprise licensing.",
                Solution = "A multi-tenant infrastructure orchestration platform built on .NET 8 that provides deployment pipelines with topological execution, RBAC with role-based access control, AES-256 credential encryption, FIFO queue processing, cron scheduling, and full audit logging — all self-hosted.",
                CoreFeatures =
                [
                    "Multi-tenant workspace isolation with RBAC (Admin / Member / Operator)",
                    "Deployment pipelines with mutable Essences and immutable snapshots",
                    "Topological sort for layer execution ordering (PowerShell / Python / C# executors)",
                    "FIFO deployment queue with stuck-run reaper and cancellation support",
                    "Cron-based deployment scheduling",
                    "AES-256 encrypted credential storage (BYOS — Bring Your Own Secrets)",
                    "JWT authentication with refresh tokens and account lockout",
                    "Redis pub/sub real-time log streaming",
                    "Essence versioning with diff tracking",
                    "User invite flow and password complexity enforcement",
                    "Prometheus custom metrics and admin dashboard",
                    "Full audit log for compliance"
                ],
                MarketSignals =
                [
                    "Internal Developer Platforms (IDPs) are the #1 trend in platform engineering — Gartner predicts 80% adoption by 2026",
                    "Self-hosted deployment tools market growing as teams reject vendor lock-in",
                    "RBAC and audit logging are table-stakes for SOC 2 / ISO 27001 compliance",
                    "Multi-tenant SaaS platforms command 3–5x revenue multiples vs single-tenant"
                ],
                BuildProgress =
                [
                    new() { Label = "4-project solution architecture (Core, Infrastructure, Api, Worker)", Completed = true },
                    new() { Label = "PostgreSQL with EF Core migrations", Completed = true },
                    new() { Label = "JWT auth with refresh tokens and account lockout", Completed = true },
                    new() { Label = "RBAC with Admin/Member/Operator roles", Completed = true },
                    new() { Label = "CRUD controllers for all entities", Completed = true },
                    new() { Label = "Deployment orchestration with FIFO queue", Completed = true },
                    new() { Label = "Layer executors (PowerShell, Python, C#)", Completed = true },
                    new() { Label = "Cron scheduler and stuck-run reaper", Completed = true },
                    new() { Label = "Redis log streaming", Completed = true },
                    new() { Label = "AES-256 credential encryption", Completed = true },
                    new() { Label = "Essence versioning and diff", Completed = true },
                    new() { Label = "Prometheus custom metrics", Completed = true },
                    new() { Label = "Admin dashboard (Razor Pages)", Completed = true },
                    new() { Label = "121 passing tests (xUnit + Moq)", Completed = true },
                    new() { Label = "CI/CD pipeline (GitHub Actions)", Completed = true }
                ],
                TechStack =
                [
                    new() { Category = "Framework", Technology = "ASP.NET Core 8", Note = "Web API + Worker" },
                    new() { Category = "Database", Technology = "PostgreSQL", Note = "EF Core / Npgsql" },
                    new() { Category = "Cache / Pub-Sub", Technology = "Redis", Note = "Log streaming" },
                    new() { Category = "Auth", Technology = "JWT + Refresh Tokens", Note = "BCrypt" },
                    new() { Category = "Encryption", Technology = "AES-256-CBC", Note = "BYOS" },
                    new() { Category = "Metrics", Technology = "prometheus-net", Note = "Custom counters" },
                    new() { Category = "Testing", Technology = "xUnit + Moq", Note = "121 tests" },
                    new() { Category = "CI/CD", Technology = "GitHub Actions", Note = "Build → Deploy → Smoke" },
                    new() { Category = "Dashboard", Technology = "Razor Pages", Note = "Server-rendered" }
                ],
                TargetUser = "Platform engineering teams and DevOps engineers at startups and mid-size companies who need a self-hosted deployment orchestration tool with proper RBAC and audit trails, without the complexity of Kubernetes.",
                LiveUrl = "https://platform.cloudaura.cloud",
                GithubUrl = "https://github.com/CloudAuraOfficial/aura-platform"
            }
        };
    }

    public IReadOnlyList<ProjectInfo> GetAll() => _projects.AsReadOnly();

    public ProjectInfo? GetById(string id) =>
        _projects.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
}
