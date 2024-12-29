﻿namespace BuildHub.App.Compliant.Application.Models;

public class ComplianceProjectViewModel
{
    public Guid Id { get; set; }
    public Guid ClientId { get; init; }
    public string ProjectName { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string ProjectLocation { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<EvidenceViewModel> Evidence { get; set; } = [];
    public ComplianceStatusViewModel ComplianceStatusViewModel { get; set; } = new();
    public List<AuditTrailViewModel> AuditTrails { get; set; } = [];
}

public class AuditTrailViewModel
{
    public Guid AuditId { get; set; }
    public DateTime Timestamp { get; set; }
    public string ActionPerformed { get; set; } = string.Empty;
    public UserViewModel PerformedBy { get; set; } = new();
    public string Remarks { get; set; } = string.Empty;
}

public class ComplianceStatusViewModel
{
    public bool IsCompliant { get; set; }
    public List<string> NonComplianceReasons { get; set; } = [];
    public DateTime? LastComplianceCheckDate { get; set; }
    public string CheckedBy { get; set; } = string.Empty;
}

public class EvidenceViewModel
{
    public Guid DocumentId { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public DocumentType DocumentType { get; set; } = DocumentType.None;
    public DateTime UploadDate { get; set; }
    public string UploadedBy { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public DateTime? VerificationDate { get; set; }
    public string VerifiedBy { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class UserViewModel
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public RoleViewModel Role { get; set; } = new();
}

public class RoleViewModel
{
    public string RoleName { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public bool IsRequired { get; set; }
}

public enum DocumentType
{
    None,
    FireSafetyPlan,
    StructuralReport,
    EnvironmentalImpactAssessment,
    HealthAndSafetyPlan,
    RiskAssessment,
    MethodStatement,
    Other
}