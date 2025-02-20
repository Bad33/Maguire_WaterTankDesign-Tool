namespace WaterTankTool_WFA
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
            panel1 = new Panel();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 266);
            panel1.TabIndex = 0;
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(32, 164);
            label8.Name = "label8";
            label8.Size = new Size(315, 30);
            label8.TabIndex = 7;
            label8.Text = "User can only add 1 Tank but can add multiple cylinder and base segments";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(32, 110);
            label7.Name = "label7";
            label7.Size = new Size(249, 13);
            label7.TabIndex = 6;
            label7.Text = "User Don't need to add Dead Load and Water Load";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(59, 68);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 5;
            // 
            // label5
            // 
            label5.Location = new Point(14, 205);
            label5.Name = "label5";
            label5.Size = new Size(333, 32);
            label5.TabIndex = 4;
            label5.Text = "4. Once desired segments are added User can click on Solve button in Define Tab to see the output table.";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            label4.Location = new Point(14, 137);
            label4.Name = "label4";
            label4.Size = new Size(333, 15);
            label4.TabIndex = 3;
            label4.Text = "3.Start Adding the segments from the Geometry in Define tab.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 83);
            label3.Name = "label3";
            label3.Size = new Size(199, 15);
            label3.TabIndex = 2;
            label3.Text = "2. Set the Loads from the Define Tab.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 51);
            label2.Name = "label2";
            label2.Size = new Size(202, 15);
            label2.TabIndex = 1;
            label2.Text = "1. Add the Materials from Define Tab.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 17);
            label1.Name = "label1";
            label1.Size = new Size(245, 15);
            label1.TabIndex = 0;
            label1.Text = "Below are the steps for using this Application:";
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new Size(374, 296);
            Controls.Add(panel1);
            DoubleBuffered = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Help";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Help";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Label label2;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label8;
    }
}