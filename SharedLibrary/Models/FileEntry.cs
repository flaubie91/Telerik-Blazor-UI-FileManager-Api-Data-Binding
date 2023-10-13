namespace SharedLibrary.Models;

// escape ASP.NET-related names like FileInfo or File
public class FileEntry
{
    public string Name { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;

    public long Size { get; set; }

    public string Path { get; set; } = string.Empty;

    public string FullPath { get; set; } = string.Empty;

    public bool IsDirectory { get; set; }

    public bool HasDirectories { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateCreatedUtc { get; set; }

    public DateTime DateModified { get; set; }

    public DateTime DateModifiedUtc { get; set; }

}