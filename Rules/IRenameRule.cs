namespace FlowerRename
{
    public interface IRenameRule
    {
        string RuleName { get; }
        //bool IsExpanded { get; set; }
        //bool IsEnabled { get; set; }
        void UpdateParameters();
        //原本規劃按下確認，後來沒使用，改成GenerateFileName()
        string[] Apply(string[] fileNames);
        //string GetNewName(string originalName); 

        string[] GenerateFileName(string[] originalFileNames);

        //bool Validate();


        UserControl GetConfigControl();
    } 
}