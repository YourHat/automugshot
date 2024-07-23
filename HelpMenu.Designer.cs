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
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(40, 21);
            label1.Name = "label1";
            label1.Size = new Size(85, 32);
            label1.TabIndex = 0;
            label1.Text = "HELP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(40, 216);
            label2.Name = "label2";
            label2.Size = new Size(271, 22);
            label2.TabIndex = 1;
            label2.Text = "HOW TO USE THIS SOFTWARE";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(66, 122);
            label3.Name = "label3";
            label3.Size = new Size(1113, 19);
            label3.TabIndex = 2;
            label3.Text = "This software makes it really easy to take mugshots by takeing pictures of Mugshot automatically and resizing them for you. ";
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
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Stencil", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(1012, 481);
            label5.Name = "label5";
            label5.Size = new Size(221, 16);
            label5.TabIndex = 4;
            label5.Text = "Developed By - Yutaroh Tanaka";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Stencil", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(1012, 497);
            label6.Name = "label6";
            label6.Size = new Size(226, 16);
            label6.TabIndex = 5;
            label6.Text = "KCSO Advisor - Sgt. Steven Stryd";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Stencil", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(40, 84);
            label7.Name = "label7";
            label7.Size = new Size(222, 22);
            label7.TabIndex = 6;
            label7.Text = "ABOUT THIS SOFTWARE";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(72, 254);
            label8.Name = "label8";
            label8.Size = new Size(853, 190);
            label8.TabIndex = 7;
            label8.Text = resources.GetString("label8.Text");
            // 
            // HelpMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 522);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "HelpMenu";
            Text = "HelpMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}