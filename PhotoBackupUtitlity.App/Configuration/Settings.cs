namespace PhotoBackupUtility.App.Configuration;

public class Settings : ISettings
{
    public string ParentDirectory { get; set; }
    public string AwsKey { get; set; }
    public string AwsSecret { get; set; }
    public string BucketName { get; set; }
    
    public string[] FileExtensions { get; set; }
}
