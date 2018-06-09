using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CleanupJunkTempFiles2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Program P2 = new Program();
                string chosenPath = P2.GetChosenPath();
                MessageBox.Show(chosenPath);
                DirectoryInfo directory = new DirectoryInfo(chosenPath);
                Program P1 = new Program();
                P1.EmptyDirectory(directory);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed press enter to close console: {0}", e.Message);
                Console.ReadLine();
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        private string GetChosenPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Path Select Dialog";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                return fbd.SelectedPath.ToString();
            }
            return "";
        }

        private void EmptyDirectory(DirectoryInfo directory)
        {
            string[] files = Directory.GetFiles(directory.ToString(), "*.js");
            string subdirPath = "";

            foreach (string file in files)
            {
                File.Delete(file);
            }

            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                subdirPath = directory.ToString() + "\\" + subdirectory.ToString();

                DirectoryInfo d2 = new DirectoryInfo(subdirPath);
                EmptyDirectory(d2);
            }
        }
    }
}
