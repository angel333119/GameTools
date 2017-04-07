namespace GT_KyleHyde
{
    partial class FormHotelDuskStatic
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.btnColorCalc = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtSelectedOffset = new System.Windows.Forms.TextBox();
            this.txtSelectedBytes1 = new System.Windows.Forms.TextBox();
            this.txtSelectedBytes2 = new System.Windows.Forms.TextBox();
            this.txtSelectedBytes4 = new System.Windows.Forms.TextBox();
            this.btnTestPalette = new System.Windows.Forms.Button();
            this.btnTestPixels = new System.Windows.Forms.Button();
            this.txtPixelHeight = new System.Windows.Forms.TextBox();
            this.txtPixelWidth = new System.Windows.Forms.TextBox();
            this.btnSetHeight = new System.Windows.Forms.Button();
            this.btnSetWidth = new System.Windows.Forms.Button();
            this.txtPaletteSize = new System.Windows.Forms.TextBox();
            this.btnSetPaletteSize = new System.Windows.Forms.Button();
            this.btnSetPixelStart = new System.Windows.Forms.Button();
            this.txtPixelStart = new System.Windows.Forms.TextBox();
            this.btnSetPaletteStart = new System.Windows.Forms.Button();
            this.txtPaletteStart = new System.Windows.Forms.TextBox();
            this.btnViewImage = new System.Windows.Forms.Button();
            this.btnPixelAfterPalette = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNeg = new System.Windows.Forms.TextBox();
            this.btnNegAdd = new System.Windows.Forms.Button();
            this.btnNegSub = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(473, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 349);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // hexBox
            // 
            this.hexBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox.Location = new System.Drawing.Point(12, 107);
            this.hexBox.Name = "hexBox";
            this.hexBox.ReadOnly = true;
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(455, 254);
            this.hexBox.TabIndex = 20;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.SelectionStartChanged += new System.EventHandler(this.hexBox_SelectionStartChanged);
            // 
            // btnColorCalc
            // 
            this.btnColorCalc.Location = new System.Drawing.Point(93, 12);
            this.btnColorCalc.Name = "btnColorCalc";
            this.btnColorCalc.Size = new System.Drawing.Size(75, 23);
            this.btnColorCalc.TabIndex = 1;
            this.btnColorCalc.Text = "Color Calc";
            this.btnColorCalc.UseVisualStyleBackColor = true;
            this.btnColorCalc.Click += new System.EventHandler(this.btnColorCalc_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtSelectedOffset
            // 
            this.txtSelectedOffset.Location = new System.Drawing.Point(3, 5);
            this.txtSelectedOffset.Name = "txtSelectedOffset";
            this.txtSelectedOffset.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedOffset.TabIndex = 0;
            // 
            // txtSelectedBytes1
            // 
            this.txtSelectedBytes1.Location = new System.Drawing.Point(3, 31);
            this.txtSelectedBytes1.Name = "txtSelectedBytes1";
            this.txtSelectedBytes1.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedBytes1.TabIndex = 1;
            // 
            // txtSelectedBytes2
            // 
            this.txtSelectedBytes2.Location = new System.Drawing.Point(109, 31);
            this.txtSelectedBytes2.Name = "txtSelectedBytes2";
            this.txtSelectedBytes2.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedBytes2.TabIndex = 2;
            // 
            // txtSelectedBytes4
            // 
            this.txtSelectedBytes4.Location = new System.Drawing.Point(215, 31);
            this.txtSelectedBytes4.Name = "txtSelectedBytes4";
            this.txtSelectedBytes4.Size = new System.Drawing.Size(100, 20);
            this.txtSelectedBytes4.TabIndex = 3;
            // 
            // btnTestPalette
            // 
            this.btnTestPalette.Location = new System.Drawing.Point(3, 89);
            this.btnTestPalette.Name = "btnTestPalette";
            this.btnTestPalette.Size = new System.Drawing.Size(75, 23);
            this.btnTestPalette.TabIndex = 5;
            this.btnTestPalette.Text = "Test Palette";
            this.btnTestPalette.UseVisualStyleBackColor = true;
            this.btnTestPalette.Click += new System.EventHandler(this.btnTestPalette_Click);
            // 
            // btnTestPixels
            // 
            this.btnTestPixels.Location = new System.Drawing.Point(3, 88);
            this.btnTestPixels.Name = "btnTestPixels";
            this.btnTestPixels.Size = new System.Drawing.Size(75, 23);
            this.btnTestPixels.TabIndex = 6;
            this.btnTestPixels.Text = "Test Pixels";
            this.btnTestPixels.UseVisualStyleBackColor = true;
            this.btnTestPixels.Click += new System.EventHandler(this.btnTestPixels_Click);
            // 
            // txtPixelHeight
            // 
            this.txtPixelHeight.Location = new System.Drawing.Point(84, 35);
            this.txtPixelHeight.Name = "txtPixelHeight";
            this.txtPixelHeight.Size = new System.Drawing.Size(100, 20);
            this.txtPixelHeight.TabIndex = 4;
            // 
            // txtPixelWidth
            // 
            this.txtPixelWidth.Location = new System.Drawing.Point(84, 5);
            this.txtPixelWidth.Name = "txtPixelWidth";
            this.txtPixelWidth.Size = new System.Drawing.Size(100, 20);
            this.txtPixelWidth.TabIndex = 3;
            // 
            // btnSetHeight
            // 
            this.btnSetHeight.Location = new System.Drawing.Point(3, 32);
            this.btnSetHeight.Name = "btnSetHeight";
            this.btnSetHeight.Size = new System.Drawing.Size(75, 23);
            this.btnSetHeight.TabIndex = 1;
            this.btnSetHeight.Text = "Height";
            this.btnSetHeight.UseVisualStyleBackColor = true;
            this.btnSetHeight.Click += new System.EventHandler(this.btnSetHeight_Click);
            // 
            // btnSetWidth
            // 
            this.btnSetWidth.Location = new System.Drawing.Point(3, 3);
            this.btnSetWidth.Name = "btnSetWidth";
            this.btnSetWidth.Size = new System.Drawing.Size(75, 23);
            this.btnSetWidth.TabIndex = 0;
            this.btnSetWidth.Text = "Width";
            this.btnSetWidth.UseVisualStyleBackColor = true;
            this.btnSetWidth.Click += new System.EventHandler(this.btnSetWidth_Click);
            // 
            // txtPaletteSize
            // 
            this.txtPaletteSize.Location = new System.Drawing.Point(84, 5);
            this.txtPaletteSize.Name = "txtPaletteSize";
            this.txtPaletteSize.Size = new System.Drawing.Size(100, 20);
            this.txtPaletteSize.TabIndex = 2;
            // 
            // btnSetPaletteSize
            // 
            this.btnSetPaletteSize.Location = new System.Drawing.Point(3, 3);
            this.btnSetPaletteSize.Name = "btnSetPaletteSize";
            this.btnSetPaletteSize.Size = new System.Drawing.Size(75, 23);
            this.btnSetPaletteSize.TabIndex = 0;
            this.btnSetPaletteSize.Text = "Size";
            this.btnSetPaletteSize.UseVisualStyleBackColor = true;
            this.btnSetPaletteSize.Click += new System.EventHandler(this.btnSetPaletteSize_Click);
            // 
            // btnSetPixelStart
            // 
            this.btnSetPixelStart.Location = new System.Drawing.Point(3, 61);
            this.btnSetPixelStart.Name = "btnSetPixelStart";
            this.btnSetPixelStart.Size = new System.Drawing.Size(75, 23);
            this.btnSetPixelStart.TabIndex = 2;
            this.btnSetPixelStart.Text = "Start";
            this.btnSetPixelStart.UseVisualStyleBackColor = true;
            this.btnSetPixelStart.Click += new System.EventHandler(this.btnSetPixelStart_Click);
            // 
            // txtPixelStart
            // 
            this.txtPixelStart.Location = new System.Drawing.Point(84, 63);
            this.txtPixelStart.Name = "txtPixelStart";
            this.txtPixelStart.Size = new System.Drawing.Size(100, 20);
            this.txtPixelStart.TabIndex = 5;
            // 
            // btnSetPaletteStart
            // 
            this.btnSetPaletteStart.Enabled = false;
            this.btnSetPaletteStart.Location = new System.Drawing.Point(3, 32);
            this.btnSetPaletteStart.Name = "btnSetPaletteStart";
            this.btnSetPaletteStart.Size = new System.Drawing.Size(75, 23);
            this.btnSetPaletteStart.TabIndex = 1;
            this.btnSetPaletteStart.Text = "Start";
            this.btnSetPaletteStart.UseVisualStyleBackColor = true;
            this.btnSetPaletteStart.Click += new System.EventHandler(this.btnSetPaletteStart_Click);
            // 
            // txtPaletteStart
            // 
            this.txtPaletteStart.Location = new System.Drawing.Point(84, 34);
            this.txtPaletteStart.Name = "txtPaletteStart";
            this.txtPaletteStart.Size = new System.Drawing.Size(100, 20);
            this.txtPaletteStart.TabIndex = 3;
            // 
            // btnViewImage
            // 
            this.btnViewImage.Location = new System.Drawing.Point(473, 367);
            this.btnViewImage.Name = "btnViewImage";
            this.btnViewImage.Size = new System.Drawing.Size(75, 23);
            this.btnViewImage.TabIndex = 2;
            this.btnViewImage.Text = "View";
            this.btnViewImage.UseVisualStyleBackColor = true;
            this.btnViewImage.Click += new System.EventHandler(this.btnViewImage_Click);
            // 
            // btnPixelAfterPalette
            // 
            this.btnPixelAfterPalette.Location = new System.Drawing.Point(3, 60);
            this.btnPixelAfterPalette.Name = "btnPixelAfterPalette";
            this.btnPixelAfterPalette.Size = new System.Drawing.Size(181, 23);
            this.btnPixelAfterPalette.TabIndex = 4;
            this.btnPixelAfterPalette.Text = "Set Pixels to After";
            this.btnPixelAfterPalette.UseVisualStyleBackColor = true;
            this.btnPixelAfterPalette.Click += new System.EventHandler(this.btnPixelAfterPalette_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSetWidth);
            this.panel1.Controls.Add(this.btnSetHeight);
            this.panel1.Controls.Add(this.btnSetPixelStart);
            this.panel1.Controls.Add(this.txtPixelWidth);
            this.panel1.Controls.Add(this.txtPixelHeight);
            this.panel1.Controls.Add(this.txtPixelStart);
            this.panel1.Controls.Add(this.btnTestPixels);
            this.panel1.Location = new System.Drawing.Point(12, 367);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 117);
            this.panel1.TabIndex = 44;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSelectedOffset);
            this.panel2.Controls.Add(this.txtSelectedBytes1);
            this.panel2.Controls.Add(this.txtSelectedBytes2);
            this.panel2.Controls.Add(this.txtSelectedBytes4);
            this.panel2.Location = new System.Drawing.Point(12, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(455, 60);
            this.panel2.TabIndex = 45;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSetPaletteSize);
            this.panel3.Controls.Add(this.btnSetPaletteStart);
            this.panel3.Controls.Add(this.txtPaletteSize);
            this.panel3.Controls.Add(this.btnPixelAfterPalette);
            this.panel3.Controls.Add(this.btnTestPalette);
            this.panel3.Controls.Add(this.txtPaletteStart);
            this.panel3.Location = new System.Drawing.Point(211, 367);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 117);
            this.panel3.TabIndex = 46;
            // 
            // txtNeg
            // 
            this.txtNeg.Location = new System.Drawing.Point(250, 15);
            this.txtNeg.Name = "txtNeg";
            this.txtNeg.Size = new System.Drawing.Size(77, 20);
            this.txtNeg.TabIndex = 47;
            this.txtNeg.Text = "0";
            // 
            // btnNegAdd
            // 
            this.btnNegAdd.Location = new System.Drawing.Point(334, 15);
            this.btnNegAdd.Name = "btnNegAdd";
            this.btnNegAdd.Size = new System.Drawing.Size(20, 23);
            this.btnNegAdd.TabIndex = 48;
            this.btnNegAdd.Text = "+";
            this.btnNegAdd.UseVisualStyleBackColor = true;
            this.btnNegAdd.Click += new System.EventHandler(this.btnNegAdd_Click);
            // 
            // btnNegSub
            // 
            this.btnNegSub.Location = new System.Drawing.Point(224, 15);
            this.btnNegSub.Name = "btnNegSub";
            this.btnNegSub.Size = new System.Drawing.Size(20, 23);
            this.btnNegSub.TabIndex = 49;
            this.btnNegSub.Text = "-";
            this.btnNegSub.UseVisualStyleBackColor = true;
            this.btnNegSub.Click += new System.EventHandler(this.btnNegSub_Click);
            // 
            // FormHotelDuskStatic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 524);
            this.Controls.Add(this.btnNegSub);
            this.Controls.Add(this.btnNegAdd);
            this.Controls.Add(this.txtNeg);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnViewImage);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnColorCalc);
            this.Controls.Add(this.hexBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormHotelDuskStatic";
            this.Text = "GT-HotelDusk-Static";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.Button btnColorCalc;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtSelectedOffset;
        private System.Windows.Forms.TextBox txtSelectedBytes1;
        private System.Windows.Forms.TextBox txtSelectedBytes2;
        private System.Windows.Forms.TextBox txtSelectedBytes4;
        private System.Windows.Forms.Button btnTestPalette;
        private System.Windows.Forms.Button btnTestPixels;
        private System.Windows.Forms.TextBox txtPixelHeight;
        private System.Windows.Forms.TextBox txtPixelWidth;
        private System.Windows.Forms.Button btnSetHeight;
        private System.Windows.Forms.Button btnSetWidth;
        private System.Windows.Forms.TextBox txtPaletteSize;
        private System.Windows.Forms.Button btnSetPaletteSize;
        private System.Windows.Forms.Button btnSetPixelStart;
        private System.Windows.Forms.TextBox txtPixelStart;
        private System.Windows.Forms.Button btnSetPaletteStart;
        private System.Windows.Forms.TextBox txtPaletteStart;
        private System.Windows.Forms.Button btnViewImage;
        private System.Windows.Forms.Button btnPixelAfterPalette;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNeg;
        private System.Windows.Forms.Button btnNegAdd;
        private System.Windows.Forms.Button btnNegSub;
    }
}

