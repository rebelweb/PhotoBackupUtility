namespace PhotoBackupUtility.App;

public class BackupStateService : IBackupStateService
{
    public List<FileInfo> GetFilesToBackup(string parentDirectory)
    {
        List<FileInfo> files = new();
        
        if (!Directory.Exists(parentDirectory))
        {
            Console.WriteLine("Parent Directory Does Not Exist - Nothing To Backup");
            return files;
        }

        string[] filePaths = Directory.GetFiles(parentDirectory);

        foreach (var filePath in filePaths)
        {
            FileInfo fileInfo = new()
            {
                FilePath = filePath
            };
            
            files.Add(fileInfo);
        }

        string[] directories = Directory.GetDirectories(parentDirectory);

        foreach (var directory in directories)
        {
            List<FileInfo> nestedFiles = GetFilesToBackup(directory);
            files.AddRange(nestedFiles);
        }
        
        return files;
    }

    public bool UpdateBackupState(List<FileInfo> updatedFiles)
    {
        throw new NotImplementedException();
    }
}
