namespace BuildHub.App.Compliant.Application.Models;

public class EvidenceViewModel
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime DateUploaded { get; init; } = DateTime.MinValue;
}