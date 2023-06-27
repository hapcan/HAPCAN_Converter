namespace HAPCAN_Converter
{
    partial class FormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBase));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.picResizer = new System.Windows.Forms.PictureBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.bnMin = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResizer)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelBottom.Controls.Add(this.picResizer);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 384);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 16);
            this.panelBottom.TabIndex = 2;
            this.panelBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseDown);
            this.panelBottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseMove);
            this.panelBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseUp);
            // 
            // picResizer
            // 
            this.picResizer.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.picResizer.Dock = System.Windows.Forms.DockStyle.Right;
            this.picResizer.Image = ((System.Drawing.Image)(resources.GetObject("picResizer.Image")));
            this.picResizer.Location = new System.Drawing.Point(784, 0);
            this.picResizer.Name = "picResizer";
            this.picResizer.Size = new System.Drawing.Size(16, 16);
            this.picResizer.TabIndex = 0;
            this.picResizer.TabStop = false;
            this.picResizer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseDown);
            this.picResizer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseMove);
            this.picResizer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseUp);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Controls.Add(this.bnMin);
            this.panelTop.Controls.Add(this.btnMax);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 35);
            this.panelTop.TabIndex = 3;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelTitle.Location = new System.Drawing.Point(10, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(29, 15);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "Title";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // bnMin
            // 
            this.bnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bnMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bnMin.BackgroundImage")));
            this.bnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.bnMin.FlatAppearance.BorderSize = 0;
            this.bnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.bnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnMin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bnMin.Location = new System.Drawing.Point(665, 0);
            this.bnMin.Name = "bnMin";
            this.bnMin.Size = new System.Drawing.Size(45, 35);
            this.bnMin.TabIndex = 1;
            this.bnMin.UseVisualStyleBackColor = false;
            this.bnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnMax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMax.BackgroundImage")));
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnMax.Location = new System.Drawing.Point(710, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(45, 35);
            this.btnMax.TabIndex = 2;
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Location = new System.Drawing.Point(755, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(200, 35);
            this.Name = "FormBase";
            this.Text = "FormBase";
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picResizer)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panelTop;
        private Button bnMin;
        private Button btnMax;
        private Button btnClose;
        private Panel panelBottom;
        private PictureBox picResizer;
        public Label labelTitle;
    }
}