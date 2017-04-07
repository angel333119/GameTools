namespace GameTools2 {
    partial class FormGameTools2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listFiles = new System.Windows.Forms.ListBox();
            this.btnLoadAuto = new System.Windows.Forms.Button();
            this.cmbGameSelect = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadDir = new System.Windows.Forms.Button();
            this.btnMassConvert = new System.Windows.Forms.Button();
            this.btnMassConvertTextures = new System.Windows.Forms.Button();
            this.chkLoadSingleExport = new System.Windows.Forms.CheckBox();
            this.chkGTFSView = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listFiles
            // 
            this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFiles.FormattingEnabled = true;
            this.listFiles.Location = new System.Drawing.Point(12, 133);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(299, 251);
            this.listFiles.TabIndex = 51;
            // 
            // btnLoadAuto
            // 
            this.btnLoadAuto.Location = new System.Drawing.Point(12, 68);
            this.btnLoadAuto.Name = "btnLoadAuto";
            this.btnLoadAuto.Size = new System.Drawing.Size(140, 23);
            this.btnLoadAuto.TabIndex = 52;
            this.btnLoadAuto.Text = "Load Single";
            this.btnLoadAuto.UseVisualStyleBackColor = true;
            this.btnLoadAuto.Click += new System.EventHandler(this.btnLoadAuto_Click);
            // 
            // cmbGameSelect
            // 
            this.cmbGameSelect.FormattingEnabled = true;
            this.cmbGameSelect.Location = new System.Drawing.Point(12, 12);
            this.cmbGameSelect.Name = "cmbGameSelect";
            this.cmbGameSelect.Size = new System.Drawing.Size(299, 21);
            this.cmbGameSelect.TabIndex = 53;
            this.cmbGameSelect.SelectedIndexChanged += new System.EventHandler(this.cmbGameSelect_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnLoadDir
            // 
            this.btnLoadDir.Location = new System.Drawing.Point(12, 39);
            this.btnLoadDir.Name = "btnLoadDir";
            this.btnLoadDir.Size = new System.Drawing.Size(140, 23);
            this.btnLoadDir.TabIndex = 54;
            this.btnLoadDir.Text = "Load Dir";
            this.btnLoadDir.UseVisualStyleBackColor = true;
            this.btnLoadDir.Click += new System.EventHandler(this.btnLoadDir_Click);
            // 
            // btnMassConvert
            // 
            this.btnMassConvert.Location = new System.Drawing.Point(12, 104);
            this.btnMassConvert.Name = "btnMassConvert";
            this.btnMassConvert.Size = new System.Drawing.Size(140, 23);
            this.btnMassConvert.TabIndex = 55;
            this.btnMassConvert.Text = "Mass Convert";
            this.btnMassConvert.UseVisualStyleBackColor = true;
            this.btnMassConvert.Click += new System.EventHandler(this.btnMassConvert_Click);
            // 
            // btnMassConvertTextures
            // 
            this.btnMassConvertTextures.Location = new System.Drawing.Point(171, 104);
            this.btnMassConvertTextures.Name = "btnMassConvertTextures";
            this.btnMassConvertTextures.Size = new System.Drawing.Size(140, 23);
            this.btnMassConvertTextures.TabIndex = 56;
            this.btnMassConvertTextures.Text = "Mass Convert Textures";
            this.btnMassConvertTextures.UseVisualStyleBackColor = true;
            this.btnMassConvertTextures.Click += new System.EventHandler(this.btnMassConvertTextures_Click);
            // 
            // chkLoadSingleExport
            // 
            this.chkLoadSingleExport.AutoSize = true;
            this.chkLoadSingleExport.Location = new System.Drawing.Point(171, 72);
            this.chkLoadSingleExport.Name = "chkLoadSingleExport";
            this.chkLoadSingleExport.Size = new System.Drawing.Size(56, 17);
            this.chkLoadSingleExport.TabIndex = 57;
            this.chkLoadSingleExport.Text = "Export";
            this.chkLoadSingleExport.UseVisualStyleBackColor = true;
            // 
            // chkGTFSView
            // 
            this.chkGTFSView.AutoSize = true;
            this.chkGTFSView.Location = new System.Drawing.Point(233, 72);
            this.chkGTFSView.Name = "chkGTFSView";
            this.chkGTFSView.Size = new System.Drawing.Size(77, 17);
            this.chkGTFSView.TabIndex = 58;
            this.chkGTFSView.Text = "GTFSView";
            this.chkGTFSView.UseVisualStyleBackColor = true;
            // 
            // FormGameTools2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 404);
            this.Controls.Add(this.chkGTFSView);
            this.Controls.Add(this.chkLoadSingleExport);
            this.Controls.Add(this.btnMassConvertTextures);
            this.Controls.Add(this.btnMassConvert);
            this.Controls.Add(this.btnLoadDir);
            this.Controls.Add(this.cmbGameSelect);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.btnLoadAuto);
            this.Name = "FormGameTools2";
            this.Text = "GameTools2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Button btnLoadAuto;
        private System.Windows.Forms.ComboBox cmbGameSelect;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLoadDir;
        private System.Windows.Forms.Button btnMassConvert;
        private System.Windows.Forms.Button btnMassConvertTextures;
        private System.Windows.Forms.CheckBox chkLoadSingleExport;
        private System.Windows.Forms.CheckBox chkGTFSView;
    }
}

