namespace GT_KyleHyde
{
    partial class FormGT
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
            this.listFiles = new System.Windows.Forms.ListBox();
            this.btnLoadAuto = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenOldInterface = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listFiles
            // 
            this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFiles.FormattingEnabled = true;
            this.listFiles.Location = new System.Drawing.Point(12, 40);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(260, 186);
            this.listFiles.TabIndex = 53;
            // 
            // btnLoadAuto
            // 
            this.btnLoadAuto.Location = new System.Drawing.Point(12, 11);
            this.btnLoadAuto.Name = "btnLoadAuto";
            this.btnLoadAuto.Size = new System.Drawing.Size(260, 23);
            this.btnLoadAuto.TabIndex = 54;
            this.btnLoadAuto.Text = "Load Auto";
            this.btnLoadAuto.UseVisualStyleBackColor = true;
            this.btnLoadAuto.Click += new System.EventHandler(this.btnLoadAuto_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenOldInterface
            // 
            this.btnOpenOldInterface.Location = new System.Drawing.Point(12, 232);
            this.btnOpenOldInterface.Name = "btnOpenOldInterface";
            this.btnOpenOldInterface.Size = new System.Drawing.Size(260, 23);
            this.btnOpenOldInterface.TabIndex = 55;
            this.btnOpenOldInterface.Text = "Open Old Interface";
            this.btnOpenOldInterface.UseVisualStyleBackColor = true;
            this.btnOpenOldInterface.Click += new System.EventHandler(this.btnOpenOldInterface_Click);
            // 
            // FormGT
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnOpenOldInterface);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.btnLoadAuto);
            this.Name = "FormGT";
            this.Text = "GT-KyleHyde";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormGT_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormGT_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Button btnLoadAuto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenOldInterface;
    }
}