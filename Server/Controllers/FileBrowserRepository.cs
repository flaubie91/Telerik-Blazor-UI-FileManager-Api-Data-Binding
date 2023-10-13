using SharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Telerik.Blazor.Components;
using Telerik.DataSource.Extensions;
using System.IO;
using Microsoft.VisualBasic;
using System.Xml.Linq;


namespace Api.Controllers;


public class FileBrowserRepository : IFileBrowserApiRepository<FlatFileEntry>
{

    #region Properties

    public readonly string PathSeparator = Path.DirectorySeparatorChar.ToString();

    const int ActionDelay = 50;

    public List<FlatFileEntry> Data { get; set; }

    public List<FlatFileEntry> FileSystem { get; set; }

    #endregion Properties



    #region ReadData
    public async Task<List<FlatFileEntry>> ReadDirectoryAsync(string rootPath)
    {
        await Task.Yield();
        if (rootPath != null)
        {
            var rootDirectory = new DirectoryInfo(rootPath);
            var directoryEntry = ConvertDirectory(rootDirectory, rootDirectory);
            var directories = rootDirectory.EnumerateDirectories();
            var files = rootDirectory.EnumerateFiles();

            var Data = new List<FlatFileEntry>();

            foreach (var directory in directories)
            {
                var entries = ReadDirectory(rootDirectory, directory);
                Data.AddRange(entries);
            }

            foreach (var file in files)
            {
                var entry = ConvertFile(rootDirectory, file, directoryEntry);
                Data.Add(entry);
            }

            return Data;
        }
        else
        {
            return new();
        }
    }


    public List<FlatFileEntry> ReadData(string rootPath)
    {
        var rootDirectory = new DirectoryInfo(rootPath);
        var directoryEntry = ConvertDirectory(rootDirectory, rootDirectory);
        var directories = rootDirectory.EnumerateDirectories();
        var files = rootDirectory.EnumerateFiles();

        var data = new List<FlatFileEntry>();

        foreach (var directory in directories)
        {
            var entries = ReadDirectory(rootDirectory, directory);
            data.AddRange(entries);
        }

        foreach (var file in files)
        {
            var entry = ConvertFile(rootDirectory, file, directoryEntry);
            data.Add(entry);
        }

        return data;
    }


    public List<FlatFileEntry> ReadDirectory(DirectoryInfo rootDirectory, DirectoryInfo directory, FlatFileEntry parentDirectory = null)
    {
        var directoryEntry = ConvertDirectory(rootDirectory, directory, parentDirectory);
        var directories = directory.EnumerateDirectories();
        var files = directory.EnumerateFiles();
        var data = new List<FlatFileEntry>();

        data.Add(directoryEntry);

        foreach (var dir in directories)
        {
            var entries = ReadDirectory(rootDirectory, dir, directoryEntry);
            data.AddRange(entries);
        }

        foreach (var file in files)
        {
            var entry = ConvertFile(rootDirectory, file, directoryEntry);
            data.Add(entry);
        }

        return data;
    }


    public FlatFileEntry ConvertDirectory(DirectoryInfo rootDirectory, DirectoryInfo directory, FlatFileEntry parentDirectory = null)
    {
        var id = directory.FullName.Substring(directory.FullName.IndexOf(rootDirectory.Name));
        string Name = directory.Name;
        bool IsDirectory = true;
        bool HasDirectories = directory.GetDirectories().Count() > 0;
        string Id = id;
        string ParentId = parentDirectory?.Id;
        DateTime DateCreated = directory.CreationTime;
        DateTime DateCreatedUtc = directory.CreationTimeUtc;
        DateTime DateModified = directory.LastWriteTime;
        DateTime DateModifiedUtc = directory.LastWriteTimeUtc;
        // trim the real path to avoid exposing it
        // for security purposes
        string Path = directory.FullName.Substring(directory.FullName.IndexOf(rootDirectory.Name));
        string FullPath = directory.FullName;
        string Extension = directory.Extension;
        long Size = 2 * 1024 * directory.GetFiles().LongCount();

        var entry = new FlatFileEntry()
        {
            Name = directory.Name,
            IsDirectory = true,
            HasDirectories = directory.GetDirectories().Count() > 0,
            Id = id,
            ParentId = parentDirectory?.Id,
            DateCreated = directory.CreationTime,
            DateCreatedUtc = directory.CreationTimeUtc,
            DateModified = directory.LastWriteTime,
            DateModifiedUtc = directory.LastWriteTimeUtc,
            // trim the real path to avoid exposing it
            // for security purposes
            Path = directory.FullName.Substring(directory.FullName.IndexOf(rootDirectory.Name)),
            FullPath = directory.ToString(),
            Extension = directory.Extension,
            Size = 2 * 1024 * directory.GetFiles().LongCount()
        };

        return entry;
    }


    public FlatFileEntry ConvertFile(DirectoryInfo rootDirectory, FileInfo file, FlatFileEntry parentDirectory)
    {
        var id = file.FullName.Substring(file.FullName.IndexOf(rootDirectory.Name));
        string Name = Path.GetFileNameWithoutExtension(file.FullName);
        string fileName = Path.GetFileName(file.FullName);
        bool IsDirectory = false;
        bool HasDirectories = false;
        string Id = id;
        string ParentId = parentDirectory?.Id;
        DateTime DateCreated = file.CreationTime;
        DateTime DateCreatedUtc = file.CreationTimeUtc;
        DateTime DateModified = file.LastWriteTime;
        DateTime DateModifiedUtc = file.LastWriteTimeUtc;
        // trim the real path to avoid exposing it
        // for security purposes
        string path = file.FullName.Substring(file.FullName.IndexOf(rootDirectory.Name));
        string FullPath = file.FullName;
        string Extension = file.Extension;
        long Size = file.Length;

        var entry = new FlatFileEntry()
        {
            Name = Path.GetFileNameWithoutExtension(file.FullName),
            IsDirectory = false,
            HasDirectories = false,
            Id = id,
            ParentId = parentDirectory?.Id,
            DateCreated = file.CreationTime,
            DateCreatedUtc = file.CreationTimeUtc,
            DateModified = file.LastWriteTime,
            DateModifiedUtc = file.LastWriteTimeUtc,
            // trim the real path to avoid exposing it
            // for security purposes
            Path = file.FullName.Substring(file.FullName.IndexOf(rootDirectory.Name)),
            FullPath = file.ToString(),
            Extension = file.Extension,
            Size = file.Length
        };

        return entry;
    }
    #endregion ReadData
}