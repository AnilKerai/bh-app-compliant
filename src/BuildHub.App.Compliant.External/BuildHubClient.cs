using System.Text;
using BuildHub.App.Compliant.External.Models;
using Newtonsoft.Json;

namespace BuildHub.App.Compliant.External;

public interface IBuildHubClient
{
    Task<IEnumerable<CompliantProjectResponse>> GetProjectBriefsAsync();
    Task<CompliantProjectResponse> GetProjectBriefByIdAsync(Guid projectId);
    Task<Guid> CreateProjectAsync(CreateCompliantProjectRequest createProjectRequest);
}

public class BuildHubClient(
    IHttpClientFactory httpClientFactory
    ) : IBuildHubClient
{
    public async Task<IEnumerable<CompliantProjectResponse>> GetProjectBriefsAsync()
    {
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync("compliant/project");
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to get project briefs");
        
        var projectBriefsJson = 
            await response.Content.ReadAsStringAsync();
        
        var projectBriefs = JsonConvert.DeserializeObject<CompliantProjectsResponse>(projectBriefsJson);
        
        return projectBriefs?.Projects ?? Enumerable.Empty<CompliantProjectResponse>();
    }

    public async Task<CompliantProjectResponse> GetProjectBriefByIdAsync(Guid projectId)
    {
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync($"compliant/project/{projectId}");
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to get project briefs");
        
        var projectBriefJson = 
            await response.Content.ReadAsStringAsync();
        
        var projectBrief = JsonConvert.DeserializeObject<CompliantProjectResponse>(projectBriefJson);
        
        if (projectBrief is null)
            throw new Exception("Failed to get project briefs");
        
        return projectBrief;
    }

    public async Task<Guid> CreateProjectAsync(CreateCompliantProjectRequest createProjectRequest)
    {
        var httpClient = GetHttpClient();

        var createProjectRequestJson = 
            JsonConvert.SerializeObject(createProjectRequest);
        
        var content = new StringContent(createProjectRequestJson, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("compliant/project", content);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to create project");
        
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateProjectResponse>(json);
        
        return result?.Id ?? Guid.Empty;
    }

    private HttpClient GetHttpClient()
    {
        var httpClient = httpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri("https://bh-api-dev.azurewebsites.net/api/");
        return httpClient;
    }
}