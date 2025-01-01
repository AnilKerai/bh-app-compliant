namespace BuildHub.App.Compliant.External.Models;

public class CreateCompliantProjectRequest
{
    public Guid ClientId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectLocation { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}