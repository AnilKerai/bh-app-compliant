using BuildHub.App.Compliant.Application.Models;
using BuildHub.App.Compliant.External;
using BuildHub.App.Compliant.External.Models;

namespace BuildHub.App.Compliant.Application.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectViewModel>> GetProjects(Guid customerId);
    Task<ProjectViewModel> GetProjectById(Guid projectId);
}

public class ProjectService(
    IBuildHubClient buildHubClient
    ) : IProjectService
{
    public async Task<IEnumerable<ProjectViewModel>> GetProjects(Guid customerId)
    {
        var projects = await buildHubClient.GetProjectBriefsAsync();
        
        var projectViewModels = projects.Select(
            project => new ProjectViewModel
                {
                    Id = project.Id,
                    ClientId = project.ClientId,
                    Overview = project.Overview,
                    Reference = project.Reference,
                    ProjectStartDate = project.ProjectStartDate.UtcDateTime,
                    Evidence = project.Evidence.Select(evidenceResponse => new EvidenceViewModel
                    {
                        Id = evidenceResponse.Id,
                        DateUploaded = evidenceResponse.DateUploaded.UtcDateTime,
                        Name = evidenceResponse.Name,
                        Type = evidenceResponse.Type,
                        Url = evidenceResponse.Url
                    })
                }
            );
        
        return projectViewModels;
    }

    public async Task<ProjectViewModel> GetProjectById(Guid projectId)
    {
        var project = await buildHubClient.GetProjectBriefByIdAsync(projectId);
        
        var projectViewModel = new ProjectViewModel 
        {
            Id = project.Id,
            ClientId = project.ClientId,
            Overview = project.Overview,
            Reference = project.Reference,
            ProjectStartDate = project.ProjectStartDate.UtcDateTime,
            Evidence = project.Evidence.Select(evidenceResponse => new EvidenceViewModel
            {
                Id = evidenceResponse.Id,
                DateUploaded = evidenceResponse.DateUploaded.UtcDateTime,
                Name = evidenceResponse.Name,
                Type = evidenceResponse.Type,
                Url = evidenceResponse.Url
            })
        };
        
        return projectViewModel;
    }
}