namespace PhotoBackupUtility.App.Test;

public class BackupStateServiceTest
{
    [Fact(DisplayName = "Get Backup State - Parent Directory Missing")]
    public void TestGetBackupState_ParentDirectoryMissing()
    {
        StringWriter writer = new();
        Console.SetOut(writer);
        
        BackupStateService service = new();
        service.GetFilesToBackup("Z:\\");

        Assert.Equal("Parent Directory Does Not Exist - Nothing To Backup\r\n", writer.ToString());
    }

    [Fact(DisplayName = "Get Backup State - Parent Directory Exists")]
    public void TestGetBackupState_ParentDirectoryExists()
    {
        BackupStateService service = new();
        List<ManagedFileInfo> files = service.GetFilesToBackup("./Directory");
        
        Assert.Equal(3, files.Count);
    }

    [Fact]
    public void TestWritingStateFile()
    {
        List<ManagedFileInfo> files = new()
        {
            new() { FilePath = "" }
        };
        
        BackupStateService service = new();
        service.UpdateBackupState(files);

        bool fileExists = File.Exists("./backup_state.json");
        Assert.True(fileExists);
    }
}
