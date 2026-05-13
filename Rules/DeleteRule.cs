using System.Diagnostics;

namespace FlowerRename
{
    /// <summary>
    /// 【刪除文字規則】的 UI 控制項。
    /// 可設定：計算方向（從前/從後）、起始位置（1-based）、刪除字元數。
    /// 任何參數變更時觸發 DeleteFileNameChanged 事件。
    /// </summary>
    public partial class DeleteRuleControl : UserControl
    {
        // 主視窗參照，用於關閉時通知移除規則
        public Form_FlowerRename? _Form_FlowerRename;

        // 規則唯一 ID
        public int RuleID;

        /// <summary>任何參數變更時觸發（方向、起始位置、刪除字元數）</summary>
        public event Action<string, int, int>? DeleteFileNameChanged;

        public DeleteRuleControl()
        {
            InitializeComponent();

            fromstartComboBox.TextChanged               += (s, e) => InvokeChange();
            startInsertNumberNumericUpDown.ValueChanged += (s, e) => InvokeChange();
            countNumericUpDown.ValueChanged             += (s, e) => InvokeChange();
        }

        private void InvokeChange()
        {
            DeleteFileNameChanged?.Invoke(
                fromstartComboBox.Text,
                (int)startInsertNumberNumericUpDown.Value,
                (int)countNumericUpDown.Value);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            DeleteFileNameChanged = null;
            _Form_FlowerRename?.RemoveRuleControlPair(RuleID);
            Parent?.Controls.Remove(this);
        }
    }


    /// <summary>
    /// 【刪除文字規則】的邏輯實作。
    /// 從每個檔名的指定位置刪除指定數量的字元。
    /// 起始位置使用 1-based（1 = 第一個字元），可選擇從前端或後端計算。
    /// 範例：從前面第 3 個字元起刪除 2 個字元，"ABCDE.jpg" → "ABDE.jpg"
    /// </summary>
    public class DeleteRule : IRenameRule
    {
        private readonly DeleteRuleControl _ruleControl;

        // 從控制項讀取到的最新參數快取
        private string _direction   = string.Empty;  // 計算方向（"從前面找起" / "從後面找起"）
        private int    _startPos;                    // 起始位置（1-based）
        private int    _deleteCount;                 // 要刪除的字元數

        public DeleteRule(DeleteRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            UpdateParameters();
        }

        /// <summary>規則識別名稱</summary>
        public string RuleName => "DeleteRule";

        /// <summary>從 UI 控制項重新讀取所有參數</summary>
        public void UpdateParameters()
        {
            _direction   = _ruleControl.fromstartComboBox.Text;
            _startPos    = (int)_ruleControl.startInsertNumberNumericUpDown.Value;
            _deleteCount = (int)_ruleControl.countNumericUpDown.Value;
            Debug.WriteLine($"DeleteRule.UpdateParameters: 方向={_direction} 起始={_startPos} 刪除={_deleteCount}");
        }

        /// <summary>套用規則（委派給 GenerateFileName）</summary>
        public string[] Apply(string[] fileNames) => GenerateFileName(fileNames);

        /// <summary>
        /// 核心邏輯：刪除每個檔名中指定位置起的指定數量字元。
        /// 起始位置為 1-based，超出長度時自動縮減刪除量，不拋出例外。
        /// </summary>
        public string[] GenerateFileName(string[] originalFileNames)
        {
            var newFileNames = new string[originalFileNames.Length];

            for (int i = 0; i < originalFileNames.Length; i++)
            {
                string nameWithoutExt = Path.GetFileNameWithoutExtension(originalFileNames[i]);
                string ext            = Path.GetExtension(originalFileNames[i]);
                UpdateParameters();

                int len = nameWithoutExt.Length;

                // 將 1-based 使用者輸入轉換為 0-based 字元索引
                int inputStart = Math.Max(1, _startPos);
                int startIdx;
                if (_direction == "從後面找起")
                    // 從後面：1 = 最後一個字元（index = len-1）
                    startIdx = len - inputStart;
                else
                    // 從前面：1 = 第一個字元（index = 0）
                    startIdx = inputStart - 1;

                // 邊界檢查：索引合法且刪除數量大於 0 才執行刪除
                if (startIdx >= 0 && startIdx < len && _deleteCount > 0)
                {
                    // 刪除量超出剩餘長度時，縮減至恰好刪到末尾
                    int actualDelete = Math.Min(_deleteCount, len - startIdx);
                    nameWithoutExt = nameWithoutExt.Remove(startIdx, actualDelete);
                    Debug.WriteLine($"DeleteRule: 從索引 {startIdx} 刪除 {actualDelete} 個字元");
                }
                else
                {
                    Debug.WriteLine($"DeleteRule: 參數超出範圍，略過此檔案（索引={startIdx}，長度={len}）");
                }

                newFileNames[i] = nameWithoutExt + ext;
            }
            return newFileNames;
        }

        /// <summary>回傳此規則的設定控制項</summary>
        public UserControl GetConfigControl() => new DeleteRuleControl();
    }
}
