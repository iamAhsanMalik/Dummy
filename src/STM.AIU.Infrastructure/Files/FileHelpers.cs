using Microsoft.AspNetCore.Hosting;
using STM.AIU.Application.Contracts.Infrastructure;

namespace STM.AIU.Infrastructure.Files;
/// <summary>
/// File Helpers methods used to work with files such as FileUploading, Path Normalization etc..
/// </summary>
internal class FileHelpers : IFileHelpers
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileHelpers(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> EmailTemplatesReaderAsync(string fileNameWithExtension) => await File.ReadAllTextAsync(NormalizeFilePath($"{_webHostEnvironment.ContentRootPath}/EmailTemplates/Auth/{fileNameWithExtension}"));

    /// <summary>
    /// Path Normalizer which will trim and add double (//) slashes to the path
    /// </summary>
    /// <param name="Path"></param>
    /// <returns>Normalize Path (String)</returns>
    public string NormalizeFilePath(string path)
    {
        return Path.GetFullPath(new Uri(path).LocalPath)
                   .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
    }
}
