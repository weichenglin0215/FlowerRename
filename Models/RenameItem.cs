namespace FlowerRename
{
    /// <summary>
    /// 記錄單一檔案在一次更名操作中的資訊，
    /// 包含原始路徑、目標路徑、是否成功，以及失敗原因。
    /// 由 RenameOperation 持有，供 Undo 功能執行反向更名。
    /// </summary>
    public class RenameItem
    {
        /// <summary>更名前的完整檔案路徑（含目錄與原始檔名）</summary>
        public string OriginalPath { get; set; } = string.Empty;

        /// <summary>更名後的完整檔案路徑（含目錄與新檔名）</summary>
        public string NewPath { get; set; } = string.Empty;

        /// <summary>此筆更名是否成功（磁碟上的實際更名已完成）</summary>
        public bool Success { get; set; }

        /// <summary>失敗原因說明（Success = false 時才有內容）</summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// 此檔案在 ListView 中的索引位置，
        /// Undo 時用來精確更新對應的 ListView 列。
        /// </summary>
        public int ListViewIndex { get; set; } = -1;
    }
}
