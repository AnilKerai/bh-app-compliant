namespace BuildHub.App.Compliant.External.Models;

public record CompliantProjectsResponse
{
    public IEnumerable<CompliantProjectResponse> Projects { get; init; } = [];
}