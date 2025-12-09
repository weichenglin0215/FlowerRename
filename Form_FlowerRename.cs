using System.Diagnostics;
using System.Windows.Forms;

namespace FlowerRename
{
    public partial class Form_FlowerRename : Form
    {
        //private FileListManager _fileListManager; //不想用這個了，很麻煩，檔案清單就直接使用fileListView
        private RuleManager? _ruleManager;
        private UndoManager _undoManager = new UndoManager(); // UNDO管理器
        private Panel? _ruleContainer;
        private int RuleID = 0; //規則的ID只是用來區分規則，避免重複名稱的規則被誤刪除
        ListViewColumnSorter m_LvwColumnSorter = new ListViewColumnSorter();

        public Form_FlowerRename()
        {
            InitializeComponent();
            fileListView.Items.Clear();
            HideButtons();
            UpdateUndoButtonState(); // 初始化UNDO按鈕狀態
            fileListView.SelectedIndexChanged += fileListView_SelectedIndexChanged;
            fileListView.DoubleClick += fileListView_DoubleClick;
            UpdateStatusLabel("啟動 FlowerRename");
        }
        private void UpdateStatusLabel(string news)
        {
            int totalCount = fileListView.Items.Count;
            int selectedCount = fileListView.SelectedItems.Count;
            toolStripStatusLabel_FilesInfo.Text = $"檔案數量：{totalCount}，被選取項目數量：{selectedCount}";
            toolStripStatusLabel_News.Text = $"{news}";
        }

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateStatusLabel("點擊項目");
        }

