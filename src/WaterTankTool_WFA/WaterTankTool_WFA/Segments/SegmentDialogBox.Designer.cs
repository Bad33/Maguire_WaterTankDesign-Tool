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
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            groupBox2 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox3 = new GroupBox();
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
            maskedTextBox2.Location = new Point(155, 37);
            maskedTextBox2.Name = "maskedTextBox2";
            maskedTextBox2.Size = new Size(124, 31);
            maskedTextBox2.TabIndex = 6;
            maskedTextBox2.TextChanged += InputFields_TextChanged;
            // 
            // maskedTextBox3
            // 
            maskedTextBox3.Location = new Point(155, 85);
            maskedTextBox3.Name = "maskedTextBox3";
            maskedTextBox3.Size = new Size(124, 31);
            maskedTextBox3.TabIndex = 7;
            maskedTextBox3.TextChanged += InputFields_TextChanged;
            // 
            // maskedTextBox4
            // 
            maskedTextBox4.Location = new Point(499, 88);
            maskedTextBox4.Name = "maskedTextBox4";
            maskedTextBox4.Size = new Size(108, 31);
            maskedTextBox4.TabIndex = 8;
            maskedTextBox4.TextChanged += InputFields_TextChanged;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(499, 40);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(108, 31);
            maskedTextBox1.TabIndex = 9;
            maskedTextBox1.TextChanged += InputFields_TextChanged;
            // 
            // Save
            // 
            Save.BackColor = SystemColors.Window;
            Save.Location = new Point(452, 552);
            Save.Name = "Save";
            Save.Size = new Size(91, 34);
            Save.TabIndex = 11;
            Save.Text = "Confirm";
            Save.UseVisualStyleBackColor = false;
            Save.Click += Save_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Window;
            button2.Location = new Point(573, 552);
            button2.Name = "button2";
            button2.Size = new Size(97, 34);
            button2.TabIndex = 12;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Location = new Point(12, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(658, 86);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Segment";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 44);
            label1.Name = "label1";
            label1.Size = new Size(135, 25);
            label1.TabIndex = 0;
            label1.Text = "Segment Name";
            label1.Click += label1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(198, 41);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(252, 29);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(maskedTextBox4);
            groupBox2.Controls.Add(maskedTextBox1);
            groupBox2.Controls.Add(maskedTextBox2);
            groupBox2.Controls.Add(maskedTextBox3);
            groupBox2.Location = new Point(12, 122);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(658, 136);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Input";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(358, 88);
            label6.Name = "label6";
            label6.Size = new Size(106, 25);
            label6.TabIndex = 13;
            label6.Text = "Height Final";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(358, 40);
            label5.Name = "label5";
            label5.Size = new Size(112, 25);
            label5.TabIndex = 12;
            label5.Text = "Height Initial";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 91);
            label4.Name = "label4";
            label4.Size = new Size(87, 25);
            label4.TabIndex = 11;
            label4.Text = "Thickness";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 43);
            label3.Name = "label3";
            label3.Size = new Size(84, 25);
            label3.TabIndex = 10;
            label3.Text = "Diameter";
            // 
            // groupBox3
            // 
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
            groupBox3.Location = new Point(12, 264);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(658, 273);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "Properties";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(358, 227);
            label15.Name = "label15";
            label15.Size = new Size(103, 25);
            label15.TabIndex = 19;
            label15.Text = "Mwindbase";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(486, 227);
            textBox10.Name = "textBox10";
            textBox10.ReadOnly = true;
            textBox10.Size = new Size(121, 31);
            textBox10.TabIndex = 18;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(486, 185);
            textBox9.Name = "textBox9";
            textBox9.ReadOnly = true;
            textBox9.Size = new Size(121, 31);
            textBox9.TabIndex = 17;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(486, 139);
            textBox8.Name = "textBox8";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(121, 31);
            textBox8.TabIndex = 16;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(486, 95);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new Size(121, 31);
            textBox7.TabIndex = 15;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(486, 48);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(121, 31);
            textBox6.TabIndex = 14;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(155, 227);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(124, 31);
            textBox5.TabIndex = 13;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(155, 182);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(124, 31);
            textBox4.TabIndex = 12;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(153, 133);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(126, 31);
            textBox3.TabIndex = 11;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(153, 89);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(126, 31);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(153, 45);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(126, 31);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox1_TextChanged_1;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(358, 185);
            label14.Name = "label14";
            label14.Size = new Size(122, 25);
            label14.TabIndex = 8;
            label14.Text = "Fwindlocation";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(358, 142);
            label13.Name = "label13";
            label13.Size = new Size(59, 25);
            label13.TabIndex = 7;
            label13.Text = "Fwind";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(358, 101);
            label12.Name = "label12";
            label12.Size = new Size(40, 25);
            label12.TabIndex = 6;
            label12.Text = "Qzf";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(358, 54);
            label11.Name = "label11";
            label11.Size = new Size(38, 25);
            label11.TabIndex = 5;
            label11.Text = "Qzi";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(16, 230);
            label10.Name = "label10";
            label10.Size = new Size(36, 25);
            label10.TabIndex = 4;
            label10.Text = "Kzf";
            // 
            // label9
            // 
            label9.Location = new Point(16, 187);
            label9.Name = "label9";
            label9.Size = new Size(100, 23);
            label9.TabIndex = 20;
            label9.Text = "Kzi";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(16, 139);
            label8.Name = "label8";
            label8.Size = new Size(53, 25);
            label8.TabIndex = 2;
            label8.Text = "COM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 95);
            label2.Name = "label2";
            label2.Size = new Size(127, 25);
            label2.TabIndex = 1;
            label2.Text = "Projected Area";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 48);
            label7.Name = "label7";
            label7.Size = new Size(68, 25);
            label7.TabIndex = 0;
            label7.Text = "Weight";
            // 
            // SegmentDialogBox
            // 
            AcceptButton = Save;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = button2;
            ClientSize = new Size(682, 608);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(Save);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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
    }
}