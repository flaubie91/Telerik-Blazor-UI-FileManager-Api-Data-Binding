namespace SharedLibrary.Models;

public class FlatFileEntry : FileEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ParentId { get; set; } = string.Empty;
}