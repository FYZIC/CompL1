namespace CompL1
{
    partial class SaveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            label1 = new Label();
            buttonNotSave = new Button();
            buttonSave = new Button();
            buttonClose = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(98, 36);
            label1.Name = "label1";
            label1.Size = new Size(243, 20);
            label1.TabIndex = 0;
            label1.Text = "Вы хотите сохранить изменения?";
            // 
            // buttonNotSave
            // 
            buttonNotSave.Location = new Point(230, 81);
            buttonNotSave.Name = "buttonNotSave";
            buttonNotSave.Size = new Size(111, 29);
            buttonNotSave.TabIndex = 1;
            buttonNotSave.Text = "Не сохранять";
            buttonNotSave.UseVisualStyleBackColor = true;
            buttonNotSave.Click += buttonNotSave_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(98, 81);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(111, 29);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(360, 81);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(111, 29);
            buttonClose.TabIndex = 3;
            buttonClose.Text = "Отмена";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // label2
            // 
            label2.Image = (Image)resources.GetObject("label2.Image");
            label2.Location = new Point(16, 9);
            label2.Name = "label2";
            label2.Size = new Size(76, 69);
            label2.TabIndex = 4;
            // 
            // SaveForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 124);
            Controls.Add(label2);
            Controls.Add(buttonClose);
            Controls.Add(buttonSave);
            Controls.Add(buttonNotSave);
            Controls.Add(label1);
            MaximumSize = new Size(532, 171);
            MinimumSize = new Size(532, 171);
            Name = "SaveForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Compilier";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button buttonNotSave;
        private Button buttonSave;
        private Button buttonClose;
        private Label label2;
    }
}