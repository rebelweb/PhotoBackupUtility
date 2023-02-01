namespace PhotoBackupUtility.App;

public interface IFileCopyService
{
    bool CopyFile(FileInfo fileInfo);
}
