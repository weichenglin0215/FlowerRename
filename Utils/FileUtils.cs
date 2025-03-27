public static class FileUtils
{
    public static bool IsValidFileName(string fileName)
    {
        return !string.IsNullOrEmpty(fileName) 
            && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
    }

    public static string GetSafeFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return string.Empty;
        return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
    }

    // 其他檔案相關工具方法
} 