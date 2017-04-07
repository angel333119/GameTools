namespace GT_KyleHyde
{
    partial class FormColor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConvertFromEightBit = new System.Windows.Forms.Button();
            this.btnConvertFromHex = new System.Windows.Forms.Button();
            this.btnConvertFromFiveBit = new System.Windows.Forms.Button();
            this.btnConvertFromBytes = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBEight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGEight = new System.Windows.Forms.TextBox();
            this.txtREight = new System.Windows.Forms.TextBox();
            this.txtBFive = new System.Windows.Forms.TextBox();
            this.txtGFive = new System.Windows.Forms.TextBox();
            this.txtRFive = new System.Windows.Forms.TextBox();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.txtBytes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(11, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 124);
            this.panel1.TabIndex = 36;
            // 
            // btnConvertFromEightBit
            // 
            this.btnConvertFromEightBit.Location = new System.Drawing.Point(201, 86);
            this.btnConvertFromEightBit.Name = "btnConvertFromEightBit";
            this.btnConvertFromEightBit.Size = new System.Drawing.Size(75, 23);
            this.btnConvertFromEightBit.TabIndex = 35;
            this.btnConvertFromEightBit.Text = "From 8-Bit";
            this.btnConvertFromEightBit.UseVisualStyleBackColor = true;
            this.btnConvertFromEightBit.Click += new System.EventHandler(this.btnConvertFromEightBit_Click);
            // 
            // btnConvertFromHex
            // 
            this.btnConvertFromHex.Location = new System.Drawing.Point(201, 35);
            this.btnConvertFromHex.Name = "btnConvertFromHex";
            this.btnConvertFromHex.Size = new System.Drawing.Size(75, 23);
            this.btnConvertFromHex.TabIndex = 34;
            this.btnConvertFromHex.Text = "From #";
            this.btnConvertFromHex.UseVisualStyleBackColor = true;
            this.btnConvertFromHex.Click += new System.EventHandler(this.btnConvertFromHex_Click);
            // 
            // btnConvertFromFiveBit
            // 
            this.btnConvertFromFiveBit.Location = new System.Drawing.Point(5, 86);
            this.btnConvertFromFiveBit.Name = "btnConvertFromFiveBit";
            this.btnConvertFromFiveBit.Size = new System.Drawing.Size(75, 23);
            this.btnConvertFromFiveBit.TabIndex = 33;
            this.btnConvertFromFiveBit.Text = "From 5-Bit";
            this.btnConvertFromFiveBit.UseVisualStyleBackColor = true;
            this.btnConvertFromFiveBit.Click += new System.EventHandler(this.btnConvertFromFiveBit_Click);
            // 
            // btnConvertFromBytes
            // 
            this.btnConvertFromBytes.Location = new System.Drawing.Point(5, 35);
            this.btnConvertFromBytes.Name = "btnConvertFromBytes";
            this.btnConvertFromBytes.Size = new System.Drawing.Size(75, 23);
            this.btnConvertFromBytes.TabIndex = 32;
            this.btnConvertFromBytes.Text = "From Bytes";
            this.btnConvertFromBytes.UseVisualStyleBackColor = true;
            this.btnConvertFromBytes.Click += new System.EventHandler(this.btnConvertFromBytes_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "G";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "R";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtBEight
            // 
            this.txtBEight.Location = new System.Drawing.Point(155, 89);
            this.txtBEight.Name = "txtBEight";
            this.txtBEight.Size = new System.Drawing.Size(40, 20);
            this.txtBEight.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "RGB888";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "RGBA1555";
            // 
            // txtGEight
            // 
            this.txtGEight.Location = new System.Drawing.Point(155, 63);
            this.txtGEight.Name = "txtGEight";
            this.txtGEight.Size = new System.Drawing.Size(40, 20);
            this.txtGEight.TabIndex = 25;
            // 
            // txtREight
            // 
            this.txtREight.Location = new System.Drawing.Point(155, 37);
            this.txtREight.Name = "txtREight";
            this.txtREight.Size = new System.Drawing.Size(40, 20);
            this.txtREight.TabIndex = 24;
            // 
            // txtBFive
            // 
            this.txtBFive.Location = new System.Drawing.Point(86, 89);
            this.txtBFive.Name = "txtBFive";
            this.txtBFive.Size = new System.Drawing.Size(40, 20);
            this.txtBFive.TabIndex = 23;
            // 
            // txtGFive
            // 
            this.txtGFive.Location = new System.Drawing.Point(86, 63);
            this.txtGFive.Name = "txtGFive";
            this.txtGFive.Size = new System.Drawing.Size(40, 20);
            this.txtGFive.TabIndex = 22;
            // 
            // txtRFive
            // 
            this.txtRFive.Location = new System.Drawing.Point(86, 37);
            this.txtRFive.Name = "txtRFive";
            this.txtRFive.Size = new System.Drawing.Size(40, 20);
            this.txtRFive.TabIndex = 21;
            // 
            // txtHex
            // 
            this.txtHex.Location = new System.Drawing.Point(155, 11);
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(70, 20);
            this.txtHex.TabIndex = 20;
            // 
            // txtBytes
            // 
            this.txtBytes.Location = new System.Drawing.Point(77, 11);
            this.txtBytes.Name = "txtBytes";
            this.txtBytes.Size = new System.Drawing.Size(70, 20);
            this.txtBytes.TabIndex = 19;
            // 
            // FormColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnConvertFromEightBit);
            this.Controls.Add(this.btnConvertFromHex);
            this.Controls.Add(this.btnConvertFromFiveBit);
            this.Controls.Add(this.btnConvertFromBytes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBEight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGEight);
            this.Controls.Add(this.txtREight);
            this.Controls.Add(this.txtBFive);
            this.Controls.Add(this.txtGFive);
            this.Controls.Add(this.txtRFive);
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.txtBytes);
            this.Name = "FormColor";
            this.Text = "FormColor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConvertFromEightBit;
        private System.Windows.Forms.Button btnConvertFromHex;
        private System.Windows.Forms.Button btnConvertFromFiveBit;
        private System.Windows.Forms.Button btnConvertFromBytes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBEight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGEight;
        private System.Windows.Forms.TextBox txtREight;
        private System.Windows.Forms.TextBox txtBFive;
        private System.Windows.Forms.TextBox txtGFive;
        private System.Windows.Forms.TextBox txtRFive;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.TextBox txtBytes;
    }
}