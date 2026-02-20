namespace WaterTankTool_WFA.Solver
{
    partial class Help
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.GradientInactiveCaption;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(223, 15);
            label1.TabIndex = 0;
            label1.Text = "Standard check is : 0.75 <= check <= 0.95";
            // 
            // label2
            // 
            label2.BackColor = SystemColors.GradientInactiveCaption;
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Location = new Point(12, 68);
            label2.Name = "label2";
            label2.Size = new Size(223, 41);
            label2.TabIndex = 1;
            label2.Text = "If the check value is over 0.95 or less than 0.75, the check value will be shown in Red.";
            // 
            // label3
            // 
            label3.BackColor = SystemColors.GradientInactiveCaption;
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Location = new Point(12, 120);
            label3.Name = "label3";
            label3.Size = new Size(223, 43);
            label3.TabIndex = 2;
            label3.Text = "User can Double click on the cell with the Red value to increase/decrease the thickness of the given segment.";
            // 
            // label4
            // 
            label4.BackColor = SystemColors.GradientInactiveCaption;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(11, 177);
            label4.Name = "label4";
            label4.Size = new Size(224, 55);
            label4.TabIndex = 3;
            label4.Text = "If the check value is less than 0.75 thickness should be increased, if check value is more than 0.95, thickness should be decreased.";
            // 
            // label5
            // 
            label5.BackColor = SystemColors.GradientInactiveCaption;
            label5.BorderStyle = BorderStyle.Fixed3D;
            label5.Location = new Point(11, 245);
            label5.Name = "label5";
            label5.Size = new Size(224, 52);
            label5.TabIndex = 4;
            label5.Text = "It is recommended to gradually increase/decrease the thickness of the segment by 25% each time.";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.GradientInactiveCaption;
            label6.BorderStyle = BorderStyle.Fixed3D;
            label6.Location = new Point(52, 9);
            label6.Name = "label6";
            label6.Size = new Size(133, 15);
            label6.TabIndex = 5;
            label6.Text = "Check Table Information";
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(255, 306);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Help";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Help";
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
    }
}