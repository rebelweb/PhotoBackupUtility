namespace PhotoBackupUtility.App.Configuration;

public interface ISettings
{
    string ParentDirectory { get; set; }
    
    string AwsKey { get; set; }
    
    string AwsSecret { get; set; }
    
    string BucketName { get; set; }
}
