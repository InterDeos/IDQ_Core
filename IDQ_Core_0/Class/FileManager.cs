using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IDQ_Core_0.Class.MessageService;

namespace IDQ_Core_0.Class
{
    public static class FileManager
    {
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
        public static bool IsExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string ReadTextFromFile(string filePath) { return ReadTextFromFile(filePath, Encoding.GetEncoding(1251)); }
        public static void WriteTextInFile(string text, string filePath) { WriteTextInFile(text, filePath, Encoding.GetEncoding(1251)); }
        public static void AppendTextInFile(string text, string filePath) { AppendTextInFile(text, filePath, Encoding.GetEncoding(1251)); }

        public static string[] ReadLinesFromFile(string filePath) { return ReadLinesFromFile(filePath, Encoding.GetEncoding(1251)); }
        public static void WriteLinesInFile(string[] lines, string filePath) { WriteLinesInFile(lines, filePath, Encoding.GetEncoding(1251)); }
        public static void AppendLinesInFile(IEnumerable<string> lines, string filePath) { AppendLinesInFile(lines, filePath, Encoding.GetEncoding(1251)); }

        public static string ReadTextFromFile(string filePath, Encoding encoding)
        {
            if (IsExist(filePath))
            {
                return File.ReadAllText(filePath, encoding);
            }
            else
            {
                WinFormMessageService.ShowError(string.Format("File: {0}\nIS NOT EXIST!!", filePath));
                return default(string);
            }
        }
        public static void WriteTextInFile(string text, string filePath, Encoding encoding)
        {
            if(filePath.Length == 0) { WinFormMessageService.ShowError("Path is void"); }
            else
            {
                List<string> temp = filePath.Split('/').ToList();
                if(temp.Count == 1)
                {
                    File.WriteAllText(filePath, text, encoding);
                }
                else
                {
                    string directory = "";
                    for(int i = 0; i < temp.Count - 1; i++)
                    {
                        directory += temp[i] + "/";
                    }
                    CreateDirectory(directory);
                    File.WriteAllText(filePath, text, encoding);
                }
            }
        }
        public static void AppendTextInFile(string text, string filePath, Encoding encoding)
        {
            if (filePath.Length == 0) { WinFormMessageService.ShowError("Path is void"); }
            else
            {
                List<string> temp = filePath.Split('/').ToList();
                if (temp.Count == 1)
                {
                    File.AppendAllText(filePath, text, encoding);
                }
                else
                {
                    string directory = "";
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        directory += temp[i] + "/";
                    }
                    CreateDirectory(directory);
                    File.AppendAllText(filePath, text, encoding);
                }
            }
        }

        public static string[] ReadLinesFromFile(string filePath, Encoding encoding)
        {
            if (IsExist(filePath))
            {
                return File.ReadAllLines(filePath, encoding);
            }
            else
            {
                WinFormMessageService.ShowError(string.Format("File: {0}\nIS NOT EXIST!!", filePath));
                return null;
            }
        }
        public static void WriteLinesInFile(string[] lines, string filePath, Encoding encoding)
        {
            if (filePath.Length == 0) { WinFormMessageService.ShowError("Path is void"); }
            else
            {
                List<string> temp = filePath.Split('/').ToList();
                if (temp.Count == 1)
                {
                    File.WriteAllLines(filePath, lines, encoding);
                }
                else
                {
                    string directory = "";
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        directory += temp[i] + "/";
                    }
                    CreateDirectory(directory);
                    File.WriteAllLines(filePath, lines, encoding);
                }
            }
        }
        public static void AppendLinesInFile(IEnumerable<string> lines, string filePath, Encoding encoding)
        {
            if (filePath.Length == 0) { WinFormMessageService.ShowError("Path is void"); }
            else
            {
                List<string> temp = filePath.Split('/').ToList();
                if (temp.Count == 1)
                {
                    File.AppendAllLines(filePath, lines, encoding);
                }
                else
                {
                    string directory = "";
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        directory += temp[i] + "/";
                    }
                    CreateDirectory(directory);
                    File.AppendAllLines(filePath, lines, encoding);
                }
            }
        }
    }
}
