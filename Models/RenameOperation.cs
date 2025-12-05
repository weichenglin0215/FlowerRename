using System;
using System.Collections.Generic;

namespace FlowerRename
{
    /// <summary>
    /// 記錄一次完整的更名操作
    /// </summary>
    public class RenameOperation
    {
        /// <summary>
        /// 操作時間戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 該次操作中所有檔案的更名記錄
        /// </summary>
        public List<RenameItem> Items { get; set; } = new List<RenameItem>();

        /// <summary>
        /// 成功更名的檔案數量
        /// </summary>
        public int SuccessCount => Items.Count(item => item.Success);

        /// <summary>
        /// 失敗的檔案數量
        /// </summary>
        public int FailCount => Items.Count(item => !item.Success);
    }
}

