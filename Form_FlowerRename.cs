using System.Diagnostics;

namespace FlowerRename
{
    public partial class Form_FlowerRename : Form
    {
        //private FileListManager _fileListManager; //不想用這個了，很麻煩，檔案清單就直接使用fileListView
        private RuleManager? _ruleManager;
        private Panel? _ruleContainer;
        private int RuleID = 0; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        ListViewColumnSorter m_LvwColumnSorter = new ListViewColumnSorter();

        public Form_FlowerRename()
        {
            InitializeComponent();
            fileListView.Items.Clear();
            HideButtons();
        }
        public void HideButtons()
        {
            btnAddFiles.Visible = false;
            btnAddDir.Visible = false;
            btnClearSelected.Visible = false;
            btnClearAll.Visible = false;
            fileListView.Visible = false;
            comboBoxAddRule.Visible = false;
            buttonRename.Visible = false;
        }
        public void ShowAllButtons()
        {
            btnOpenFiles.Visible = true;
            btnOpenDir.Visible = true;
            btnAddFiles.Visible = true;
            btnAddDir.Visible = true;
            btnClearSelected.Visible = true;
            btnClearAll.Visible = true;
            fileListView.Visible = true;
            comboBoxAddRule.Visible = true;
            comboBoxAddRule.SelectedIndex = 0;
            buttonRename.Visible = true;
        }
        public void InitializeRules_AddDefaultRule()
        {
            if (_ruleManager != null && _ruleManager.GetRuleControlPair().Count == 0)
            {
                Debug.WriteLine("_ruleManager.Count: " + _ruleManager.GetRuleControlPair().Count.ToString());
            }
        }
        public void UpdateFileListView(string[] newFileNames)
        {
            fileListView.BeginUpdate(); 
            //string[] originalNames = fileListView.Items.Cast<ListViewItem>().Select(item => item.Text).ToArray();
            //string[] newNames = _ruleManager.ProcessNewFileName(originalNames);
            // 確保 newNames 的長度與 fileListView.Items.Count 相同
            int count = Math.Min(fileListView.Items.Count, newFileNames.Length);
            for (int i = 0; i < count; i++)
            {
                Debug.WriteLine("newNames[" + i + "]: " + newFileNames[i]);
                // 更新 ListViewItem 的 SubItems[1] 為新的檔名
                fileListView.Items[i].SubItems[1].Text = newFileNames[i];
            }
            fileListView.EndUpdate();
        }
        private void Form_FlowerRename_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                var data = e.Data.GetData(DataFormats.FileDrop);
                if (data is string[] paths && paths != null)
                {
                    // 檢查是否至少有一個檔案或目錄
                    if (paths.Any(path => System.IO.File.Exists(path) || System.IO.Directory.Exists(path)))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void Form_FlowerRename_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                var data = e.Data.GetData(DataFormats.FileDrop);
                if (data is string[] paths && paths != null)
                {
                    // 先處理目錄
                    foreach (var dir in paths.Where(System.IO.Directory.Exists))
                    {
                        DragDropOpenDir(dir);
                    }

                    // 再處理檔案
                    var files = paths.Where(System.IO.File.Exists).ToArray();
                    if (files.Length > 0)
                    {
                        Debug.WriteLine("files " + files[0]);
                        DragDropOpenFiles(files);
                    }
                }
            }
        }

        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            fileListView.BeginUpdate();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 設定對話框屬性
                openFileDialog.Title = "選擇檔案";
                openFileDialog.Filter = "所有檔案 (*.*)|*.*"; // 可以根據需要修改檔案類型
                openFileDialog.Multiselect = true; // 允許多選

                // 顯示對話框並檢查使用者是否選擇了檔案
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 清空現有的項目
                    fileListView.Items.Clear();

