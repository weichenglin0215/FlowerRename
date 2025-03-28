using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlowerRename
{
    public partial class InsertRuleControl : UserControl
    {
        public Form_FlowerRename _Form_FlowerRename;
        public int RuleID; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        public event Action<string, string, string, int, bool, int, int, int> InsertFileNameChanged;

        public InsertRuleControl()
        {
            InitializeComponent();
            baseFileNameTextBox.TextChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            InsertFileNameTextBox.TextChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            fromstartComboBox.TextChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            startInsertNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            fromstartComboBox.TextChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };

            InsertNumberCheckBox.CheckStateChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            startNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            incNumberNumericUpDown.ValueChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };
            paddingNumericUpDown.ValueChanged += (s, e) =>
            {
                InsertFileNameChanged?.Invoke(baseFileNameTextBox.Text, InsertFileNameTextBox.Text, fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, InsertNumberCheckBox.Checked, (int)startNumberNumericUpDown.Value, (int)incNumberNumericUpDown.Value, (int)paddingNumericUpDown.Value);
            };

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 移除所有事件订阅者
            InsertFileNameChanged = delegate { };
            //通知Form_FlowerRename把_numberingRule從_ruleManager中刪除
            _Form_FlowerRename.RemoveRuleControlPair(RuleID);
            //刪除自己
            Parent?.Controls.Remove(this);
        }

        private void InsertNumberCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (InsertNumberCheckBox.Checked)
            {
                startNumberNumericUpDown.Enabled = true;
                incNumberNumericUpDown.Enabled = true;
                paddingNumericUpDown.Enabled = true;
            }
            else
            {
                startNumberNumericUpDown.Enabled = false;
                incNumberNumericUpDown.Enabled = false;
                paddingNumericUpDown.Enabled = false;
            }
        }
    }


    //public class InsertRule : RuleBase
    public class InsertRule : IRenameRule
    {
        private InsertRuleControl _ruleControl;
        private string _baseFileName;
        private string _InsertFileName;
        private string _fromstartComboBox;
        private int _startInsertNumber;

        private bool _InsertNumberCheckBox;
        private int _startNumber;
        private int _incNumber;
        private int _padding;

        //private bool _isExpanded;
        //private bool _isEnabled;
        private string[] _originalFileNamesWithoutExt;
        private string[] _originalFileExtNames;
        
        public InsertRule(InsertRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            UpdateParameters();
            /*
            _baseFileName = _ruleControl.baseFileNameTextBox.Text;
            _InsertFileName = _ruleControl.InsertFileNameTextBox.Text;    
            _fromstartComboBox = _ruleControl.fromstartComboBox.Text;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;

            _InsertNumberCheckBox = _ruleControl.InsertNumberCheckBox.Checked;
            _startInsertNumber = (int)_ruleControl.startInsertNumberNumericUpDown.Value;
            _incNumber = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding = (int)_ruleControl.paddingNumericUpDown.Value;
            */
        }

        public  string RuleName
        {
            get { return "InsertRule"; }
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
            _InsertFileName = _ruleControl.InsertFileNameTextBox.Text;
            _fromstartComboBox = _ruleControl.fromstartComboBox.Text;
            _startInsertNumber = (int)_ruleControl.startInsertNumberNumericUpDown.Value;

            _InsertNumberCheckBox = _ruleControl.InsertNumberCheckBox.Checked;
            _startNumber = (int)_ruleControl.startNumberNumericUpDown.Value;
            _incNumber = (int)_ruleControl.incNumberNumericUpDown.Value;
            _padding = (int)_ruleControl.paddingNumericUpDown.Value;
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
                //先使用string.indexof()或string.lastindexof()比對_baseFileName，如果找到，則替換成_InsertFileName
                //_startNumberNumericUpDown是從第幾個開始，如果_InsertAllStringCheckBox是true，則替換所有_baseFileName  


                //根據_startInsertNumber找出插入位置，將_baseFileName插入到originalFileNameWithoutExt

                if (_fromstartComboBox == "從後面找起")
                {
                    int tmp_startInsertNumber = originalFileNameWithoutExt.Length - _startInsertNumber;
                    _startInsertNumber = tmp_startInsertNumber;
                }
                string tmp_NumString = "";
                if (_InsertNumberCheckBox)
                {
                    tmp_NumString = $"{(_startNumber + (i * _incNumber)).ToString().PadLeft(_padding, '0')}";
                }
                _startInsertNumber = Math.Min(_startInsertNumber, originalFileNameWithoutExt.Length);
                _startInsertNumber = Math.Max(_startInsertNumber, 0);

                newFileNames[i] = originalFileNameWithoutExt.Insert(_startInsertNumber, _baseFileName + tmp_NumString) + originalFileExtName;


            }
            return newFileNames;
        }



        public UserControl GetConfigControl()
        {
            return new InsertRuleControl();
        }
    }
}