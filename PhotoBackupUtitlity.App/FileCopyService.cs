using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace PhotoBackupUtility.App;

public class FileCopyService : IFileCopyService
{
    private readonly IAmazonS3 _s3Client;

    public FileCopyService(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }
    
    public async Task<bool> CopyFile(ManagedFileInfo managedFileInfo)
    {
        FileStream stream = File.Open(managedFileInfo.FilePath, FileMode.Open);
        
        PutObjectRequest request = new()
        {
            BucketName = "mybucket",
            Key = managedFileInfo.FilePath,
            InputStream = stream
        };
        
        PutObjectResponse response = await _s3Client.PutObjectAsync(request);
        stream.Close();
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}
