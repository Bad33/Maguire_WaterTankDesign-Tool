namespace WaterTankTool_WFA
{
    partial class TanksList
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
            comboBox1 = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "50,000 gallon", "75,000 gallon", "100,000 gallon", "125,000 gallon", "150,000 gallon", "200,000 gallon", "250,000 gallon", "300,000 gallon", "400,000 gallon", "500,000 gallon", "600,000 gallon", "750,000 gallon" });
            comboBox1.Location = new Point(146, 28);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(152, 23);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Select Tank Capacity";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 28);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 1;
            label1.Text = "Select Tank Type :";
            // 
            // button1
            // 
            button1.Location = new Point(146, 80);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(69, 29);
            button1.TabIndex = 2;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(229, 80);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(69, 29);
            button2.TabIndex = 3;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // TanksList
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(316, 120);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "TanksList";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tanks List";
            Load += TanksList_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private Button button1;
        private Button button2;
    }
}