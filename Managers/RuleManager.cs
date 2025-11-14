using System.Diagnostics;
using System.Security.Policy;
namespace FlowerRename
{
    public class RuleManager
    {
        private Form_FlowerRename _form_FlowerRename;
        private string[] _originalFileNames = Array.Empty<string>();
        private string[] _newFileNames = Array.Empty<string>();
        public struct RuleControlPair
        {
            public int RuleID;
            public IRenameRule Rule;
            public UserControl Control;
        }
        private List<RuleControlPair> _ruleControlPairs = new List<RuleControlPair>();

        public RuleManager(Form_FlowerRename form)
        {
            _form_FlowerRename = form;
            _originalFileNames = _form_FlowerRename.GetFileList();
        }

        // 取得所有規則組(包含規則介面和控制項)
        public List<RuleControlPair> GetRuleControlPair()
        {
            return _ruleControlPairs;
        }

        // 取得所有規則介面的控制項
        public List<UserControl> GetRuleControls()
        {
            return _ruleControlPairs.Select(pair => pair.Control).ToList();
        }
        //如何透過UserControl去找到RuleControlPair  
        public RuleControlPair FindRuleControlPair(UserControl userControl)
        {
            Debug.WriteLine("findRuleControlPair" + _ruleControlPairs.FirstOrDefault(pair => pair.Control == userControl));
            return _ruleControlPairs.FirstOrDefault(pair => pair.Control == userControl);
        }
        public void RemoveRuleControlPair(int RuleID)
        {
            //請顯示每一個RuleControlPair的RuleID
            foreach (var pair in _ruleControlPairs)
            {
                Debug.WriteLine("RuleID: " + pair.RuleID);
            }
            Debug.WriteLine("_ruleControlPairs.Count = " + _ruleControlPairs.Count); 
            Debug.WriteLine("RemoveRuleControlPair = " + RuleID);
            var removedCount = _ruleControlPairs.RemoveAll(pair => pair.RuleID == RuleID);
            Debug.WriteLine("Removed Count: " + removedCount);
            Debug.WriteLine("_ruleControlPairs.Count = " + _ruleControlPairs.Count); 
        }

        //如何透過UserControl去找到並刪除RuleControlPair  
        public void RemoveRuleControlPair(UserControl userControl)
        {
            //不要刪除所有的RuleControlPair，只刪除一個 
            _ruleControlPairs.RemoveAll(pair => pair.Control == userControl);
        }


        public void AddRuleControlPair(IRenameRule rule, UserControl ruleControl, int RuleID)
        {
            _ruleControlPairs.Add(new RuleControlPair { Rule = rule, Control = ruleControl, RuleID = RuleID });
            // 訂閱 ruleControl 的事件
            
            if (ruleControl is NumberingRuleControl numberingRuleControl)
            {
                numberingRuleControl.BaseFileNameChanged += (baseFileName, startNumber, incNumber, padding) =>
                {
                    RefreshForm_FlowerRename_FileList(); //更新計算並更新主介面的新舊檔名。
                };
            }
            else if (ruleControl is ReplaceRuleControl replaceRuleControl)
            {
                replaceRuleControl.ReplaceFileNameChanged += (baseFileName, replaceFileName, fromstartComboBox, startNumber, replaceAllStringCheckBox) =>
                {
                    RefreshForm_FlowerRename_FileList(); //更新計算並更新主介面的新舊檔名。
                };
            }
            else if (ruleControl is InsertRuleControl InsertRuleControl)
            {
                InsertRuleControl.InsertFileNameChanged += (baseFileName, insertFileName, fromstartComboBox, startInsertNumber, InsertNumberCheckBox, startNumber, incNumber, padding) =>
                {
                    RefreshForm_FlowerRename_FileList(); //更新計算並更新主介面的新舊檔名。
                };
            }
            else if (ruleControl is GroupRuleControl GroupRuleControl)
            {
                GroupRuleControl.GroupFileNameChanged += (GroupFileName, startNumber, incNumber, padding, groupAmount) =>
                {
                    RefreshForm_FlowerRename_FileList(); //更新計算並更新主介面的新舊檔名。
                };
            }
        }


        public void MoveRule(int oldIndex, int newIndex)
        {
            if (oldIndex < 0 || oldIndex >= _ruleControlPairs.Count ||
                newIndex < 0 || newIndex >= _ruleControlPairs.Count)
                return;

            var rule = _ruleControlPairs[oldIndex];
            _ruleControlPairs.RemoveAt(oldIndex);
            _ruleControlPairs.Insert(newIndex, rule);
        }
        private void UpdateRuleParameters()
        {
            foreach (var rule in _ruleControlPairs)
            {
                if (rule.Rule != null)
                {
                    rule.Rule.UpdateParameters();
                }
            }
        }

        // 定義事件
        public event Action<string[]>? FileNamesUpdated;

        private void UpdateFileNames()
        {
            // 觸發事件而不是直接調用方法
            FileNamesUpdated?.Invoke(_newFileNames);
        }

        public void GetOriginalFileNames() 
        {
            _originalFileNames = _form_FlowerRename.GetFileList();
        }
        public void SetOriginalFileNames(string[] originalFileNames)
        {
            _originalFileNames = originalFileNames;
        }
        public string[] ProcessNewFileName(string[] originalNames)
        {
            string[] results = originalNames;
            foreach (var rule in _ruleControlPairs)
            {
                if (rule.Rule != null)
                {
                    results = rule.Rule.Apply(results);
                }
            }
            return results;
        }

        public void RefreshForm_FlowerRename_FileList()
        {
            // 更新規則的參數
            UpdateRuleParameters();
            // 計算新的檔名
            _originalFileNames = _form_FlowerRename.GetSelectedFileList();
            Debug.WriteLine("_originalFileNames：" + _originalFileNames);
            _newFileNames = ProcessNewFileName(_originalFileNames);
            //將_newFileNames刪除目錄字串只留下檔名與副檔名 
            _newFileNames = _newFileNames.Select(name => System.IO.Path.GetFileName(name)).ToArray();

            _form_FlowerRename.SetFileList(_newFileNames);
        }
    }
}