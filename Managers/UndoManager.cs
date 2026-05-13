namespace FlowerRename
{
    /// <summary>
    /// 管理批次更名的復原（Undo）功能。
    /// 使用堆疊（Stack）記錄每次成功的更名操作，最多保留 50 筆歷史。
    /// </summary>
    public class UndoManager
    {
        // 使用 LIFO 堆疊儲存更名操作記錄，Pop 即可取得最近一次操作
        private readonly Stack<RenameOperation> _undoStack = new Stack<RenameOperation>();

        // 最多保留的更名歷史筆數，避免記憶體無限增長
        private const int MaxHistoryCount = 50;

        /// <summary>是否有可復原的操作（堆疊非空）</summary>
        public bool CanUndo => _undoStack.Count > 0;

        /// <summary>目前可復原的次數</summary>
        public int UndoCount => _undoStack.Count;

        /// <summary>
        /// 將一次更名操作推入復原堆疊。
        /// 只有至少有一個檔案成功更名的操作才會被記錄；
        /// 當超過上限時，自動移除最舊的記錄。
        /// </summary>
        /// <param name="operation">要記錄的更名操作</param>
        public void PushOperation(RenameOperation operation)
        {
            if (operation == null || operation.SuccessCount == 0)
                return;

            _undoStack.Push(operation);

            // 超過上限時，移除堆疊底部（最舊）的記錄
            if (_undoStack.Count > MaxHistoryCount)
            {
                var list = _undoStack.ToList();
                list.RemoveAt(list.Count - 1);         // 移除最舊的
                _undoStack.Clear();
                foreach (var op in Enumerable.Reverse(list))
                    _undoStack.Push(op);
            }
        }

        /// <summary>
        /// 取出最近一次更名操作的記錄（從堆疊移除）。
        /// </summary>
        /// <returns>最近一次更名操作；若無可復原的操作則回傳 null</returns>
        public RenameOperation? PopOperation()
        {
            return CanUndo ? _undoStack.Pop() : null;
        }

        /// <summary>
        /// 清除所有復原歷史記錄（通常在清空檔案清單時呼叫）。
        /// </summary>
        public void Clear()
        {
            _undoStack.Clear();
        }
    }
}
