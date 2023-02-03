namespace PhotoBackupUtility.App;

public interface IBackupStateService
{
    List<ManagedFileInfo> GetFilesToBackup(string parentDirectory);

    bool UpdateBackupState(List<ManagedFileInfo> updatedFiles);
}
