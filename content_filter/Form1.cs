using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace content_filter
{
    public partial class Form1 : Form
    {
        public string filename = null;
        public string regex = null;
        public string newFile = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "UsageLog & DebugLog & text files (*.zar, *.log, *.txt)|*.zar; *.log; *.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((filename = openFileDialog.FileName) != null)
                {
                    textBox1.Text = filename;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please select a file for filter.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please input the key word to filter.");
                    return;
                }
            }

            regex = textBox2.Text;
            newFile = Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + "_Result.txt";
            //File.Create(newFile + "\\Result.txt");
            StreamWriter sw = new StreamWriter(newFile);

            foreach (string textLine in File.ReadLines(filename))
            {
                if (textLine.ToLower().Contains(regex.ToLower())) //soppot for upcase/lowcase 
                {
                    sw.WriteLine(textLine);
                }
            }
            sw.Close();
            MessageBox.Show("Success!");
        }

        //add tool tip for some contorls
        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.button3, "Open the result file");
            toolTip1.SetToolTip(this.button1, "Select a file");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(newFile))
                MessageBox.Show("Result file dose not exist, please get the result first.");
            else
            System.Diagnostics.Process.Start(newFile);
        }
    }
}



    


