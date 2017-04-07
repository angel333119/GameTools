namespace GameTools {
    partial class GTFSView {
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
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.listboxTracking = new System.Windows.Forms.ListBox();
            this.listboxTrackingGaps = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGotoUInt16 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSelectedUInt16 = new System.Windows.Forms.TextBox();
            this.btnGotoUInt32 = new System.Windows.Forms.Button();
            this.checkEndian = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSelectedOffset = new System.Windows.Forms.TextBox();
            this.txtSelectedUInt32 = new System.Windows.Forms.TextBox();
            this.txtSelectedFloat = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // hexBox1
            // 
            this.hexBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(337, 72);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ReadOnly = true;
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(836, 529);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 0;
            this.hexBox1.VScrollBarVisible = true;
            this.hexBox1.SelectionStartChanged += new System.EventHandler(this.hexBox1_SelectionStartChanged);
            // 
            // listboxTracking
            // 
            this.listboxTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listboxTracking.FormattingEnabled = true;
            this.listboxTracking.Location = new System.Drawing.Point(12, 25);
            this.listboxTracking.Name = "listboxTracking";
            this.listboxTracking.Size = new System.Drawing.Size(156, 576);
            this.listboxTracking.TabIndex = 1;
            this.listboxTracking.SelectedIndexChanged += new System.EventHandler(this.listboxTracking_SelectedIndexChanged);
            // 
            // listboxTrackingGaps
            // 
            this.listboxTrackingGaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listboxTrackingGaps.FormattingEnabled = true;
            this.listboxTrackingGaps.Location = new System.Drawing.Point(174, 25);
            this.listboxTrackingGaps.Name = "listboxTrackingGaps";
            this.listboxTrackingGaps.Size = new System.Drawing.Size(157, 576);
            this.listboxTrackingGaps.TabIndex = 4;
            this.listboxTrackingGaps.SelectedIndexChanged += new System.EventHandler(this.listboxTrackingGaps_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Read Sections";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Section Gaps";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGotoUInt16);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtSelectedUInt16);
            this.panel2.Controls.Add(this.btnGotoUInt32);
            this.panel2.Controls.Add(this.checkEndian);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtSelectedOffset);
            this.panel2.Controls.Add(this.txtSelectedUInt32);
            this.panel2.Controls.Add(this.txtSelectedFloat);
            this.panel2.Location = new System.Drawing.Point(337, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(497, 54);
            this.panel2.TabIndex = 46;
            // 
            // btnGotoUInt16
            // 
            this.btnGotoUInt16.Location = new System.Drawing.Point(108, 26);
            this.btnGotoUInt16.Name = "btnGotoUInt16";
            this.btnGotoUInt16.Size = new System.Drawing.Size(21, 23);
            this.btnGotoUInt16.TabIndex = 11;
            this.btnGotoUInt16.Text = "O";
            this.btnGotoUInt16.UseVisualStyleBackColor = true;
            this.btnGotoUInt16.Click += new System.EventHandler(this.btnGotoUInt16_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "UInt16";
            // 
            // txtSelectedUInt16
            // 
            this.txtSelectedUInt16.Location = new System.Drawing.Point(48, 28);
            this.txtSelectedUInt16.Name = "txtSelectedUInt16";
            this.txtSelectedUInt16.Size = new System.Drawing.Size(54, 20);
            this.txtSelectedUInt16.TabIndex = 9;
            // 
            // btnGotoUInt32
            // 
            this.btnGotoUInt32.Location = new System.Drawing.Point(304, 26);
            this.btnGotoUInt32.Name = "btnGotoUInt32";
            this.btnGotoUInt32.Size = new System.Drawing.Size(21, 23);
            this.btnGotoUInt32.TabIndex = 8;
            this.btnGotoUInt32.Text = "O";
            this.btnGotoUInt32.UseVisualStyleBackColor = true;
            this.btnGotoUInt32.Click += new System.EventHandler(this.btnGotoUInt32_Click);
            // 
            // checkEndian
            // 
            this.checkEndian.AutoSize = true;
            this.checkEndian.Location = new System.Drawing.Point(452, 5);
            this.checkEndian.Name = "checkEndian";
            this.checkEndian.Size = new System.Drawing.Size(42, 17);
            this.checkEndian.TabIndex = 7;
            this.checkEndian.Text = "Flip";
            this.checkEndian.UseVisualStyleBackColor = true;
            this.checkEndian.CheckedChanged += new System.EventHandler(this.checkEndian_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Float";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "UInt32";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Offset:";
            // 
            // txtSelectedOffset
            // 
            this.txtSelectedOffset.Location = new System.Drawing.Point(48, 3);
            this.txtSelectedOffset.Name = "txtSelectedOffset";
            this.txtSelectedOffset.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedOffset.TabIndex = 0;
            this.txtSelectedOffset.TextChanged += new System.EventHandler(this.txtSelectedOffset_TextChanged);
            this.txtSelectedOffset.Leave += new System.EventHandler(this.txtSelectedOffset_Leave);
            // 
            // txtSelectedUInt32
            // 
            this.txtSelectedUInt32.Location = new System.Drawing.Point(198, 28);
            this.txtSelectedUInt32.Name = "txtSelectedUInt32";
            this.txtSelectedUInt32.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedUInt32.TabIndex = 1;
            // 
            // txtSelectedFloat
            // 
            this.txtSelectedFloat.Location = new System.Drawing.Point(394, 28);
            this.txtSelectedFloat.Name = "txtSelectedFloat";
            this.txtSelectedFloat.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedFloat.TabIndex = 2;
            // 
            // GTFSView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 614);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listboxTrackingGaps);
            this.Controls.Add(this.listboxTracking);
            this.Controls.Add(this.hexBox1);
            this.Name = "GTFSView";
            this.Text = "GTFSView";
            this.Load += new System.EventHandler(this.GTFSView_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.ListBox listboxTracking;
        private System.Windows.Forms.ListBox listboxTrackingGaps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSelectedOffset;
        private System.Windows.Forms.TextBox txtSelectedUInt32;
        private System.Windows.Forms.TextBox txtSelectedFloat;
        private System.Windows.Forms.CheckBox checkEndian;
        private System.Windows.Forms.Button btnGotoUInt32;
        private System.Windows.Forms.Button btnGotoUInt16;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSelectedUInt16;
    }
}

