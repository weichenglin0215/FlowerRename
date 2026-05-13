namespace FlowerRename
{
    /// <summary>
    /// 記錄一次完整的批次更名操作，供 Undo 功能使用。
    /// 每次點擊「執行更名」就會產生一個 RenameOperation，
    /// 其中包含所有參與本次更名的檔案記錄（成功與失敗）。
    /// </summary>
    public class RenameOperation
    {
        /// <summary>此次更名操作的執行時間</summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>本次操作中每個檔案的更名記錄清單（包含成功與失敗）</summary>
        public List<RenameItem> Items { get; set; } = new List<RenameItem>();

        /// <summary>本次操作中成功更名的檔案數量</summary>
        public int SuccessCount => Items.Count(item => item.Success);

        /// <summary>本次操作中更名失敗的檔案數量</summary>
        public int FailCount => Items.Count(item => !item.Success);
    }
}
