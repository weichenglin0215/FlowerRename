using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlowerRename
{
    public partial class NumberingRuleControl : UserControl
    {
        public Form_FlowerRename _Form_FlowerRename;
        public int RuleID; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        public event Action<string, int, int, int> BaseFileNameChanged;

        public NumberingRuleControl()
        {
            InitializeComponent();
            baseFileNameTextBox.TextChanged += (s, e) =>
            {
                BaseFileNameChanged?.Invoke(baseFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            startNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                BaseFileNameChanged?.Invoke(baseFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            incNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                BaseFileNameChanged?.Invoke(baseFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            paddingNumericUpDown.ValueChanged += (s, e) =>
            {
                BaseFileNameChanged?.Invoke(baseFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 移除所有事件订阅者
            BaseFileNameChanged = delegate { };
            //通知Form_FlowerRename把_numberingRule從_ruleManager中刪除
            _Form_FlowerRename.RemoveRuleControlPair(RuleID);
            //刪除自己
            Parent?.Controls.Remove(this);
        }
    }


    public class NumberingRule : IRenameRule
    {
        private NumberingRuleControl _ruleControl;
        private string _baseFileName;
        private int _startNumber;
        private int _incNumber;
        private int _padding;
        //private bool _isExpanded;
        //private bool _isEnabled;
        private string[] _originalFileNamesWithoutExt;
        private string[] _originalFileExtNames;

        public NumberingRule(NumberingRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            _baseFileName = _ruleControl.baseFileNameTextBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding = (int)_ruleControl.paddingNumericUpDown.Value;
        }
        public string RuleName
        {
            get { return "NumberingRule"; }
        }
        /*
       public bool IsExpanded
       {
           get => _isExpanded;
           set => _isExpanded = value;
       }
       public  bool IsEnabled
       {
           get => _isEnabled;
           set => _isEnabled = value;
       } 
       */
        public void UpdateParameters()
        {
            _baseFileName = _ruleControl.baseFileNameTextBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding = (int)_ruleControl.paddingNumericUpDown.Value;
            //Debug.WriteLine("UpdateParameters: " + _baseFileName + " " + _startNumber + " " + _padding);
        }
        //原本規劃按下確認，後來沒使用，改成GenerateFileName()
        public string[] Apply(string[] fileNames)
        {
            return GenerateFileName(fileNames);
        }
        // 

        public string[] GenerateFileName(string[] originalFileNames)
        {
            string[] newFileNames = new string[originalFileNames.Length];
            for (int i = 0; i < originalFileNames.Length; i++)
            {
                string originalFileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileNames[i]);
                string originalFileExtName = Path.GetExtension(originalFileNames[i]);
                newFileNames[i] = $"{_baseFileName}{(_startNumber + (i * _incNumber)).ToString().PadLeft(_padding, '0')}{originalFileExtName}";
            }
            return newFileNames;
        }



        public UserControl GetConfigControl()
        {
            return new NumberingRuleControl();
        }
    }
}