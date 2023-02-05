using System.Text.Json;

namespace PhotoBackupUtility.App;

public class BackupStateService : IBackupStateService
{
    private readonly ILogger<BackupStateService> _logger;

    public BackupStateService(ILogger<BackupStateService> logger)
    {
        _logger = logger;
    }

    public List<ManagedFileInfo> GetFilesToBackup(string parentDirectory)
    {
        List<ManagedFileInfo> files = new();
        
        if (!Directory.Exists(parentDirectory))
        {
            _logger.LogWarning("Parent Directory Does Not Exist - Nothing To Backup");
            return files;
        }

        string[] filePaths = Directory.GetFiles(parentDirectory);

        foreach (var filePath in filePaths)
        {
            ManagedFileInfo managedFileInfo = new()
            {
                FilePath = filePath
            };
            
            files.Add(managedFileInfo);
        }

        string[] directories = Directory.GetDirectories(parentDirectory);

        foreach (var directory in directories)
        {
            List<ManagedFileInfo> nestedFiles = GetFilesToBackup(directory);
            files.AddRange(nestedFiles);
        }
        
        return files;
    }

    public bool UpdateBackupState(List<ManagedFileInfo> updatedFiles)
    {
        BackupState state = new() { Files = updatedFiles };
        string jsonData = JsonSerializer.Serialize(state);
        
        File.WriteAllText("backup_state.json", jsonData);

        return true;
    }
}
