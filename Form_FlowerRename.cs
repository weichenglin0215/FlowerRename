using System.Diagnostics;

namespace FlowerRename
{
    /// <summary>
    /// FlowerRename 的主視窗。
    /// 負責：檔案清單（fileListView）的管理、規則 UI 的建立與移除、
    /// 執行批次更名、Undo 復原，以及所有按鈕與拖放的事件處理。
    /// </summary>
    public partial class Form_FlowerRename : Form
    {
        // 規則管理器：負責維護所有作用中的命名規則並計算新檔名預覽
        private RuleManager? _ruleManager;

        // 復原管理器：記錄每次成功的更名操作，供 Undo 使用
        private readonly UndoManager _undoManager = new UndoManager();

        // 規則 ID 計數器：每新增一個規則遞增，確保 ID 唯一（即使規則名稱相同也不會誤刪）
        private int _nextRuleID = 0;

        // ListView 欄位排序器（點擊欄位標題時使用）
        private readonly ListViewColumnSorter _lvwColumnSorter = new ListViewColumnSorter();

        public Form_FlowerRename()
        {
            InitializeComponent();
            fileListView.Items.Clear();
            HideButtons();
            UpdateUndoButtonState();
            fileListView.DoubleClick += fileListView_DoubleClick;
            UpdateStatusLabel("啟動 FlowerRename");
        }

        // ───────────────────────────────────────────────
        //  狀態列更新
        // ───────────────────────────────────────────────

        /// <summary>
        /// 更新底部狀態列，顯示總檔案數、選取數量，以及最新的操作訊息。
        /// </summary>
        private void UpdateStatusLabel(string message)
        {
            toolStripStatusLabel_FilesInfo.Text =
                $"檔案數量：{fileListView.Items.Count}，被選取項目數量：{fileListView.SelectedItems.Count}";
            toolStripStatusLabel_News.Text = message;
        }

        // ───────────────────────────────────────────────
        //  UI 顯示切換（載入檔案前隱藏大部分按鈕）
        // ───────────────────────────────────────────────

        /// <summary>
        /// 隱藏所有操作按鈕（初始狀態，等待使用者載入檔案）。
        /// </summary>
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

        /// <summary>
        /// 顯示所有操作按鈕（載入檔案後呼叫）。
        /// </summary>
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
            buttonUndo.Visible = true;
            UpdateUndoButtonState();
        }

        // ───────────────────────────────────────────────
        //  檔案清單的讀取與更新（供 RuleManager 呼叫）
        // ───────────────────────────────────────────────

