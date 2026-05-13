# FlowerRename — 批次檔案更名工具

## 專案目標

FlowerRename 是以 **Windows Forms（C# / .NET 9）** 開發的批次檔案更名工具。  
目標是讓使用者能以「**規則組合**」的方式自由搭配多個命名規則，對大量檔案進行系統化的批次更名，無需手動逐一修改。

主要應用場景：
- **AI 圖片生成工作流程**：對同一 Prompt 產生的多組圖片進行有系統的分組命名，方便比對
- **相片整理**：統一命名格式、加入流水號、移除相機自動產生的雜亂前綴
- **批次清理**：移除特定文字、修正拼字、統一大小寫等

---

## 功能說明

### 載入檔案

| 操作 | 說明 |
|------|------|
| 開啟檔案 | 透過對話框選擇一或多個檔案，**清空**現有清單後重新載入 |
| 開啟資料夾 | 載入整個資料夾內的所有檔案，**清空**現有清單後重新載入 |
| 追加檔案 | 在現有清單上**追加**更多檔案（自動略過重複項目） |
| 追加資料夾 | 在現有清單上**追加**一整個資料夾的檔案（自動略過重複項目） |
| 拖放支援 | 可將檔案或資料夾直接**拖放**到視窗，自動以追加方式加入清單 |

### 命名規則

所有規則以**疊加方式**依加入順序依序執行，可同時啟用多個規則：

| 規則名稱 | 說明 |
|----------|------|
| **重新編號** | 以「前綴 + 流水號」重新命名所有檔案，可設定起始號、遞增量、補零位數 |
| **置換指定文字（亦可刪除）** | 在檔名中尋找特定文字並替換；替換文字設為空字串即等同刪除；支援忽略大小寫、全部替換、指定搜尋起始位置 |
| **添加文字（根據位置）** | 在指定位置插入文字，可從前端或後端計算位置；也支援同時插入流水號 |
| **刪除文字（根據位置）** | 從指定位置起刪除指定數量的字元，可從前端或後端計算起始位置 |
| **群組化（AI 產圖比對）** | 將檔案平均分成 N 組，組內以流水號排列，各組以 A/B/C… 字母標示，方便對應比對 |

### 其他操作功能

| 功能 | 說明 |
|------|------|
| 即時預覽 | 任何規則參數變更都會立即更新清單中的「新檔名」欄位 |
| 批次更名 | 確認預覽後一次更名所有檔案，回報成功/失敗數量 |
| 復原（Undo） | 支援多步驟復原，最多保留 50 次更名歷史 |
| 欄位排序 | 點擊清單欄位標題排序（支援數字與文字智慧排序，不支援新檔名欄） |
| 移除選取 | 在清單中選取項目後點擊「清除選取」移除（不刪除磁碟上的實際檔案） |
| 開啟檔案 | 雙擊清單中的項目，用系統預設程式開啟該檔案 |
| 複製檔名 | 右鍵選單可複製選取項目的原始檔名到剪貼簿 |

---

## 開發環境

- **IDE**：Visual Studio 2022
- **框架**：.NET 9.0（Windows 10.0.22000.0+）
- **平台**：x64
- **UI 框架**：Windows Forms

---

## 新增命名規則的流程

若要新增一種命名規則（以「大小寫轉換」為例），請依照以下步驟操作：

### 步驟一：建立規則的控制項與邏輯類別（1 個新檔案）

在 `Rules/` 資料夾中建立 `CaseRule.cs`，在同一檔案中定義兩個類別：

**1-A. `CaseRuleControl : UserControl`（部分類別，UI 側）**
```csharp
public partial class CaseRuleControl : UserControl
{
    public Form_FlowerRename? _Form_FlowerRename;
    public int RuleID;
    public event Action<string>? CaseFileNameChanged;  // 依實際參數調整

    public CaseRuleControl()
    {
        InitializeComponent();
        // 所有輸入控制項的變更都觸發事件
        someComboBox.TextChanged += (s, e) => CaseFileNameChanged?.Invoke(someComboBox.Text);
    }

    private void closeBtn_Click(object sender, EventArgs e)
    {
        CaseFileNameChanged = null;                         // 清除事件訂閱
        _Form_FlowerRename?.RemoveRuleControlPair(RuleID); // 通知主視窗移除規則
        Parent?.Controls.Remove(this);                     // 從 UI 移除自身
    }
}
```

