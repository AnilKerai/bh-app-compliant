using BuildHub.App.Compliant.External;

namespace BuildHub.App.Compliant.Application.Services;

public interface IUploadService
{
    Task<string> UploadEvidenceDocumentAsync(string fileName, Stream fileStream);
}

public class UploadService(
    IBuildHubClient buildHubClient
    ) : IUploadService
{
    public Task<string> UploadEvidenceDocumentAsync(string fileName, Stream fileStream)
    {
        return buildHubClient.UploadEvidenceAsync(fileName, fileStream);
    }
}