        /// <summary>
        /// 取得 ListView 中所有項目的「原始完整路徑」陣列（目錄 + 原始檔名）。
        /// </summary>
        public string[] GetFileList()
        {
            return fileListView.Items
                .Cast<ListViewItem>()
                .Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text))
                .ToArray();
        }

        /// <summary>
        /// 將 RuleManager 計算後的新檔名陣列寫入 ListView 的「新檔名」欄位（SubItems[1]）。
        /// </summary>
        /// <param name="newFileNames">僅含檔名（不含路徑）的新檔名陣列，長度應與清單項目數相符</param>
        public void SetFileList(string[] newFileNames)
        {
            fileListView.BeginUpdate();
            int count = Math.Min(fileListView.Items.Count, newFileNames.Length);
            for (int i = 0; i < count; i++)
            {
                Debug.WriteLine($"SetFileList[{i}]: {newFileNames[i]}");
                fileListView.Items[i].SubItems[1].Text = newFileNames[i];
            }
            fileListView.EndUpdate();
        }

        // ───────────────────────────────────────────────
        //  拖放事件
        // ───────────────────────────────────────────────

        private void Form_FlowerRename_DragEnter(object sender, DragEventArgs e)
        {
            // 只接受包含檔案/目錄的拖放操作
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                var paths = e.Data.GetData(DataFormats.FileDrop) as string[];
                bool hasValid = paths?.Any(p => File.Exists(p) || Directory.Exists(p)) == true;
                e.Effect = hasValid ? DragDropEffects.Copy : DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form_FlowerRename_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) != true)
                return;

            var paths = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (paths == null) return;

            // 先處理拖入的目錄，再處理拖入的檔案（都以「追加」方式加入，不清空現有清單）
            foreach (var dir in paths.Where(Directory.Exists))
                DragDropOpenDir(dir);

            var files = paths.Where(File.Exists).ToArray();
            if (files.Length > 0)
                DragDropOpenFiles(files);
        }

        // ───────────────────────────────────────────────
        //  開啟檔案 / 開啟資料夾（清空現有清單後重新載入）
        // ───────────────────────────────────────────────

        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "選擇檔案",
                Filter = "所有檔案 (*.*)|*.*",
                Multiselect = true
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            fileListView.BeginUpdate();
            fileListView.Items.Clear();

            int addedCount = 0;
            foreach (string file in dlg.FileNames)
                if (AddFileToListView(file, checkDuplicate: false)) addedCount++;

            OnFilesLoaded();
            fileListView.EndUpdate();
            UpdateStatusLabel($"清除原有清單並新增 {addedCount} 個檔案");
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog { Description = "選擇資料夾" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            fileListView.BeginUpdate();
            fileListView.Items.Clear();

            int addedCount = 0;
            try
            {
                foreach (var file in Directory.GetFiles(dlg.SelectedPath))
                    if (AddFileToListView(file, checkDuplicate: false)) addedCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取資料夾時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            OnFilesLoaded();
            fileListView.EndUpdate();
            UpdateStatusLabel($"清除原有清單並新增 {addedCount} 個檔案");
        }

        // ───────────────────────────────────────────────
        //  追加檔案 / 追加資料夾（保留現有清單，僅加入新項目）
        // ───────────────────────────────────────────────

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "選擇檔案",
                Filter = "所有檔案 (*.*)|*.*",
                Multiselect = true
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            fileListView.BeginUpdate();
            int addedCount = 0;
            foreach (string file in dlg.FileNames)
                if (AddFileToListView(file, checkDuplicate: true)) addedCount++;

            OnFilesLoaded();
            fileListView.EndUpdate();
            UpdateStatusLabel($"新增 {addedCount} 個檔案");
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog { Description = "選擇資料夾" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            fileListView.BeginUpdate();
            int addedCount = 0;
            try
            {
                foreach (var file in Directory.GetFiles(dlg.SelectedPath))
                    if (AddFileToListView(file, checkDuplicate: true)) addedCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取資料夾時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            OnFilesLoaded();
            fileListView.EndUpdate();
            UpdateStatusLabel($"新增 {addedCount} 個檔案");
        }

        // ───────────────────────────────────────────────
        //  拖放載入（內部輔助方法）
        // ───────────────────────────────────────────────

        private void DragDropOpenDir(string path)
        {
            fileListView.BeginUpdate();
            int addedCount = 0;
            try
            {
                foreach (var file in Directory.GetFiles(path))
                    if (AddFileToListView(file, checkDuplicate: true)) addedCount++;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"拖放開啟資料夾失敗: {path} → {ex.Message}");
                MessageBox.Show($"讀取資料夾時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            OnFilesLoaded();
            fileListView.EndUpdate();
            UpdateStatusLabel($"新增 {addedCount} 個檔案");
        }

        private void DragDropOpenFiles(string[] files)
        {
            fileListView.BeginUpdate();
            int addedCount = 0;
            foreach (var file in files)
                if (AddFileToListView(file, checkDuplicate: true)) addedCount++;

            OnFilesLoaded();
            fileListView.EndUpdate();
            UpdateStatusLabel($"新增 {addedCount} 個檔案");
        }

        /// <summary>
        /// 每次有新檔案加入清單後的共用後處理：
        /// 若 RuleManager 尚未建立則初始化，並顯示所有操作按鈕。
        /// </summary>
        private void OnFilesLoaded()
        {
            if (fileListView.Items.Count == 0) return;

            if (_ruleManager == null)
                _ruleManager = new RuleManager(this);
            else
                _ruleManager.GetOriginalFileNames(); // 重新同步原始檔名

            ShowAllButtons();
        }

        // ───────────────────────────────────────────────
        //  清除清單項目
        // ───────────────────────────────────────────────

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            fileListView.Items.Clear();
            _undoManager.Clear();       // 清空清單時一併清除 Undo 歷史
            HideButtons();
            UpdateStatusLabel("清除所有項目");
        }

        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            // 檢查是否有選取的項目
            bool hasSelected = fileListView.Items.Cast<ListViewItem>().Any(item => item.Selected);
            if (!hasSelected)
            {
                MessageBox.Show("您沒有選取任何項目。\n\n請先選取要移除的項目再清除。",
                    "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 從後往前刪除，避免索引位移問題
            int removedCount = 0;
            for (int i = fileListView.Items.Count - 1; i >= 0; i--)
            {
                if (fileListView.Items[i].Selected)
                {
                    fileListView.Items.RemoveAt(i);
                    removedCount++;
                }
            }

            if (fileListView.Items.Count == 0)
                HideButtons();

            UpdateStatusLabel($"已移除所選 {removedCount} 個項目");
        }

        // ───────────────────────────────────────────────
        //  ListView 項目的新增與重複檢查
        // ───────────────────────────────────────────────

        /// <summary>
        /// 將單一檔案加入 ListView。
        /// SubItems 欄位配置：[0] 原始檔名、[1] 新檔名（預覽）、[2] 大小（Bytes）、[3] 修改日期、[4] 所在目錄
        /// </summary>
        /// <param name="filePath">檔案完整路徑</param>
        /// <param name="checkDuplicate">是否檢查重複（追加時為 true，重新載入時為 false）</param>
        /// <returns>成功加入回傳 true；檔案不存在或重複則回傳 false</returns>
        private bool AddFileToListView(string filePath, bool checkDuplicate)
        {
            try
            {
                if (!File.Exists(filePath)) return false;

                var info = new FileInfo(filePath);

                // 追加模式下檢查同目錄同檔名是否已存在
                if (checkDuplicate && IsFileInListView(info.Name, info.DirectoryName ?? ""))
                    return false;

                var item = new ListViewItem(info.Name);
                item.SubItems.Add(info.Name);                                       // [1] 新檔名（初始等於原始檔名）
                item.SubItems.Add(info.Length.ToString());                          // [2] 檔案大小
                item.SubItems.Add(info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")); // [3] 修改日期
                item.SubItems.Add(info.DirectoryName ?? string.Empty);              // [4] 所在目錄
                fileListView.Items.Add(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AddFileToListView 失敗: {filePath} → {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 檢查指定的檔名與目錄組合是否已存在於 ListView 中（不分大小寫）。
        /// </summary>
        private bool IsFileInListView(string fileName, string directory)
        {
            return fileListView.Items.Cast<ListViewItem>()
                .Any(item =>
                    item.Text.Equals(fileName, StringComparison.OrdinalIgnoreCase) &&
                    item.SubItems[4].Text.Equals(directory, StringComparison.OrdinalIgnoreCase));
        }

        // ───────────────────────────────────────────────
        //  執行更名
        // ───────────────────────────────────────────────

        private void buttonRename_Click(object sender, EventArgs e)
        {
            // 必須先加入至少一個規則才能執行更名
            if (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0)
            {
                MessageBox.Show("請先在上方新增命名規則（例如：重新編號、置換指定文字等）",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RenameFiles();
        }

        /// <summary>
        /// 執行批次更名：讀取 ListView 的原始檔名與新檔名，逐一更名磁碟上的實際檔案，
        /// 成功後更新 ListView 顯示，並將操作記錄推入 Undo 堆疊。
        /// </summary>
        private void RenameFiles()
        {
            // 取得原始完整路徑 與 新完整路徑
            var originalPaths = fileListView.Items.Cast<ListViewItem>()
                .Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text))
                .ToArray();
            var newPaths = fileListView.Items.Cast<ListViewItem>()
                .Select(item => Path.Combine(item.SubItems[4].Text, item.SubItems[1].Text))
                .ToArray();

            int successCount = 0, failCount = 0;
            var failedMessages = new List<string>();
            var operation = new RenameOperation();

            for (int i = 0; i < originalPaths.Length; i++)
            {
                var record = new RenameItem
                {
                    OriginalPath = originalPaths[i],
                    NewPath = newPaths[i],
                    ListViewIndex = i
                };

                try
                {
                    // 目標檔案已存在 → 無法覆蓋
                    if (File.Exists(newPaths[i]))
                    {
                        record.Success = false;
                        record.ErrorMessage = "目標檔案已存在";
                        failedMessages.Add($"{originalPaths[i]} → {newPaths[i]}（目標檔案已存在）");
                        failCount++;
                        operation.Items.Add(record);
                        continue;
                    }

                    // 原始檔案不存在（可能已被手動刪除）
                    if (!File.Exists(originalPaths[i]))
                    {
                        record.Success = false;
                        record.ErrorMessage = "原始檔案不存在";
                        failedMessages.Add($"{originalPaths[i]}（原始檔案不存在）");
                        failCount++;
                        operation.Items.Add(record);
                        continue;
                    }

                    File.Move(originalPaths[i], newPaths[i]);

                    // 更名成功：同步更新 ListView 的原始檔名欄位
                    fileListView.Items[i].SubItems[0].Text = fileListView.Items[i].SubItems[1].Text;
                    record.Success = true;
                    successCount++;
                    operation.Items.Add(record);
                }
                catch (UnauthorizedAccessException ex)
                {
                    record.Success = false;
                    record.ErrorMessage = $"權限不足：{ex.Message}";
                    failedMessages.Add($"{originalPaths[i]} → {newPaths[i]}（{record.ErrorMessage}）");
                    failCount++;
                    operation.Items.Add(record);
                }
                catch (IOException ex)
                {
                    record.Success = false;
                    record.ErrorMessage = $"IO 錯誤：{ex.Message}";
                    failedMessages.Add($"{originalPaths[i]} → {newPaths[i]}（{record.ErrorMessage}）");
                    failCount++;
                    operation.Items.Add(record);
                }
                catch (Exception ex)
                {
                    record.Success = false;
                    record.ErrorMessage = $"錯誤：{ex.Message}";
                    failedMessages.Add($"{originalPaths[i]} → {newPaths[i]}（{record.ErrorMessage}）");
                    failCount++;
                    operation.Items.Add(record);
                }
            }

            // 有成功更名的操作才推入 Undo 堆疊
            if (operation.SuccessCount > 0)
            {
                _undoManager.PushOperation(operation);
                UpdateUndoButtonState();

                // 告知 RuleManager 更名後的新路徑，作為下次計算的基準
                _ruleManager?.SetOriginalFileNames(newPaths);
            }

            ShowRenameResult(successCount, failCount, failedMessages);
        }

        // ───────────────────────────────────────────────
        //  Undo 復原
        // ───────────────────────────────────────────────

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            // Undo 後需要根據規則重新計算新檔名預覽，因此必須有規則存在
            if (_ruleManager == null || _ruleManager.GetRuleControlPair().Count == 0)
            {
                MessageBox.Show("請先新增命名規則（Undo 後需要規則來重新計算新檔名預覽）",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UndoRenameFiles();
        }

        /// <summary>
        /// 執行 Undo：將上一次成功更名的檔案改回原始檔名，並重新整理 ListView 與新檔名預覽。
        /// </summary>
        private void UndoRenameFiles()
        {
            if (!_undoManager.CanUndo)
            {
                MessageBox.Show("沒有可復原的操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var operation = _undoManager.PopOperation();
            if (operation == null || operation.Items.Count == 0)
            {
                MessageBox.Show("沒有可復原的操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUndoButtonState();
                return;
            }

            // 只對上次成功更名的檔案執行反向更名
            var successItems = operation.Items.Where(item => item.Success).ToList();
            if (successItems.Count == 0)
            {
                MessageBox.Show("該次操作沒有成功更名的檔案，無需復原",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUndoButtonState();
                return;
            }

            int successCount = 0, failCount = 0;
            var failedMessages = new List<string>();

            foreach (var item in successItems)
            {
                try
                {
                    // 新路徑的檔案必須存在（代表更名確實成功過）
                    if (!File.Exists(item.NewPath))
                    {
                        failedMessages.Add($"{item.NewPath} → {item.OriginalPath}（新路徑檔案不存在，可能已被刪除）");
                        failCount++;
                        continue;
                    }

                    // 原始路徑不能已被佔用（否則無法復原）
                    if (File.Exists(item.OriginalPath))
                    {
                        failedMessages.Add($"{item.NewPath} → {item.OriginalPath}（原始檔名已存在，無法復原）");
                        failCount++;
                        continue;
                    }

                    File.Move(item.NewPath, item.OriginalPath);

                    // 更新 ListView 中的原始檔名欄位
                    if (item.ListViewIndex >= 0 && item.ListViewIndex < fileListView.Items.Count)
                        fileListView.Items[item.ListViewIndex].SubItems[0].Text =
                            Path.GetFileName(item.OriginalPath);

                    successCount++;
                }
                catch (UnauthorizedAccessException ex)
                {
                    failedMessages.Add($"{item.NewPath} → {item.OriginalPath}（權限不足：{ex.Message}）");
                    failCount++;
                }
                catch (IOException ex)
                {
                    failedMessages.Add($"{item.NewPath} → {item.OriginalPath}（IO 錯誤：{ex.Message}）");
                    failCount++;
                }
                catch (Exception ex)
                {
                    failedMessages.Add($"{item.NewPath} → {item.OriginalPath}（錯誤：{ex.Message}）");
                    failCount++;
                }
            }

            // 復原成功後重新同步 RuleManager 的原始檔名，並觸發預覽更新
            if (_ruleManager != null && successCount > 0)
            {
                var restoredPaths = fileListView.Items.Cast<ListViewItem>()
                    .Select(i => Path.Combine(i.SubItems[4].Text, i.SubItems[0].Text))
                    .ToArray();
                _ruleManager.SetOriginalFileNames(restoredPaths);
                _ruleManager.RefreshFileList();
            }

            UpdateUndoButtonState();
            ShowRenameResult(successCount, failCount, failedMessages, isUndo: true);
        }

        /// <summary>
        /// 依據 Undo 堆疊狀態更新 Undo 按鈕的啟用狀態與文字。
        /// </summary>
        private void UpdateUndoButtonState()
        {
            buttonUndo.Enabled = _undoManager.CanUndo;
            buttonUndo.Text = _undoManager.CanUndo
                ? $"復原 Undo ({_undoManager.UndoCount})"
                : "復原 Undo";
        }

        /// <summary>
        /// 顯示更名或復原的結果對話框。
        /// </summary>
        private void ShowRenameResult(int successCount, int failCount, List<string> failedMessages,
            bool isUndo = false)
        {
            string action = isUndo ? "復原" : "更名";
            string message = $"成功{action} {successCount} 個檔案";

            if (failCount > 0)
            {
                message += $"\n失敗 {failCount} 個檔案";
                var sample = failedMessages.Take(10).ToList();
                message += "\n\n" + (failedMessages.Count <= 10 ? "失敗的檔案：" : "前 10 個失敗的檔案：");
                message += "\n" + string.Join("\n", sample);
                if (failedMessages.Count > 10)
                    message += $"\n… 還有 {failedMessages.Count - 10} 個失敗";

                MessageBox.Show(message, $"部分{action}成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(message, $"{action}成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            UpdateStatusLabel(message.Split('\n')[0]);
        }

        // ───────────────────────────────────────────────
        //  規則管理
        // ───────────────────────────────────────────────

        /// <summary>
        /// ComboBox 選項變更時，依選取的規則名稱新增對應的規則控制項。
        /// </summary>
        private void comboBoxAddRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ruleManager == null) return;

            string? selected = comboBoxAddRule.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected) || selected == "請選擇新增命名規則") return;

            AddRenameRule(selected);
            comboBoxAddRule.SelectedIndex = 0; // 選完後重設回提示文字
        }

        // MenuStrip 對應的點擊事件（委派給統一的 AddRenameRule 方法）
        private void menuItemNumberingRule_Click(object sender, EventArgs e) => AddRenameRule("重新編號");
        private void menuItemReplaceRule_Click(object sender, EventArgs e) => AddRenameRule("置換指定文字(亦可刪除)");
        private void menuItemInsertRule_Click(object sender, EventArgs e) => AddRenameRule("添加文字(根據位置)");
        private void menuItemDeleteRule_Click(object sender, EventArgs e) => AddRenameRule("刪除文字(根據位置)");
        private void menuItemGroupRule_Click(object sender, EventArgs e) => AddRenameRule("群組化(AI產圖方便比對)");

        /// <summary>
        /// 依規則名稱建立對應的 UserControl 與 IRenameRule，
        /// 並將其加入規則容器（ruleContainer）及 RuleManager。
        /// ─────────────────────────────────────────────
        /// 【新增命名規則時，在此方法的 switch 加入新的 case】
        /// ─────────────────────────────────────────────
        /// </summary>
        private void AddRenameRule(string ruleName)
        {
            if (_ruleManager == null) return;

            switch (ruleName)
            {
                case "重新編號":
                {
                    var ctrl = CreateRuleControl<NumberingRuleControl>();
                    var rule = new NumberingRule(ctrl);
                    _ruleManager.AddRuleControlPair(rule, ctrl, _nextRuleID++);

                    // 自動以清單前兩個檔名的共同前綴填入前綴文字欄位
                    string prefix = GetCommonPrefixFromFileList();
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        ctrl.baseFileNameTextBox.Text = prefix;
                        _ruleManager.RefreshFileList();
                    }
                    break;
                }
                case "置換指定文字(亦可刪除)":
                {
                    var ctrl = CreateRuleControl<ReplaceRuleControl>();
                    var rule = new ReplaceRule(ctrl);
                    _ruleManager.AddRuleControlPair(rule, ctrl, _nextRuleID++);
                    break;
                }
                case "添加文字(根據位置)":
                {
                    var ctrl = CreateRuleControl<InsertRuleControl>();
                    var rule = new InsertRule(ctrl);
                    _ruleManager.AddRuleControlPair(rule, ctrl, _nextRuleID++);
                    break;
                }
                case "刪除文字(根據位置)":
                {
                    var ctrl = CreateRuleControl<DeleteRuleControl>();
                    var rule = new DeleteRule(ctrl);
                    _ruleManager.AddRuleControlPair(rule, ctrl, _nextRuleID++);
                    break;
                }
                case "群組化(AI產圖方便比對)":
                {
                    var ctrl = CreateRuleControl<GroupRuleControl>();
                    var rule = new GroupRule(ctrl);
                    _ruleManager.AddRuleControlPair(rule, ctrl, _nextRuleID++);

                    string prefix = GetCommonPrefixFromFileList();
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        ctrl.GroupFileNameTextBox.Text = prefix;
                        _ruleManager.RefreshFileList();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 建立規則控制項的共用輔助方法：設定 RuleID、Dock、父容器，並移至最上方。
        /// </summary>
        private T CreateRuleControl<T>() where T : UserControl, new()
        {
            var ctrl = new T();

            // 透過反射設定公開的 RuleID 與 _Form_FlowerRename 欄位
            var type = typeof(T);
            type.GetField("RuleID")?.SetValue(ctrl, _nextRuleID);
            type.GetField("_Form_FlowerRename")?.SetValue(ctrl, this);

            ctrl.Dock = DockStyle.Top;
            ruleContainer.Controls.Add(ctrl);
            ruleContainer.Controls.SetChildIndex(ctrl, 0); // 新規則顯示在最上方
            ctrl.Focus();
            return ctrl;
        }

        /// <summary>
        /// 由規則控制項（關閉按鈕）呼叫，要求 RuleManager 移除對應的規則。
        /// </summary>
        public void RemoveRuleControlPair(int ruleID)
        {
            _ruleManager?.RemoveRuleControlPair(ruleID);
        }

        // ───────────────────────────────────────────────
        //  ListView 排序（點擊欄位標題）
        // ───────────────────────────────────────────────

        private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 「新檔名」欄不支援排序（值會因規則而動態改變）
            if (e.Column == 1)
            {
                toolStripStatusLabel_News.Text = $"提示！無法依〔{fileListView.Columns[e.Column].Text}〕排序";
                return;
            }

            fileListView.ListViewItemSorter = _lvwColumnSorter;

            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // 點擊相同欄位：切換升序 / 降序
                _lvwColumnSorter.Order = _lvwColumnSorter.Order == SortOrder.Ascending
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }
            else
            {
                // 點擊不同欄位：切換到新欄位，預設升序
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            string orderText = _lvwColumnSorter.Order == SortOrder.Ascending ? "升序" : "降序";
            toolStripStatusLabel_News.Text =
                $"依〔{fileListView.Columns[e.Column].Text}〕排序，方式：{orderText}";

            fileListView.BeginUpdate();
            fileListView.Sort();

            // 排序後重新計算新檔名預覽（排序改變了檔案順序，規則結果也會隨之變化）
            _ruleManager?.RefreshFileList();
            fileListView.EndUpdate();
        }

        // ───────────────────────────────────────────────
        //  ListView 項目互動
        // ───────────────────────────────────────────────

        private void fileListView_DoubleClick(object sender, EventArgs e)
        {
            // 雙擊清單項目 → 用系統預設程式開啟該檔案
            if (fileListView.SelectedItems.Count == 0) return;

            var item = fileListView.SelectedItems[0];
            string filePath = Path.Combine(item.SubItems[4].Text, item.SubItems[0].Text);
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"無法開啟檔案：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem_CopyFileNameToClipboard_Click(object sender, EventArgs e)
        {
            // 右鍵選單：複製選取項目的原始檔名到剪貼簿
            if (fileListView.SelectedItems.Count == 0) return;

            try
            {
                var fileNames = fileListView.SelectedItems.Cast<ListViewItem>().Select(i => i.Text);
                string text = string.Join(Environment.NewLine, fileNames);
                if (!string.IsNullOrEmpty(text))
                {
                    Clipboard.SetText(text);
                    string preview = text.Replace(Environment.NewLine, "、");
                    toolStripStatusLabel_News.Text = $"複製檔名至剪貼簿：{preview}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"複製到剪貼簿時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ───────────────────────────────────────────────
        //  輔助工具方法
        // ───────────────────────────────────────────────

        /// <summary>
        /// 從清單前兩個檔案的名稱（不含副檔名）計算共同前綴字串，
        /// 用於新增規則時自動填入前綴文字欄位，提升使用便利性。
        /// </summary>
        private string GetCommonPrefixFromFileList()
        {
            if (fileListView.Items.Count == 0) return string.Empty;

            string first = Path.GetFileNameWithoutExtension(fileListView.Items[0].SubItems[0].Text);
            if (fileListView.Items.Count == 1) return first;

            string second = Path.GetFileNameWithoutExtension(fileListView.Items[1].SubItems[0].Text);

            int commonLength = 0;
            int minLen = Math.Min(first.Length, second.Length);
            for (int i = 0; i < minLen; i++)
            {
                if (char.ToLowerInvariant(first[i]) != char.ToLowerInvariant(second[i]))
                    break;
                commonLength++;
            }

            return commonLength > 0 ? first.Substring(0, commonLength) : first;
        }
    }
}
