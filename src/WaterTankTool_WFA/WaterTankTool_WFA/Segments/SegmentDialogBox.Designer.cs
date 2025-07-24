namespace WaterTankTool_WFA
{
    partial class SegmentDialogBox
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
            maskedTextBox2 = new MaskedTextBox();
            maskedTextBox3 = new MaskedTextBox();
            maskedTextBox4 = new MaskedTextBox();
            maskedTextBox1 = new MaskedTextBox();
            Save = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            groupBox2 = new GroupBox();
            maskedTextBox5 = new MaskedTextBox();
            label26 = new Label();
            label25 = new Label();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox3 = new GroupBox();
            label24 = new Label();
            label23 = new Label();
            label22 = new Label();
            label21 = new Label();
            label20 = new Label();
            label15 = new Label();
            textBox10 = new TextBox();
            textBox9 = new TextBox();
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label2 = new Label();
            label7 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // maskedTextBox2
            // 
            maskedTextBox2.Location = new Point(108, 22);
            maskedTextBox2.Margin = new Padding(2);
            maskedTextBox2.Name = "maskedTextBox2";
            maskedTextBox2.Size = new Size(88, 23);
            maskedTextBox2.TabIndex = 6;
            maskedTextBox2.MaskInputRejected += maskedTextBox2_MaskInputRejected;
            maskedTextBox2.TextChanged += InputFields_TextChanged;
            // 
            // maskedTextBox3
            // 
            maskedTextBox3.Location = new Point(108, 51);
            maskedTextBox3.Margin = new Padding(2);
            maskedTextBox3.Name = "maskedTextBox3";
            maskedTextBox3.Size = new Size(88, 23);
            maskedTextBox3.TabIndex = 7;
            maskedTextBox3.MaskInputRejected += maskedTextBox3_MaskInputRejected;
            maskedTextBox3.TextChanged += InputFields_TextChanged;
            // 
            // maskedTextBox4
            // 
            maskedTextBox4.Location = new Point(349, 53);
            maskedTextBox4.Margin = new Padding(2);
            maskedTextBox4.Name = "maskedTextBox4";
            maskedTextBox4.Size = new Size(77, 23);
            maskedTextBox4.TabIndex = 9;
            maskedTextBox4.TextChanged += InputFields_TextChanged;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(349, 24);
            maskedTextBox1.Margin = new Padding(2);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(77, 23);
            maskedTextBox1.TabIndex = 8;
            maskedTextBox1.TextChanged += InputFields_TextChanged;
            // 
            // Save
            // 
            Save.BackColor = SystemColors.Window;
            Save.Location = new Point(307, 361);
            Save.Margin = new Padding(2);
            Save.Name = "Save";
            Save.Size = new Size(73, 30);
            Save.TabIndex = 11;
            Save.Text = "Confirm";
            Save.UseVisualStyleBackColor = false;
            Save.Click += Save_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Window;
            button2.Location = new Point(393, 361);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(81, 30);
            button2.TabIndex = 12;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Location = new Point(8, 13);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(466, 52);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Segment";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "50,000 gallon", "75,000 gallon", "100,000 gallon", "125,000 gallon", "150,000 gallon", "200,000 gallon", "250,000 gallon", "300,000 gallon", "400,000 gallon", "500,000 gallon", "600,000 gallon", "750,000 gallon" });
            comboBox1.Location = new Point(139, 21);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(178, 23);
            comboBox1.TabIndex = 5;
            comboBox1.UseWaitCursor = true;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 23);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Segment Name";
            label1.Click += label1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(139, 20);
            richTextBox1.Margin = new Padding(2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(178, 24);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(maskedTextBox5);
            groupBox2.Controls.Add(label26);
            groupBox2.Controls.Add(label25);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(label18);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(maskedTextBox4);
            groupBox2.Controls.Add(maskedTextBox1);
            groupBox2.Controls.Add(maskedTextBox2);
            groupBox2.Controls.Add(maskedTextBox3);
            groupBox2.Location = new Point(8, 73);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2);
            groupBox2.Size = new Size(466, 110);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Input";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // maskedTextBox5
            // 
            maskedTextBox5.Location = new Point(107, 78);
            maskedTextBox5.Margin = new Padding(2);
            maskedTextBox5.Name = "maskedTextBox5";
            maskedTextBox5.Size = new Size(89, 23);
            maskedTextBox5.TabIndex = 21;
            maskedTextBox5.MaskInputRejected += maskedTextBox5_MaskInputRejected;
            maskedTextBox5.TextChanged += InputFields_TextChanged;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Arial Narrow", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label26.Location = new Point(203, 80);
            label26.Margin = new Padding(2, 0, 2, 0);
            label26.Name = "label26";
            label26.Size = new Size(14, 16);
            label26.TabIndex = 20;
            label26.Text = "in";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(11, 80);
            label25.Margin = new Padding(2, 0, 2, 0);
            label25.Name = "label25";
            label25.Size = new Size(58, 15);
            label25.TabIndex = 18;
            label25.Text = "Thickness";
            label25.Click += label25_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label19.Location = new Point(431, 55);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new Size(13, 16);
            label19.TabIndex = 17;
            label19.Text = "ft";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label18.Location = new Point(433, 26);
            label18.Margin = new Padding(2, 0, 2, 0);
            label18.Name = "label18";
            label18.Size = new Size(13, 16);
            label18.TabIndex = 16;
            label18.Text = "ft";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Arial Narrow", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label17.Location = new Point(204, 53);
            label17.Margin = new Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new Size(14, 16);
            label17.TabIndex = 15;
            label17.Text = "in";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Arial Narrow", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label16.Location = new Point(205, 25);
            label16.Margin = new Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new Size(13, 16);
            label16.TabIndex = 14;
            label16.Text = "ft";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(251, 59);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 13;
            label6.Text = "Height Final";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(251, 30);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 12;
            label5.Text = "Height Initial";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 54);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 11;
            label4.Text = "Thickness";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 26);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 10;
            label3.Text = "Diameter";
            label3.Click += label3_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label24);
            groupBox3.Controls.Add(label23);
            groupBox3.Controls.Add(label22);
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(textBox10);
            groupBox3.Controls.Add(textBox9);
            groupBox3.Controls.Add(textBox8);
            groupBox3.Controls.Add(textBox7);
            groupBox3.Controls.Add(textBox6);
            groupBox3.Controls.Add(textBox5);
            groupBox3.Controls.Add(textBox4);
            groupBox3.Controls.Add(textBox3);
            groupBox3.Controls.Add(textBox2);
            groupBox3.Controls.Add(textBox1);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label7);
            groupBox3.Location = new Point(8, 187);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(466, 164);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "Properties";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label24.Location = new Point(428, 140);
            label24.Margin = new Padding(2, 0, 2, 0);
            label24.Name = "label24";
            label24.Size = new Size(35, 16);
            label24.TabIndex = 25;
            label24.Text = "Kips-ft";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label23.Location = new Point(431, 112);
            label23.Margin = new Padding(2, 0, 2, 0);
            label23.Name = "label23";
            label23.Size = new Size(13, 16);
            label23.TabIndex = 24;
            label23.Text = "ft";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label22.Location = new Point(428, 86);
            label22.Margin = new Padding(2, 0, 2, 0);
            label22.Name = "label22";
            label22.Size = new Size(26, 16);
            label22.TabIndex = 23;
            label22.Text = "Kips";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label21.Location = new Point(205, 55);
            label21.Margin = new Padding(2, 0, 2, 0);
            label21.Name = "label21";
            label21.Size = new Size(16, 16);
            label21.TabIndex = 22;
            label21.Text = "ft²";
            label21.UseMnemonic = false;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Arial Narrow", 9F, FontStyle.Italic);
            label20.Location = new Point(203, 29);
            label20.Margin = new Padding(2, 0, 2, 0);
            label20.Name = "label20";
            label20.Size = new Size(26, 16);
            label20.TabIndex = 21;
            label20.Text = "Kips";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(251, 136);
            label15.Margin = new Padding(2, 0, 2, 0);
            label15.Name = "label15";
            label15.Size = new Size(68, 15);
            label15.TabIndex = 19;
            label15.Text = "Mwindbase";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(340, 136);
            textBox10.Margin = new Padding(2);
            textBox10.Name = "textBox10";
            textBox10.ReadOnly = true;
            textBox10.Size = new Size(86, 23);
            textBox10.TabIndex = 18;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(340, 111);
            textBox9.Margin = new Padding(2);
            textBox9.Name = "textBox9";
            textBox9.ReadOnly = true;
            textBox9.Size = new Size(86, 23);
            textBox9.TabIndex = 17;
            textBox9.TextChanged += textBox9_TextChanged;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(340, 83);
            textBox8.Margin = new Padding(2);
            textBox8.Name = "textBox8";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(86, 23);
            textBox8.TabIndex = 16;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(340, 57);
            textBox7.Margin = new Padding(2);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new Size(86, 23);
            textBox7.TabIndex = 15;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(340, 29);
            textBox6.Margin = new Padding(2);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(86, 23);
            textBox6.TabIndex = 14;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(108, 136);
            textBox5.Margin = new Padding(2);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(88, 23);
            textBox5.TabIndex = 13;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(108, 109);
            textBox4.Margin = new Padding(2);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(88, 23);
            textBox4.TabIndex = 12;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(107, 80);
            textBox3.Margin = new Padding(2);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(89, 23);
            textBox3.TabIndex = 11;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(107, 53);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(89, 23);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(107, 27);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(89, 23);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox1_TextChanged_1;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(251, 111);
            label14.Margin = new Padding(2, 0, 2, 0);
            label14.Name = "label14";
            label14.Size = new Size(82, 15);
            label14.TabIndex = 8;
            label14.Text = "Fwindlocation";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(251, 85);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new Size(39, 15);
            label13.TabIndex = 7;
            label13.Text = "Fwind";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(251, 61);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(23, 15);
            label12.TabIndex = 6;
            label12.Text = "qzf";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(251, 32);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(22, 15);
            label11.TabIndex = 5;
            label11.Text = "qzi";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 138);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(23, 15);
            label10.TabIndex = 4;
            label10.Text = "Kzf";
            // 
            // label9
            // 
            label9.Location = new Point(11, 112);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(70, 14);
            label9.TabIndex = 20;
            label9.Text = "Kzi";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(11, 83);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 2;
            label8.Text = "COM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 57);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 1;
            label2.Text = "Projected Area";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 29);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 0;
            label7.Text = "Weight";
            // 
            // SegmentDialogBox
            // 
            AcceptButton = Save;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = button2;
            ClientSize = new Size(482, 402);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(Save);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "SegmentDialogBox";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Segment Property";
            Load += SegmentDialogBox_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private MaskedTextBox maskedTextBox2;
        private MaskedTextBox maskedTextBox3;
        private MaskedTextBox maskedTextBox4;
        private MaskedTextBox maskedTextBox1;
        private Button Save;
        private Button button2;
        private GroupBox groupBox1;
        private RichTextBox richTextBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label7;
        private Label label2;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label15;
        private TextBox textBox10;
        private TextBox textBox9;
        private TextBox textBox8;
        private TextBox textBox7;
        private TextBox textBox6;
        private Label label16;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label21;
        private Label label20;
        private Label label24;
        private Label label23;
        private Label label22;
        private Label label26;
        private Label label25;
        private MaskedTextBox maskedTextBox5;
        private ComboBox comboBox1;
    }
}