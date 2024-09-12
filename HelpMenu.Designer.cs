namespace automugshot
{
    partial class HelpMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpMenu));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label7 = new Label();
            label8 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            label9 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(110, 42);
            label1.TabIndex = 0;
            label1.Text = "HELP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(40, 115);
            label2.Name = "label2";
            label2.Size = new Size(145, 22);
            label2.TabIndex = 1;
            label2.Text = "INSTRUCTION";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(66, 73);
            label3.Name = "label3";
            label3.Size = new Size(1098, 19);
            label3.TabIndex = 2;
            label3.Text = "This software makes it simple to take mugshots by taking the Mugshots automatically, verifying, and resizing them for you. ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Stencil", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(66, 369);
            label4.Name = "label4";
            label4.Size = new Size(0, 22);
            label4.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Stencil", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(40, 51);
            label7.Name = "label7";
            label7.Size = new Size(241, 22);
            label7.TabIndex = 6;
            label7.Text = "ABOUT THIS SOFTWARE";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(443, 161);
            label8.Name = "label8";
            label8.Size = new Size(783, 399);
            label8.TabIndex = 7;
            label8.Text = resources.GetString("label8.Text");
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(40, 140);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(385, 289);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(40, 445);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(373, 248);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.Control;
            label9.Font = new Font("Stencil", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = SystemColors.ButtonHighlight;
            label9.Location = new Point(1219, 667);
            label9.Name = "label9";
            label9.Size = new Size(97, 26);
            label9.TabIndex = 11;
            label9.Text = "KCSO - Y.Tanaka\r\nKCSO - S.Stryd";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(466, 611);
            label10.Name = "label10";
            label10.Size = new Size(439, 42);
            label10.TabIndex = 12;
            label10.Text = "*** to select mugshots, click images.\r\n*** use PTZ camera using VISCA protocol for full functionality\r\n*** make sure to download latest firmware for all the equipments\r\n";
            // 
            // HelpMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1337, 713);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "HelpMenu";
            Text = "AUTOMUGSHOT - HELP";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label7;
        private Label label8;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private Label label9;
        private Label label10;
    }
}