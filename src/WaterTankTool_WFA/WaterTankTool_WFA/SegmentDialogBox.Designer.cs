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
            comboBox1 = new ComboBox();
            maskedTextBox2 = new MaskedTextBox();
            maskedTextBox3 = new MaskedTextBox();
            maskedTextBox4 = new MaskedTextBox();
            maskedTextBox1 = new MaskedTextBox();
            Save = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            comboBox2 = new ComboBox();
            label9 = new Label();
            label2 = new Label();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            groupBox2 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox3 = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            groupBox4 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Cylinder", "Cone", "Spheroid" });
            comboBox1.Location = new Point(198, 67);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(164, 33);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Cylinder";
            // 
            // maskedTextBox2
            // 
            maskedTextBox2.Location = new Point(155, 37);
            maskedTextBox2.Name = "maskedTextBox2";
            maskedTextBox2.Size = new Size(77, 31);
            maskedTextBox2.TabIndex = 6;
            // 
            // maskedTextBox3
            // 
            maskedTextBox3.Location = new Point(155, 85);
            maskedTextBox3.Name = "maskedTextBox3";
            maskedTextBox3.Size = new Size(77, 31);
            maskedTextBox3.TabIndex = 7;
            // 
            // maskedTextBox4
            // 
            maskedTextBox4.Location = new Point(155, 178);
            maskedTextBox4.Name = "maskedTextBox4";
            maskedTextBox4.Size = new Size(77, 31);
            maskedTextBox4.TabIndex = 8;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(155, 132);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(77, 31);
            maskedTextBox1.TabIndex = 9;
            // 
            // Save
            // 
            Save.BackColor = SystemColors.Window;
            Save.Location = new Point(385, 408);
            Save.Name = "Save";
            Save.Size = new Size(91, 34);
            Save.TabIndex = 11;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Window;
            button2.Location = new Point(498, 408);
            button2.Name = "button2";
            button2.Size = new Size(97, 34);
            button2.TabIndex = 12;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Location = new Point(12, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(596, 110);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Segment";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(498, 31);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(85, 33);
            comboBox2.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(410, 32);
            label9.Name = "label9";
            label9.Size = new Size(75, 25);
            label9.TabIndex = 6;
            label9.Text = "Material";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 70);
            label2.Name = "label2";
            label2.Size = new Size(125, 25);
            label2.TabIndex = 5;
            label2.Text = "Segment Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 33);
            label1.Name = "label1";
            label1.Size = new Size(135, 25);
            label1.TabIndex = 0;
            label1.Text = "Segment Name";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(198, 30);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(164, 29);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
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
            groupBox2.Location = new Point(12, 149);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(249, 233);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Dimensions";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 181);
            label6.Name = "label6";
            label6.Size = new Size(106, 25);
            label6.TabIndex = 13;
            label6.Text = "Height Final";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 138);
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
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label7);
            groupBox3.Location = new Point(267, 149);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(165, 233);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "Calculations";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 91);
            label8.Name = "label8";
            label8.Size = new Size(71, 25);
            label8.TabIndex = 1;
            label8.Text = "Density";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 48);
            label7.Name = "label7";
            label7.Size = new Size(68, 25);
            label7.TabIndex = 0;
            label7.Text = "Weight";
            // 
            // groupBox4
            // 
            groupBox4.Location = new Point(438, 149);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(170, 233);
            groupBox4.TabIndex = 16;
            groupBox4.TabStop = false;
            groupBox4.Text = "Section";
            // 
            // SegmentDialogBox
            // 
            AcceptButton = Save;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(620, 471);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(Save);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SegmentDialogBox";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SegmentDialogBox";
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

        private ComboBox comboBox1;
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
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label8;
        private Label label7;
        private ComboBox comboBox2;
        private Label label9;
        private GroupBox groupBox4;
    }
}