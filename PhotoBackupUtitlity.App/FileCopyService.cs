using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using PhotoBackupUtility.App.Configuration;

namespace PhotoBackupUtility.App;

public class FileCopyService : IFileCopyService
{
    private readonly IAmazonS3 _s3Client;
    private readonly ISettings _settings;

    public FileCopyService(IAmazonS3 s3Client,
        ISettings settings)
    {
        _s3Client = s3Client;
        _settings = settings;
    }
    
    public async Task<bool> CopyFile(ManagedFileInfo managedFileInfo)
    {
        FileStream stream = File.Open(managedFileInfo.FilePath, FileMode.Open);
        
        PutObjectRequest request = new()
        {
            BucketName = _settings.BucketName,
            Key = managedFileInfo.FilePath,
            InputStream = stream,
        };
        
        PutObjectResponse response = await _s3Client.PutObjectAsync(request);
        stream.Close();
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}
