using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class WinForm : Form
{
    private Button button1;
    private TextBox textBox1;

    public WinForm()
    {
    }
	
	DllImport("kernel32.dll", SetLastError=true, ExactSpelling=true)]
    static extern bool FreeConsole();
	
    static void Main(string[] args)
    {
		FreeConsole();
        WinForm myFrm = new WinForm();

        // Create Button and TextBox objects
        myFrm.button1 = new System.Windows.Forms.Button();
        myFrm.textBox1 = new System.Windows.Forms.TextBox();

        // Setting the Button control Properties
        myFrm.button1.BackColor = System.Drawing.Color.Blue;
        myFrm.button1.ForeColor = System.Drawing.Color.Yellow;
        myFrm.button1.Location = new System.Drawing.Point(24, 40);
        myFrm.button1.Name = "button1";
        myFrm.button1.Size = new System.Drawing.Size(112, 32);
        myFrm.button1.TabIndex = 0;
        myFrm.button1.Text = "Click Me";

        // The button click event handler
        myFrm.button1.Click += new
        System.EventHandler(myFrm.button1_Click);

        // Setting the TextBox control Properties
        myFrm.textBox1.Location = new System.Drawing.Point(168, 48);
        myFrm.textBox1.Name = "textBox1";
        myFrm.textBox1.Size = new System.Drawing.Size(104, 20);
        myFrm.textBox1.TabIndex = 1;
        myFrm.textBox1.Text = "textBox1";

        // Setting the form Properties
        myFrm.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        myFrm.ClientSize = new System.Drawing.Size(292, 273);
        myFrm.Controls.AddRange(new System.Windows.Forms.Control[] { myFrm.textBox1, myFrm.button1 });
        myFrm.Text = "My First Windows Application";
        myFrm.BackColor = Color.Red;
        myFrm.ForeColor = Color.Yellow;
        myFrm.ResumeLayout(false);
        Application.Run(myFrm);
    }

    // The button click event handler
    private void button1_Click(object sender, System.EventArgs e)
    {
        textBox1.Text = "Button is clicked";
    }
}