                    // 將選擇的檔案添加到 fileListView
                    foreach (string file in openFileDialog.FileNames)
                    {
                        AddFileToListView(file, checkDuplicate: false);
                    }
                    if (fileListView.Items.Count > 0 && (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0))
                    {
                        _ruleManager = new RuleManager(this); // 確保初始化
                    }
                    if (fileListView.Items.Count > 0)
                    {
                        ShowAllButtons();
                        if (_ruleManager != null)
                        {
                            _ruleManager.GetOriginalFileNames();
                        }
                    }
                    fileListView.EndUpdate();

                }
            }
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            fileListView.BeginUpdate();
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "選擇資料夾";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    fileListView.Items.Clear();

                    // 獲取資料夾中的所有檔案
                    try
                    {
                        var files = System.IO.Directory.GetFiles(selectedPath);
                        foreach (var file in files)
                        {
                            AddFileToListView(file, checkDuplicate: false);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"讀取資料夾時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (fileListView.Items.Count > 0 && (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0))
                    {
                        _ruleManager = new RuleManager(this); // 確保初始化
                    }
                    if (fileListView.Items.Count > 0)
                    {
                        ShowAllButtons();
                        if (_ruleManager != null)
                        {
                            _ruleManager.GetOriginalFileNames();
                        }
                    }
                    fileListView.EndUpdate();

                }
            }
        }

        private void DragDropOpenDir(string path)
        {
            fileListView.BeginUpdate();
            try
            {
                // 獲取資料夾中的所有檔案
                var files = System.IO.Directory.GetFiles(path);
                foreach (var file in files)
                {
                    AddFileToListView(file, checkDuplicate: true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"拖放開啟資料夾時發生錯誤: {path}, 錯誤訊息: {ex.Message}");
                MessageBox.Show($"讀取資料夾時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (fileListView.Items.Count > 0 && (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0))
            {
                _ruleManager = new RuleManager(this); // 確保初始化
            }
            if (fileListView.Items.Count > 0)
            {
                ShowAllButtons();
                if (_ruleManager != null)
                {
                    _ruleManager.GetOriginalFileNames();
                }
            }
            fileListView.EndUpdate();
        }

        private void DragDropOpenFiles(string[] files)
        {
            fileListView.BeginUpdate();
            foreach (var file in files)
            {
                Debug.WriteLine("file " + file);
                if (AddFileToListView(file, checkDuplicate: true))
                {
                    Debug.WriteLine("file add " + file);
                }
            }
            if (fileListView.Items.Count > 0 && (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0))
            {
                _ruleManager = new RuleManager(this); // 確保初始化
            }
            if (fileListView.Items.Count > 0)
            {
                ShowAllButtons();
                if (_ruleManager != null)
                {
                    _ruleManager.GetOriginalFileNames();
                }
            }
            fileListView.EndUpdate();
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            fileListView.BeginUpdate();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "選擇檔案";
                openFileDialog.Filter = "所有檔案 (*.*)|*.*";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        AddFileToListView(file, checkDuplicate: true);
                    }
                }
            }
            fileListView.EndUpdate();
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            fileListView.BeginUpdate();
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "選擇資料夾";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;

                    // 獲取資料夾中的所有檔案
                    try
                    {
                        var files = System.IO.Directory.GetFiles(selectedPath);
                        foreach (var file in files)
                        {
                            AddFileToListView(file, checkDuplicate: true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"讀取資料夾時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            fileListView.EndUpdate();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            fileListView.Items.Clear(); // 清空 ListView 中的所有項目
            HideButtons();
        }
        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            bool hasCheckedItems = false;

            // 檢查是否有勾選的項目
            foreach (ListViewItem item in fileListView.Items)
            {
                if (item.Checked)
                {
                    hasCheckedItems = true;
                    break; // 找到勾選的項目後可以提前退出迴圈
                }
            }

            // 如果沒有勾選的項目，顯示警告對話框
            if (!hasCheckedItems)
            {
                MessageBox.Show("您沒有勾選任何項目。\n\n請先勾選要刪除的項目再清除。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 退出方法
            }

            // 從後往前刪除已勾選的項目，以避免索引錯誤
            for (int i = fileListView.Items.Count - 1; i >= 0; i--)
            {
                ListViewItem item = fileListView.Items[i];
                if (item.Checked) // 檢查項目是否被勾選
                {
                    fileListView.Items.Remove(item);
                }
            }
            if (fileListView.Items.Count == 0)
            {
                HideButtons();
            }
        }
        /// <summary>
        /// 將檔案添加到 ListView 的共用方法
        /// </summary>
        /// <param name="filePath">檔案完整路徑</param>
        /// <param name="checkDuplicate">是否檢查重複檔案</param>
        /// <returns>是否成功添加</returns>
        private bool AddFileToListView(string filePath, bool checkDuplicate = true)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return false;
                }

                var fileInfo = new System.IO.FileInfo(filePath);
                string originalFileName = fileInfo.Name;
                string newFileName = originalFileName;
                string fileSize = fileInfo.Length.ToString();
                string fileDate = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                string fileDirectory = fileInfo.DirectoryName ?? string.Empty;

                // 檢查檔案是否已存在
                if (checkDuplicate && IsFileInListView(originalFileName, fileDirectory))
                {
                    return false;
                }

                ListViewItem item = new ListViewItem(originalFileName);
                item.SubItems.Add(newFileName);
                item.SubItems.Add(fileSize);
                item.SubItems.Add(fileDate);
                item.SubItems.Add(fileDirectory);
                fileListView.Items.Add(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"添加檔案到 ListView 時發生錯誤: {filePath}, 錯誤訊息: {ex.Message}");
                return false;
            }
        }

        private bool IsFileInListView(string fileName, string fileDirectory)
        {
            foreach (ListViewItem item in fileListView.Items)
            {
                if (item.Text.Equals(fileName, StringComparison.OrdinalIgnoreCase) && item.SubItems[4].Text.Equals(fileDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // 檔案已存在
                }
            }
            return false; // 檔案不存在
        }


        public string[] GetFileList()
        {
            //回傳fileListView所有項目的原始檔案路徑和原始檔案名稱
            return fileListView.Items.Cast<ListViewItem>().Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text)).ToArray();
        }
        public string[] GetSelectedFileList()
        {
            //取得fileListView的原始檔案路徑和原始檔案名稱
            return fileListView.Items.Cast<ListViewItem>().Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text)).ToArray();
        }

        public void SetFileList(string[] newFileNames)
        {
            fileListView.BeginUpdate();
            //把newFileNames設定給fileListView的SubItems[1]新檔名
            int count = Math.Min(fileListView.Items.Count, newFileNames.Length);
            for (int i = 0; i < count; i++)
            {
                Debug.WriteLine("newNames[" + i + "]: " + newFileNames[i]);
                // 更新 ListViewItem 的 SubItems[1] 為新的檔名
                fileListView.Items[i].SubItems[1].Text = newFileNames[i];
            }
            fileListView.EndUpdate();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            //取出fileListView的原始檔案路徑和原始檔案名稱，將該檔案的檔名更改成fileListView的SubItems[1]新檔名
            string[] originalFileNames = fileListView.Items.Cast<ListViewItem>().Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text)).ToArray();
            //新檔案名稱必須包含原始檔案路徑
            string[] newFileNames = fileListView.Items.Cast<ListViewItem>().Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[1].Text)).ToArray();
            int successCount = 0;
            int failCount = 0;
            List<string> failedFiles = new List<string>();
            
            //要真的修改磁碟機中的檔案名稱
            for (int i = 0; i < originalFileNames.Length; i++)
            {
                try
                {
                    // 檢查新檔案名稱是否已存在
                    if (System.IO.File.Exists(newFileNames[i]))
                    {
                        failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (目標檔案已存在)");
                        failCount++;
                        continue;
                    }
                    
                    // 檢查原始檔案是否存在
                    if (!System.IO.File.Exists(originalFileNames[i]))
                    {
                        failedFiles.Add($"{originalFileNames[i]} (原始檔案不存在)");
                        failCount++;
                        continue;
                    }
                    
                    // 執行檔案更名
                    System.IO.File.Move(originalFileNames[i], newFileNames[i]);
                    
                    // 如果更名成功沒有出錯，則把fileListView的原始檔名更成新檔名
                    fileListView.Items[i].SubItems[0].Text = fileListView.Items[i].SubItems[1].Text;
                    successCount++;
                }
                catch (UnauthorizedAccessException ex)
                {
                    failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (權限不足: {ex.Message})");
                    failCount++;
                }
                catch (System.IO.IOException ex)
                {
                    failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (IO錯誤: {ex.Message})");
                    failCount++;
                }
                catch (Exception ex)
                {
                    failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (錯誤: {ex.Message})");
                    failCount++;
                }
            }
            
            // 顯示結果訊息
            string message = $"成功更名 {successCount} 個檔案";
            if (failCount > 0)
            {
                message += $"\n失敗 {failCount} 個檔案";
                if (failedFiles.Count <= 10)
                {
                    message += "\n\n失敗的檔案：\n" + string.Join("\n", failedFiles);
                }
                else
                {
                    message += "\n\n前10個失敗的檔案：\n" + string.Join("\n", failedFiles.Take(10));
                    message += $"\n... 還有 {failedFiles.Count - 10} 個檔案失敗";
                }
                MessageBox.Show(message, failCount > 0 ? "部分成功" : "成功", MessageBoxButtons.OK, failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(message, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            //並將檔名資料傳送給_ruleManager
            if (_ruleManager != null && successCount > 0)
            {
                _ruleManager.SetOriginalFileNames(newFileNames);
            }
        }

        private void comboBoxAddRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAddRule.SelectedItem == null || _ruleManager == null)
            {
                return;
            }
            //MessageBox.Show("測試" + comboBoxAddRule.SelectedItem.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            switch (comboBoxAddRule.SelectedItem.ToString())
            {
                case "重新編號":
                    var numberingRuleControl = new NumberingRuleControl();
                    numberingRuleControl.RuleID = RuleID;
                    Debug.WriteLine("Add NumberingRuleControl RuleID = " + RuleID);
                    numberingRuleControl.Dock = DockStyle.Top; // 設置停靠方式
                    ruleContainer.Controls.Add(numberingRuleControl); // 將控制項添加到Form_FlowerRename容器中
                    numberingRuleControl._Form_FlowerRename = this;
                    //將新增的numberingRuleControl在ruleContainer的位置移到最上方
                    ruleContainer.Controls.SetChildIndex(numberingRuleControl, 0);
                    numberingRuleControl.Focus();                    // 創建 RuleManager 中的 NumberingRule
                    var numberingRule = new NumberingRule(numberingRuleControl);
                    _ruleManager.AddRuleControlPair(numberingRule, numberingRuleControl, RuleID); // 添加或更新規則
                    RuleID++;
                    break;
                case "置換指定文字(亦可刪除)":
                    var ReplaceRuleControl = new ReplaceRuleControl();
                    ReplaceRuleControl.RuleID = RuleID;
                    Debug.WriteLine("Add ReplaceRuleControl RuleID = " + RuleID);
                    ReplaceRuleControl.Dock = DockStyle.Top; // 設置停靠方式
                    ruleContainer.Controls.Add(ReplaceRuleControl); // 將控制項添加到Form_FlowerRename容器中
                    ReplaceRuleControl._Form_FlowerRename = this;
                    //將新增的numberingRuleControl在ruleContainer的位置移到最上方
                    ruleContainer.Controls.SetChildIndex(ReplaceRuleControl, 0);
                    ReplaceRuleControl.Focus();                    // 創建 RuleManager 中的 NumberingRule
                    var ReplaceRule = new ReplaceRule(ReplaceRuleControl);
                    _ruleManager.AddRuleControlPair(ReplaceRule, ReplaceRuleControl, RuleID); // 添加或更新規則
                    RuleID++;
                    break;
                case "添加文字(根據位置)":
                    var InsertRuleControl = new InsertRuleControl();
                    InsertRuleControl.RuleID = RuleID;
                    Debug.WriteLine("Add ReplaceRuleControl RuleID = " + RuleID);
                    InsertRuleControl.Dock = DockStyle.Top; // 設置停靠方式
                    ruleContainer.Controls.Add(InsertRuleControl); // 將控制項添加到Form_FlowerRename容器中
                    InsertRuleControl._Form_FlowerRename = this;
                    //將新增的numberingRuleControl在ruleContainer的位置移到最上方
                    ruleContainer.Controls.SetChildIndex(InsertRuleControl, 0);
                    InsertRuleControl.Focus();                    // 創建 RuleManager 中的 NumberingRule
                    var InsertRule = new InsertRule(InsertRuleControl);
                    _ruleManager.AddRuleControlPair(InsertRule, InsertRuleControl, RuleID); // 添加或更新規則
                    RuleID++;
                    break;
                case "群組化(AI產圖方便比對)":
                    var GroupRuleControl = new GroupRuleControl();
                    GroupRuleControl.RuleID = RuleID;
                    Debug.WriteLine("Add GroupRuleControl RuleID = " + RuleID);
                    GroupRuleControl.Dock = DockStyle.Top; // 設置停靠方式
                    ruleContainer.Controls.Add(GroupRuleControl); // 將控制項添加到Form_FlowerRename容器中
                    GroupRuleControl._Form_FlowerRename = this;
                    //將新增的numberingRuleControl在ruleContainer的位置移到最上方
                    ruleContainer.Controls.SetChildIndex(GroupRuleControl, 0);
                    GroupRuleControl.Focus();                    // 創建 RuleManager 中的 NumberingRule
                    var GroupRule = new GroupRule(GroupRuleControl);
                    _ruleManager.AddRuleControlPair(GroupRule, GroupRuleControl, RuleID); // 添加或更新規則
                    RuleID++;
                    break;
            }
            //讓comboBoxAddRule的選項變成第一個
            comboBoxAddRule.SelectedIndex = 0;

            //_ruleManager.AddRuleControlPair(comboBoxAddRule.SelectedItem.ToString(), comboBoxAddRule.SelectedItem.ToString());

        }

        public void RemoveRuleControlPair(int RuleID)
        {
            if (_ruleManager != null)
            {
                _ruleManager.RemoveRuleControlPair(RuleID);
            }
        }
        // 處理檔案列表欄位點擊排序
        private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.fileListView.ListViewItemSorter = m_LvwColumnSorter;
            Debug.WriteLine("fileListView_ColumnClick");
            // 檢查點擊的欄位是否為目前的排序欄位
            Debug.WriteLine(e.Column.ToString());
            if (e.Column == 1) //新檔名欄位不進行排序
            {
                return;
            }
          
            if (e.Column == m_LvwColumnSorter.SortColumn)
            {
                Debug.WriteLine("點到同一個欄位");
                // 切換排序方向（升序/降序）
                if (m_LvwColumnSorter.Order == SortOrder.Ascending)
                {
                    m_LvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    m_LvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                Debug.WriteLine("點到不同欄位");
                // 設定新的排序欄位，預設為升序
                m_LvwColumnSorter.SortColumn = e.Column;
                m_LvwColumnSorter.Order = SortOrder.Ascending;
            }
            Debug.WriteLine("fileListView_ColumnClick2");

            fileListView.BeginUpdate();
            // 執行排序
            this.fileListView.Sort();
            Debug.WriteLine("SORT");
            if (_ruleManager != null)
            {
                _ruleManager.RefreshForm_FlowerRename_FileList(); //通知RuleManager更新計算並更新主介面的新舊檔名。
            }
            fileListView.EndUpdate();
        }
    }
}