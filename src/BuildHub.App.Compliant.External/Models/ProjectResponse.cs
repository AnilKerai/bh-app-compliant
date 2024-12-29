namespace BuildHub.App.Compliant.External.Models;

public record ProjectResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Overview { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public DateTimeOffset ProjectStartDate { get; set; }
    public IEnumerable<EvidenceResponse> Evidence { get; set; } = [];
}