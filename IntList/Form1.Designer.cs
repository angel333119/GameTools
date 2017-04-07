namespace IntList {
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtSkipBytes = new System.Windows.Forms.TextBox();
            this.txtFor = new System.Windows.Forms.TextBox();
            this.btnSelectedSum = new System.Windows.Forms.Button();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(181, 433);
            this.listBox1.TabIndex = 0;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(87, 10);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(106, 23);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtSkipBytes
            // 
            this.txtSkipBytes.Location = new System.Drawing.Point(12, 12);
            this.txtSkipBytes.Name = "txtSkipBytes";
            this.txtSkipBytes.Size = new System.Drawing.Size(32, 20);
            this.txtSkipBytes.TabIndex = 2;
            this.txtSkipBytes.Text = "0";
            // 
            // txtFor
            // 
            this.txtFor.Location = new System.Drawing.Point(50, 12);
            this.txtFor.Name = "txtFor";
            this.txtFor.Size = new System.Drawing.Size(32, 20);
            this.txtFor.TabIndex = 3;
            this.txtFor.Text = "20";
            // 
            // btnSelectedSum
            // 
            this.btnSelectedSum.Location = new System.Drawing.Point(12, 484);
            this.btnSelectedSum.Name = "btnSelectedSum";
            this.btnSelectedSum.Size = new System.Drawing.Size(53, 23);
            this.btnSelectedSum.TabIndex = 4;
            this.btnSelectedSum.Text = "Sum";
            this.btnSelectedSum.UseVisualStyleBackColor = true;
            this.btnSelectedSum.Click += new System.EventHandler(this.btnSelectedSum_Click);
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(71, 486);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(122, 20);
            this.txtSum.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 519);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.btnSelectedSum);
            this.Controls.Add(this.txtFor);
            this.Controls.Add(this.txtSkipBytes);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "IntList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtSkipBytes;
        private System.Windows.Forms.TextBox txtFor;
        private System.Windows.Forms.Button btnSelectedSum;
        private System.Windows.Forms.TextBox txtSum;
    }
}

