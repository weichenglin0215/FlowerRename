namespace FlowerRename
{
    /// <summary>
    /// 檔案名稱驗證與清理的靜態工具類別。
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// 檢查字串是否為合法的 Windows 檔案名稱（不含路徑）。
        /// 不可為空字串，且不可包含 Path.GetInvalidFileNameChars() 中的任何字元。
        /// </summary>
        /// <param name="fileName">要驗證的檔案名稱（不含目錄路徑）</param>
        /// <returns>合法回傳 true，否則回傳 false</returns>
        public static bool IsValidFileName(string fileName)
        {
            return !string.IsNullOrEmpty(fileName)
                && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        /// <summary>
        /// 將不合法的檔案名稱字元全部替換為底線 _，回傳安全的檔案名稱。
        /// 若輸入為空字串，回傳空字串。
        /// </summary>
        /// <param name="fileName">原始檔案名稱（可能包含非法字元）</param>
        /// <returns>已清理的合法檔案名稱</returns>
        public static string GetSafeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
