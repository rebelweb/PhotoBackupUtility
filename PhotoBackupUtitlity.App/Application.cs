namespace PhotoBackupUtility.App;

public class Application
{
    private readonly IBackupStateService _backupStateService;
    private readonly IFileCopyService _fileCopyService;

    public Application(IBackupStateService backupStateService,
        IFileCopyService fileCopyService)
    {
        _backupStateService = backupStateService;
        _fileCopyService = fileCopyService;
    }
    
    public async Task Call()
    {
        List<ManagedFileInfo> filesToBackup = _backupStateService.GetFilesToBackup("./");

        if (!filesToBackup.Any())
        {
            Console.WriteLine("No Files To Backup");
            return;
        }

        foreach (var file in filesToBackup)
        {
            Console.WriteLine($"Backing Up - {file.FilePath}");
            await _fileCopyService.CopyFile(file);
        }

        _backupStateService.UpdateBackupState(filesToBackup);
    }
}
