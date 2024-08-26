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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(saveSettings));
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
            cameralistbox = new ListBox();
            label5 = new Label();
            label6 = new Label();
            ControllerBox = new ListBox();
            openfoldercheckbox = new CheckBox();
            groupBox2 = new GroupBox();
            dateradiobutton = new RadioButton();
            inmatedateradiobutton = new RadioButton();
            combinepiccheckbox = new CheckBox();
            widthbox = new TextBox();
            heightbox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(73, 139);
            label1.Name = "label1";
            label1.Size = new Size(122, 19);
            label1.TabIndex = 0;
            label1.Text = "Folder Path :";
            // 
            // changePathButton
            // 
            changePathButton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            changePathButton.Location = new Point(45, 89);
            changePathButton.Name = "changePathButton";
            changePathButton.Size = new Size(403, 47);
            changePathButton.TabIndex = 1;
            changePathButton.Text = "Change Folder Path to Save Mughshots";
            changePathButton.UseVisualStyleBackColor = true;
            changePathButton.Click += changePathButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(493, 197);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(278, 114);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Saving Procedure";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(20, 71);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(250, 23);
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
            radioButton1.Size = new Size(191, 23);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Override Mugshots";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(201, 139);
            label2.Name = "label2";
            label2.Size = new Size(283, 19);
            label2.TabIndex = 3;
            label2.Text = "folder path to save pictures.....";
            // 
            // saveSettingsButton
            // 
            saveSettingsButton.Font = new Font("Stencil", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            saveSettingsButton.Location = new Point(856, 12);
            saveSettingsButton.Name = "saveSettingsButton";
            saveSettingsButton.Size = new Size(251, 120);
            saveSettingsButton.TabIndex = 4;
            saveSettingsButton.Text = "Save Settings";
            saveSettingsButton.UseVisualStyleBackColor = true;
            saveSettingsButton.Click += saveSettingsButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Stencil", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(22, 18);
            label3.Name = "label3";
            label3.Size = new Size(254, 57);
            label3.TabIndex = 5;
            label3.Text = "SETTINGS";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Stencil", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Green;
            label4.Location = new Point(556, 20);
            label4.Name = "label4";
            label4.Size = new Size(255, 76);
            label4.TabIndex = 6;
            label4.Text = "saved!";
            // 
            // cameralistbox
            // 
            cameralistbox.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cameralistbox.FormattingEnabled = true;
            cameralistbox.ItemHeight = 19;
            cameralistbox.Location = new Point(45, 211);
            cameralistbox.Name = "cameralistbox";
            cameralistbox.Size = new Size(278, 99);
            cameralistbox.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(45, 191);
            label5.Name = "label5";
            label5.Size = new Size(80, 19);
            label5.TabIndex = 8;
            label5.Text = "Cameras";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(45, 345);
            label6.Name = "label6";
            label6.Size = new Size(108, 19);
            label6.TabIndex = 9;
            label6.Text = "Controller";
            // 
            // ControllerBox
            // 
            ControllerBox.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ControllerBox.FormattingEnabled = true;
            ControllerBox.ItemHeight = 19;
            ControllerBox.Location = new Point(45, 367);
            ControllerBox.Name = "ControllerBox";
            ControllerBox.Size = new Size(403, 99);
            ControllerBox.TabIndex = 10;
            // 
            // openfoldercheckbox
            // 
            openfoldercheckbox.AutoSize = true;
            openfoldercheckbox.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            openfoldercheckbox.Location = new Point(45, 533);
            openfoldercheckbox.Name = "openfoldercheckbox";
            openfoldercheckbox.Size = new Size(231, 23);
            openfoldercheckbox.TabIndex = 11;
            openfoldercheckbox.Text = "Open Folder when saved";
            openfoldercheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dateradiobutton);
            groupBox2.Controls.Add(inmatedateradiobutton);
            groupBox2.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(493, 352);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(487, 114);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "File Name (if \"Do Not Override Mugshots\" is selected)";
            // 
            // dateradiobutton
            // 
            dateradiobutton.AutoSize = true;
            dateradiobutton.Location = new Point(25, 70);
            dateradiobutton.Name = "dateradiobutton";
            dateradiobutton.Size = new Size(104, 23);
            dateradiobutton.TabIndex = 1;
            dateradiobutton.TabStop = true;
            dateradiobutton.Text = "DateTime";
            dateradiobutton.UseVisualStyleBackColor = true;
            // 
            // inmatedateradiobutton
            // 
            inmatedateradiobutton.AutoSize = true;
            inmatedateradiobutton.Location = new Point(25, 33);
            inmatedateradiobutton.Name = "inmatedateradiobutton";
            inmatedateradiobutton.Size = new Size(275, 23);
            inmatedateradiobutton.TabIndex = 0;
            inmatedateradiobutton.TabStop = true;
            inmatedateradiobutton.Text = "DateTime + Inmate Identifier ";
            inmatedateradiobutton.UseVisualStyleBackColor = true;
            // 
            // combinepiccheckbox
            // 
            combinepiccheckbox.AutoSize = true;
            combinepiccheckbox.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            combinepiccheckbox.Location = new Point(23, 33);
            combinepiccheckbox.Name = "combinepiccheckbox";
            combinepiccheckbox.Size = new Size(248, 23);
            combinepiccheckbox.TabIndex = 13;
            combinepiccheckbox.Text = "Create Combined picture?";
            combinepiccheckbox.UseVisualStyleBackColor = true;
            combinepiccheckbox.CheckedChanged += combinepiccheckbox_CheckedChanged;
            // 
            // widthbox
            // 
            widthbox.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            widthbox.Location = new Point(111, 107);
            widthbox.Name = "widthbox";
            widthbox.Size = new Size(115, 26);
            widthbox.TabIndex = 14;
            // 
            // heightbox
            // 
            heightbox.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightbox.Location = new Point(111, 76);
            heightbox.Name = "heightbox";
            heightbox.Size = new Size(115, 26);
            heightbox.TabIndex = 15;
            heightbox.KeyPress += combinekeypress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(36, 110);
            label7.Name = "label7";
            label7.Size = new Size(69, 19);
            label7.TabIndex = 16;
            label7.Text = "Width :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(28, 79);
            label8.Name = "label8";
            label8.Size = new Size(77, 19);
            label8.TabIndex = 17;
            label8.Text = "Height :";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(232, 80);
            label9.Name = "label9";
            label9.Size = new Size(53, 19);
            label9.TabIndex = 18;
            label9.Text = "pixel";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(232, 110);
            label10.Name = "label10";
            label10.Size = new Size(53, 19);
            label10.TabIndex = 19;
            label10.Text = "pixel";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(combinepiccheckbox);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(widthbox);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(heightbox);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label7);
            groupBox3.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(490, 482);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(339, 170);
            groupBox3.TabIndex = 20;
            groupBox3.TabStop = false;
            groupBox3.Text = "Combine Mugshots";
            // 
            // saveSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1119, 714);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(openfoldercheckbox);
            Controls.Add(ControllerBox);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(cameralistbox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(saveSettingsButton);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(changePathButton);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "saveSettings";
            Text = "AUTOMUGSHOT - SETTINGS";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
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
        private ListBox cameralistbox;
        private Label label5;
        private Label label6;
        private ListBox ControllerBox;
        private CheckBox openfoldercheckbox;
        private GroupBox groupBox2;
        private RadioButton dateradiobutton;
        private RadioButton inmatedateradiobutton;
        private CheckBox combinepiccheckbox;
        private TextBox widthbox;
        private TextBox heightbox;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private GroupBox groupBox3;
    }
}