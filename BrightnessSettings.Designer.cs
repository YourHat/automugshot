namespace automugshot
{
    partial class BrightnessSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrightnessSettings));
            pictureBox1 = new PictureBox();
            brighterbutton = new Button();
            darkerbutton = new Button();
            button3 = new Button();
            saveasdefaultbutton = new Button();
            AdminButton = new Button();
            lowerssbutton = new Button();
            higherssbutton = new Button();
            lowerflbutton = new Button();
            higherflbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(960, 540);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // brighterbutton
            // 
            brighterbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            brighterbutton.Location = new Point(978, 126);
            brighterbutton.Name = "brighterbutton";
            brighterbutton.Size = new Size(175, 161);
            brighterbutton.TabIndex = 4;
            brighterbutton.Text = "Brighter";
            brighterbutton.UseVisualStyleBackColor = true;
            brighterbutton.Click += button1_Click;
            // 
            // darkerbutton
            // 
            darkerbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkerbutton.Location = new Point(978, 293);
            darkerbutton.Name = "darkerbutton";
            darkerbutton.Size = new Size(175, 161);
            darkerbutton.TabIndex = 5;
            darkerbutton.Text = "Darker";
            darkerbutton.UseVisualStyleBackColor = true;
            darkerbutton.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.Red;
            button3.Location = new Point(978, 12);
            button3.Name = "button3";
            button3.Size = new Size(175, 42);
            button3.TabIndex = 6;
            button3.Text = "Close";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // saveasdefaultbutton
            // 
            saveasdefaultbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            saveasdefaultbutton.ForeColor = SystemColors.ControlText;
            saveasdefaultbutton.Location = new Point(978, 558);
            saveasdefaultbutton.Name = "saveasdefaultbutton";
            saveasdefaultbutton.Size = new Size(175, 57);
            saveasdefaultbutton.TabIndex = 7;
            saveasdefaultbutton.Text = "See Camera Data";
            saveasdefaultbutton.UseVisualStyleBackColor = true;
            saveasdefaultbutton.Click += saveasdefaultbutton_Click;
            // 
            // AdminButton
            // 
            AdminButton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AdminButton.ForeColor = SystemColors.ControlText;
            AdminButton.Location = new Point(12, 558);
            AdminButton.Name = "AdminButton";
            AdminButton.Size = new Size(69, 57);
            AdminButton.TabIndex = 8;
            AdminButton.Text = "Admin";
            AdminButton.UseVisualStyleBackColor = true;
            AdminButton.Click += AdminButton_Click;
            // 
            // lowerssbutton
            // 
            lowerssbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lowerssbutton.ForeColor = SystemColors.ControlText;
            lowerssbutton.Location = new Point(87, 558);
            lowerssbutton.Name = "lowerssbutton";
            lowerssbutton.Size = new Size(210, 57);
            lowerssbutton.TabIndex = 9;
            lowerssbutton.Text = "Lower Shutter Speed";
            lowerssbutton.UseVisualStyleBackColor = true;
            lowerssbutton.Click += lowerssbutton_Click;
            // 
            // higherssbutton
            // 
            higherssbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            higherssbutton.ForeColor = SystemColors.ControlText;
            higherssbutton.Location = new Point(303, 558);
            higherssbutton.Name = "higherssbutton";
            higherssbutton.Size = new Size(210, 57);
            higherssbutton.TabIndex = 10;
            higherssbutton.Text = "Higher Shutter Speed";
            higherssbutton.UseVisualStyleBackColor = true;
            higherssbutton.Click += higherssbutton_Click;
            // 
            // lowerflbutton
            // 
            lowerflbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lowerflbutton.ForeColor = SystemColors.ControlText;
            lowerflbutton.Location = new Point(535, 558);
            lowerflbutton.Name = "lowerflbutton";
            lowerflbutton.Size = new Size(210, 57);
            lowerflbutton.TabIndex = 11;
            lowerflbutton.Text = "Lower Focal Length";
            lowerflbutton.UseVisualStyleBackColor = true;
            lowerflbutton.Click += lowerflbutton_Click;
            // 
            // higherflbutton
            // 
            higherflbutton.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            higherflbutton.ForeColor = SystemColors.ControlText;
            higherflbutton.Location = new Point(751, 558);
            higherflbutton.Name = "higherflbutton";
            higherflbutton.Size = new Size(210, 57);
            higherflbutton.TabIndex = 12;
            higherflbutton.Text = "Higher Focal Length";
            higherflbutton.UseVisualStyleBackColor = true;
            higherflbutton.Click += higherflbutton_Click;
            // 
            // BrightnessSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 627);
            Controls.Add(higherflbutton);
            Controls.Add(lowerflbutton);
            Controls.Add(higherssbutton);
            Controls.Add(lowerssbutton);
            Controls.Add(AdminButton);
            Controls.Add(saveasdefaultbutton);
            Controls.Add(button3);
            Controls.Add(darkerbutton);
            Controls.Add(brighterbutton);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BrightnessSettings";
            Text = "AUTOMUGSHOT - BRIGHTNESS SETTINGS";
            Load += BrightnessSettings_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button brighterbutton;
        private Button darkerbutton;
        private Button button3;
        private Button saveasdefaultbutton;
        private Button AdminButton;
        private Button lowerssbutton;
        private Button higherssbutton;
        private Button lowerflbutton;
        private Button higherflbutton;
    }
}