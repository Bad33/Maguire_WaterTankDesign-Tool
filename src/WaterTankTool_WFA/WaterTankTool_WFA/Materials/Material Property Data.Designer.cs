namespace WaterTankTool_WFA
{
    partial class Material_Property_Data
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
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            groupBox3 = new GroupBox();
            numericUpDown4 = new NumericUpDown();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label4 = new Label();
            label3 = new Label();
            numericUpDown3 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            button1 = new Button();
            button2 = new Button();
            label11 = new Label();
            label12 = new Label();
            richTextBox1 = new RichTextBox();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(18, 7);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(338, 74);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "General Data";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Steel" });
            comboBox2.Location = new Point(185, 45);
            comboBox2.Margin = new Padding(2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(136, 23);
            comboBox2.TabIndex = 3;
            comboBox2.Text = "Steel";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(184, 18);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(137, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 46);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 1;
            label2.Text = "Material Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 22);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 0;
            label1.Text = "Material Name";
            label1.Click += label1_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(numericUpDown4);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(numericUpDown3);
            groupBox3.Controls.Add(numericUpDown2);
            groupBox3.Controls.Add(numericUpDown1);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new Point(18, 85);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(338, 133);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Property Data";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(185, 98);
            numericUpDown4.Margin = new Padding(2);
            numericUpDown4.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(106, 23);
            numericUpDown4.TabIndex = 17;
            numericUpDown4.Value = new decimal(new int[] { 46, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(300, 100);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(22, 15);
            label10.TabIndex = 16;
            label10.Text = "Ksi";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(300, 72);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(22, 15);
            label9.TabIndex = 15;
            label9.Text = "Ksi";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(300, 46);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(22, 15);
            label8.TabIndex = 14;
            label8.Text = "Ksi";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(300, 20);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(24, 15);
            label4.TabIndex = 13;
            label4.Text = "Pcf";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 102);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(124, 15);
            label3.TabIndex = 12;
            label3.Text = "Tensile Ultimate Stress";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(185, 71);
            numericUpDown3.Margin = new Padding(2);
            numericUpDown3.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(106, 23);
            numericUpDown3.TabIndex = 11;
            numericUpDown3.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(185, 44);
            numericUpDown2.Margin = new Padding(2);
            numericUpDown2.Maximum = new decimal(new int[] { 40000, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(106, 23);
            numericUpDown2.TabIndex = 10;
            numericUpDown2.Value = new decimal(new int[] { 29000, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(184, 19);
            numericUpDown1.Margin = new Padding(2);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(106, 23);
            numericUpDown1.TabIndex = 9;
            numericUpDown1.Value = new decimal(new int[] { 490, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(18, 74);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(105, 15);
            label7.TabIndex = 8;
            label7.Text = "Tensile Yield Stress";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 48);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(131, 15);
            label6.TabIndex = 7;
            label6.Text = "Modulus of Elasticity Es";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 22);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 6;
            label5.Text = "Density";
            // 
            // button1
            // 
            button1.Location = new Point(277, 273);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(78, 26);
            button1.TabIndex = 4;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(190, 273);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(78, 26);
            button2.TabIndex = 5;
            button2.Text = "Ok";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label11.Location = new Point(27, 237);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(38, 13);
            label11.TabIndex = 6;
            label11.Text = "Note : ";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label12.Location = new Point(70, 233);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(0, 13);
            label12.TabIndex = 7;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(70, 233);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(286, 25);
            richTextBox1.TabIndex = 8;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // Material_Property_Data
            // 
            AcceptButton = button2;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button1;
            ClientSize = new Size(372, 307);
            Controls.Add(richTextBox1);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2);
            Name = "Material_Property_Data";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Material Property Data";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private Button button1;
        private Button button2;
        private Label label2;
        private Label label1;
        private Label label7;
        private Label label6;
        private Label label5;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBox2;
        private TextBox textBox1;
        private Label label8;
        private Label label4;
        private Label label3;
        private NumericUpDown numericUpDown4;
        private Label label10;
        private Label label9;
        private Label label11;
        private Label label12;
        private RichTextBox richTextBox1;
    }
}