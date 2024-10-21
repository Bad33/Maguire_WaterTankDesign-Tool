namespace WaterTankTool_WFA
{
    partial class Define_Materials
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
            button5 = new Button();
            button4 = new Button();
            groupBox3 = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            groupBox2 = new GroupBox();
            listView1 = new ListView();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(575, 410);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // button5
            // 
            button5.Location = new Point(441, 339);
            button5.Name = "button5";
            button5.Size = new Size(112, 34);
            button5.TabIndex = 3;
            button5.Text = "Cancel";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(315, 339);
            button4.Name = "button4";
            button4.Size = new Size(112, 34);
            button4.TabIndex = 2;
            button4.Text = "Ok";
            button4.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(button1);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(305, 30);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(248, 267);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Click to:";
            // 
            // button3
            // 
            button3.Location = new Point(21, 200);
            button3.Name = "button3";
            button3.Size = new Size(217, 34);
            button3.TabIndex = 2;
            button3.Text = "Delete Material";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(21, 133);
            button2.Name = "button2";
            button2.Size = new Size(217, 34);
            button2.TabIndex = 1;
            button2.Text = "Modify/Show Materials";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(21, 61);
            button1.Name = "button1";
            button1.Size = new Size(217, 34);
            button1.TabIndex = 0;
            button1.Text = "Add New Material...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(listView1);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(16, 30);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(283, 360);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Materials";
            // 
            // listView1
            // 
            listView1.Location = new Point(15, 30);
            listView1.Name = "listView1";
            listView1.Size = new Size(249, 313);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Define_Materials
            // 
            AcceptButton = button4;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            CancelButton = button5;
            ClientSize = new Size(599, 437);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Define_Materials";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Define Materials";
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private ListView listView1;
    }
}