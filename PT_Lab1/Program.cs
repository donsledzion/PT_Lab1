using System;
using System.IO;

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
                Console.WriteLine(Indentation(directory) + "-" +Path.GetFileName(directory));
                ListContent(directory);
            }
            foreach(string file in Directory.GetFiles(path))
            {
                Console.WriteLine(Indentation(file) + Path.GetFileName(file));
                //ListContent(file);
            }
        }

        static string Indentation(string path)
        {
            string prefix = "";

            for(int i = 0; i < Path.GetFullPath(path).Split("\\").Length -2; i++)
            {
                prefix += "    ";
            }

            return prefix;
        }
    }
}
