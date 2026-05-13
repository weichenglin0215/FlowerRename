using System.Diagnostics;

namespace FlowerRename
{
    /// <summary>
    /// 【重新編號規則】的 UI 控制項。
    /// 提供前綴文字、起始號、遞增量、補零位數等設定，
    /// 任何參數變更時觸發 BaseFileNameChanged 事件通知 RuleManager 重新計算。
    /// </summary>
    public partial class NumberingRuleControl : UserControl
    {
        // 主視窗參照，用於關閉時通知移除規則
        public Form_FlowerRename? _Form_FlowerRename;

        // 規則唯一 ID，用於精準移除（避免同名規則誤刪）
        public int RuleID;

        /// <summary>任何參數變更時觸發（前綴、起始號、遞增量、補零位數）</summary>
        public event Action<string, int, int, int>? BaseFileNameChanged;

        public NumberingRuleControl()
        {
            InitializeComponent();

            // 所有輸入控制項的變更事件都統一觸發 BaseFileNameChanged
            baseFileNameTextBox.TextChanged += (s, e) => FireChanged();
            startNumberNumericUpDown.ValueChanged += (s, e) => FireChanged();
            incNumberNumericUpDown.ValueChanged += (s, e) => FireChanged();
            paddingNumericUpDown.ValueChanged += (s, e) => FireChanged();
        }

        private void FireChanged()
        {
            BaseFileNameChanged?.Invoke(
                baseFileNameTextBox.Text,
                (int)startNumberNumericUpDown.Value,
                (int)incNumberNumericUpDown.Value,
                (int)paddingNumericUpDown.Value);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 清除所有事件訂閱，避免記憶體洩漏
            BaseFileNameChanged = null;
            // 通知主視窗從 RuleManager 中移除此規則
            _Form_FlowerRename?.RemoveRuleControlPair(RuleID);
            // 從父容器移除此控制項（自我銷毀）
            Parent?.Controls.Remove(this);
        }
    }


    /// <summary>
    /// 【重新編號規則】的邏輯實作。
    /// 將所有檔案以「前綴 + 流水號」的格式重新命名。
    /// 範例：前綴「IMG_」、起始 1、遞增 1、補零 4 → IMG_0001.jpg、IMG_0002.jpg…
    /// </summary>
    public class NumberingRule : IRenameRule
    {
        private readonly NumberingRuleControl _ruleControl;

        // 從控制項讀取到的最新參數快取
        private string _baseFileName = string.Empty;
        private int _startNumber;
        private int _incNumber;
        private int _padding;

        public NumberingRule(NumberingRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            UpdateParameters();
        }

        /// <summary>規則識別名稱</summary>
        public string RuleName => "NumberingRule";

        /// <summary>從 UI 控制項重新讀取所有參數（由 RuleManager 在每次計算前呼叫）</summary>
        public void UpdateParameters()
        {
            _baseFileName = _ruleControl.baseFileNameTextBox.Text;
            _startNumber  = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber    = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding      = (int)_ruleControl.paddingNumericUpDown.Value;
        }

        /// <summary>套用規則（委派給 GenerateFileName）</summary>
        public string[] Apply(string[] fileNames) => GenerateFileName(fileNames);

        /// <summary>
        /// 核心邏輯：為每個檔案產生「前綴 + 流水號 + 原始副檔名」的新檔名。
        /// </summary>
        public string[] GenerateFileName(string[] originalFileNames)
        {
            var newFileNames = new string[originalFileNames.Length];
            for (int i = 0; i < originalFileNames.Length; i++)
            {
                string ext = Path.GetExtension(originalFileNames[i]);
                // 流水號 = 起始號 + 索引 × 遞增量，不足位數時補零
                string number = (_startNumber + i * _incNumber).ToString().PadLeft(_padding, '0');
                newFileNames[i] = $"{_baseFileName}{number}{ext}";
            }
            return newFileNames;
        }

        /// <summary>回傳此規則的設定控制項（用於 GetConfigControl 介面）</summary>
        public UserControl GetConfigControl() => new NumberingRuleControl();
    }
}
