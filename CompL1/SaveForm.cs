using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompL1
{
    public partial class SaveForm : Form
    {
        MainForm form1;
        string filePath1;

        public SaveForm(MainForm owner)
        {
            InitializeComponent();
            form1 = owner;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            filePath1 = form1.filePath;

            if (filePath1 != null)
                File.WriteAllText(filePath1, form1.richTextBox1.Text);
            else
            {
                var filePath1 = string.Empty;

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "c:\\";
                    saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath1 = saveFileDialog.FileName;
                        var fileStream = saveFileDialog.OpenFile();

                        using (StreamWriter writer = new StreamWriter(fileStream))
                        {
                            writer.Write(form1.richTextBox1.Text);
                        }
                    }
                }
            }
            this.Close();
            form1.Close();
        }

        private void buttonNotSave_Click(object sender, EventArgs e)
        {
            this.Close();
            form1.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
