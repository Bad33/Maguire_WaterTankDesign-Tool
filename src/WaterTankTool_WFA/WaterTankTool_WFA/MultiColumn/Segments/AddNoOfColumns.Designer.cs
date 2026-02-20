namespace WaterTankTool_WFA.MultiColumn.Segments
{
    partial class AddNoOfColumns
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
            numericUpDown1 = new NumericUpDown();
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            label2 = new Label();
            numericUpDown2 = new NumericUpDown();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 0;
            label1.Text = "No of Columns";
            label1.Click += label1_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(132, 17);
            numericUpDown1.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(119, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Location = new Point(223, 257);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(142, 257);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericUpDown2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(286, 118);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 61);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 2;
            label2.Text = "No of Segment";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(131, 59);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(13, 140);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(285, 91);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Define Weights On Columns";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 28);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 0;
            label3.Text = "Struts";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 64);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 1;
            label4.Text = "Cross Bracing";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(130, 25);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 23);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(130, 61);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(120, 23);
            textBox2.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.Location = new Point(252, 28);
            label5.Name = "label5";
            label5.Size = new Size(27, 13);
            label5.TabIndex = 4;
            label5.Text = "Kips";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(252, 66);
            label6.Name = "label6";
            label6.Size = new Size(27, 13);
            label6.TabIndex = 5;
            label6.Text = "Kips";
            // 
            // AddNoOfColumns
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(314, 292);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddNoOfColumns";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add No. Of Columns";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private NumericUpDown numericUpDown2;
        private Label label2;
        private GroupBox groupBox2;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label4;
        private Label label3;
        private Label label6;
        private Label label5;
    }
}