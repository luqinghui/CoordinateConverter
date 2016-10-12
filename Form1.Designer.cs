namespace CoordinateConverter
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.LatTextBox = new System.Windows.Forms.TextBox();
            this.LngTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.AResultTextBox = new System.Windows.Forms.TextBox();
            this.ALabel = new System.Windows.Forms.Label();
            this.BLabel = new System.Windows.Forms.Label();
            this.BResultTextBox = new System.Windows.Forms.TextBox();
            this.WGSRadio = new System.Windows.Forms.RadioButton();
            this.GCJRadio = new System.Windows.Forms.RadioButton();
            this.BDRadio = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.GoolgeTileResultTextBox = new System.Windows.Forms.TextBox();
            this.TecentTileResultTextBox = new System.Windows.Forms.TextBox();
            this.BaiduResultTextBox = new System.Windows.Forms.TextBox();
            this.ZoomLevelTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lat:";
            // 
            // LatTextBox
            // 
            this.LatTextBox.Location = new System.Drawing.Point(47, 9);
            this.LatTextBox.Name = "LatTextBox";
            this.LatTextBox.Size = new System.Drawing.Size(261, 21);
            this.LatTextBox.TabIndex = 1;
            // 
            // LngTextBox
            // 
            this.LngTextBox.Location = new System.Drawing.Point(47, 52);
            this.LngTextBox.Name = "LngTextBox";
            this.LngTextBox.Size = new System.Drawing.Size(261, 21);
            this.LngTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lng:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "转换";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AResultTextBox
            // 
            this.AResultTextBox.Location = new System.Drawing.Point(59, 174);
            this.AResultTextBox.Name = "AResultTextBox";
            this.AResultTextBox.Size = new System.Drawing.Size(401, 21);
            this.AResultTextBox.TabIndex = 5;
            // 
            // ALabel
            // 
            this.ALabel.AutoSize = true;
            this.ALabel.Location = new System.Drawing.Point(12, 177);
            this.ALabel.Name = "ALabel";
            this.ALabel.Size = new System.Drawing.Size(11, 12);
            this.ALabel.TabIndex = 6;
            this.ALabel.Text = "A";
            // 
            // BLabel
            // 
            this.BLabel.AutoSize = true;
            this.BLabel.Location = new System.Drawing.Point(12, 216);
            this.BLabel.Name = "BLabel";
            this.BLabel.Size = new System.Drawing.Size(11, 12);
            this.BLabel.TabIndex = 7;
            this.BLabel.Text = "B";
            // 
            // BResultTextBox
            // 
            this.BResultTextBox.Location = new System.Drawing.Point(59, 213);
            this.BResultTextBox.Name = "BResultTextBox";
            this.BResultTextBox.Size = new System.Drawing.Size(401, 21);
            this.BResultTextBox.TabIndex = 8;
            // 
            // WGSRadio
            // 
            this.WGSRadio.AutoSize = true;
            this.WGSRadio.Location = new System.Drawing.Point(135, 102);
            this.WGSRadio.Name = "WGSRadio";
            this.WGSRadio.Size = new System.Drawing.Size(53, 16);
            this.WGSRadio.TabIndex = 9;
            this.WGSRadio.TabStop = true;
            this.WGSRadio.Text = "WGS84";
            this.WGSRadio.UseVisualStyleBackColor = true;
            // 
            // GCJRadio
            // 
            this.GCJRadio.AutoSize = true;
            this.GCJRadio.Location = new System.Drawing.Point(194, 102);
            this.GCJRadio.Name = "GCJRadio";
            this.GCJRadio.Size = new System.Drawing.Size(53, 16);
            this.GCJRadio.TabIndex = 10;
            this.GCJRadio.TabStop = true;
            this.GCJRadio.Text = "GCJ02";
            this.GCJRadio.UseVisualStyleBackColor = true;
            // 
            // BDRadio
            // 
            this.BDRadio.AutoSize = true;
            this.BDRadio.Location = new System.Drawing.Point(253, 102);
            this.BDRadio.Name = "BDRadio";
            this.BDRadio.Size = new System.Drawing.Size(47, 16);
            this.BDRadio.TabIndex = 11;
            this.BDRadio.TabStop = true;
            this.BDRadio.Text = "BD09";
            this.BDRadio.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "转换Tile";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GoolgeTileResultTextBox
            // 
            this.GoolgeTileResultTextBox.Location = new System.Drawing.Point(73, 325);
            this.GoolgeTileResultTextBox.Name = "GoolgeTileResultTextBox";
            this.GoolgeTileResultTextBox.Size = new System.Drawing.Size(284, 21);
            this.GoolgeTileResultTextBox.TabIndex = 13;
            // 
            // TecentTileResultTextBox
            // 
            this.TecentTileResultTextBox.Location = new System.Drawing.Point(73, 371);
            this.TecentTileResultTextBox.Name = "TecentTileResultTextBox";
            this.TecentTileResultTextBox.Size = new System.Drawing.Size(284, 21);
            this.TecentTileResultTextBox.TabIndex = 13;
            // 
            // BaiduResultTextBox
            // 
            this.BaiduResultTextBox.Location = new System.Drawing.Point(73, 417);
            this.BaiduResultTextBox.Name = "BaiduResultTextBox";
            this.BaiduResultTextBox.Size = new System.Drawing.Size(284, 21);
            this.BaiduResultTextBox.TabIndex = 13;
            // 
            // ZoomLevelTextBox
            // 
            this.ZoomLevelTextBox.Location = new System.Drawing.Point(362, 52);
            this.ZoomLevelTextBox.Name = "ZoomLevelTextBox";
            this.ZoomLevelTextBox.Size = new System.Drawing.Size(98, 21);
            this.ZoomLevelTextBox.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Zoom:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 473);
            this.Controls.Add(this.ZoomLevelTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BaiduResultTextBox);
            this.Controls.Add(this.TecentTileResultTextBox);
            this.Controls.Add(this.GoolgeTileResultTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BDRadio);
            this.Controls.Add(this.GCJRadio);
            this.Controls.Add(this.WGSRadio);
            this.Controls.Add(this.BResultTextBox);
            this.Controls.Add(this.BLabel);
            this.Controls.Add(this.ALabel);
            this.Controls.Add(this.AResultTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LngTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LatTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LatTextBox;
        private System.Windows.Forms.TextBox LngTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox AResultTextBox;
        private System.Windows.Forms.Label ALabel;
        private System.Windows.Forms.Label BLabel;
        private System.Windows.Forms.TextBox BResultTextBox;
        private System.Windows.Forms.RadioButton WGSRadio;
        private System.Windows.Forms.RadioButton GCJRadio;
        private System.Windows.Forms.RadioButton BDRadio;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox GoolgeTileResultTextBox;
        private System.Windows.Forms.TextBox TecentTileResultTextBox;
        private System.Windows.Forms.TextBox BaiduResultTextBox;
        private System.Windows.Forms.TextBox ZoomLevelTextBox;
        private System.Windows.Forms.Label label3;
    }
}

