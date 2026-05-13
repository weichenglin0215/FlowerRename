using System.Diagnostics;

namespace FlowerRename
{
    /// <summary>
    /// 管理所有作用中的命名規則，負責規則的新增、移除，以及套用所有規則並更新主視窗的新檔名預覽。
    /// </summary>
    public class RuleManager
    {
        // 主視窗的參照，用於讀取/更新檔案清單
        private readonly Form_FlowerRename _form;

        // 目前清單中所有檔案的完整路徑（作為規則運算的輸入）
        private string[] _originalFileNames = Array.Empty<string>();

        // 規則運算後的新檔名結果（僅含檔名，不含路徑）
        private string[] _newFileNames = Array.Empty<string>();

        /// <summary>
        /// 規則組合結構，將規則邏輯（IRenameRule）與對應的 UI 控制項（UserControl）及唯一 ID 綁定在一起。
        /// </summary>
        public struct RuleControlPair
        {
            /// <summary>規則的唯一識別碼，用於精準移除特定規則（避免同名規則誤刪）</summary>
            public int RuleID;
            /// <summary>實作 IRenameRule 的規則邏輯物件</summary>
            public IRenameRule Rule;
            /// <summary>規則對應的 UI 設定控制項</summary>
            public UserControl Control;
        }

        // 所有已加入的規則清單（依加入順序排列，規則會依序套用）
        private readonly List<RuleControlPair> _ruleControlPairs = new List<RuleControlPair>();

        /// <summary>
        /// 建構子：初始化 RuleManager，並從主視窗讀取目前的檔案清單。
        /// </summary>
        /// <param name="form">主視窗（Form_FlowerRename）的參照</param>
        public RuleManager(Form_FlowerRename form)
        {
            _form = form;
            _originalFileNames = _form.GetFileList();
        }

        // ───────────────────────────────────────────────
        //  規則的增刪查
        // ───────────────────────────────────────────────

        /// <summary>
        /// 取得目前所有規則組合的清單（唯讀用途）。
        /// </summary>
        public List<RuleControlPair> GetRuleControlPair() => _ruleControlPairs;

        /// <summary>
        /// 新增一個規則組合，並訂閱該控制項的變更事件，讓任何參數修改都能即時觸發重新計算。
        /// </summary>
        /// <param name="rule">規則邏輯物件（實作 IRenameRule）</param>
        /// <param name="ruleControl">規則對應的 UI 控制項</param>
        /// <param name="ruleID">規則的唯一 ID</param>
        public void AddRuleControlPair(IRenameRule rule, UserControl ruleControl, int ruleID)
        {
            _ruleControlPairs.Add(new RuleControlPair
            {
                Rule = rule,
                Control = ruleControl,
                RuleID = ruleID
            });

            // 根據控制項類型訂閱對應的參數變更事件
            // 每當使用者修改規則參數時，立即重新計算所有新檔名並更新預覽
            if (ruleControl is NumberingRuleControl numberingCtrl)
            {
                numberingCtrl.BaseFileNameChanged += (_, _, _, _) => RefreshFileList();
            }
            else if (ruleControl is ReplaceRuleControl replaceCtrl)
            {
                replaceCtrl.ReplaceFileNameChanged += (_, _, _, _, _) => RefreshFileList();
            }
            else if (ruleControl is InsertRuleControl insertCtrl)
            {
                insertCtrl.InsertFileNameChanged += (_, _, _, _, _, _, _, _) => RefreshFileList();
            }
            else if (ruleControl is DeleteRuleControl deleteCtrl)
            {
                deleteCtrl.DeleteFileNameChanged += (_, _, _) => RefreshFileList();
            }
            else if (ruleControl is GroupRuleControl groupCtrl)
            {
                groupCtrl.GroupFileNameChanged += (_, _, _, _, _) => RefreshFileList();
            }
        }

        /// <summary>
        /// 依規則 ID 移除對應的規則組合。
        /// </summary>
        /// <param name="ruleID">要移除的規則唯一識別碼</param>
        public void RemoveRuleControlPair(int ruleID)
        {
            Debug.WriteLine($"RemoveRuleControlPair RuleID={ruleID}，移除前共 {_ruleControlPairs.Count} 個規則");
            int removed = _ruleControlPairs.RemoveAll(pair => pair.RuleID == ruleID);
            Debug.WriteLine($"移除了 {removed} 個規則，目前共 {_ruleControlPairs.Count} 個規則");
        }

        // ───────────────────────────────────────────────
        //  原始檔名的存取
        // ───────────────────────────────────────────────

        /// <summary>
        /// 從主視窗重新讀取目前的檔案清單（通常在載入新檔案後呼叫）。
        /// </summary>
        public void GetOriginalFileNames()
        {
            _originalFileNames = _form.GetFileList();
        }

        /// <summary>
        /// 直接設定原始檔名清單（通常在更名完成或復原後呼叫，以更新基準檔名）。
        /// </summary>
        /// <param name="fileNames">新的原始檔名完整路徑陣列</param>
        public void SetOriginalFileNames(string[] fileNames)
        {
            _originalFileNames = fileNames;
        }

        // ───────────────────────────────────────────────
        //  規則套用與預覽更新
        // ───────────────────────────────────────────────

        /// <summary>
        /// 依序將所有規則套用到給定的檔名陣列，回傳經過所有規則處理後的新檔名。
        /// </summary>
        /// <param name="originalNames">原始檔名陣列（可包含完整路徑）</param>
        /// <returns>所有規則套用後的新檔名陣列</returns>
        public string[] ProcessNewFileName(string[] originalNames)
        {
            string[] results = originalNames;
            foreach (var pair in _ruleControlPairs)
            {
                if (pair.Rule != null)
                    results = pair.Rule.Apply(results);
            }
            return results;
        }

        /// <summary>
        /// 重新計算所有檔案的新檔名，並更新主視窗的 ListView 預覽。
        /// 每當任何規則參數變更時呼叫此方法。
        /// </summary>
        public void RefreshFileList()
        {
            // 先讓每個規則從 UI 控制項重新讀取最新的參數
            foreach (var pair in _ruleControlPairs)
                pair.Rule?.UpdateParameters();

            // 從主視窗取得最新的原始檔名清單（含完整路徑）
            _originalFileNames = _form.GetFileList();
            Debug.WriteLine($"RefreshFileList: 共 {_originalFileNames.Length} 個檔案");

            // 套用所有規則計算新檔名
            _newFileNames = ProcessNewFileName(_originalFileNames);

            // 僅保留檔名部分（去除路徑），再更新主視窗的預覽欄位
            _newFileNames = _newFileNames.Select(n => Path.GetFileName(n)).ToArray();
            _form.SetFileList(_newFileNames);
        }

        // 保留 RefreshForm_FlowerRename_FileList 作為 RefreshFileList 的別名，維持對舊呼叫端的相容性
        /// <summary>
        /// <see cref="RefreshFileList"/> 的別名，維持向後相容。
        /// </summary>
        public void RefreshForm_FlowerRename_FileList() => RefreshFileList();
    }
}
