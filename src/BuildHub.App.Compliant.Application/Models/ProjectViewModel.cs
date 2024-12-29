namespace BuildHub.App.Compliant.Application.Models;

public class ProjectViewModel
{
    public Guid Id { get; init; }
    public Guid ClientId { get; set; }
    public string Overview { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public DateTime ProjectStartDate { get; set; }
    public IEnumerable<EvidenceViewModel> Evidence { get; init; } = [];
}