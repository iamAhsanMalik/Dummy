namespace STM.AIU.Application.Contracts.Infrastructure;

public interface IFileHelpers
{
    Task<string> EmailTemplatesReaderAsync(string fileNameWithExtension);
    string NormalizeFilePath(string path);
}