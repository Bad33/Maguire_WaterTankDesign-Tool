namespace WaterTankTool_WFA.Load
{
    partial class Load_Combinations
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            advancedDataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
            groupBox2 = new GroupBox();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            groupBox4 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)advancedDataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(advancedDataGridView1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(428, 189);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // advancedDataGridView1
            // 
            advancedDataGridView1.AllowUserToAddRows = false;
            advancedDataGridView1.AllowUserToDeleteRows = false;
            advancedDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            advancedDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            advancedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            advancedDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            advancedDataGridView1.BackgroundColor = SystemColors.ControlLight;
            advancedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            advancedDataGridView1.Dock = DockStyle.Fill;
            advancedDataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            advancedDataGridView1.FilterAndSortEnabled = false;
            advancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            advancedDataGridView1.Location = new Point(3, 19);
            advancedDataGridView1.MaxFilterButtonImageHeight = 23;
            advancedDataGridView1.MultiSelect = false;
            advancedDataGridView1.Name = "advancedDataGridView1";
            advancedDataGridView1.ReadOnly = true;
            advancedDataGridView1.RightToLeft = RightToLeft.No;
            advancedDataGridView1.RowHeadersVisible = false;
            advancedDataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            advancedDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            advancedDataGridView1.Size = new Size(422, 167);
            advancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            advancedDataGridView1.TabIndex = 0;
            advancedDataGridView1.CellContentClick += advancedDataGridView1_CellContentClick;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.BackColor = SystemColors.ButtonHighlight;
            groupBox2.Controls.Add(richTextBox1);
            groupBox2.Location = new Point(15, 290);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(422, 54);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Note";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(13, 23);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(390, 25);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(309, 31);
            button1.Name = "button1";
            button1.Size = new Size(109, 23);
            button1.TabIndex = 5;
            button1.Text = "0.6D + 0.6(W or E)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(153, 31);
            button2.Name = "button2";
            button2.Size = new Size(131, 23);
            button2.TabIndex = 6;
            button2.Text = "D + 0.75L + 0.75(0.6W or 0.6E) + 0.75(Lr or S)";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(13, 31);
            button3.Name = "button3";
            button3.Size = new Size(106, 23);
            button3.TabIndex = 7;
            button3.Text = "D + 0.6(W or E)";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(button1);
            groupBox4.Controls.Add(button3);
            groupBox4.Controls.Add(button2);
            groupBox4.Location = new Point(15, 208);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(424, 76);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "Select Load Combination for Check Table";
            // 
            // Load_Combinations
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(449, 356);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Load_Combinations";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Load Combinations";
            Load += Load_Combinations_Load;
            Shown += Load_Combinations_Shown;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)advancedDataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView1;
        private DataGridViewTextBoxColumn Load_Combination;
        private DataGridViewTextBoxColumn P;
        private DataGridViewTextBoxColumn M;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private GroupBox groupBox2;
        private RichTextBox richTextBox1;
        private GroupBox groupBox3;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox textBox2;
        private Label label3;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private GroupBox groupBox4;
    }
}