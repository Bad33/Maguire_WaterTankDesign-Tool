namespace WaterTankTool_WFA.Load
{
    partial class Live_Load
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
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            label7 = new Label();
            textBox4 = new TextBox();
            label8 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Location = new Point(20, 99);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(241, 66);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Notes";
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(13, 20);
            richTextBox1.Margin = new Padding(2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(212, 32);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(99, 181);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(78, 31);
            button1.TabIndex = 3;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(183, 181);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(78, 31);
            button2.TabIndex = 4;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 44);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 12;
            label7.Text = "Live Load";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(121, 41);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(93, 23);
            textBox4.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(232, 44);
            label8.Name = "label8";
            label8.Size = new Size(29, 15);
            label8.TabIndex = 14;
            label8.Text = "Kips";
            // 
            // Live_Load
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(284, 232);
            Controls.Add(label8);
            Controls.Add(textBox4);
            Controls.Add(label7);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "Live_Load";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Live Load";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
        private Label label7;
        private TextBox textBox4;
        private Label label8;
    }
}