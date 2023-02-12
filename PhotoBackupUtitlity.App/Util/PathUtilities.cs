namespace PhotoBackupUtility.App.Util;

public static class PathUtilities
{
    public static string S3ObjectKeyFromPath(string path)
    {
        if (!Path.IsPathRooted(path)) return path;

        if (path.Substring(1,1) == ":") return path.Substring(3);

        if (path.Substring(0, 1) == "/") return path.Substring(1);

        return path;
    }
}
