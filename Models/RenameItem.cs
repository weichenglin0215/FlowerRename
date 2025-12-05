namespace FlowerRename
{
    /// <summary>
    /// 記錄單個檔案的更名資訊
    /// </summary>
    public class RenameItem
    {
        /// <summary>
        /// 原始完整路徑（更名前）
        /// </summary>
        public string OriginalPath { get; set; } = string.Empty;

        /// <summary>
        /// 新完整路徑（更名後）
        /// </summary>
        public string NewPath { get; set; } = string.Empty;

        /// <summary>
        /// 是否更名成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 失敗原因（如果失敗）
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// 在ListView中的索引位置
        /// </summary>
        public int ListViewIndex { get; set; } = -1;
    }
}

