using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menus
{
    public partial class Form1 : Form
    {
        public string Copy { get; set; }
        public Form1()
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap(toolStripButton2.Width, toolStripButton2.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Black);
            toolStripButton2.Image = bitmap;


            toolStripStatusLabel1.Text = "Symbol : 0";
            toolStripStatusLabel2.Text = "Line : 0";
            toolStripStatusLabel3.Text = "Zoom : " + richTextBox1.ZoomFactor * 100;
        }

        private void one3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            print.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            var result = color.ShowDialog();

            if (result == DialogResult.OK)
            {
                richTextBox1.SelectionColor = color.Color;

                Bitmap bitmap = new Bitmap(toolStripButton2.Width,toolStripButton2.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(color.Color);
                toolStripButton2.Image = bitmap;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            var result = font.ShowDialog();

            if (result == DialogResult.OK)
            {
                richTextBox1.SelectionFont = font.Font;
                toolStripButton1.Text = font.Font.Name;
                toolStripButton1.Font = new Font(font.Font.FontFamily,14,font.Font.Style);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = $"Symbol : {richTextBox1.Text.Length}";

            toolStripStatusLabel2.Text = $"Line : {richTextBox1.Text.Count(x=>x=='\n')+1}";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();
            save.Filter = "Text file (*.txt |*.txt|RTF files (*.rtf)|*.rtf";
            var result = save.ShowDialog();

            if (result == DialogResult.OK)
            {
                string text = "";

                switch (Path.GetExtension(save.FileName))
                {
                    case ".txt":
                        text = richTextBox1.Text;
                        break;
                    case ".rtf":
                        text = richTextBox1.Rtf;
                        break;
                        default:break;
                }

                File.WriteAllText(save.FileName,text);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to save data?", "Notebook", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Close();

            }else if (result == DialogResult.No)
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Close();
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); 
            form1.Show(); 
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            richTextBox1.SelectAll();
            Copy = richTextBox1.SelectedText;
            //MessageBox.Show(Copy.Length.ToString());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText !=null)
            {
                string del = richTextBox1.SelectedText;
                int i = richTextBox1.SelectionStart;
                richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.IndexOf(del), del.Length);
                richTextBox1.SelectionStart = i;
            }
        }

        private void increaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float zoom = richTextBox1.ZoomFactor;
            richTextBox1.ZoomFactor = zoom * 2;
        }

        private void decreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float zoom = richTextBox1.ZoomFactor;
            richTextBox1.ZoomFactor = zoom / 2;
        }

        private void restoreDefaultScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor = 1;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            float zoom = richTextBox1.ZoomFactor;

            //if ((zoom * 2 < 64) && (zoom/2>0.015625))
            //{
            //    if (e.KeyCode == Keys.Add && e.Control)
            //    {
            //        MessageBox.Show("UPS");
            //        richTextBox1.ZoomFactor = zoom * 2;
            //    }

            //    if (e.KeyCode == Keys.Subtract && e.Control)
            //    {
            //        MessageBox.Show("UP2");
            //        richTextBox1.ZoomFactor = zoom / 2;
            //    }
            //}

            toolStripStatusLabel3.Text = "Zoom : " + richTextBox1.ZoomFactor * 100 + "%";
        }

        private void cutOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null)
            {
                int i = richTextBox1.SelectionStart;
                Copy = richTextBox1.SelectedText;
                richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.IndexOf(Copy),Copy.Length);
                richTextBox1.SelectionStart = i;
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Insert(i, Copy);
            richTextBox1.SelectionStart = i + Copy.Length;
        }
    }
}
