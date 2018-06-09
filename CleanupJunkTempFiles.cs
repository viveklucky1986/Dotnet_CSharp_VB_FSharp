using System;
using System.IO;

namespace CleanUpJunkTempFiles
{
    class Program
    {
        private static string targetPath = @"C:\Webroot\Apache2433\htdocs\Cakelearn35 - Copy";

        static void Main(string[] args)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show(Program.targetPath);
                // Environment.Exit(-1);
                DirectoryInfo directory = new DirectoryInfo(Program.targetPath);
                Program P1 = new Program();
                P1.EmptyDirectory(directory);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed press enter to close console: {0}", e.Message);
                Console.ReadLine();
            }
        }

        private void EmptyDirectory(DirectoryInfo directory)
        {
            string[] files = Directory.GetFiles(directory.ToString(), "*.php");
            string subdirPath = "";

            foreach (string file in files)
            {
                File.Delete(file);
            }

            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                subdirPath = directory.ToString() + "\\" + subdirectory.ToString();

                DirectoryInfo d2 = new DirectoryInfo(subdirPath);
                // System.Windows.Forms.MessageBox.Show(d2.ToString());
                EmptyDirectory(d2);
            }
        }
    }
}
