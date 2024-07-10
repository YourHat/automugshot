namespace automugshot
{
    partial class saveSettings
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
            label1 = new Label();
            changePathButton = new Button();
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label2 = new Label();
            saveSettingsButton = new Button();
            label3 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            label4 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(71, 97);
            label1.Name = "label1";
            label1.Size = new Size(92, 14);
            label1.TabIndex = 0;
            label1.Text = "Folder Path :";
            // 
            // changePathButton
            // 
            changePathButton.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            changePathButton.Location = new Point(43, 71);
            changePathButton.Name = "changePathButton";
            changePathButton.Size = new Size(281, 23);
            changePathButton.TabIndex = 1;
            changePathButton.Text = "Change the Folder Path to Save Mughshots";
            changePathButton.UseVisualStyleBackColor = true;
            changePathButton.Click += changePathButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(43, 128);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(232, 114);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Saving Procedure";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(20, 71);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(193, 18);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Do Not Override Mugshots";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(20, 34);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(148, 18);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Override Mugshots";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(169, 97);
            label2.Name = "label2";
            label2.Size = new Size(214, 14);
            label2.TabIndex = 3;
            label2.Text = "folder path to save pictures.....";
            // 
            // saveSettingsButton
            // 
            saveSettingsButton.Font = new Font("Stencil", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            saveSettingsButton.Location = new Point(830, 188);
            saveSettingsButton.Name = "saveSettingsButton";
            saveSettingsButton.Size = new Size(141, 54);
            saveSettingsButton.TabIndex = 4;
            saveSettingsButton.Text = "Save Settings";
            saveSettingsButton.UseVisualStyleBackColor = true;
            saveSettingsButton.Click += saveSettingsButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Stencil", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(43, 18);
            label3.Name = "label3";
            label3.Size = new Size(146, 32);
            label3.TabIndex = 5;
            label3.Text = "SETTINGS";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Stencil", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Green;
            label4.Location = new Point(641, 97);
            label4.Name = "label4";
            label4.Size = new Size(143, 42);
            label4.TabIndex = 6;
            label4.Text = "saved!";
            // 
            // saveSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(983, 254);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(saveSettingsButton);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(changePathButton);
            Controls.Add(label1);
            Name = "saveSettings";
            Text = "SettingsMenu";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button changePathButton;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label2;
        private Button saveSettingsButton;
        private Label label3;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label4;
    }
}