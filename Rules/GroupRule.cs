using System.Diagnostics;

namespace FlowerRename
{
    /// <summary>
    /// 【群組化規則】的 UI 控制項。
    /// 可設定：前綴文字、起始號、遞增量、補零位數、群組數量。
    /// 任何參數變更時觸發 GroupFileNameChanged 事件。
    /// </summary>
    public partial class GroupRuleControl : UserControl
    {
        // 主視窗參照，用於關閉時通知移除規則
        public Form_FlowerRename? _Form_FlowerRename;

        // 規則唯一 ID
        public int RuleID;

        /// <summary>任何參數變更時觸發（前綴、起始號、遞增量、補零位數、群組數量）</summary>
        public event Action<string, int, int, int, int>? GroupFileNameChanged;

        public GroupRuleControl()
        {
            InitializeComponent();

            GroupFileNameTextBox.TextChanged        += (s, e) => FireChanged();
            startNumberNumericUpDown.ValueChanged   += (s, e) => FireChanged();
            incNumberNumericUpDown.ValueChanged     += (s, e) => FireChanged();
            paddingNumericUpDown.ValueChanged       += (s, e) => FireChanged();
            groupAmountUpDown.ValueChanged          += (s, e) => FireChanged();
        }

        private void FireChanged()
        {
            GroupFileNameChanged?.Invoke(
                GroupFileNameTextBox.Text,
                (int)startNumberNumericUpDown.Value,
                (int)incNumberNumericUpDown.Value,
                (int)paddingNumericUpDown.Value,
                (int)groupAmountUpDown.Value);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            GroupFileNameChanged = null;
            _Form_FlowerRename?.RemoveRuleControlPair(RuleID);
            Parent?.Controls.Remove(this);
        }
    }


    /// <summary>
    /// 【群組化規則】的邏輯實作。
    /// 將所有檔案平均分成 N 組，組內以流水號排列，組別以 A/B/C… 字母標示。
    /// 範例：12 個檔案分成 3 組，前綴「Image」，起始 1，遞增 1，補零 2：
    ///   第 0 組：Image01-A、Image02-A、Image03-A、Image04-A
    ///   第 1 組：Image01-B、Image02-B、Image03-B、Image04-B
    ///   第 2 組：Image01-C、Image02-C、Image03-C、Image04-C
    /// 適合 AI 生成圖片時，同一組代表相同 Prompt 的不同變體，方便比對。
    /// </summary>
    public class GroupRule : IRenameRule
    {
        private readonly GroupRuleControl _ruleControl;

        // 從控制項讀取到的最新參數快取
        private string _prefix      = string.Empty;  // 檔名前綴文字
        private int    _startNumber;                 // 組內流水號起始值
        private int    _incNumber;                   // 組內流水號遞增量
        private int    _padding;                     // 流水號補零位數
        private int    _groupCount;                  // 群組數量

        // 群組標籤字母（最多支援 25 組，跳過字母 N 避免與數字 0 混淆）
        private static readonly string[] GroupLabels =
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "O","P","Q","R","S","T","U","V","W","X","Y","Z"
        };

        public GroupRule(GroupRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            UpdateParameters();
        }

        /// <summary>規則識別名稱</summary>
        public string RuleName => "GroupRule";

        /// <summary>從 UI 控制項重新讀取所有參數</summary>
        public void UpdateParameters()
        {
            _prefix      = _ruleControl.GroupFileNameTextBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber   = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding     = (int)_ruleControl.paddingNumericUpDown.Value;
            _groupCount  = (int)_ruleControl.groupAmountUpDown.Value;
            Debug.WriteLine($"GroupRule.UpdateParameters: 前綴='{_prefix}' 群組數={_groupCount}");
        }

        /// <summary>套用規則（委派給 GenerateFileName）</summary>
        public string[] Apply(string[] fileNames) => GenerateFileName(fileNames);

        /// <summary>
        /// 核心邏輯：將 N 個檔案平均分成 _groupCount 組，
        /// 每個檔案的新檔名格式為「前綴 + 組內流水號 + "-" + 組別字母 + 副檔名」。
        /// </summary>
        public string[] GenerateFileName(string[] originalFileNames)
        {
            var newFileNames = new string[originalFileNames.Length];

            // 計算每組應有幾個檔案（無條件進位，確保所有檔案都被涵蓋）
            int filesPerGroup = (int)Math.Ceiling((double)originalFileNames.Length / _groupCount);

            for (int i = 0; i < originalFileNames.Length; i++)
            {
                string ext = Path.GetExtension(originalFileNames[i]);

                // 組內的排名（決定流水號）
                int posInGroup = i % filesPerGroup;
                // 所在群組的索引（決定字母標籤）
                int groupIdx = i / filesPerGroup;

                string number = (_startNumber + posInGroup * _incNumber).ToString().PadLeft(_padding, '0');
                string label  = groupIdx < GroupLabels.Length ? GroupLabels[groupIdx] : groupIdx.ToString();

                newFileNames[i] = $"{_prefix}{number}-{label}{ext}";
            }
            return newFileNames;
        }

        /// <summary>回傳此規則的設定控制項</summary>
        public UserControl GetConfigControl() => new GroupRuleControl();
    }
}
