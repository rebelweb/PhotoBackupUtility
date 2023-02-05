using Moq.AutoMock;

namespace PhotoBackupUtility.App.Test;

public class BackupStateServiceTest
{
    [Fact(DisplayName = "Get Backup State - Parent Directory Missing")]
    public void TestGetBackupState_ParentDirectoryMissing()
    {
        AutoMocker mocker = new();
        BackupStateService service = mocker.CreateInstance<BackupStateService>();
        List<ManagedFileInfo> files = service.GetFilesToBackup("Z:\\");
        Assert.Empty(files);
    }

    [Fact(DisplayName = "Get Backup State - Parent Directory Exists")]
    public void TestGetBackupState_ParentDirectoryExists()
    {
        AutoMocker mocker = new();
        BackupStateService service = mocker.CreateInstance<BackupStateService>();
        List<ManagedFileInfo> files = service.GetFilesToBackup("./Directory");
        
        Assert.Equal(3, files.Count);
    }

    [Fact(DisplayName = "Writes State File")]
    public void TestWritingStateFile()
    {
        AutoMocker mocker = new();
        
        List<ManagedFileInfo> files = new()
        {
            new() { FilePath = "" }
        };
        
        BackupStateService service = mocker.CreateInstance<BackupStateService>();
        service.UpdateBackupState(files);

        bool fileExists = File.Exists("./backup_state.json");
        Assert.True(fileExists);
    }
}
