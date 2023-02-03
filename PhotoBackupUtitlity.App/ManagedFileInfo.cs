using System.Text.Json.Serialization;

namespace PhotoBackupUtility.App;

public class ManagedFileInfo
{
    [JsonPropertyName("file_path")]
    public string FilePath { get; set; }
}
