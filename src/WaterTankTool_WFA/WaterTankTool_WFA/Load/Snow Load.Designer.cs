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
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 32);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 0;
            label1.Text = "Snow Pressure";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 76);
            label2.Name = "label2";
            label2.Size = new Size(132, 15);
            label2.TabIndex = 1;
            label2.Text = "Area Subjected to Snow";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(167, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(167, 73);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(273, 28);
            label3.Name = "label3";
            label3.Size = new Size(23, 15);
            label3.TabIndex = 4;
            label3.Text = "psf";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(276, 77);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 5;
            label4.Text = "ft²";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 120);
            label5.Name = "label5";
            label5.Size = new Size(94, 15);
            label5.TabIndex = 6;
            label5.Text = "Total Snow Load";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(167, 118);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(102, 23);
            textBox3.TabIndex = 7;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(276, 120);
            label6.Name = "label6";
            label6.Size = new Size(29, 15);
            label6.TabIndex = 8;
            label6.Text = "Kips";
            // 
            // button1
            // 
            button1.Location = new Point(140, 226);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(221, 226);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(19, 19);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(245, 30);
            richTextBox1.TabIndex = 12;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Location = new Point(26, 147);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(270, 68);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Notes";
            // 
            // Snow_Load
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(315, 261);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
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
    }
}