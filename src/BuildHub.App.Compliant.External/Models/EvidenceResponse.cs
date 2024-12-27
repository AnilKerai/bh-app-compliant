namespace BuildHub.App.Compliant.External.Models;

public record EvidenceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTimeOffset DateUploaded { get; set; } = DateTimeOffset.MinValue;
}