using BuildHub.App.Compliant.External.Models;
using Newtonsoft.Json;

namespace BuildHub.App.Compliant.External;

public interface IBuildHubClient
{
    Task<IEnumerable<ProjectResponse>> GetProjectBriefsAsync();
    Task<ProjectResponse> GetProjectBriefByIdAsync(Guid projectId);
}

public class BuildHubClient(
    IHttpClientFactory httpClientFactory
    ) : IBuildHubClient
{
    public async Task<IEnumerable<ProjectResponse>> GetProjectBriefsAsync()
    {
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync("project-brief");
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to get project briefs");
        
        var projectBriefsJson = 
            await response.Content.ReadAsStringAsync();
        
        var projectBriefs = JsonConvert.DeserializeObject<ProjectsResponse>(projectBriefsJson);
        
        return projectBriefs?.Projects ?? Enumerable.Empty<ProjectResponse>();
    }

    public async Task<ProjectResponse> GetProjectBriefByIdAsync(Guid projectId)
    {
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync($"project-brief/{projectId}");
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to get project briefs");
        
        var projectBriefJson = 
            await response.Content.ReadAsStringAsync();
        
        var projectBrief = JsonConvert.DeserializeObject<ProjectResponse>(projectBriefJson);
        
        if (projectBrief is null)
            throw new Exception("Failed to get project briefs");
        
        return projectBrief;
    }
    
    private HttpClient GetHttpClient()
    {
        var httpClient = httpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri("https://bh-api-dev.azurewebsites.net/api/");
        return httpClient;
    }
}