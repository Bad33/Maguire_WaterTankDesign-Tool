namespace WaterTankTool_WFA.Load
{
    partial class Snow_Load
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            button1 = new Button();
            button2 = new Button();
            richTextBox1 = new RichTextBox();
            groupBox1 = new GroupBox();
            label7 = new Label();
            label8 = new Label();
            comboBox1 = new ComboBox();
            label9 = new Label();
            textBox5 = new TextBox();
            label10 = new Label();
            label11 = new Label();
            comboBox2 = new ComboBox();
            label12 = new Label();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            label13 = new Label();
            textBox4 = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 64);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 0;
            label1.Text = "Gound Snow Load";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 268);
            label2.Name = "label2";
            label2.Size = new Size(132, 15);
            label2.TabIndex = 1;
            label2.Text = "Area Subjected to Snow";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(167, 61);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(167, 159);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 31;
            textBox2.Text = "1.2";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(273, 64);
            label3.Name = "label3";
            label3.Size = new Size(23, 15);
            label3.TabIndex = 4;
            label3.Text = "psf";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(280, 268);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 5;
            label4.Text = "ft²";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 301);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 6;
            label5.Text = "Total Snow Load";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(167, 298);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 7;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(273, 301);
            label6.Name = "label6";
            label6.Size = new Size(29, 15);
            label6.TabIndex = 8;
            label6.Text = "Kips";
            // 
            // button1
            // 
            button1.Location = new Point(143, 408);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(224, 408);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(19, 19);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(245, 30);
            richTextBox1.TabIndex = 12;
            richTextBox1.Text = "Height to consider is Roof's height + half of Top Knuckle's height";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Location = new Point(25, 334);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(270, 68);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Notes";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(26, 33);
            label7.Name = "label7";
            label7.Size = new Size(108, 15);
            label7.TabIndex = 14;
            label7.Text = "Height To Consider";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(26, 95);
            label8.Name = "label8";
            label8.Size = new Size(79, 15);
            label8.TabIndex = 16;
            label8.Text = "Risk Category";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "II", "III", "IV" });
            comboBox1.Location = new Point(167, 95);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(100, 23);
            comboBox1.TabIndex = 3;
            comboBox1.Text = "Select Risk Category";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(26, 127);
            label9.Name = "label9";
            label9.Size = new Size(100, 15);
            label9.TabIndex = 18;
            label9.Text = "Impotance Factor";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(167, 127);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 4;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(26, 159);
            label10.Name = "label10";
            label10.Size = new Size(86, 15);
            label10.TabIndex = 20;
            label10.Text = "Thermal Factor";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(28, 196);
            label11.Name = "label11";
            label11.Size = new Size(55, 15);
            label11.TabIndex = 21;
            label11.Text = "Exposure";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "C", "D" });
            comboBox2.Location = new Point(167, 193);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(100, 23);
            comboBox2.TabIndex = 5;
            comboBox2.Text = "Select Exposure";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(28, 234);
            label12.Name = "label12";
            label12.Size = new Size(91, 15);
            label12.TabIndex = 23;
            label12.Text = "Exposure Factor";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(167, 231);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 6;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(167, 265);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 25;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label13.Location = new Point(277, 35);
            label13.Name = "label13";
            label13.Size = new Size(14, 13);
            label13.TabIndex = 26;
            label13.Text = "ft";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(167, 30);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 1;
            textBox4.TextChanged += textBox4_TextChanged_2;
            // 
            // Snow_Load
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(322, 439);
            Controls.Add(textBox4);
            Controls.Add(label13);
            Controls.Add(label5);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(label12);
            Controls.Add(comboBox2);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(textBox5);
            Controls.Add(label9);
            Controls.Add(comboBox1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Snow_Load";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Snow Load";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox3;
        private Label label6;
        private Button button1;
        private Button button2;
        private RichTextBox richTextBox1;
        private GroupBox groupBox1;
        private Label label7;
        private Label label8;
        private ComboBox comboBox1;
        private Label label9;
        private TextBox textBox5;
        private Label label10;
        private Label label11;
        private ComboBox comboBox2;
        private Label label12;
        private TextBox textBox6;
        private TextBox textBox7;
        private Label label13;
        private TextBox textBox4;
    }
}