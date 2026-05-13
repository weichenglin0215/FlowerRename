namespace FlowerRename
{
    /// <summary>
    /// 所有命名規則的共同介面。
    /// 每個規則必須實作：識別名稱、參數更新、套用邏輯，以及回傳設定控制項。
    ///
    /// 新增規則時，請同時建立：
    ///   1. XxxRuleControl（UserControl）── UI 設定介面
    ///   2. XxxRule（: IRenameRule）       ── 命名邏輯實作
    /// </summary>
    public interface IRenameRule
    {
        /// <summary>規則的顯示名稱，用於識別與除錯</summary>
        string RuleName { get; }

        /// <summary>
        /// 從 UI 控制項重新讀取最新的參數設定。
        /// RuleManager 在每次重新計算新檔名前會呼叫此方法，確保使用最新值。
        /// </summary>
        void UpdateParameters();

        /// <summary>
        /// 將規則套用到給定的檔名陣列，回傳處理後的新檔名陣列。
        /// 通常直接呼叫 GenerateFileName(fileNames)。
        /// </summary>
        /// <param name="fileNames">輸入的檔名陣列（可包含完整路徑）</param>
        /// <returns>套用此規則後的新檔名陣列</returns>
        string[] Apply(string[] fileNames);

        /// <summary>
        /// 核心命名邏輯：依規則計算每個檔案的新檔名。
        /// Apply() 的實際執行者，可被外部直接呼叫以取得預覽結果。
        /// </summary>
        /// <param name="originalFileNames">原始檔名陣列（可包含完整路徑）</param>
        /// <returns>計算後的新檔名陣列</returns>
        string[] GenerateFileName(string[] originalFileNames);

        /// <summary>
        /// 回傳此規則的 UI 設定控制項實例（通常為 XxxRuleControl 的新實例）。
        /// </summary>
        UserControl GetConfigControl();
    }
}