**1-B. `CaseRule : IRenameRule`（邏輯側）**
```csharp
public class CaseRule : IRenameRule
{
    private readonly CaseRuleControl _ruleControl;
    private string _mode = string.Empty;

    public CaseRule(CaseRuleControl ruleControl)
    {
        _ruleControl = ruleControl;
        UpdateParameters();
    }

    public string RuleName => "CaseRule";

    public void UpdateParameters()
    {
        _mode = _ruleControl.someComboBox.Text;
    }

    public string[] Apply(string[] fileNames) => GenerateFileName(fileNames);

    public string[] GenerateFileName(string[] originalFileNames)
    {
        // ─── 核心命名邏輯 ───
        var newNames = new string[originalFileNames.Length];
        for (int i = 0; i < originalFileNames.Length; i++)
        {
            string nameWithoutExt = Path.GetFileNameWithoutExtension(originalFileNames[i]);
            string ext = Path.GetExtension(originalFileNames[i]);
            // 依 _mode 轉換大小寫...
            newNames[i] = nameWithoutExt + ext;
        }
        return newNames;
    }

    public UserControl GetConfigControl() => new CaseRuleControl();
}
```

### 步驟二：設計 UserControl 介面（Visual Studio 自動產生 Designer 檔）

1. 在 Visual Studio 中開啟 `CaseRule.cs`，切換到「設計介面」
2. 加入所需的輸入控制項（ComboBox、TextBox、CheckBox 等）
3. 加入一個「✕」關閉按鈕，並連結到 `closeBtn_Click`
4. VS 會自動產生對應的 `CaseRule.Designer.cs`

### 步驟三：在 `Form_FlowerRename.cs` 的 `AddRenameRule()` 加入新 case

找到 `AddRenameRule()` 方法中的 `switch` 區塊，加入：

```csharp
case "大小寫轉換":
{
    var ctrl = CreateRuleControl<CaseRuleControl>();
    var rule = new CaseRule(ctrl);
    _ruleManager.AddRuleControlPair(rule, ctrl, _nextRuleID++);
    break;
}
```

> `CreateRuleControl<T>()` 是共用輔助方法，會自動設定 RuleID、Dock、父容器，並移至最上方。

### 步驟四：在 `RuleManager.cs` 的 `AddRuleControlPair()` 加入事件訂閱

```csharp
else if (ruleControl is CaseRuleControl caseCtrl)
{
    caseCtrl.CaseFileNameChanged += (_) => RefreshFileList();
}
```

### 步驟五：在 UI 選單加入新選項

透過 Visual Studio 設計介面（或直接編輯 `Form_FlowerRename.Designer.cs`）：
- 在 `comboBoxAddRule` 的 Items 中加入 `"大小寫轉換"`
- 若使用 MenuStrip，在 `menuStripAddRule` 加入對應的 MenuItem，
  並在 `Form_FlowerRename.cs` 加入事件方法：
  ```csharp
  private void menuItemCaseRule_Click(object sender, EventArgs e) => AddRenameRule("大小寫轉換");
  ```

**整理：共需異動 2 個現有檔案 + 1 個新檔案（+1 個 Designer 自動產生）**

| 檔案 | 異動內容 |
|------|----------|
| `Rules/CaseRule.cs` | **新增**（含控制項類別與規則邏輯） |
| `Form_FlowerRename.cs` | `AddRenameRule()` 加入新 case；MenuStrip 事件方法 |
| `Managers/RuleManager.cs` | `AddRuleControlPair()` 加入新控制項的事件訂閱 |
| `Form_FlowerRename.Designer.cs` | comboBoxAddRule Items 及 MenuStrip（透過設計介面修改） |

---

## 使用者操作說明

### 基本流程

1. **啟動程式**  
   畫面只顯示「開啟檔案」和「開啟資料夾」兩個按鈕，等待載入檔案。

2. **載入檔案**  
   - 點擊「**開啟檔案**」或「**開啟資料夾**」→ 清空現有清單並載入選取的檔案
   - 或直接將檔案 / 資料夾**拖放到視窗**（以追加方式加入，不清空清單）

3. **新增命名規則**  
   - 從下拉選單「請選擇新增命名規則」中選擇規則，或透過上方 MenuStrip 點選
   - 規則控制項會堆疊在視窗上方，**可同時加入多個規則**
   - 規則依加入順序**由上至下依序套用**

4. **即時預覽**  
   - 調整任何規則參數，清單的「**新檔名**」欄會立即顯示套用所有規則後的結果
   - 確認預覽正確後再執行更名

5. **執行更名**  
   - 點擊「**執行更名**」按鈕
   - 程式回報成功/失敗數量；失敗時顯示原因

