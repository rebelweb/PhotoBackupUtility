using Microsoft.Extensions.Logging;
using Moq.AutoMock;

namespace PhotoBackupUtility.App.Test;

public class ApplicationTest
{
    private ManagedFileInfo _managedFile = new() { FilePath = "101_0001.NEF" };
    private ManagedFileInfo file2 = new() { FilePath = "101_0002.NEF" };
    
    [Fact(DisplayName = "Test Backing Up Files - No Files To Backup")]
    public async Task TestBackingUpFile_NoFilesToBackUp()
    {
        AutoMocker mocker = new();
        
        Mock<IBackupStateService> backupStateServiceMock = new();
        Mock<IFileCopyService> fileCopyService = new();

        mocker.GetMock<IBackupStateService>().Setup(q => q.GetFilesToBackup("Z:\\"))
            .Returns(new List<ManagedFileInfo>());

        Application app = mocker.CreateInstance<Application>();
        await app.Call();
        
        mocker.GetMock<IFileCopyService>().Verify(q => q.CopyFile(It.IsAny<ManagedFileInfo>()), Times.Never);
        mocker.GetMock<IBackupStateService>().Verify(q => q.UpdateBackupState(It.IsAny<List<ManagedFileInfo>>()), Times.Never());
    }

    [Fact(DisplayName = "Test Backing Up Files - Backing Up One File")]
    public async Task TestBackingUpFiles_SingleFile()
    {
        AutoMocker mocker = new();

        mocker.GetMock<IBackupStateService>().Setup(q => q.GetFilesToBackup("Z:\\"))
            .Returns(new List<ManagedFileInfo>() { _managedFile });

        Application app = mocker.CreateInstance<Application>();
        await app.Call();
        
        mocker.GetMock<IFileCopyService>().Verify(q => q.CopyFile(It.IsAny<ManagedFileInfo>()), Times.Once);
        mocker.GetMock<IBackupStateService>().Verify(q => q.UpdateBackupState(It.IsAny<List<ManagedFileInfo>>()), Times.Once);
    }

    [Fact(DisplayName = "Test Backing Up Files - Backing Up Multiple Files")]
    public async Task TestBackingUpFiles_MultipleFiles()
    {
        AutoMocker mocker = new();
        
        mocker.GetMock<IBackupStateService>().Setup(q => q.GetFilesToBackup("Z:\\"))
            .Returns(new List<ManagedFileInfo>() { _managedFile, file2 });

        Application app = mocker.CreateInstance<Application>();
        await app.Call();
        
        mocker.GetMock<IFileCopyService>().Verify(q => q.CopyFile(It.IsAny<ManagedFileInfo>()), Times.Exactly(2));
        mocker.GetMock<IBackupStateService>().Verify(q => q.UpdateBackupState(It.IsAny<List<ManagedFileInfo>>()), Times.Once);
    }
}
