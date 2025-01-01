namespace BuildHub.App.Compliant.External.Models;

public class CreateCompliantEvidenceRequest
{
    public Guid DocumentId { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public DocumentType DocumentType { get; set; } = DocumentType.None;
    public DateTime UploadDate { get; set; }
    public string UploadedBy { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
