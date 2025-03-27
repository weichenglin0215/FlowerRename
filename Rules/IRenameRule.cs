namespace FlowerRename
{
    public interface IRenameRule
    {
        string RuleName { get; }
        //bool IsExpanded { get; set; }
        //bool IsEnabled { get; set; }
        void UpdateParameters();
        //�쥻�W�����U�T�{�A��ӨS�ϥΡA�令GenerateFileName()
        string[] Apply(string[] fileNames);
        //string GetNewName(string originalName); 

        string[] GenerateFileName(string[] originalFileNames);

        //bool Validate();


        UserControl GetConfigControl();
    } 
}