﻿using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System.IO;
using System.Threading.Tasks;

public class FileStorageService
{
    private readonly ShareClient _shareClient;

    public FileStorageService(string connectionString, string shareName)
    {
        _shareClient = new ShareClient(connectionString, shareName);
        _shareClient.CreateIfNotExists();
    }

    public async Task UploadFileAsync(string directoryName, string fileName, Stream fileStream)
    {
        var directoryClient = _shareClient.GetDirectoryClient(directoryName);
        await directoryClient.CreateIfNotExistsAsync();
        var fileClient = directoryClient.GetFileClient(fileName);
        await fileClient.CreateAsync(fileStream.Length);
        await fileClient.UploadRangeAsync(new HttpRange(0, fileStream.Length), fileStream);
    }

    public async Task<Stream> DownloadFileAsync(string directoryName, string fileName)
    {
        var directoryClient = _shareClient.GetDirectoryClient(directoryName);
        var fileClient = directoryClient.GetFileClient(fileName);
        return await fileClient.OpenReadAsync();
    }
}

