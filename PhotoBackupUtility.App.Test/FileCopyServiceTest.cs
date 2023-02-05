using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Moq.AutoMock;

namespace PhotoBackupUtility.App.Test;

public class FileCopyServiceTest
{
    [Fact]
    public async Task TestCopyFile_Fails()
    {
        AutoMocker mocker = new();
        ManagedFileInfo info = new() { FilePath = "./Directory/sample1.txt" };
        PutObjectResponse response = new() { HttpStatusCode = HttpStatusCode.UnprocessableEntity };

        mocker.GetMock<IAmazonS3>().Setup(q => q.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        FileCopyService service = mocker.CreateInstance<FileCopyService>();
        bool copied = await service.CopyFile(info);
        
        Assert.False(copied);
    }

    [Fact]
    public async Task TestCopyFile_Success()
    {
        AutoMocker mocker = new();
        ManagedFileInfo info = new() { FilePath = "./Directory/sample1.txt" };
        PutObjectResponse response = new() { HttpStatusCode = HttpStatusCode.OK };

        mocker.GetMock<IAmazonS3>().Setup(q => q.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        FileCopyService service = mocker.CreateInstance<FileCopyService>();
        bool copied = await service.CopyFile(info);
        
        Assert.True(copied);
    }
}

