using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CompL1
{
    public partial class Form1 : Form
    {
        Stream fileStream;
        public string filePath;
        bool isEdited = false;

        string lexemes = @"\b(int|char|string)";

        public Form1()
        {
            this.AllowDrop = true;

            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            InitializeComponent();
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }

            richTextBox1.Text = string.Empty;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            richTextBox1.Text = fileContent;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath != null)
                File.WriteAllText(filePath, richTextBox1.Text);
            else
                SaveAsToolStripMenuItem_Click(sender, e);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    var fileStream = saveFileDialog.OpenFile();

                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.Write(richTextBox1.Text);
                    }
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveForm saveForm = new SaveForm(this);
            if (isEdited)
                Close();
            else
                saveForm.Show();

        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void RunToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var HelpForm = new HelpForm();
            HelpForm.Show();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var InfoForm = new InfoForm();
            InfoForm.Show();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.SelectedText = "";
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.SelectAll();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            parser.state = State.STATE_INIT;
            string expr = richTextBox1.Text;
            List<Token> tokens = lexer.tokenize(expr);
            ParseResult error;
            int error_count = 0;
            richTextBox2.Text = "";
            foreach (var token in tokens)
            {
                error = parser.parse(token);
                if (error.is_error)
                {
                    richTextBox2.Text += error.Stringize(expr);
                    error_count++;
                }
            }
            richTextBox2.Text += "\n¬сего ошибок: " + error_count;
            parser = new Parser();
            parser.state = State.STATE_INIT;
            richTextBox4.Text = "";
            foreach (var token in tokens) // Ќейтрализаци€
            {
                error = parser.parse(token);
                if (!error.is_error)
                    richTextBox4.Text += error.actualValue() + " ";
            }
        }


        //private void richTextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    MatchCollection lexeme_matches = Regex.Matches(richTextBox1.Text, lexemes);
        //    int st = richTextBox1.SelectionStart, end = richTextBox1.SelectionLength;
        //    Color orig = Color.Black;

        //    richTextBox2.Text = Lexer.tokenize(richTextBox1.Text);
        //    richTextBox4.Text = Lexer.QuickFixErrors(richTextBox1.Text);

        //    richTextBox1.SelectionStart = 0;
        //    richTextBox1.SelectionLength = richTextBox1.Text.Length;
        //    richTextBox1.SelectionColor = orig;

        //    richTextBox2.Focus();
        //    foreach (Match match in lexeme_matches)
        //    {
        //        richTextBox1.SelectionStart = match.Index;
        //        richTextBox1.SelectionLength = match.Length;
        //        richTextBox1.SelectionColor = Color.Red;
        //    }
        //    richTextBox1.Focus();
        //    richTextBox1.SelectionStart = st;
        //    richTextBox1.SelectionLength = end;
        //    richTextBox1.SelectionColor = orig;

        //    string[] splittedLines = richTextBox1.Text.Split(new string[] { "\r", "\n", "\r\n" }
        //    , StringSplitOptions.None);
        //    int linecount = splittedLines.Length;

        //    if (linecount != 0)
        //    {
        //        richTextBox3.Clear();
        //        for (int i = 1; i < linecount + 1; i++)
        //        {
        //            richTextBox3.AppendText(Convert.ToString(i) + "\n");
        //        }
        //    }
        //}



        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string File in FileList)
                this.richTextBox1.Text += File + "\n";
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void р¬1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(richTextBox1.Text, "^[0-1][0-9]:[0-5][0-9]:[0-5][0-9]$")|| Regex.IsMatch(richTextBox1.Text, "^[2][0-3]:[0-5][0-9]:[0-5][0-9]$"))
                richTextBox2.Text = "строка " + richTextBox1.Text + " соответсвует регул€рному выражению 1}";
            else
                richTextBox2.Text = "строка " + richTextBox1.Text + " не соответсвует регул€рному выражению 1}";

        }

        private void р¬2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(richTextBox1.Text, "^[0-9]{12}$") || Regex.IsMatch(richTextBox1.Text, "^[0-9]{10}$"))
                richTextBox2.Text = "строка " + richTextBox1.Text + " соответсвует регул€рному выражению 2";
            else
                richTextBox2.Text = "строка " + richTextBox1.Text + " не соответсвует регул€рному выражению 2";
        }

        private void р¬3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(richTextBox1.Text, "^([0-9]{1,3}.){3}[0-9]{1,3}:([0-9]{1,5})?$"))
                richTextBox2.Text = "строка " + richTextBox1.Text + " соответсвует регул€рному выражению 3";
            else
                richTextBox2.Text = "строка " + richTextBox1.Text + " не соответсвует регул€рному выражению 3";
        }
    }
}
