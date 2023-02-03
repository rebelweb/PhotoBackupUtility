namespace PhotoBackupUtility.App.Test;

public class ApplicationTest
{
    private ManagedFileInfo _managedFile = new() { FilePath = "101_0001.NEF" };
    private ManagedFileInfo file2 = new() { FilePath = "101_0002.NEF" };
    
    [Fact(DisplayName = "Test Backing Up Files - No Files To Backup")]
    public async Task TestBackingUpFile_NoFilesToBackUp()
    {
        StringWriter writer = new();
        Console.SetOut(writer);
        Mock<IBackupStateService> backupStateServiceMock = new();
        Mock<IFileCopyService> fileCopyService = new();

        backupStateServiceMock.Setup(q => q.GetFilesToBackup("./"))
            .Returns(new List<ManagedFileInfo>());

        Application app = new Application(backupStateServiceMock.Object, fileCopyService.Object);
        await app.Call();
        
        fileCopyService.Verify(q => q.CopyFile(It.IsAny<ManagedFileInfo>()), Times.Never);
        backupStateServiceMock.Verify(q => q.UpdateBackupState(It.IsAny<List<ManagedFileInfo>>()), Times.Never());
        Assert.Equal("No Files To Backup\r\n", writer.ToString());
    }

    [Fact(DisplayName = "Test Backing Up Files - Backing Up One File")]
    public async Task TestBackingUpFiles_SingleFile()
    {
        StringWriter writer = new();
        Console.SetOut(writer);
        Mock<IBackupStateService> backupStateService = new();
        Mock<IFileCopyService> fileCopyService = new();

        backupStateService.Setup(q => q.GetFilesToBackup("./"))
            .Returns(new List<ManagedFileInfo>() { _managedFile });

        Application app = new Application(backupStateService.Object, fileCopyService.Object);
        await app.Call();
        
        fileCopyService.Verify(q => q.CopyFile(It.IsAny<ManagedFileInfo>()), Times.Once);
        backupStateService.Verify(q => q.UpdateBackupState(It.IsAny<List<ManagedFileInfo>>()), Times.Once);

        Assert.Equal("Backing Up - 101_0001.NEF\r\n", writer.ToString());
    }

    [Fact(DisplayName = "Test Backing Up Files - Backing Up Multiple Files")]
    public async Task TestBackingUpFiles_MultipleFiles()
    {
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        Mock<IBackupStateService> backupStateService = new();
        Mock<IFileCopyService> fileCopyService = new();

        backupStateService.Setup(q => q.GetFilesToBackup("./"))
            .Returns(new List<ManagedFileInfo>() { _managedFile, file2 });

        Application app = new(backupStateService.Object, fileCopyService.Object);
        await app.Call();
        
        fileCopyService.Verify(q => q.CopyFile(It.IsAny<ManagedFileInfo>()), Times.Exactly(2));
        backupStateService.Verify(q => q.UpdateBackupState(It.IsAny<List<ManagedFileInfo>>()), Times.Once);
        Assert.Equal("Backing Up - 101_0001.NEF\r\nBacking Up - 101_0002.NEF\r\n", writer.ToString());
    }
}
