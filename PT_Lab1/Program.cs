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
                Console.WriteLine(Indentation(file) + Path.GetFileName(file) + " | " + fileInfo.FormattedAttributes());
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
                return info.Attributes.ToString();
            }

            public static string ContentCount(this DirectoryInfo info)
            {
                return info.GetFiles().Length.ToString();
            }
        }
    }
