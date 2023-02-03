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
    
    public async Task<bool> CopyFile(FileInfo fileInfo)
    {
        FileStream stream = File.Open(fileInfo.FilePath, FileMode.Open);
        
        PutObjectRequest request = new()
        {
            BucketName = "mybucket",
            Key = fileInfo.FilePath,
            InputStream = stream
        };
        
        PutObjectResponse response = await _s3Client.PutObjectAsync(request);
        stream.Close();
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}
