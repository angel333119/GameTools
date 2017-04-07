namespace FloatTable {
    partial class Form1 {
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
            this.comboWidth = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkFlip = new System.Windows.Forms.CheckBox();
            this.btnUseAsOffset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboWidth
            // 
            this.comboWidth.FormattingEnabled = true;
            this.comboWidth.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboWidth.Location = new System.Drawing.Point(12, 12);
            this.comboWidth.Name = "comboWidth";
            this.comboWidth.Size = new System.Drawing.Size(121, 21);
            this.comboWidth.TabIndex = 0;
            this.comboWidth.SelectedIndexChanged += new System.EventHandler(this.comboWidth_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(512, 9);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(575, 421);
            this.dataGridView1.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkFlip
            // 
            this.checkFlip.AutoSize = true;
            this.checkFlip.Location = new System.Drawing.Point(139, 14);
            this.checkFlip.Name = "checkFlip";
            this.checkFlip.Size = new System.Drawing.Size(42, 17);
            this.checkFlip.TabIndex = 3;
            this.checkFlip.Text = "Flip";
            this.checkFlip.UseVisualStyleBackColor = true;
            this.checkFlip.CheckedChanged += new System.EventHandler(this.checkFlip_CheckedChanged);
            // 
            // btnUseAsOffset
            // 
            this.btnUseAsOffset.Location = new System.Drawing.Point(187, 10);
            this.btnUseAsOffset.Name = "btnUseAsOffset";
            this.btnUseAsOffset.Size = new System.Drawing.Size(89, 23);
            this.btnUseAsOffset.TabIndex = 4;
            this.btnUseAsOffset.Text = "Use As Offset";
            this.btnUseAsOffset.UseVisualStyleBackColor = true;
            this.btnUseAsOffset.Click += new System.EventHandler(this.btnUseAsOffset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 472);
            this.Controls.Add(this.btnUseAsOffset);
            this.Controls.Add(this.checkFlip);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.comboWidth);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboWidth;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkFlip;
        private System.Windows.Forms.Button btnUseAsOffset;
    }
}

