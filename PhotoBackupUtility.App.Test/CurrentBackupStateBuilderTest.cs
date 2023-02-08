using Amazon.S3;
using Amazon.S3.Model;
using Moq.AutoMock;
using PhotoBackupUtility.App.Configuration;

namespace PhotoBackupUtility.App.Test;

public class CurrentBackupStateBuilderTest
{
    [Fact(DisplayName = "Build Current Backup State File")]
    public async Task TestGetState()
    {
        ListObjectsV2Response resp = new()
        {
            S3Objects = new List<S3Object>() { new() {Key = "2020/10-01-2020/101_1000.NEF"} },
            IsTruncated = false
        };
        
        AutoMocker mocker = new();

        mocker.GetMock<ISettings>().Setup(q => q.StateFileName)
            .Returns("state.json");

        mocker.GetMock<ISettings>().Setup(q => q.BucketName)
            .Returns("sample");

        mocker.GetMock<IAmazonS3>()
            .Setup(q => q.ListObjectsV2Async(It.IsAny<ListObjectsV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resp);
        
        CurrentBackupStateBuilder builder = mocker.CreateInstance<CurrentBackupStateBuilder>();
        await builder.Call();
        
        Assert.True(File.Exists("state.json"));
    }
}
