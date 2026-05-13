namespace FlowerRename
{
    /// <summary>
    /// 應用程式進入點。初始化 Windows Forms 執行環境後啟動主視窗。
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 套用高 DPI 與預設字型等應用程式組態
            ApplicationConfiguration.Initialize();
            Application.Run(new Form_FlowerRename());
        }
    }
}
