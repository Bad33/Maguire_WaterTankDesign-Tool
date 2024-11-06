namespace WaterTankTool_WFA.Load
{
    partial class Seismic
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
            groupBox1 = new GroupBox();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            comboBox1 = new ComboBox();
            textBox10 = new TextBox();
            textBox9 = new TextBox();
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericUpDown2);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox10);
            groupBox1.Controls.Add(textBox9);
            groupBox1.Controls.Add(textBox8);
            groupBox1.Controls.Add(textBox7);
            groupBox1.Controls.Add(textBox6);
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(25, 26);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(342, 544);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Properties";
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numericUpDown2.Location = new Point(127, 107);
            numericUpDown2.Maximum = new decimal(new int[] { 125, 0, 0, 131072 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(149, 31);
            numericUpDown2.TabIndex = 28;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numericUpDown1.Location = new Point(126, 54);
            numericUpDown1.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 31);
            numericUpDown1.TabIndex = 27;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(286, 443);
            label16.Name = "label16";
            label16.Size = new Size(38, 25);
            label16.TabIndex = 26;
            label16.Text = "%g";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(287, 388);
            label15.Name = "label15";
            label15.Size = new Size(37, 25);
            label15.TabIndex = 25;
            label15.Text = "sec";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(283, 342);
            label14.Name = "label14";
            label14.Size = new Size(38, 25);
            label14.TabIndex = 24;
            label14.Text = "%g";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(283, 298);
            label13.Name = "label13";
            label13.Size = new Size(38, 25);
            label13.TabIndex = 23;
            label13.Text = "%g";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(294, 110);
            label12.Name = "label12";
            label12.Size = new Size(38, 25);
            label12.TabIndex = 22;
            label12.Text = "%g";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(293, 63);
            label11.Name = "label11";
            label11.Size = new Size(38, 25);
            label11.TabIndex = 21;
            label11.Text = "%g";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "A", "B", "C", "D", "E", "F" });
            comboBox1.Location = new Point(126, 155);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(150, 33);
            comboBox1.TabIndex = 20;
            comboBox1.Text = "Select Site Class";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox10
            // 
            textBox10.Location = new Point(126, 483);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(150, 31);
            textBox10.TabIndex = 19;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(126, 440);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(150, 31);
            textBox9.TabIndex = 18;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(126, 388);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(150, 31);
            textBox8.TabIndex = 17;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(126, 339);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(150, 31);
            textBox7.TabIndex = 16;
            textBox7.TextChanged += textBox7_TextChanged;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(126, 295);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(150, 31);
            textBox6.TabIndex = 15;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(126, 251);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(150, 31);
            textBox5.TabIndex = 14;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(126, 207);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(150, 31);
            textBox4.TabIndex = 13;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 483);
            label10.Name = "label10";
            label10.Size = new Size(31, 25);
            label10.TabIndex = 9;
            label10.Text = "Cs";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(20, 440);
            label9.Name = "label9";
            label9.Size = new Size(31, 25);
            label9.TabIndex = 8;
            label9.Text = "Sa";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 388);
            label8.Name = "label8";
            label8.Size = new Size(25, 25);
            label8.TabIndex = 7;
            label8.Text = "Ti";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 339);
            label7.Name = "label7";
            label7.Size = new Size(41, 25);
            label7.TabIndex = 6;
            label7.Text = "Sds";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 295);
            label6.Name = "label6";
            label6.Size = new Size(43, 25);
            label6.TabIndex = 5;
            label6.Text = "Sd1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 251);
            label5.Name = "label5";
            label5.Size = new Size(29, 25);
            label5.TabIndex = 4;
            label5.Text = "Fa";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 207);
            label4.Name = "label4";
            label4.Size = new Size(30, 25);
            label4.TabIndex = 3;
            label4.Text = "Fv";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 158);
            label3.Name = "label3";
            label3.Size = new Size(86, 25);
            label3.TabIndex = 2;
            label3.Text = "Site Class";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 107);
            label2.Name = "label2";
            label2.Size = new Size(30, 25);
            label2.TabIndex = 1;
            label2.Text = "Ss";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 60);
            label1.Name = "label1";
            label1.Size = new Size(32, 25);
            label1.TabIndex = 0;
            label1.Text = "S1";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(119, 596);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 1;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(255, 596);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 2;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // Seismic
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(396, 655);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Name = "Seismic";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seismic Load";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
        private Label label1;
        private TextBox textBox10;
        private TextBox textBox9;
        private TextBox textBox8;
        private TextBox textBox7;
        private TextBox textBox6;
        private TextBox textBox5;
        private TextBox textBox4;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox comboBox1;
        private Label label11;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
    }
}