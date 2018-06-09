using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CleanupJunkTempFiles3
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Program P2 = new Program();
                FreeConsole();
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
            // fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.RootFolder = Environment.SpecialFolder.UserProfile;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                return fbd.SelectedPath.ToString();
            }
            return "Invalid or No path chosen";
        }

        private void EmptyDirectory(DirectoryInfo directory)
        {
            string[] PHPTmpFiles = Directory.GetFiles(directory.ToString(), "*.php-tmp");
            string[] GitMergeOrigFiles = Directory.GetFiles(directory.ToString(), "*.orig");

            List<string> list = new List<string>();
            list.AddRange(PHPTmpFiles);
            list.AddRange(GitMergeOrigFiles);
            string[] AllFiles = list.ToArray();

            string toDisplay = string.Join(Environment.NewLine, AllFiles);
            MessageBox.Show(toDisplay);

            string subdirPath = "";

            foreach (string file in AllFiles)
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
