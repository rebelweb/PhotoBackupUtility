using System.Text.Json;
using Amazon.S3;
using Amazon.S3.Model;
using PhotoBackupUtility.App.Configuration;

namespace PhotoBackupUtility.App;

public class CurrentBackupStateBuilder : ICurrentBackupStateBuilder
{
    private readonly IAmazonS3 _s3Client;
    private readonly ISettings _settings;

    public CurrentBackupStateBuilder(IAmazonS3 s3Client, ISettings settings)
    {
        _s3Client = s3Client;
        _settings = settings;
    }

    public async Task Call()
    {
        List<S3Object> objects = await GetItems();
        List<ManagedFileInfo> files = new();
        
        foreach (var item in objects)
        {
            ManagedFileInfo file = new() { FilePath = item.Key };
            files.Add(file);
        }

        WriteFileAsync(files);
    }
    
    private async Task<List<S3Object>> GetItems()
    {
        bool makeRequests = true;
        string? token = null;
        List<S3Object> objects = new();
        
        while (makeRequests)
        {
            ListObjectsV2Response response = await GetItemsRequest(token);
            objects.AddRange(response.S3Objects);
            token = response.NextContinuationToken;
            makeRequests = response.IsTruncated;
        }

        return objects;
    }

    private bool WriteFileAsync(List<ManagedFileInfo> files)
    {
        BackupState state = new() { Files = files };
        string data = JsonSerializer.Serialize(state);
        File.WriteAllText("backup_state.json", data);
        return true;
    }
    
    private async Task<ListObjectsV2Response> GetItemsRequest(string? continuationToken)
    {
        ListObjectsV2Request request = new ListObjectsV2Request()
        {
            BucketName = _settings.BucketName,
        };

        if (continuationToken != null)
            request.ContinuationToken = continuationToken;

        ListObjectsV2Response response = await _s3Client.ListObjectsV2Async(request);
        return response;
    }
}
