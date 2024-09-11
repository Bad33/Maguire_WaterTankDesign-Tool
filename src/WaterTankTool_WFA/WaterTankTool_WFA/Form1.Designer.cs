namespace WaterTankTool_WFA;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        menuStrip1 = new MenuStrip();
        toolStripMenuItem1 = new ToolStripMenuItem();
        filesToolStripMenuItem = new ToolStripMenuItem();
        toolStripMenuItem2 = new ToolStripMenuItem();
        flowLayoutPanel1 = new FlowLayoutPanel();
        flowLayoutPanel2 = new FlowLayoutPanel();
        flowLayoutPanel3 = new FlowLayoutPanel();
        flowLayoutPanel4 = new FlowLayoutPanel();
        flowLayoutPanel5 = new FlowLayoutPanel();
        flowLayoutPanel6 = new FlowLayoutPanel();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.BackColor = SystemColors.Control;
        menuStrip1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        menuStrip1.GripStyle = ToolStripGripStyle.Visible;
        menuStrip1.ImageScalingSize = new Size(24, 24);
        menuStrip1.ImeMode = ImeMode.NoControl;
        menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, filesToolStripMenuItem, toolStripMenuItem2 });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.RenderMode = ToolStripRenderMode.Professional;
        menuStrip1.Size = new Size(938, 29);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(50, 25);
        toolStripMenuItem1.Text = "File";
        toolStripMenuItem1.Click += toolStripMenuItem1_Click;
        // 
        // filesToolStripMenuItem
        // 
        filesToolStripMenuItem.Name = "filesToolStripMenuItem";
        filesToolStripMenuItem.Size = new Size(81, 25);
        filesToolStripMenuItem.Text = "Options";
        // 
        // toolStripMenuItem2
        // 
        toolStripMenuItem2.Name = "toolStripMenuItem2";
        toolStripMenuItem2.Size = new Size(77, 25);
        toolStripMenuItem2.Text = "Display";
        toolStripMenuItem2.Click += toolStripMenuItem2_Click;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flowLayoutPanel1.BackColor = SystemColors.Control;
        flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
        flowLayoutPanel1.Location = new Point(49, 99);
        flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(684, 317);
        flowLayoutPanel1.TabIndex = 1;
        // 
        // flowLayoutPanel2
        // 
        flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flowLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
        flowLayoutPanel2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        flowLayoutPanel2.Location = new Point(739, 99);
        flowLayoutPanel2.Margin = new Padding(1);
        flowLayoutPanel2.Name = "flowLayoutPanel2";
        flowLayoutPanel2.Size = new Size(186, 342);
        flowLayoutPanel2.TabIndex = 0;
        // 
        // flowLayoutPanel3
        // 
        flowLayoutPanel3.BackColor = SystemColors.Control;
        flowLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
        flowLayoutPanel3.Location = new Point(12, 73);
        flowLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel3.Name = "flowLayoutPanel3";
        flowLayoutPanel3.Size = new Size(31, 369);
        flowLayoutPanel3.TabIndex = 2;
        // 
        // flowLayoutPanel4
        // 
        flowLayoutPanel4.BackColor = SystemColors.Control;
        flowLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
        flowLayoutPanel4.Location = new Point(49, 423);
        flowLayoutPanel4.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel4.Name = "flowLayoutPanel4";
        flowLayoutPanel4.Size = new Size(684, 19);
        flowLayoutPanel4.TabIndex = 3;
        // 
        // flowLayoutPanel5
        // 
        flowLayoutPanel5.BackColor = SystemColors.Control;
        flowLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
        flowLayoutPanel5.Location = new Point(49, 71);
        flowLayoutPanel5.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel5.Name = "flowLayoutPanel5";
        flowLayoutPanel5.Size = new Size(878, 21);
        flowLayoutPanel5.TabIndex = 4;
        // 
        // flowLayoutPanel6
        // 
        flowLayoutPanel6.BackColor = SystemColors.Control;
        flowLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
        flowLayoutPanel6.Location = new Point(12, 45);
        flowLayoutPanel6.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel6.Name = "flowLayoutPanel6";
        flowLayoutPanel6.Size = new Size(914, 22);
        flowLayoutPanel6.TabIndex = 5;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        BackColor = SystemColors.Control;
        ClientSize = new Size(938, 454);
        Controls.Add(flowLayoutPanel6);
        Controls.Add(flowLayoutPanel5);
        Controls.Add(flowLayoutPanel4);
        Controls.Add(flowLayoutPanel3);
        Controls.Add(flowLayoutPanel2);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(menuStrip1);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Icon = (Icon)resources.GetObject("$this.Icon");
        IsMdiContainer = true;
        MainMenuStrip = menuStrip1;
        Margin = new Padding(3, 4, 3, 4);
        Name = "Form1";
        RightToLeft = RightToLeft.No;
        Text = "Water Tank Design Tool";
        Load += Form1_Load;
        Click += Form1_Click;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem filesToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolToolStripMenuItem;
    private FlowLayoutPanel flowLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel2;
    private FlowLayoutPanel flowLayoutPanel3;
    private FlowLayoutPanel flowLayoutPanel4;
    private FlowLayoutPanel flowLayoutPanel5;
    private FlowLayoutPanel flowLayoutPanel6;
}