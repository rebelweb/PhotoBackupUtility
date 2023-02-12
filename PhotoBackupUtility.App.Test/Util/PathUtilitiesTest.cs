using PhotoBackupUtility.App.Util;

namespace PhotoBackupUtility.App.Test.Util;

public class PathUtilitiesTest
{
    [Theory]
    [InlineData("E:\\Pictures\\2022\\2022-01-01\\101_0010.NEF", "Pictures\\2022\\2022-01-01\\101_0010.NEF")]
    [InlineData("Pictures\\2022\\2022-01-01\\101_0010.NEF", "Pictures\\2022\\2022-01-01\\101_0010.NEF")]
    [InlineData("/Pictures/2022/2022-01-01/100_0010.NEF", "Pictures/2022/2022-01-01/100_0010.NEF")]
    public void TestS3KeyFromPath(string input, string output)
    {
        Assert.Equal(output, PathUtilities.S3ObjectKeyFromPath(input));
    }
}
