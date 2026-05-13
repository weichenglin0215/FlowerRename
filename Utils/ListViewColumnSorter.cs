using System.Collections;

namespace FlowerRename
{
    /// <summary>
    /// 提供 ListView 欄位排序功能的比較器（IComparer 實作）。
    /// 自動偵測欄位內容是否為數值：
    ///   - 兩者皆為數值 → 以數值大小排序
    ///   - 其中一個為數值 → 數值排在前面
    ///   - 兩者皆為字串 → 以忽略大小寫的字串順序排序
    /// 支援升序（Ascending）與降序（Descending）切換。
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        // 目前排序依據的欄位索引（對應 ListView.Columns 的索引）
        private int _sortColumn = 0;

        // 目前的排序方向
        private SortOrder _sortOrder = SortOrder.None;

        // 用於字串比較的工具（忽略大小寫）
        private readonly CaseInsensitiveComparer _comparer = new CaseInsensitiveComparer();

        /// <summary>設定或取得排序依據的欄位索引</summary>
        public int SortColumn
        {
            get => _sortColumn;
            set => _sortColumn = value;
        }

        /// <summary>設定或取得排序方向（Ascending / Descending / None）</summary>
        public SortOrder Order
        {
            get => _sortOrder;
            set => _sortOrder = value;
        }

        /// <summary>
        /// IComparer 介面實作：比較兩個 ListViewItem 指定欄位的值。
        /// </summary>
        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem itemX || y is not ListViewItem itemY)
                return 0;

            string sx = itemX.SubItems[_sortColumn].Text;
            string sy = itemY.SubItems[_sortColumn].Text;

            // 嘗試將欄位值解析為數值，以決定排序策略
            bool isNumX = double.TryParse(sx, out double dx);
            bool isNumY = double.TryParse(sy, out double dy);

            int result;
            if (isNumX && isNumY)
                result = dx.CompareTo(dy);      // 兩者都是數字：數值比較
            else if (isNumX)
                result = -1;                    // 只有 x 是數字：數字排前面
            else if (isNumY)
                result = 1;                     // 只有 y 是數字：數字排前面
            else
                result = _comparer.Compare(sx, sy); // 兩者都是字串：字串比較

            return _sortOrder switch
            {
                SortOrder.Ascending  =>  result,
                SortOrder.Descending => -result,
                _                    =>  0
            };
        }
    }
}
