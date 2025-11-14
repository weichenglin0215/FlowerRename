namespace FlowerRename
{
    public class FileItem
    {
        public string originalFileName { get; set; }
        public string newFileName { get; set; }
        public long fileSize { get; set; }
        public DateTime fileDate { get; set; }
        public string fileDirectory { get; set; }
        public FileItem(string filePath, string _newFileName)
        {
            var fileInfo = new FileInfo(filePath);
            originalFileName = fileInfo.Name;
            if (_newFileName == "")
            {
                newFileName = originalFileName; // 如果新檔名為空，則新檔名與原始檔名相同
            }
            else
            {
                newFileName = _newFileName;
            }
            fileSize = fileInfo.Length;
            fileDate = fileInfo.LastWriteTime;
            fileDirectory = fileInfo.DirectoryName;
        }
    } 
}