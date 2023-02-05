namespace PhotoBackupUtility.App;

public class Application
{
    private readonly IBackupStateService _backupStateService;
    private readonly IFileCopyService _fileCopyService;
    private readonly ILogger<Application> _logger;

    public Application(IBackupStateService backupStateService,
        IFileCopyService fileCopyService,
        ILogger<Application> logger)
    {
        _backupStateService = backupStateService;
        _fileCopyService = fileCopyService;
        _logger = logger;
    }
    
    public async Task Call()
    {
        _logger.LogInformation("Backing Up Files");
        List<ManagedFileInfo> filesToBackup = _backupStateService.GetFilesToBackup("Z:\\");

        if (!filesToBackup.Any())
        {
            _logger.LogInformation("No Files To Backup");
            return;
        }

        foreach (var file in filesToBackup)
        {
            _logger.LogInformation($"Backing Up - {file.FilePath}");
            await _fileCopyService.CopyFile(file);
        }

        _backupStateService.UpdateBackupState(filesToBackup);
    }
}
