namespace FlowerRename
{
    public interface IRenameRule
    {
        string RuleName { get; }
        //bool IsExpanded { get; set; }
        //bool IsEnabled { get; set; }
        void UpdateParameters();
        // 主要用於重新命名檔案，不需要參數，請使用GenerateFileName()
        string[] Apply(string[] fileNames);
        //string GetNewName(string originalName); 

        string[] GenerateFileName(string[] originalFileNames);

        //bool Validate();


        UserControl GetConfigControl();
    } 
}