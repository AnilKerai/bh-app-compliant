using BuildHub.App.Compliant.Application.Models;
using BuildHub.App.Compliant.External;
using BuildHub.App.Compliant.External.Models;
using ExternalDocumentType = BuildHub.App.Compliant.External.Models.DocumentType;
using ViewModelDocumentType = BuildHub.App.Compliant.Application.Models.DocumentType;

namespace BuildHub.App.Compliant.Application.Services;

public interface IProjectService
{
    Task<IEnumerable<ComplianceProjectViewModel>> GetProjects(Guid clientId);
    Task<ComplianceProjectViewModel> GetProjectById(Guid projectId);
    Task<Guid> CreateProject(ComplianceProjectViewModel project);
}

public class ProjectService(
    IBuildHubClient buildHubClient
    ) : IProjectService
{
    public async Task<IEnumerable<ComplianceProjectViewModel>> GetProjects(Guid clientId)
    {
        var projects = await buildHubClient.GetProjectBriefsAsync();
        
        var projectViewModels = 
            projects
                .Select(MapComplianceProjectViewModel)
                .ToList();
        
        return projectViewModels;
    }

    public async Task<ComplianceProjectViewModel> GetProjectById(Guid projectId)
    {
        var project = await buildHubClient.GetProjectBriefByIdAsync(projectId);
        
        var projectViewModel = MapComplianceProjectViewModel(project);
        
        return projectViewModel;
    }

    public Task<Guid> CreateProject(ComplianceProjectViewModel project)
    {
        var createProjectRequest = new CreateCompliantProjectRequest
        {
            ClientId = project.ClientId,
            ProjectName = project.ProjectName,
            ProjectLocation = project.ProjectLocation,
            Reference = project.Reference,
            Overview = project.Overview,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };
        
        return buildHubClient.CreateProjectAsync(createProjectRequest);
    }

    private static ComplianceProjectViewModel MapComplianceProjectViewModel(CompliantProjectResponse project)
    {
        return new ComplianceProjectViewModel 
        {
            Id = project.Id,
            ClientId = project.ClientId,
            ProjectName = project.ProjectName,
            ProjectLocation = project.ProjectLocation,
            Reference = project.Reference,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Overview = project.Overview,
            Evidence = project.Evidence.Select(evidence => new EvidenceViewModel
            {
                DocumentId = evidence.DocumentId,
                DocumentName = evidence.DocumentName,
                DocumentType = MapDocumentType(evidence.DocumentType),
                UploadDate = evidence.UploadDate,
                UploadedBy = evidence.UploadedBy,
                IsVerified = evidence.IsVerified,
                VerificationDate = evidence.VerificationDate,
                VerifiedBy = evidence.VerifiedBy,
                Url = evidence.Url
            }).ToList(),
            ComplianceStatusViewModel = new ComplianceStatusViewModel
            {
                CheckedBy = project.ComplianceStatus.CheckedBy, 
                IsCompliant = project.ComplianceStatus.IsCompliant, 
                LastComplianceCheckDate = project.ComplianceStatus.LastComplianceCheckDate, 
                NonComplianceReasons = project.ComplianceStatus.NonComplianceReasons
            },
            AuditTrails = project.AuditTrails.Select(audit => new AuditTrailViewModel
            {
                AuditId = audit.AuditId,
                Remarks = audit.Remarks,
                Timestamp = audit.Timestamp,
                ActionPerformed = audit.ActionPerformed,
                PerformedBy = new UserViewModel
                {
                    Email = audit.PerformedBy.Email,
                    FullName = audit.PerformedBy.FullName,
                    UserId = audit.PerformedBy.UserId,
                    Role = new RoleViewModel
                    {
                        UserId = audit.PerformedBy.Role.UserId, 
                        IsRequired = audit.PerformedBy.Role.IsRequired, 
                        RoleName = audit.PerformedBy.Role.RoleName, 
                        UserName = audit.PerformedBy.Role.UserName,
                    }
                }
            }).ToList()
        };
    }

    private static ViewModelDocumentType MapDocumentType(ExternalDocumentType documentType)
    {
        switch (documentType)
        {
            case ExternalDocumentType.None:
                return ViewModelDocumentType.None;
            case ExternalDocumentType.FireSafetyPlan:
                return ViewModelDocumentType.FireSafetyPlan;
            case ExternalDocumentType.StructuralReport:
                return ViewModelDocumentType.StructuralReport;
            case ExternalDocumentType.EnvironmentalImpactAssessment:
                return ViewModelDocumentType.EnvironmentalImpactAssessment;
            case ExternalDocumentType.HealthAndSafetyPlan:
                return ViewModelDocumentType.HealthAndSafetyPlan;
            case ExternalDocumentType.RiskAssessment:
                return ViewModelDocumentType.RiskAssessment;
            case ExternalDocumentType.MethodStatement:
                return ViewModelDocumentType.MethodStatement;
            case ExternalDocumentType.Other:
            default:
                return ViewModelDocumentType.Other;
        }
    }
}