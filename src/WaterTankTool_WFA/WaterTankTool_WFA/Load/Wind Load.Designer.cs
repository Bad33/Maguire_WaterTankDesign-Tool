namespace WaterTankTool_WFA.Load
{
    partial class Wind_Load
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
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            label11 = new Label();
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            comboBox1 = new ComboBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            richTextBox2 = new RichTextBox();
            button3 = new Button();
            groupBox3 = new GroupBox();
            richTextBox1 = new RichTextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(194, 329);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(78, 20);
            button1.TabIndex = 0;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(290, 329);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(78, 20);
            button2.TabIndex = 1;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(textBox8);
            groupBox1.Controls.Add(textBox7);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox6);
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(8, 18);
            groupBox1.Margin = new Padding(2, 2, 2, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 2, 2, 2);
            groupBox1.Size = new Size(213, 298);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Properties";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label11.Location = new Point(176, 242);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(13, 16);
            label11.TabIndex = 19;
            label11.Text = "ft";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(97, 266);
            textBox8.Margin = new Padding(2, 2, 2, 2);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(74, 23);
            textBox8.TabIndex = 18;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(97, 239);
            textBox7.Margin = new Padding(2, 2, 2, 2);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(74, 23);
            textBox7.TabIndex = 17;
            textBox7.TextChanged += textBox7_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(17, 266);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(14, 15);
            label10.TabIndex = 16;
            label10.Text = "α";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 239);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(21, 15);
            label9.TabIndex = 15;
            label9.Text = "Zg";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label8.Location = new Point(174, 211);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(25, 16);
            label8.TabIndex = 14;
            label8.Text = "mph";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "C", "D" });
            comboBox1.Location = new Point(97, 27);
            comboBox1.Margin = new Padding(2, 2, 2, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(74, 23);
            comboBox1.TabIndex = 13;
            comboBox1.Text = "Select Exposure";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(97, 209);
            textBox6.Margin = new Padding(2, 2, 2, 2);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(74, 23);
            textBox6.TabIndex = 12;
            textBox6.Text = "90";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(97, 178);
            textBox5.Margin = new Padding(2, 2, 2, 2);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(74, 23);
            textBox5.TabIndex = 11;
            textBox5.Text = "1.15";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(97, 148);
            textBox4.Margin = new Padding(2, 2, 2, 2);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(74, 23);
            textBox4.TabIndex = 10;
            textBox4.Text = "1.0";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(97, 116);
            textBox3.Margin = new Padding(2, 2, 2, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(74, 23);
            textBox3.TabIndex = 9;
            textBox3.Text = "0.85";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(97, 86);
            textBox2.Margin = new Padding(2, 2, 2, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(74, 23);
            textBox2.TabIndex = 8;
            textBox2.Text = "1.0";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(97, 56);
            textBox1.Margin = new Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(74, 23);
            textBox1.TabIndex = 7;
            textBox1.Text = "1.0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 211);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(14, 15);
            label7.TabIndex = 6;
            label7.Text = "V";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 180);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(10, 15);
            label6.TabIndex = 5;
            label6.Text = "I";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 150);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(15, 15);
            label5.TabIndex = 4;
            label5.Text = "G";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 118);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(21, 15);
            label4.TabIndex = 3;
            label4.Text = "Kd";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 88);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(20, 15);
            label3.TabIndex = 2;
            label3.Text = "Ke";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 60);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(23, 15);
            label2.TabIndex = 1;
            label2.Text = "Kzt";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 27);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 0;
            label1.Text = "Exposure";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(richTextBox2);
            groupBox2.Controls.Add(button3);
            groupBox2.Location = new Point(225, 18);
            groupBox2.Margin = new Padding(2, 2, 2, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 2, 2, 2);
            groupBox2.Size = new Size(156, 153);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Calculation";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(11, 75);
            richTextBox2.Margin = new Padding(2, 2, 2, 2);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(133, 50);
            richTextBox2.TabIndex = 1;
            richTextBox2.Text = "";
            richTextBox2.TextChanged += richTextBox2_TextChanged;
            // 
            // button3
            // 
            button3.Location = new Point(11, 35);
            button3.Margin = new Padding(2, 2, 2, 2);
            button3.Name = "button3";
            button3.Size = new Size(132, 20);
            button3.TabIndex = 0;
            button3.Text = "Calculate";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(richTextBox1);
            groupBox3.Location = new Point(225, 175);
            groupBox3.Margin = new Padding(2, 2, 2, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2, 2, 2, 2);
            groupBox3.Size = new Size(156, 141);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Notes";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(11, 24);
            richTextBox1.Margin = new Padding(2, 2, 2, 2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(133, 88);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // Wind_Load
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = button2;
            ClientSize = new Size(390, 366);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            Name = "Wind_Load";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Wind Load";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private RichTextBox richTextBox2;
        private Button button3;
        private RichTextBox richTextBox1;
        private ComboBox comboBox1;
        private TextBox textBox6;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox7;
        private Label label10;
        private Label label9;
        private Label label8;
        private TextBox textBox8;
        private Label label11;
    }
}