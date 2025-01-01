using System.Net.Http.Headers;
using System.Text;
using BuildHub.App.Compliant.External.Models;
using Newtonsoft.Json;

namespace BuildHub.App.Compliant.External;

public interface IBuildHubClient
{
    Task<IEnumerable<CompliantProjectResponse>> GetProjectBriefsAsync();
    Task<CompliantProjectResponse> GetProjectBriefByIdAsync(Guid projectId);
    Task<Guid> CreateProjectAsync(CreateCompliantProjectRequest createProjectRequest);
    Task CreateEvidenceAsync(Guid projectId, CreateCompliantEvidenceRequest createEvidenceRequest);
    Task<string> UploadEvidenceAsync(string fileName, Stream fileStream);
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
        var result = JsonConvert.DeserializeObject<CreateCompliantProjectResponse>(json);
        
        return result?.Id ?? Guid.Empty;
    }

    public async Task CreateEvidenceAsync(Guid projectId, CreateCompliantEvidenceRequest createEvidenceRequest)
    {
        var httpClient = GetHttpClient();
        
        var createEvidenceRequestJson = 
            JsonConvert.SerializeObject(createEvidenceRequest);
        
        var content = new StringContent(createEvidenceRequestJson, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync($"compliant/evidence/{projectId}", content);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to create project");
    }

    public async Task<string> UploadEvidenceAsync(string fileName, Stream fileStream)
    {
        var httpClient = GetHttpClient();
        using var content = new MultipartFormDataContent();
        fileStream.Position = 0;
        
        using var fileContent = new StreamContent(fileStream);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
        content.Add(fileContent, "file", fileName);

        var response = await httpClient.PostAsync("compliant/evidence/upload", content);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to upload file: {response.ReasonPhrase} - {body}");
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var uri = JsonConvert.DeserializeObject<Uri>(json);

        if (uri is null)
            throw new Exception("Failed to deserialize file link");
        
        return uri.ToString();
    }

    private HttpClient GetHttpClient()
    {
        var httpClient = httpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri("https://bh-api-dev.azurewebsites.net/api/");
        // httpClient.BaseAddress = new Uri(" http://localhost:7015/api/");
        return httpClient;
    }
}