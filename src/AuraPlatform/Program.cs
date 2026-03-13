using AuraPlatform.Components;
using AuraPlatform.Configuration;
using AuraPlatform.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind configuration from environment variables
builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<HealthCheckSettings>(settings =>
{
    settings.P1HealthUrl = builder.Configuration["P1_HEALTH_URL"];
    settings.P2HealthUrl = builder.Configuration["P2_HEALTH_URL"];
    settings.P3HealthUrl = builder.Configuration["P3_HEALTH_URL"];
    settings.P4HealthUrl = builder.Configuration["P4_HEALTH_URL"];
    settings.P5HealthUrl = builder.Configuration["P5_HEALTH_URL"];
    settings.P6HealthUrl = builder.Configuration["P6_HEALTH_URL"];

    if (int.TryParse(builder.Configuration["HEALTH_POLL_INTERVAL_SECONDS"], out var interval))
        settings.PollIntervalSeconds = interval;
});

builder.Services.Configure<PrometheusSettings>(settings =>
{
    settings.Url = builder.Configuration["PROMETHEUS_URL"];
});

// HTTP clients
builder.Services.AddHttpClient("HealthCheck", client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddHttpClient("Prometheus", client =>
{
    client.Timeout = TimeSpan.FromSeconds(15);
});

// Services
builder.Services.AddSingleton<ProjectRegistry>();
builder.Services.AddSingleton<ProjectStatusService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<ProjectStatusService>());
builder.Services.AddSingleton<MetricsService>();

// Health checks
builder.Services.AddHealthChecks();

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapHealthChecks("/healthz");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
