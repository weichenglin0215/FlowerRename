
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlowerRename
{
    public partial class DeleteRuleControl : UserControl
    {
        public Form_FlowerRename? _Form_FlowerRename;
        public int RuleID; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        public event Action<string, int, int>? DeleteFileNameChanged;

        public DeleteRuleControl()
        {
            InitializeComponent();
            fromstartComboBox.TextChanged += (s, e) => InvokeChange();
            startInsertNumberNumericUpDown.ValueChanged += (s, e) => InvokeChange();
            countNumericUpDown.ValueChanged += (s, e) => InvokeChange();
            Console.WriteLine("新增DeleteRuleControl");
        }

        private void InvokeChange()
        {
            DeleteFileNameChanged?.Invoke(fromstartComboBox.Text, (int)startInsertNumberNumericUpDown.Value, (int)countNumericUpDown.Value);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 移除所有事件订阅者
            DeleteFileNameChanged = delegate { };
            //通知Form_FlowerRename把_numberingRule從_ruleManager中刪除
            _Form_FlowerRename?.RemoveRuleControlPair(RuleID);
            //刪除自己
            Parent?.Controls.Remove(this);
        }
    }

    public class DeleteRule : IRenameRule
    {
        private DeleteRuleControl _ruleControl;
        private string _fromStartComboBox;
        private int _startNumber;
        private int _countNumber;

        public DeleteRule(DeleteRuleControl ruleControl)
        {
            _ruleControl = ruleControl;
            UpdateParameters();
        }

        public string RuleName
        {
            get { return "DeleteRule"; }
        }

        public void UpdateParameters()
        {
            _fromStartComboBox = _ruleControl.fromstartComboBox.Text;
            _startNumber = (int)_ruleControl.startInsertNumberNumericUpDown.Value;
            _countNumber = (int)_ruleControl.countNumericUpDown.Value;
            Console.WriteLine("UpdateParameters: " + _fromStartComboBox + " " + _startNumber + " " + _countNumber);

        }

        public string[] Apply(string[] fileNames)
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

                string currentName = originalFileNameWithoutExt;
                int len = currentName.Length;
                int startIndex = -1;

                // Adjust for 1-based user input to 0-based index
                int inputStart = _startNumber; // 1-based
                if (inputStart < 1) inputStart = 1; // Minimum 1

                if (_fromStartComboBox == "從後面找起")
                {
                    // From End
                    // 1 means last char (index len-1)
                    startIndex = len - inputStart;
                }
                else
                {
                    // From Start
                    // 1 means first char (index 0)
                    startIndex = inputStart - 1;
                }

                int countToDelete = _countNumber;
                
                // Validate and Delete
                if (startIndex >= 0 && startIndex < len && countToDelete > 0)
                {
                    // If count exceeds available chars, clamp it
                    if (startIndex + countToDelete > len)
                    {
                        countToDelete = len - startIndex;
                    }
                    
                    if (countToDelete > 0)
                    {
                        currentName = currentName.Remove(startIndex, countToDelete);
                        Console.WriteLine("DeleteRule 從第 " + startIndex + " 刪除 " + countToDelete);
                    }
                }
                else
                {
                    Console.WriteLine("DeleteRule 參數錯誤");
                }

                newFileNames[i] = currentName + originalFileExtName;
            }
            return newFileNames;
        }
        public UserControl GetConfigControl()
        {
            return new DeleteRuleControl();
        }
    }
}
