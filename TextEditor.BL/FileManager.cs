using System.IO;
using System.Text;

namespace TextEditor.BL
{
    public interface IFileManager<in T>
    {
        string GetContent(T filePath, Encoding encoding);
        string GetContent(T filePath);
        void SaveContent(string content, T filePath, Encoding encoding);
        void SaveContent(string content, T filePath);
        int GetSymbolCount(string content);
        bool IsExist(string filePath);
    }

    public class FileManager : IFileManager<string>
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        public bool IsExist(string filePath) 
        {
            bool isExist = File.Exists(filePath);
            return isExist;
        }

        public string GetContent(string filePath, Encoding encoding)
        {
            string content = File.ReadAllText(filePath, encoding);
            return content;
        }

        public string GetContent(string filePath)
        {
            return GetContent(filePath, _defaultEncoding);
        }

        public void SaveContent(string content, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, content, encoding);
        }

        public void SaveContent(string content, string filePath)
        {
            SaveContent(content, filePath, _defaultEncoding);
        }

        public int GetSymbolCount(string content) 
        {
            int count = content.Length;
            return count;
        }
    }
}
