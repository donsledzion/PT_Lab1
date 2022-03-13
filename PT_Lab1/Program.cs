using System;
using System.IO;
using ExtensionMethods;
using System.Linq;
using System.Collections.Generic;

namespace PT_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = args[0];
            string sortType = args[1];
            string sortOrder = args[2];
            string[] allfiles = Directory.GetDirectories(sourcePath);
            Console.WriteLine("=================================================================\n");
            Console.WriteLine(sourcePath);
            ListContent(sourcePath, sortType, sortOrder);
            Console.WriteLine("=================================================================\n");
        }

        static void ListContent(string path, string sortType, string sortOrder)
        {
            var directories = new List<string>(Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly));

            var files = new List<string>(Directory.GetFiles(path));

            if (sortType == "size")
            {
                if (sortOrder == "asc")
                    files.Sort((f1, f2) => new FileInfo(f1).Length.CompareTo(new FileInfo(f2).Length));
                else
                    files.Sort((f1, f2) => new FileInfo(f2).Length.CompareTo(new FileInfo(f1).Length));
            }
            if (sortType == "time")
            {
                if (sortOrder == "asc")
                    files.Sort((f1, f2) => File.GetLastWriteTime(f1).CompareTo(File.GetLastWriteTime(f2)));
                else
                    files.Sort((f1, f2) => File.GetLastWriteTime(f2).CompareTo(File.GetLastWriteTime(f1)));
            }
            else if (sortType == "name")
            {
                if (sortOrder == "asc")
                    files.Sort((f1, f2) => f1.CompareTo(f2));
                else
                    files.Sort((f1, f2) => f2.CompareTo(f1));
            }



            foreach (string directory in directories)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                Console.WriteLine(Indentation(directory) + "-" + Path.GetFileName(directory) + "[" + dirInfo.ContentCount() + "]");

                ListContent(directory, sortType, sortOrder);
            }


            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.WriteLine(Indentation(file) + Path.GetFileName(file) + "\t|| " + fileInfo.FormattedAttributes());
            }
        }

        static string Indentation(string path)
        {
            string prefix = "";
            for (int i = 0; i < Path.GetFullPath(path).Split("\\").Length - 2; i++)
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
            string fileInfo = "Modified: " + fi.LastWriteTime.ToString() + " | Size: " + BytesFormat((int)fi.Length) + " | Attribute: " + fi.Attributes.ToString();

            return fileInfo;
        }

        public static string ContentCount(this DirectoryInfo info)
        {
            return (info.GetFiles().Length + info.GetDirectories().Length).ToString();
        }
        static string BytesFormat(int bytes)
        {
            string result = bytes.ToString();

            if (result.Length > 12) return ((float)bytes / 1000000000).ToString("0.00") + " TB";
            else if (result.Length > 9) return ((float)bytes / 1000000000).ToString("0.00") + " GB";
            else if (result.Length > 6) return ((float)bytes / 1000000).ToString("0.00") + " MB";
            else if (result.Length > 3) return ((float)bytes / 1000).ToString("0.00") + " KB";

            return result + "B";
        }
    }
}
