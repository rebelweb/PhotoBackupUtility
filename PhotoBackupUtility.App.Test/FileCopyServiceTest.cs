using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace PhotoBackupUtility.App.Test;

public class FileCopyServiceTest
{
    [Fact]
    public async Task TestCopyFile_Fails()
    {
        Mock<IAmazonS3> s3Client = new();
        ManagedFileInfo info = new() { FilePath = "./Directory/sample1.txt" };

        PutObjectResponse response = new() { HttpStatusCode = HttpStatusCode.UnprocessableEntity };

        s3Client.Setup(q => q.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        FileCopyService service = new(s3Client.Object);
        bool copied = await service.CopyFile(info);
        
        Assert.False(copied);
    }

    [Fact]
    public async Task TestCopyFile_Success()
    {
        Mock<IAmazonS3> s3Client = new();
        ManagedFileInfo info = new() { FilePath = "./Directory/sample1.txt" };

        PutObjectResponse response = new() { HttpStatusCode = HttpStatusCode.OK };

        s3Client.Setup(q => q.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        FileCopyService service = new(s3Client.Object);
        bool copied = await service.CopyFile(info);
        
        Assert.True(copied);
    }
}

