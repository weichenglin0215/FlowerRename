using System;
using System.Collections.Generic;
using System.IO;

namespace FlowerRename
{
    public class FileListManagerDELETE
    {
        public IReadOnlyList<FileItem> Files => _files.AsReadOnly();
        private List<FileItem> _files = new List<FileItem>();
        
        public string[] getAllFileList()
        {
            return _files.Select(file => file.originalFileName).ToArray();
        }

        public void setAllFileList(string[] filePaths, string[] newFileNames)
        {
            Console.WriteLine("setAllFileList");
            _files.Clear();
            for (int i = 0; i < filePaths.Length; i++)
            {
                _files.Add(new FileItem(filePaths[i], newFileNames[i]));
            }
        }
        public void OpenFiles(string[] paths)
        {
            Clear();
            AddFiles(paths);
        }

        public void AddFiles(string[] paths)
        {
            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    string newFileName = fileInfo.Name; //���ѭ즳�ɦW�����s�ɦW�A�]���u�O�Ĥ@�����J�C
                    _files.Add(new FileItem(path,newFileName));
                }
            }
        }

        public void OpenDirectory(string path)
        {
            Clear();
            AddDirectory(path);
        }

        public void AddDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                string[] filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                AddFiles(filePaths);
            }
        }

        public void Clear()
        {
            _files.Clear();
        }
        public void RemoveSelectedFile(FileItem fileItem)
        {
            _files.Remove(fileItem);
        }
    } 
}