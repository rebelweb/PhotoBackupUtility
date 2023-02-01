namespace PhotoBackupUtility.App;

public class Application
{
    private readonly IBackupStateService _backupStateService;

    public Application(IBackupStateService backupStateService)
    {
        _backupStateService = backupStateService;
    }
    
    public void Call()
    {
        List<FileInfo> filesToBackup = _backupStateService.GetFilesToBackup();

        if (!filesToBackup.Any())
        {
            Console.WriteLine("No Files To Backup");
            return;
        }

        foreach (var file in filesToBackup)
        {
            Console.WriteLine($"Backing Up - {file.FilePath}");
        }
    }
}
