namespace BuildHub.App.Compliant.External.Models;

public record ProjectsResponse
{
    public IEnumerable<ProjectResponse> Projects { get; init; } = [];
}