6. **復原（Undo）**  
   - 若結果不滿意，點擊「**復原 Undo (N)**」可還原上一次更名
   - N 表示目前可復原的次數（最多 50 次）

### 各規則參數說明

#### 重新編號
| 參數 | 說明 | 範例 |
|------|------|------|
| 前綴文字 | 檔名的固定前綴部分 | `IMG_`、`花卉_` |
| 起始號 | 第一個檔案的流水號 | `1` |
| 遞增量 | 每個檔案號碼的增加量 | `1`（連續）、`2`（跳號） |
| 補零位數 | 數字補零至幾位（不足補 0） | `4` → `0001` |

> 範例：前綴 `Photo_`、起始 1、遞增 1、補零 4 → `Photo_0001.jpg`、`Photo_0002.jpg`…

#### 置換指定文字（亦可刪除）
| 參數 | 說明 |
|------|------|
| 搜尋文字 | 要被替換的文字 |
| 替換為 | 替換後的文字；**留空即等同刪除** |
| 搜尋方向 | 從前面找起 / 從後面找起 |
| 起始偏移 | 從第幾個字元之後才開始搜尋 |
| 忽略大小寫 | 勾選後不區分英文大小寫 |
| 全部替換 | 勾選後替換所有符合的文字，否則只替換第一個 |

> 範例：搜尋 `(副本)`，替換為空 → 移除所有 `(副本)` 字串

#### 添加文字（根據位置）
| 參數 | 說明 |
|------|------|
| 插入文字 | 要插入的文字內容 |
| 插入位置 | 從第幾個字元之後插入（0 = 最前面） |
| 從後面找起 | 改為從檔名結尾往前計算位置 |
| 同時插入號碼 | 勾選後在插入文字後附加流水號 |
| 起始號/遞增量/補零位數 | 流水號的格式設定 |
| 號碼後綴 | 號碼後面附加的文字（如 `_`） |

#### 刪除文字（根據位置）
| 參數 | 說明 |
|------|------|
| 起始位置 | 從第幾個字元開始刪除（1 = 第一個字元） |
| 刪除字元數 | 要刪除的字元數量 |
| 從後面找起 | 改為從檔名結尾往前計算起始位置 |

> 範例：從前面第 1 個字元起刪除 4 個字元，`DSC_0001.jpg` → `0001.jpg`

#### 群組化（AI 產圖比對）
| 參數 | 說明 |
|------|------|
| 前綴文字 | 所有群組共用的前綴 |
| 起始號/遞增量/補零位數 | 組內流水號格式 |
| 群組數量 | 要分成幾組 |

> 範例：12 個檔案分 3 組，前綴 `Image`，補零 2：  
> 第 1 組：`Image01-A`、`Image02-A`、`Image03-A`、`Image04-A`  
> 第 2 組：`Image01-B`、`Image02-B`、`Image03-B`、`Image04-B`  
> 第 3 組：`Image01-C`、`Image02-C`、`Image03-C`、`Image04-C`

### 其他功能
- **點擊欄位標題**：依該欄位排序（不支援「新檔名」欄，因其值為動態計算結果）
- **雙擊清單項目**：用系統預設程式開啟該檔案
- **右鍵選單**：複製選取項目的原始檔名到剪貼簿
- **清除選取**：先在清單中選取（單選或 Ctrl+多選）項目，再點擊「清除選取」移除清單列（**不刪除磁碟上的實際檔案**）
- **清除所有**：清空整個清單，並一併清除 Undo 歷史

---

## 版本更新說明

### V0.5.0（目前版本）
- 新增「**刪除文字（根據位置）**」規則
- 新增雙擊清單項目開啟檔案功能
- 新增底部狀態列（顯示檔案總數、選取數量、操作訊息）
- 新增支援拖放多個目錄與檔案
- 新增**復原（Undo）**功能，最多保留 50 步歷史
- 修正清除選取項目時的索引錯誤

### V0.4.3
- 新增開啟選取項目（雙擊）
- 新增狀態列
- 新增拖入多個目錄與檔案
- 新增 Undo 功能
- 修正清除項目的錯誤

### V0.2.2.0
- 數字欄位改為靠右對齊，避免螢幕解析度變更後數值被遮蔽

### V0.1.x
- 修正以時間排序的跨夜判斷錯誤
- 新增應用程式圖示（.ico）
- 新增拖曳多個檔案功能
- 修正更新清單時的錯誤
- 發布單一執行檔（自包含部署，支援拖曳目錄）
- 第一次版本上傳
