namespace PhotoBackupUtility.App;

public interface IFileCopyService
{
    Task<bool> CopyFile(ManagedFileInfo managedFileInfo);
}
