using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlowerRename
{
    public partial class GroupRuleControl : UserControl
    {
        public Form_FlowerRename _Form_FlowerRename;
        public int RuleID; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        public event Action<string, int, int, int, int> GroupFileNameChanged;

        public GroupRuleControl()
        {
            InitializeComponent();
            GroupFileNameTextBox.TextChanged += (s, e) =>
            {
                GroupFileNameChanged?.Invoke(GroupFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value, (int)groupAmountUpDown.Value);
            };
            startNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                GroupFileNameChanged?.Invoke(GroupFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value, (int)groupAmountUpDown.Value);
            };
            incNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                GroupFileNameChanged?.Invoke(GroupFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value, (int)groupAmountUpDown.Value);
            };
            paddingNumericUpDown.ValueChanged += (s, e) =>
            {
                GroupFileNameChanged?.Invoke(GroupFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value, (int)groupAmountUpDown.Value);
            };
            groupAmountUpDown.ValueChanged += (s, e) =>
            {
                GroupFileNameChanged?.Invoke(GroupFileNameTextBox.Text, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value, (int)groupAmountUpDown.Value);
            };
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 移除所有事件订阅者
            GroupFileNameChanged = delegate { };
            //通知Form_FlowerRename把_GroupRule從_ruleManager中刪除
            _Form_FlowerRename.RemoveRuleControlPair(RuleID);
            //刪除自己
            Parent?.Controls.Remove(this);
        }
    }


    public class GroupRule : IRenameRule
    {
        private GroupRuleControl _ruleControl;
        private string _GroupFileName;
        private int _startNumber;
        private int _incNumber;
        private int _padding;
        private int _groupAmount;
        //private bool _isExpanded;
        //private bool _isEnabled;
        private string[] _originalFileNamesWithoutExt;
        private string[] _originalFileExtNames;

        public GroupRule(GroupRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            _GroupFileName = _ruleControl.GroupFileNameTextBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding = (int)_ruleControl.paddingNumericUpDown.Value;
            _groupAmount = (int)_ruleControl.groupAmountUpDown.Value;
        }
        public string RuleName
        {
            get { return "GroupRule"; }
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
            _GroupFileName = _ruleControl.GroupFileNameTextBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding = (int)_ruleControl.paddingNumericUpDown.Value;
            _groupAmount = (int)_ruleControl.groupAmountUpDown.Value;
            //Debug.WriteLine("UpdateParameters: " + _GroupFileName + " " + _startNumber + " " + _padding);
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
            string[] groupName = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            float tmpFloat = (newFileNames.Length / _groupAmount); //避免整數除整數得到整數，先轉成浮點數
            int howManyFilesInOneGroup = (int)Math.Ceiling(tmpFloat); //主要是Math.Ceiling()可帶入整數與浮點數，不確認會報錯
            for (int i = 0; i < originalFileNames.Length; i++)
            {
                string originalFileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileNames[i]);
                string originalFileExtName = Path.GetExtension(originalFileNames[i]);
                Debug.WriteLine("_groupAmount = " + _groupAmount);
                newFileNames[i] = $"{_GroupFileName}{(_startNumber + ((i % howManyFilesInOneGroup) * _incNumber)).ToString().PadLeft(_padding, '0')}{"-"}{groupName[(int)((i) / howManyFilesInOneGroup)]}{originalFileExtName}";
            }
            return newFileNames;
        }



        public UserControl GetConfigControl()
        {
            return new GroupRuleControl();
        }
    }
}