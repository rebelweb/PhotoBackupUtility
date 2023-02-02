using System.Text.Json.Serialization;

namespace PhotoBackupUtility.App;

public class BackupState
{
    [JsonPropertyName("files")]
    public List<FileInfo> Files { get; set; }
}
