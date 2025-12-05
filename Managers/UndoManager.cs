using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowerRename
{
    /// <summary>
    /// 管理UNDO功能的類別
    /// </summary>
    public class UndoManager
    {
        private readonly Stack<RenameOperation> _undoStack = new Stack<RenameOperation>();
        private const int MAX_HISTORY_COUNT = 50; // 最多保存50次操作歷史

        /// <summary>
        /// 是否可以執行UNDO
        /// </summary>
        public bool CanUndo => _undoStack.Count > 0;

        /// <summary>
        /// 當前可UNDO的次數
        /// </summary>
        public int UndoCount => _undoStack.Count;

        /// <summary>
        /// 將一次更名操作推入UNDO堆疊
        /// </summary>
        /// <param name="operation">更名操作記錄</param>
        public void PushOperation(RenameOperation operation)
        {
            if (operation == null)
                return;

            // 只記錄有成功更名的操作
            if (operation.SuccessCount > 0)
            {
                _undoStack.Push(operation);

                // 限制歷史記錄數量，避免記憶體占用過多
                if (_undoStack.Count > MAX_HISTORY_COUNT)
                {
                    // 移除最舊的記錄（由於Stack的特性，需要轉換為List操作）
                    var list = _undoStack.ToList();
                    list.RemoveAt(list.Count - 1); // 移除最舊的
                    _undoStack.Clear();
                    // 使用 LINQ 的 Reverse() 擴充方法，它返回可枚舉的序列
                    foreach (var op in list.AsEnumerable().Reverse())
                    {
                        _undoStack.Push(op);
                    }
                }
            }
        }

        /// <summary>
        /// 執行UNDO操作，返回最後一次更名操作的記錄
        /// </summary>
        /// <returns>最後一次更名操作的記錄，如果沒有可UNDO的操作則返回null</returns>
        public RenameOperation? PopOperation()
        {
            if (!CanUndo)
                return null;

            return _undoStack.Pop();
        }

        /// <summary>
        /// 查看最後一次操作（不取出）
        /// </summary>
        /// <returns>最後一次更名操作的記錄，如果沒有則返回null</returns>
        public RenameOperation? PeekOperation()
        {
            if (!CanUndo)
                return null;

            return _undoStack.Peek();
        }

        /// <summary>
        /// 清除所有UNDO歷史記錄
        /// </summary>
        public void Clear()
        {
            _undoStack.Clear();
        }
    }
}

