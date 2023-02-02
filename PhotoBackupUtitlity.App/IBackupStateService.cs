namespace PhotoBackupUtility.App;

public interface IBackupStateService
{
    List<FileInfo> GetFilesToBackup(string parentDirectory);

    bool UpdateBackupState(List<FileInfo> updatedFiles);
}
