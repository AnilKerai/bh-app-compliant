namespace BuildHub.App.Compliant.External.Models;

public record ProjectsResponse
{
    public IEnumerable<CompliantProjectResponse> Projects { get; init; } = [];
}