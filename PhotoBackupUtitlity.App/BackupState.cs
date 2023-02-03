using System.Text.Json.Serialization;

namespace PhotoBackupUtility.App;

public class BackupState
{
    [JsonPropertyName("files")]
    public List<ManagedFileInfo> Files { get; set; }
}
