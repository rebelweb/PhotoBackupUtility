namespace PhotoBackupUtility.App;

public interface IFileCopyService
{
    Task<bool> CopyFile(FileInfo fileInfo);
}
