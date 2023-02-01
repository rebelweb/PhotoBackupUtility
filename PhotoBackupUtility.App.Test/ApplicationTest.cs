namespace PhotoBackupUtility.App.Test;

public class ApplicationTest
{
    private FileInfo file = new() { FilePath = "101_0001.NEF" };
    private FileInfo file2 = new() { FilePath = "101_0002.NEF" };
    
    [Fact(DisplayName = "Test Backing Up Files - No Files To Backup")]
    public void TestBackingUpFile_NoFilesToBackUp()
    {
        StringWriter writer = new();
        Console.SetOut(writer);
        Mock<IBackupStateService> backupStateServiceMock = new();

        backupStateServiceMock.Setup(q => q.GetFilesToBackup())
            .Returns(new List<FileInfo>());
        
        Application app = new Application(backupStateServiceMock.Object);
        app.Call();
        
        Assert.Equal("No Files To Backup\r\n", writer.ToString());
    }

    [Fact(DisplayName = "Test Backing Up Files - Backing Up One File")]
    public void TestBackingUpFiles_SingleFile()
    {
        StringWriter writer = new();
        Console.SetOut(writer);
        Mock<IBackupStateService> backupStateService = new();

        backupStateService.Setup(q => q.GetFilesToBackup())
            .Returns(new List<FileInfo>() { file });

        Application app = new Application(backupStateService.Object);
        app.Call();
        
        Assert.Equal("Backing Up - 101_0001.NEF\r\n", writer.ToString());
    }

    [Fact(DisplayName = "Test Backing Up Files - Backing Up Multiple File")]
    public void TestBackingUpFiles_MultipleFiles()
    {
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        Mock<IBackupStateService> backupStateService = new();

        backupStateService.Setup(q => q.GetFilesToBackup())
            .Returns(new List<FileInfo>() { file, file2 });

        Application app = new(backupStateService.Object);
        app.Call();
        
        Assert.Equal("Backing Up - 101_0001.NEF\r\nBacking Up - 101_0002.NEF\r\n", writer.ToString());
    }
}
