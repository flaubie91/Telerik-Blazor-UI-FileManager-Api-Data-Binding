using SharedLibrary.Models;

namespace SharedLibrary.Features;
public class FileBrowserManager : FileBrowserApiRepository<FlatFileEntry>
{
    HttpClient _http;

    public FileBrowserManager(HttpClient http)
        : base(http, "api/filebrowser", "rootPath")
    {
        _http = http;
    }
}