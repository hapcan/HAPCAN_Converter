namespace HAPCAN_Converter
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            buttonConvert = new Button();
            buttonOpenFile = new Button();
            richTextBox1 = new RichTextBox();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonConvert
            // 
            buttonConvert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonConvert.BackColor = Color.FromArgb(65, 65, 65);
            buttonConvert.Enabled = false;
            buttonConvert.FlatAppearance.BorderSize = 0;
            buttonConvert.FlatStyle = FlatStyle.Flat;
            buttonConvert.ForeColor = SystemColors.Window;
            buttonConvert.Image = (Image)resources.GetObject("buttonConvert.Image");
            buttonConvert.ImageAlign = ContentAlignment.MiddleLeft;
            buttonConvert.Location = new Point(690, 41);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Padding = new Padding(10);
            buttonConvert.Size = new Size(200, 50);
            buttonConvert.TabIndex = 7;
            buttonConvert.Text = "Convert";
            buttonConvert.UseVisualStyleBackColor = false;
            buttonConvert.Click += buttonConvert_Click;
            // 
            // buttonOpenFile
            // 
            buttonOpenFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOpenFile.BackColor = Color.FromArgb(65, 65, 65);
            buttonOpenFile.FlatAppearance.BorderSize = 0;
            buttonOpenFile.FlatStyle = FlatStyle.Flat;
            buttonOpenFile.ForeColor = SystemColors.Window;
            buttonOpenFile.Image = (Image)resources.GetObject("buttonOpenFile.Image");
            buttonOpenFile.ImageAlign = ContentAlignment.MiddleLeft;
            buttonOpenFile.Location = new Point(484, 41);
            buttonOpenFile.Name = "buttonOpenFile";
            buttonOpenFile.Padding = new Padding(10);
            buttonOpenFile.Size = new Size(200, 50);
            buttonOpenFile.TabIndex = 8;
            buttonOpenFile.Text = "Open File";
            buttonOpenFile.UseVisualStyleBackColor = false;
            buttonOpenFile.Click += buttonOpenFile_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.FromArgb(25, 25, 25);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.ForeColor = SystemColors.Control;
            richTextBox1.Location = new Point(10, 101);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(880, 369);
            richTextBox1.TabIndex = 9;
            richTextBox1.Text = "";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(24, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(256, 75);
            label2.Name = "label2";
            label2.Size = new Size(105, 14);
            label2.TabIndex = 10;
            label2.Text = "*.hex -> *.haf";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(208, 47);
            label1.Name = "label1";
            label1.Size = new Size(223, 15);
            label1.TabIndex = 11;
            label1.Text = "UNIV 3 && UNIV 4 Firmware File Converter";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(12, 477);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 11;
            label3.Text = "No open file";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(900, 500);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(buttonConvert);
            Controls.Add(buttonOpenFile);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form";
            Controls.SetChildIndex(buttonOpenFile, 0);
            Controls.SetChildIndex(buttonConvert, 0);
            Controls.SetChildIndex(richTextBox1, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonConvert;
        private RichTextBox richTextBox1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private Label label3;
        private Button buttonOpenFile;
    }
}