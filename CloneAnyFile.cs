using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CloneAnyFile
{
    public partial class Form1 : Form
    {
		public Button button1;
        public Form1()
        {
            button1 = new Button();
            button1.Size = new Size(100, 50);
            button1.Location = new Point(30, 30);
            button1.Text = "Open File Dialog";
            this.Controls.Add(button1);
            button1.Click += new EventHandler(button1_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
			string ScriptPath = Application.ExecutablePath;
			string ScriptDirectory = Path.GetDirectoryName(Application.ExecutablePath);;
			// MessageBox.Show(ScriptDirectory, ScriptPath);
			OpenFileDialog dlg = new OpenFileDialog();
			// dlg.InitialDirectory = Environment.ExpandEnvironmentVariables(@"%UserProfile%\Desktop\");
			dlg.InitialDirectory = @"C:\Webroot\Apache2433\htdocs";
			dlg.Filter = "PHP (*.php;*.phtml;*.twig,*.tpl,*.ctp)|*.php;*.phtml;*.twig;*.tpl;*.ctp|"
						+ "HTML (*.html;*.htm;*.twig,*.tpl,*.ctp)|*.html;*.htm;*.twig;*.tpl;*.ctp";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string origFileName = dlg.FileName;
				string origFileNameWithNoExt = Path.GetFileNameWithoutExtension(origFileName);
				string origFileExt = Path.GetExtension(origFileName);
				// string timestampSuffix = string.Format("{0:yyyy-MM-dd_hh-mm-ss-fff}",DateTime.Now);
				string timestampSuffix = string.Format("{0:yyyy-MM-dd_hh-mm-ss}",DateTime.Now);
				string cloneFileName = origFileNameWithNoExt + '_' + timestampSuffix + origFileExt + "-clone";
                string destFile = Path.Combine(Path.GetDirectoryName(origFileName), Path.GetFileName(cloneFileName));
				File.Copy(dlg.FileName, destFile, true);
				MessageBox.Show("File cloning was successful", "File Clone Status");
				Process.Start("explorer.exe", Path.GetDirectoryName(origFileName));
            }
			System.Windows.Forms.Application.Exit();
        }
		[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
