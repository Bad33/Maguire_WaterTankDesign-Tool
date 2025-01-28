namespace WaterTankTool_WFA
{
    partial class Help
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
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "1. Add and Save the Material from Material Tab" }, -1, Color.Empty, Color.Empty, new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0));
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "2. Add all the loads from Load Tab" }, -1, Color.Empty, Color.Empty, new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0));
            panel1 = new Panel();
            listView1 = new ListView();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(listView1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(325, 348);
            panel1.TabIndex = 0;
            // 
            // listView1
            // 
            listView1.Dock = DockStyle.Fill;
            listViewItem2.IndentCount = 4;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2 });
            listView1.LabelWrap = false;
            listView1.Location = new Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(325, 348);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Tile;
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 379);
            Controls.Add(panel1);
            Name = "Help";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Help";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}