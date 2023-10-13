public interface IFileBrowserApiRepository<FlatFileEntry>
{
    Task<List<FlatFileEntry>> ReadDirectoryAsync(string rootPath);
}