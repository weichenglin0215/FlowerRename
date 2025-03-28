using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlowerRename
{
    public partial class ReplaceRuleControl : UserControl
    {
        public Form_FlowerRename _Form_FlowerRename;
        public int RuleID; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        public event Action<string,string,string,int, bool> ReplaceFileNameChanged;

        public ReplaceRuleControl()
        {
            InitializeComponent();
            baseFileNameTextBox.TextChanged += (s, e) =>
            {
                ReplaceFileNameChanged?.Invoke(baseFileNameTextBox.Text, replaceFileNameTextBox.Text, fromstartComboBox.Text, (int)startNumberNumericUpDown.Value, replaceAllStringCheckBox.Checked);
            };
            replaceFileNameTextBox.TextChanged += (s, e) =>
            {
                ReplaceFileNameChanged?.Invoke(baseFileNameTextBox.Text, replaceFileNameTextBox.Text, fromstartComboBox.Text, (int)startNumberNumericUpDown.Value, replaceAllStringCheckBox.Checked);
            };
            fromstartComboBox.TextChanged += (s, e) =>
            {
                ReplaceFileNameChanged?.Invoke(baseFileNameTextBox.Text, replaceFileNameTextBox.Text, fromstartComboBox.Text, (int)startNumberNumericUpDown.Value, replaceAllStringCheckBox.Checked);
            };
            startNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                ReplaceFileNameChanged?.Invoke(baseFileNameTextBox.Text, replaceFileNameTextBox.Text, fromstartComboBox.Text, (int)startNumberNumericUpDown.Value, replaceAllStringCheckBox.Checked);
            };
            replaceAllStringCheckBox.CheckStateChanged += (s, e) =>
            {
                ReplaceFileNameChanged?.Invoke(baseFileNameTextBox.Text, replaceFileNameTextBox.Text, fromstartComboBox.Text, (int)startNumberNumericUpDown.Value, replaceAllStringCheckBox.Checked);
            };
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 移除所有事件订阅者
            ReplaceFileNameChanged = delegate { };
            //通知Form_FlowerRename把_numberingRule從_ruleManager中刪除
            _Form_FlowerRename.RemoveRuleControlPair(RuleID);
            //刪除自己
            Parent?.Controls.Remove(this);
        }

        private void replaceAllStringCheckBox_CheckedChanged(object sender, EventArgs e)
        {
          if (replaceAllStringCheckBox.Checked)
          {
            fromstartComboBox.Enabled = false;
            startNumberNumericUpDown.Enabled = false;
          }
          else
          {
            fromstartComboBox.Enabled = true;
            startNumberNumericUpDown.Enabled = true;
          } 
        }
    }


    //public class ReplaceRule : RuleBase
    public class ReplaceRule : IRenameRule
    {
        private ReplaceRuleControl _ruleControl;
        private string _baseFileName;
        private string _replaceFileName;
        private string _fromstartComboBox;
        private int _startNumber;
        private bool _replaceAllStringCheckBox;

        //private bool _isExpanded;
        //private bool _isEnabled;
        private string[] _originalFileNamesWithoutExt;
        private string[] _originalFileExtNames;
        
        public ReplaceRule(ReplaceRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            _baseFileName = _ruleControl.baseFileNameTextBox.Text;
            _replaceFileName = _ruleControl.replaceFileNameTextBox.Text;    
            _fromstartComboBox = _ruleControl.fromstartComboBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _replaceAllStringCheckBox = _ruleControl.replaceAllStringCheckBox.Checked;
        }

        public  string RuleName
        {
            get { return "ReplaceRule"; }
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
            _replaceFileName = _ruleControl.replaceFileNameTextBox.Text;
            _fromstartComboBox = _ruleControl.fromstartComboBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _replaceAllStringCheckBox = _ruleControl.replaceAllStringCheckBox.Checked;
            Debug.WriteLine("UpdateParameters: " + _baseFileName + " " + _startNumber);
        }
        public  string[] Apply(string[] fileNames)
        {

            return GenerateFileName(fileNames);
        }

        public string[] GenerateFileName(string[] originalFileNames)
        {
            string[] newFileNames = new string[originalFileNames.Length];
            for (int i = 0; i < originalFileNames.Length; i++)
            {
                string originalFileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileNames[i]);
                string originalFileExtName = Path.GetExtension(originalFileNames[i]);
                UpdateParameters();
                //如果_fromstartComboBox是"從開頭開始"，從一開始比對;如果_fromstartComboBox是"從後面開始找"，則從後面開始比對
                //先使用string.indexof()或string.lastindexof()比對_baseFileName，如果找到，則替換成_replaceFileName
                //_startNumberNumericUpDown是從第幾個開始，如果_replaceAllStringCheckBox是true，則替換所有_baseFileName  
                if (_replaceAllStringCheckBox){
                    if (_baseFileName != "" && originalFileNameWithoutExt.IndexOf(_baseFileName, _startNumber - 1, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        //newFileNames[i] = originalFileNameWithoutExt.Replace(_baseFileName, _replaceFileName);
                        newFileNames[i] = originalFileNameWithoutExt.Replace(_baseFileName, _replaceFileName, StringComparison.OrdinalIgnoreCase)+ originalFileExtName;
                    }
                    else
                    {
                        newFileNames[i] = $"{originalFileNameWithoutExt}{originalFileExtName}";
                        //MessageBox.Show("找不到" + _baseFileName + "，無法替換");
                    }
                }
                else //string.replace()會替換所有相同字串，所以要用不同方法去替換字串，還沒寫好
                {
                    newFileNames[i] = $"{originalFileNameWithoutExt}{originalFileExtName}";
                }
                /*
                else if (_fromstartComboBox == "從開頭開始")
                {
                    if (originalFileNameWithoutExt.IndexOf(_baseFileName, _startNumber, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        newFileNames[i] = originalFileNameWithoutExt.Replace(_baseFileName, _replaceFileName, StringComparison.OrdinalIgnoreCase);
                    }
                }
                else if (_fromstartComboBox == "從後面開始找")
                {
                    if (originalFileNameWithoutExt.Contains(_baseFileName))
                    {
                        newFileNames[i] = originalFileNameWithoutExt.Replace(_baseFileName, _replaceFileName, StringComparison.OrdinalIgnoreCase);
                    }
                }
                */
            }
            return newFileNames;
        }



        public UserControl GetConfigControl()
        {
            return new ReplaceRuleControl();
        }
    }
}