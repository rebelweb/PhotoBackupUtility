namespace PhotoBackupUtility.App;

public interface IBackupStateService
{
    List<FileInfo> GetFilesToBackup();
}