        private void fileListView_DoubleClick(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                ListViewItem item = fileListView.SelectedItems[0];
                string filePath = Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text);
                try
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"無法開啟檔案: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void HideButtons()
        {
            btnAddFiles.Visible = false;
            btnAddDir.Visible = false;
            btnClearSelected.Visible = false;
            btnClearAll.Visible = false;
            fileListView.Visible = false;
            comboBoxAddRule.Visible = false;
            menuStripAddRule.Visible = false;
            buttonRename.Visible = false;
            buttonUndo.Visible = false;
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
            menuStripAddRule.Visible = true;
            buttonRename.Visible = true;
            buttonUndo.Visible = true; // UNDO按鈕始終顯示，但啟用狀態由 UpdateUndoButtonState() 控制
            UpdateUndoButtonState(); // 更新UNDO按鈕啟用狀態
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
            int addedCount = 0;
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
                    //addedCount = openFileDialog.FileNames.Length;
                    // 將選擇的檔案添加到 fileListView
                    foreach (string file in openFileDialog.FileNames)
                    {
                        if (AddFileToListView(file, checkDuplicate: false)) addedCount++;
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
                    UpdateStatusLabel("清除原有清單項目並新增 " + addedCount + " 檔案");

                }
            }
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            int addedCount = 0;
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
                        //addedCount = files.Length;
                        foreach (var file in files)
                        {
                            if (AddFileToListView(file, checkDuplicate: false)) addedCount++;
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
                    UpdateStatusLabel("清除原有清單項目並新增 " + addedCount + " 檔案");

                }
            }
        }

        private void DragDropOpenDir(string path)
        {
            int addedCount = 0;
            fileListView.BeginUpdate();
            try
            {
                // 獲取資料夾中的所有檔案
                var files = System.IO.Directory.GetFiles(path);
                //addedCount = files.Length;
                foreach (var file in files)
                {
                    if (AddFileToListView(file, checkDuplicate: true)) addedCount++;
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
            UpdateStatusLabel("新增 " + addedCount + " 檔案");
        }

        private void DragDropOpenFiles(string[] files)
        {
            int addedCount = 0;
            //addedCount = files.Length;
            fileListView.BeginUpdate();
            foreach (var file in files)
            {
                Debug.WriteLine("file " + file);
                if (AddFileToListView(file, checkDuplicate: true))
                {
                    addedCount++;
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
            UpdateStatusLabel("新增 " + addedCount + " 檔案");
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            int addedCount = 0;
            fileListView.BeginUpdate();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "選擇檔案";
                openFileDialog.Filter = "所有檔案 (*.*)|*.*";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //addedCount = openFileDialog.FileNames.Length;
                    foreach (string file in openFileDialog.FileNames)
                    {
                        if (AddFileToListView(file, checkDuplicate: true)) addedCount++;
                    }
                }
            }
            fileListView.EndUpdate();
            UpdateStatusLabel("新增 " + addedCount + " 檔案");
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            fileListView.BeginUpdate();
            int addedCount = 0;
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
                        addedCount = files.Length;
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
            UpdateStatusLabel("新增 " + addedCount + " 檔案");
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            fileListView.Items.Clear(); // 清空 ListView 中的所有項目
            _undoManager.Clear(); // 清除UNDO歷史記錄
            HideButtons();
            UpdateStatusLabel("清除所有項目");
        }
        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            bool hasCheckedItems = false;
            int removedCount = 0;

            // 檢查是否有勾選的項目
            foreach (ListViewItem item in fileListView.Items)
            {
                if (item.Selected)
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
                if (item.Selected) // 檢查項目是否被勾選
                {
                    fileListView.Items.Remove(item);
                    removedCount++;
                }
            }
            if (fileListView.Items.Count == 0)
            {
                HideButtons();
            }
            //toolStripStatusLabel_News.Text = "已清除所選 " + removedCount + " 項目";
            UpdateStatusLabel("已清除所選 " + removedCount + " 項目");
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
            // 檢查是否有添加更名功能
            if (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0)
            {
                MessageBox.Show("請先在上方添加更名功能（例如：重新編號、置換指定文字等）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RenameFiles();
        }

        private void RenameFiles()
        {
            //取出fileListView的原始檔案路徑和原始檔案名稱，將該檔案的檔名更改成fileListView的SubItems[1]新檔名
            string[] originalFileNames = fileListView.Items.Cast<ListViewItem>().Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text)).ToArray();
            //新檔案名稱必須包含原始檔案路徑
            string[] newFileNames = fileListView.Items.Cast<ListViewItem>().Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[1].Text)).ToArray();
            int successCount = 0;
            int failCount = 0;
            List<string> failedFiles = new List<string>();

            // 創建更名操作記錄
            RenameOperation operation = new RenameOperation();

            //要真的修改磁碟機中的檔案名稱
            for (int i = 0; i < originalFileNames.Length; i++)
            {
                RenameItem item = new RenameItem
                {
                    OriginalPath = originalFileNames[i],
                    NewPath = newFileNames[i],
                    ListViewIndex = i
                };

                try
                {
                    // 檢查新檔案名稱是否已存在
                    if (System.IO.File.Exists(newFileNames[i]))
                    {
                        item.Success = false;
                        item.ErrorMessage = "目標檔案已存在";
                        failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (目標檔案已存在)");
                        failCount++;
                        operation.Items.Add(item);
                        continue;
                    }

                    // 檢查原始檔案是否存在
                    if (!System.IO.File.Exists(originalFileNames[i]))
                    {
                        item.Success = false;
                        item.ErrorMessage = "原始檔案不存在";
                        failedFiles.Add($"{originalFileNames[i]} (原始檔案不存在)");
                        failCount++;
                        operation.Items.Add(item);
                        continue;
                    }

                    // 執行檔案更名
                    System.IO.File.Move(originalFileNames[i], newFileNames[i]);

                    // 如果更名成功沒有出錯，則把fileListView的原始檔名更成新檔名
                    fileListView.Items[i].SubItems[0].Text = fileListView.Items[i].SubItems[1].Text;
                    item.Success = true;
                    successCount++;
                    operation.Items.Add(item);
                }
                catch (UnauthorizedAccessException ex)
                {
                    item.Success = false;
                    item.ErrorMessage = $"權限不足: {ex.Message}";
                    failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (權限不足: {ex.Message})");
                    failCount++;
                    operation.Items.Add(item);
                }
                catch (System.IO.IOException ex)
                {
                    item.Success = false;
                    item.ErrorMessage = $"IO錯誤: {ex.Message}";
                    failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (IO錯誤: {ex.Message})");
                    failCount++;
                    operation.Items.Add(item);
                }
                catch (Exception ex)
                {
                    item.Success = false;
                    item.ErrorMessage = $"錯誤: {ex.Message}";
                    failedFiles.Add($"{originalFileNames[i]} -> {newFileNames[i]} (錯誤: {ex.Message})");
                    failCount++;
                    operation.Items.Add(item);
                }
            }

            // 如果有成功更名的檔案，將操作記錄推入UNDO堆疊
            if (operation.SuccessCount > 0)
            {
                _undoManager.PushOperation(operation);
                UpdateUndoButtonState();
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

            string? selectedRule = comboBoxAddRule.SelectedItem.ToString();
            // 跳過預設選項
            if (selectedRule == null || selectedRule == "請選擇新增命名規則")
            {
                return;
            }

            // 調用統一的添加規則方法
            AddRenameRule(selectedRule);

            //讓comboBoxAddRule的選項變成第一個
            comboBoxAddRule.SelectedIndex = 0;
        }

        /// <summary>
        /// 添加更名規則的統一方法
        /// </summary>
        /// <param name="ruleName">規則名稱</param>
        private void AddRenameRule(string ruleName)
        {
            if (_ruleManager == null)
            {
                return;
            }

            switch (ruleName)
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

                    // 自動設定 baseFileNameTextBox 為前兩個檔名的相同部分（在訂閱事件之後設定，才能觸發更新）
                    string commonPrefix = GetCommonPrefixFromFileList();
                    if (!string.IsNullOrEmpty(commonPrefix))
                    {
                        numberingRuleControl.baseFileNameTextBox.Text = commonPrefix;
                        // 手動觸發更新檔案列表（因為 TextChanged 事件已經會觸發，但為了確保更新，這裡也調用一次）
                        if (_ruleManager != null)
                        {
                            _ruleManager.RefreshForm_FlowerRename_FileList();
                        }
                    }

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
                    Debug.WriteLine("Add InsertRuleControl RuleID = " + RuleID);
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
                case "刪除文字(根據位置)":
                    var DeleteRuleControl = new DeleteRuleControl();
                    DeleteRuleControl.RuleID = RuleID;
                    Debug.WriteLine("Add DeleteRuleControl RuleID = " + RuleID);
                    DeleteRuleControl.Dock = DockStyle.Top; // 設置停靠方式
                    ruleContainer.Controls.Add(DeleteRuleControl); // 將控制項添加到Form_FlowerRename容器中
                    DeleteRuleControl._Form_FlowerRename = this;
                    //將新增的numberingRuleControl在ruleContainer的位置移到最上方
                    ruleContainer.Controls.SetChildIndex(DeleteRuleControl, 0);
                    DeleteRuleControl.Focus();                    // 創建 RuleManager 中的 NumberingRule
                    var DeleteRule = new DeleteRule(DeleteRuleControl);
                    _ruleManager.AddRuleControlPair(DeleteRule, DeleteRuleControl, RuleID); // 添加或更新規則
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

                    // 自動設定 baseFileNameTextBox 為前兩個檔名的相同部分（在訂閱事件之後設定，才能觸發更新）
                    commonPrefix = GetCommonPrefixFromFileList();
                    if (!string.IsNullOrEmpty(commonPrefix))
                    {
                        GroupRuleControl.GroupFileNameTextBox.Text = commonPrefix;
                        // 手動觸發更新檔案列表（因為 TextChanged 事件已經會觸發，但為了確保更新，這裡也調用一次）
                        if (_ruleManager != null)
                        {
                            _ruleManager.RefreshForm_FlowerRename_FileList();
                        }
                    }

                    RuleID++;
                    break;
            }
        }

        // MenuStrip 事件處理方法
        private void menuItemNumberingRule_Click(object sender, EventArgs e)
        {
            AddRenameRule("重新編號");
        }

        private void menuItemReplaceRule_Click(object sender, EventArgs e)
        {
            AddRenameRule("置換指定文字(亦可刪除)");
        }

        private void menuItemInsertRule_Click(object sender, EventArgs e)
        {
            AddRenameRule("添加文字(根據位置)");
        }

        private void menuItemGroupRule_Click(object sender, EventArgs e)
        {
            AddRenameRule("群組化(AI產圖方便比對)");
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
                toolStripStatusLabel_News.Text = "提示！無法依［" + fileListView.Columns[e.Column].Text + "］排序";
                return;
            }

            if (e.Column == m_LvwColumnSorter.SortColumn)
            {
                Debug.WriteLine("點到同一個欄位");
                // 切換排序方向（升序/降序）
                if (m_LvwColumnSorter.Order == SortOrder.Ascending)
                {
                    m_LvwColumnSorter.Order = SortOrder.Descending;
                    toolStripStatusLabel_News.Text = "依［" + fileListView.Columns[e.Column].Text + "］排序，方式：降序";
                }
                else
                {
                    m_LvwColumnSorter.Order = SortOrder.Ascending;
                    toolStripStatusLabel_News.Text = "依［" + fileListView.Columns[e.Column].Text + "］排序，方式：升序";
                }
            }
            else
            {
                Debug.WriteLine("點到不同欄位");
                // 設定新的排序欄位，預設為升序
                m_LvwColumnSorter.SortColumn = e.Column;
                m_LvwColumnSorter.Order = SortOrder.Ascending;
                toolStripStatusLabel_News.Text = "依［" + fileListView.Columns[e.Column].Text + "］排序，方式：升序";
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

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            // 檢查是否有添加更名功能（UNDO後需要重新計算新檔名）
            if (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0)
            {
                MessageBox.Show("請先在上方添加更名功能（例如：重新編號、置換指定文字等）\n\nUNDO功能需要更名規則來重新計算新檔名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UndoRenameFiles();
        }

        private void UndoRenameFiles()
        {
            // 檢查是否可以執行UNDO
            if (!_undoManager.CanUndo)
            {
                MessageBox.Show("沒有可復原的操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 取得最後一次更名操作
            RenameOperation? operation = _undoManager.PopOperation();
            if (operation == null || operation.Items.Count == 0)
            {
                MessageBox.Show("沒有可復原的操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUndoButtonState();
                return;
            }

            int successCount = 0;
            int failCount = 0;
            List<string> failedFiles = new List<string>();

            // 只處理成功更名的檔案（失敗的檔案不需要復原）
            var successItems = operation.Items.Where(item => item.Success).ToList();

            if (successItems.Count == 0)
            {
                MessageBox.Show("該次操作沒有成功更名的檔案，無需復原", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUndoButtonState();
                return;
            }

            // 執行反向更名（將新檔名改回原始檔名）
            foreach (var item in successItems)
            {
                try
                {
                    // 檢查新檔案是否存在（應該存在，因為之前更名成功了）
                    if (!System.IO.File.Exists(item.NewPath))
                    {
                        failedFiles.Add($"{item.NewPath} -> {item.OriginalPath} (檔案不存在，可能已被刪除)");
                        failCount++;
                        continue;
                    }

                    // 檢查原始檔案是否已存在（如果存在，表示可能被其他操作修改過）
                    if (System.IO.File.Exists(item.OriginalPath))
                    {
                        failedFiles.Add($"{item.NewPath} -> {item.OriginalPath} (原始檔名已存在)");
                        failCount++;
                        continue;
                    }

                    // 執行反向更名
                    System.IO.File.Move(item.NewPath, item.OriginalPath);

                    // 更新ListView顯示（只更新原始檔名，新檔名會由後續的RefreshForm_FlowerRename_FileList重新計算）
                    if (item.ListViewIndex >= 0 && item.ListViewIndex < fileListView.Items.Count)
                    {
                        fileListView.Items[item.ListViewIndex].SubItems[0].Text = Path.GetFileName(item.OriginalPath);
                    }

                    successCount++;
                }
                catch (UnauthorizedAccessException ex)
                {
                    failedFiles.Add($"{item.NewPath} -> {item.OriginalPath} (權限不足: {ex.Message})");
                    failCount++;
                }
                catch (System.IO.IOException ex)
                {
                    failedFiles.Add($"{item.NewPath} -> {item.OriginalPath} (IO錯誤: {ex.Message})");
                    failCount++;
                }
                catch (Exception ex)
                {
                    failedFiles.Add($"{item.NewPath} -> {item.OriginalPath} (錯誤: {ex.Message})");
                    failCount++;
                }
            }

            // 更新RuleManager的原始檔名列表，並重新計算新檔名
            if (_ruleManager != null && successCount > 0)
            {
                string[] restoredFileNames = fileListView.Items.Cast<ListViewItem>()
                    .Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text))
                    .ToArray();
                _ruleManager.SetOriginalFileNames(restoredFileNames);

                // 強制重新計算並更新新檔名（根據當前的更名規則）
                _ruleManager.RefreshForm_FlowerRename_FileList();
            }

            // 更新UNDO按鈕狀態
            UpdateUndoButtonState();

            // 顯示復原結果訊息
            string message = $"成功復原 {successCount} 個檔案";
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
                MessageBox.Show(message, failCount > 0 ? "部分成功" : "復原成功", MessageBoxButtons.OK, failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(message, "復原成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 更新UNDO按鈕的啟用狀態
        /// </summary>
        private void UpdateUndoButtonState()
        {
            // UNDO按鈕只有在有UNDO歷史記錄時才啟用（可點擊）
            buttonUndo.Enabled = _undoManager.CanUndo;
            if (_undoManager.CanUndo)
            {
                buttonUndo.Text = $"復原 Undo ({_undoManager.UndoCount})";
            }
            else
            {
                buttonUndo.Text = "復原 Undo";
            }
        }

        /// <summary>
        /// 從檔案列表中取得前兩個檔名的相同前綴部分（不含副檔名）
        /// </summary>
        /// <returns>相同的前綴字串，如果沒有相同部分則返回第一個檔名（不含副檔名）</returns>
        private string GetCommonPrefixFromFileList()
        {
            if (fileListView.Items.Count == 0)
            {
                return string.Empty;
            }

            // 取得前兩個檔名（不含副檔名）
            string firstFileName = Path.GetFileNameWithoutExtension(fileListView.Items[0].SubItems[0].Text);

            if (fileListView.Items.Count == 1)
            {
                // 只有一個檔案，返回該檔名
                return firstFileName;
            }

            string secondFileName = Path.GetFileNameWithoutExtension(fileListView.Items[1].SubItems[0].Text);

            // 計算相同的前綴部分（不區分大小寫）
            int commonLength = 0;
            int minLength = Math.Min(firstFileName.Length, secondFileName.Length);

            for (int i = 0; i < minLength; i++)
            {
                // 使用不區分大小寫的比較
                if (char.ToLowerInvariant(firstFileName[i]) == char.ToLowerInvariant(secondFileName[i]))
                {
                    commonLength++;
                }
                else
                {
                    break;
                }
            }

            // 如果有相同部分，返回相同部分；否則返回第一個檔名
            if (commonLength > 0)
            {
                return firstFileName.Substring(0, commonLength);
            }
            else
            {
                return firstFileName;
            }
        }

        private void toolStripMenuItem_CopyFileNameToClipboard_Click(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                try
                {
                    // 取得選取項目的檔案名稱 (SubItems[0] 為原始檔名)
                    var fileNames = fileListView.SelectedItems.Cast<ListViewItem>()
                                              .Select(item => item.Text);
                    string textToCopy = string.Join(Environment.NewLine, fileNames);
                    
                    if (!string.IsNullOrEmpty(textToCopy))
                    {
                        Clipboard.SetText(textToCopy);
                        string trimText = textToCopy.Replace("\r\n", "、");
                        toolStripStatusLabel_News.Text = "複製檔名至剪貼簿 " + trimText;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"複製到剪貼簿時發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}