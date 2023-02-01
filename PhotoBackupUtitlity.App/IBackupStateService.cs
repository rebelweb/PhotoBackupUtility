namespace PhotoBackupUtility.App;

public interface IBackupStateService
{
    List<FileInfo> GetFilesToBackup();

    bool UpdateBackupState(List<FileInfo> updatedFiles);
}
