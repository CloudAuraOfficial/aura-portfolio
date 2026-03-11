using AuraPlatform.Configuration;
using AuraPlatform.Services;
using Microsoft.Extensions.Options;

namespace AuraPlatform.Tests.Unit;

public class ProjectRegistryTests
{
    private readonly ProjectRegistry _registry;

    public ProjectRegistryTests()
    {
        var settings = Options.Create(new HealthCheckSettings
        {
            P1HealthUrl = "http://localhost:8001/health",
            P3HealthUrl = "http://localhost:3000/api/health",
            P5HealthUrl = "http://localhost:8005/health",
            PollIntervalSeconds = 30
        });

        _registry = new ProjectRegistry(settings);
    }

    [Fact]
    public void GetAll_ReturnsSixProjects()
    {
        var projects = _registry.GetAll();
        Assert.Equal(6, projects.Count);
    }

    [Theory]
    [InlineData("P1", "cloudaura-rag")]
    [InlineData("P5", "cloudaura-voice")]
    [InlineData("P6", "aura-platform")]
    public void GetById_ReturnsCorrectProject(string id, string expectedName)
    {
        var project = _registry.GetById(id);
        Assert.NotNull(project);
        Assert.Equal(expectedName, project.Name);
    }

    [Fact]
    public void GetById_WithInvalidId_ReturnsNull()
    {
        var project = _registry.GetById("P99");
        Assert.Null(project);
    }

    [Fact]
    public void GetById_IsCaseInsensitive()
    {
        var project = _registry.GetById("p1");
        Assert.NotNull(project);
        Assert.Equal("P1", project.Id);
    }

    [Theory]
    [InlineData("P1")]
    [InlineData("P3")]
    [InlineData("P5")]
    public void MonitoredProjects_HaveHealthUrls(string id)
    {
        var project = _registry.GetById(id);
        Assert.NotNull(project);
        Assert.NotNull(project.HealthUrl);
    }

    [Theory]
    [InlineData("P2")]
    [InlineData("P4")]
    [InlineData("P6")]
    public void StaticAndSelfProjects_HaveNoHealthUrls(string id)
    {
        var project = _registry.GetById(id);
        Assert.NotNull(project);
        Assert.Null(project.HealthUrl);
    }
}
