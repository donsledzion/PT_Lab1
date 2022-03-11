using System;
using System.IO;
using ExtensionMethods;

namespace PT_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = args[0];
            string[] allfiles = Directory.GetDirectories(sourcePath);
            Console.WriteLine("=================================================================\n");
            Console.WriteLine(sourcePath);
            ListContent(sourcePath);
            Console.WriteLine("=================================================================\n");
        }

        static void ListContent(string path)
        {
            foreach(string directory in Directory.GetDirectories(path,"*",SearchOption.TopDirectoryOnly))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);                
                Console.WriteLine(Indentation(directory) + "-" + Path.GetFileName(directory) + "[" + dirInfo.ContentCount() + "]" );
                
                ListContent(directory);
            }
            foreach(string file in Directory.GetFiles(path))
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.WriteLine(Indentation(file) + Path.GetFileName(file) + "\t|| " + fileInfo.FormattedAttributes());
            }
        }

        static string Indentation(string path)
        {
            string prefix = "";
            for(int i = 0; i < Path.GetFullPath(path).Split("\\").Length -2; i++)
                prefix += "    ";
            return prefix;
        }
    }
}
namespace ExtensionMethods
{
    public static class CustomExtenstions
    {
        public static string FormattedAttributes(this FileSystemInfo info)
        {
        FileInfo fi = info as FileInfo;
        string fileInfo = "Modified: " +fi.LastWriteTime.ToString() + " | Size: " + BytesFormat((int)fi.Length);
                

            return fileInfo;
        }

        public static string ContentCount(this DirectoryInfo info)
        {
            return info.GetFiles().Length.ToString();
        }
        static string BytesFormat(int bytes)
        {
            string result = bytes.ToString();

            if (result.Length > 12) return ((float)bytes / 1000000000).ToString("0.00") + " TB";
            else if (result.Length > 9) return ((float)bytes / 1000000000).ToString("0.00") + " GB";
            else if (result.Length > 6) return ((float)bytes/1000000).ToString("0.00") + " MB";
            else if (result.Length > 3) return ((float)bytes / 1000).ToString("0.00") + " KB";

            return result + "B";
        }
    }
}
