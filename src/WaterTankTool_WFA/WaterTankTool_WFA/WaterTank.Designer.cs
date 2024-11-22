namespace WaterTankTool_WFA;

partial class WaterTank
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterTank));
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        newToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator3 = new ToolStripSeparator();
        printToolStripMenuItem = new ToolStripMenuItem();
        printPreviewToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator4 = new ToolStripSeparator();
        exitToolStripMenuItem = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        undoToolStripMenuItem = new ToolStripMenuItem();
        redoToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator5 = new ToolStripSeparator();
        cutToolStripMenuItem = new ToolStripMenuItem();
        copyToolStripMenuItem = new ToolStripMenuItem();
        pasteToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator6 = new ToolStripSeparator();
        selectAllToolStripMenuItem = new ToolStripMenuItem();
        toolsToolStripMenuItem = new ToolStripMenuItem();
        optionsToolStripMenuItem = new ToolStripMenuItem();
        loadToolStripMenuItem = new ToolStripMenuItem();
        liveLoadToolStripMenuItem = new ToolStripMenuItem();
        deadLoadToolStripMenuItem = new ToolStripMenuItem();
        waterLoadToolStripMenuItem = new ToolStripMenuItem();
        snowLoadToolStripMenuItem = new ToolStripMenuItem();
        windLoadToolStripMenuItem = new ToolStripMenuItem();
        seismicLoadToolStripMenuItem = new ToolStripMenuItem();
        materialToolStripMenuItem = new ToolStripMenuItem();
        solveToolStripMenuItem = new ToolStripMenuItem();
        aboutToolStripMenuItem = new ToolStripMenuItem();
        toolStrip1 = new ToolStrip();
        newToolStripButton = new ToolStripButton();
        openToolStripButton = new ToolStripButton();
        saveToolStripButton = new ToolStripButton();
        printToolStripButton = new ToolStripButton();
        toolStripSeparator = new ToolStripSeparator();
        cutToolStripButton = new ToolStripButton();
        copyToolStripButton = new ToolStripButton();
        pasteToolStripButton = new ToolStripButton();
        toolStripSeparator1 = new ToolStripSeparator();
        helpToolStripButton = new ToolStripButton();
        toolStrip3 = new ToolStrip();
        toolStripButton3 = new ToolStripButton();
        pasteToolStripButton1 = new ToolStripButton();
        toolStripButton1 = new ToolStripButton();
        toolStripSeparator7 = new ToolStripSeparator();
        toolStripButton4 = new ToolStripButton();
        toolStripButton2 = new ToolStripButton();
        toolStripButton6 = new ToolStripButton();
        toolStripButton5 = new ToolStripButton();
        statusStrip2 = new StatusStrip();
        toolStrip2 = new ToolStrip();
        toolStripLabel1 = new ToolStripLabel();
        toolStripTextBox1 = new ToolStripTextBox();
        toolStripSeparator8 = new ToolStripSeparator();
        toolStripLabel2 = new ToolStripLabel();
        toolStripTextBox2 = new ToolStripTextBox();
        panel1 = new Panel();
        splitContainer2 = new SplitContainer();
        menuStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        toolStrip3.SuspendLayout();
        toolStrip2.SuspendLayout();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
        splitContainer2.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.AllowMerge = false;
        menuStrip1.BackColor = SystemColors.ControlLight;
        menuStrip1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        menuStrip1.GripMargin = new Padding(3);
        menuStrip1.ImageScalingSize = new Size(24, 24);
        menuStrip1.ImeMode = ImeMode.NoControl;
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, aboutToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Padding = new Padding(0, 2, 0, 2);
        menuStrip1.Size = new Size(1112, 24);
        menuStrip1.Stretch = false;
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator2, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator3, printToolStripMenuItem, printPreviewToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "&File";
        // 
        // newToolStripMenuItem
        // 
        newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
        newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
        newToolStripMenuItem.Size = new Size(188, 30);
        newToolStripMenuItem.Text = "&New";
        newToolStripMenuItem.Click += newToolStripMenuItem_Click;
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
        openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
        openToolStripMenuItem.Size = new Size(188, 30);
        openToolStripMenuItem.Text = "&Open";
        openToolStripMenuItem.Click += openToolStripMenuItem_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(185, 6);
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
        saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
        saveToolStripMenuItem.Size = new Size(188, 30);
        saveToolStripMenuItem.Text = "&Save";
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Size = new Size(188, 30);
        saveAsToolStripMenuItem.Text = "Save &As";
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new Size(185, 6);
        // 
        // printToolStripMenuItem
        // 
        printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
        printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        printToolStripMenuItem.Name = "printToolStripMenuItem";
        printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
        printToolStripMenuItem.Size = new Size(188, 30);
        printToolStripMenuItem.Text = "&Print";
        // 
        // printPreviewToolStripMenuItem
        // 
        printPreviewToolStripMenuItem.Image = (Image)resources.GetObject("printPreviewToolStripMenuItem.Image");
        printPreviewToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
        printPreviewToolStripMenuItem.Size = new Size(188, 30);
        printPreviewToolStripMenuItem.Text = "Print Pre&view";
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        toolStripSeparator4.Size = new Size(185, 6);
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(188, 30);
        exitToolStripMenuItem.Text = "E&xit";
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator5, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator6, selectAllToolStripMenuItem });
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new Size(39, 20);
        editToolStripMenuItem.Text = "&Edit";
        // 
        // undoToolStripMenuItem
        // 
        undoToolStripMenuItem.Name = "undoToolStripMenuItem";
        undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
        undoToolStripMenuItem.Size = new Size(150, 30);
        undoToolStripMenuItem.Text = "&Undo";
        // 
        // redoToolStripMenuItem
        // 
        redoToolStripMenuItem.Name = "redoToolStripMenuItem";
        redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
        redoToolStripMenuItem.Size = new Size(150, 30);
        redoToolStripMenuItem.Text = "&Redo";
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        toolStripSeparator5.Size = new Size(147, 6);
        // 
        // cutToolStripMenuItem
        // 
        cutToolStripMenuItem.Image = (Image)resources.GetObject("cutToolStripMenuItem.Image");
        cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        cutToolStripMenuItem.Name = "cutToolStripMenuItem";
        cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
        cutToolStripMenuItem.Size = new Size(150, 30);
        cutToolStripMenuItem.Text = "Cu&t";
        // 
        // copyToolStripMenuItem
        // 
        copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
        copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
        copyToolStripMenuItem.Size = new Size(150, 30);
        copyToolStripMenuItem.Text = "&Copy";
        // 
        // pasteToolStripMenuItem
        // 
        pasteToolStripMenuItem.Image = (Image)resources.GetObject("pasteToolStripMenuItem.Image");
        pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
        pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
        pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
        pasteToolStripMenuItem.Size = new Size(150, 30);
        pasteToolStripMenuItem.Text = "&Paste";
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        toolStripSeparator6.Size = new Size(147, 6);
        // 
        // selectAllToolStripMenuItem
        // 
        selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
        selectAllToolStripMenuItem.Size = new Size(150, 30);
        selectAllToolStripMenuItem.Text = "Select &All";
        // 
        // toolsToolStripMenuItem
        // 
        toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { optionsToolStripMenuItem, loadToolStripMenuItem, materialToolStripMenuItem, solveToolStripMenuItem });
        toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
        toolsToolStripMenuItem.Size = new Size(53, 20);
        toolsToolStripMenuItem.Text = "&Define";
        toolsToolStripMenuItem.Click += toolsToolStripMenuItem_Click;
        // 
        // optionsToolStripMenuItem
        // 
        optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
        optionsToolStripMenuItem.Size = new Size(123, 22);
        optionsToolStripMenuItem.Text = "&Geometry";
        optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
        // 
        // loadToolStripMenuItem
        // 
        loadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { liveLoadToolStripMenuItem, deadLoadToolStripMenuItem, waterLoadToolStripMenuItem, snowLoadToolStripMenuItem, windLoadToolStripMenuItem, seismicLoadToolStripMenuItem });
        loadToolStripMenuItem.Name = "loadToolStripMenuItem";
        loadToolStripMenuItem.Size = new Size(123, 22);
        loadToolStripMenuItem.Text = "Load";
        // 
        // liveLoadToolStripMenuItem
        // 
        liveLoadToolStripMenuItem.Name = "liveLoadToolStripMenuItem";
        liveLoadToolStripMenuItem.Size = new Size(139, 22);
        liveLoadToolStripMenuItem.Text = "Live Load";
        liveLoadToolStripMenuItem.Click += liveLoadToolStripMenuItem_Click;
        // 
        // deadLoadToolStripMenuItem
        // 
        deadLoadToolStripMenuItem.Name = "deadLoadToolStripMenuItem";
        deadLoadToolStripMenuItem.Size = new Size(139, 22);
        deadLoadToolStripMenuItem.Text = "Dead Load";
        deadLoadToolStripMenuItem.Click += deadLoadToolStripMenuItem_Click;
        // 
        // waterLoadToolStripMenuItem
        // 
        waterLoadToolStripMenuItem.Name = "waterLoadToolStripMenuItem";
        waterLoadToolStripMenuItem.Size = new Size(139, 22);
        waterLoadToolStripMenuItem.Text = "Water Load";
        waterLoadToolStripMenuItem.Click += waterLoadToolStripMenuItem_Click;
        // 
        // snowLoadToolStripMenuItem
        // 
        snowLoadToolStripMenuItem.Name = "snowLoadToolStripMenuItem";
        snowLoadToolStripMenuItem.Size = new Size(139, 22);
        snowLoadToolStripMenuItem.Text = "Snow Load";
        snowLoadToolStripMenuItem.Click += snowLoadToolStripMenuItem_Click;
        // 
        // windLoadToolStripMenuItem
        // 
        windLoadToolStripMenuItem.Name = "windLoadToolStripMenuItem";
        windLoadToolStripMenuItem.Size = new Size(139, 22);
        windLoadToolStripMenuItem.Text = "Wind Load";
        windLoadToolStripMenuItem.Click += windLoadToolStripMenuItem_Click;
        // 
        // seismicLoadToolStripMenuItem
        // 
        seismicLoadToolStripMenuItem.Name = "seismicLoadToolStripMenuItem";
        seismicLoadToolStripMenuItem.Size = new Size(139, 22);
        seismicLoadToolStripMenuItem.Text = "Seismic Load";
        seismicLoadToolStripMenuItem.Click += seismicLoadToolStripMenuItem_Click;
        // 
        // materialToolStripMenuItem
        // 
        materialToolStripMenuItem.Name = "materialToolStripMenuItem";
        materialToolStripMenuItem.Size = new Size(123, 22);
        materialToolStripMenuItem.Text = "Material";
        materialToolStripMenuItem.Click += materialToolStripMenuItem_Click;
        // 
        // solveToolStripMenuItem
        // 
        solveToolStripMenuItem.Name = "solveToolStripMenuItem";
        solveToolStripMenuItem.Size = new Size(123, 22);
        solveToolStripMenuItem.Text = "Solve";
        solveToolStripMenuItem.Click += solveToolStripMenuItem_Click;
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Size = new Size(51, 20);
        aboutToolStripMenuItem.Text = "About";
        aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
        // 
        // toolStrip1
        // 
        toolStrip1.BackColor = SystemColors.ControlLight;
        toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
        toolStrip1.ImageScalingSize = new Size(24, 24);
        toolStrip1.Items.AddRange(new ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, printToolStripButton, toolStripSeparator, cutToolStripButton, copyToolStripButton, pasteToolStripButton, toolStripSeparator1, helpToolStripButton });
        toolStrip1.Location = new Point(0, 24);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(1112, 31);
        toolStrip1.TabIndex = 7;
        toolStrip1.Text = "toolStrip1";
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        newToolStripButton.Image = (Image)resources.GetObject("newToolStripButton.Image");
        newToolStripButton.ImageTransparentColor = Color.Magenta;
        newToolStripButton.Name = "newToolStripButton";
        newToolStripButton.Size = new Size(28, 28);
        newToolStripButton.Text = "&New";
        newToolStripButton.Click += newToolStripButton_Click;
        // 
        // openToolStripButton
        // 
        openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        openToolStripButton.Image = (Image)resources.GetObject("openToolStripButton.Image");
        openToolStripButton.ImageTransparentColor = Color.Magenta;
        openToolStripButton.Name = "openToolStripButton";
        openToolStripButton.Size = new Size(28, 28);
        openToolStripButton.Text = "&Open";
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        saveToolStripButton.Image = (Image)resources.GetObject("saveToolStripButton.Image");
        saveToolStripButton.ImageTransparentColor = Color.Magenta;
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Size = new Size(28, 28);
        saveToolStripButton.Text = "&Save";
        // 
        // printToolStripButton
        // 
        printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
        printToolStripButton.ImageTransparentColor = Color.Magenta;
        printToolStripButton.Name = "printToolStripButton";
        printToolStripButton.Size = new Size(28, 28);
        printToolStripButton.Text = "&Print";
        // 
        // toolStripSeparator
        // 
        toolStripSeparator.Name = "toolStripSeparator";
        toolStripSeparator.Size = new Size(6, 31);
        // 
        // cutToolStripButton
        // 
        cutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        cutToolStripButton.Image = (Image)resources.GetObject("cutToolStripButton.Image");
        cutToolStripButton.ImageTransparentColor = Color.Magenta;
        cutToolStripButton.Name = "cutToolStripButton";
        cutToolStripButton.Size = new Size(28, 28);
        cutToolStripButton.Text = "C&ut";
        // 
        // copyToolStripButton
        // 
        copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        copyToolStripButton.Image = (Image)resources.GetObject("copyToolStripButton.Image");
        copyToolStripButton.ImageTransparentColor = Color.Magenta;
        copyToolStripButton.Name = "copyToolStripButton";
        copyToolStripButton.Size = new Size(28, 28);
        copyToolStripButton.Text = "&Copy";
        // 
        // pasteToolStripButton
        // 
        pasteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        pasteToolStripButton.Image = (Image)resources.GetObject("pasteToolStripButton.Image");
        pasteToolStripButton.ImageTransparentColor = Color.Magenta;
        pasteToolStripButton.Name = "pasteToolStripButton";
        pasteToolStripButton.Size = new Size(28, 28);
        pasteToolStripButton.Text = "&Paste";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(6, 31);
        // 
        // helpToolStripButton
        // 
        helpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        helpToolStripButton.Image = (Image)resources.GetObject("helpToolStripButton.Image");
        helpToolStripButton.ImageTransparentColor = Color.Magenta;
        helpToolStripButton.Name = "helpToolStripButton";
        helpToolStripButton.Size = new Size(28, 28);
        helpToolStripButton.Text = "He&lp";
        // 
        // toolStrip3
        // 
        toolStrip3.BackColor = SystemColors.ControlLight;
        toolStrip3.Dock = DockStyle.Left;
        toolStrip3.GripStyle = ToolStripGripStyle.Hidden;
        toolStrip3.ImageScalingSize = new Size(24, 24);
        toolStrip3.Items.AddRange(new ToolStripItem[] { toolStripButton3, pasteToolStripButton1, toolStripButton1, toolStripSeparator7, toolStripButton4, toolStripButton2, toolStripButton6, toolStripButton5 });
        toolStrip3.Location = new Point(0, 55);
        toolStrip3.Name = "toolStrip3";
        toolStrip3.RightToLeft = RightToLeft.No;
        toolStrip3.Size = new Size(29, 561);
        toolStrip3.TabIndex = 12;
        toolStrip3.Text = "toolStrip3";
        // 
        // toolStripButton3
        // 
        toolStripButton3.Alignment = ToolStripItemAlignment.Right;
        toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
        toolStripButton3.ImageTransparentColor = Color.Magenta;
        toolStripButton3.Name = "toolStripButton3";
        toolStripButton3.Size = new Size(26, 28);
        toolStripButton3.Text = "&Zoom Out";
        toolStripButton3.Click += toolStripButton3_Click;
        // 
        // pasteToolStripButton1
        // 
        pasteToolStripButton1.Alignment = ToolStripItemAlignment.Right;
        pasteToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
        pasteToolStripButton1.Image = (Image)resources.GetObject("pasteToolStripButton1.Image");
        pasteToolStripButton1.ImageTransparentColor = Color.Magenta;
        pasteToolStripButton1.Name = "pasteToolStripButton1";
        pasteToolStripButton1.Size = new Size(26, 28);
        pasteToolStripButton1.Text = "&Zoom In";
        pasteToolStripButton1.Click += pasteToolStripButton1_Click;
        // 
        // toolStripButton1
        // 
        toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
        toolStripButton1.ImageTransparentColor = Color.Magenta;
        toolStripButton1.Name = "toolStripButton1";
        toolStripButton1.Size = new Size(26, 28);
        toolStripButton1.Text = "toolStripButton1";
        toolStripButton1.Click += toolStripButton1_Click;
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        toolStripSeparator7.Size = new Size(26, 6);
        // 
        // toolStripButton4
        // 
        toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
        toolStripButton4.ImageTransparentColor = Color.Magenta;
        toolStripButton4.Name = "toolStripButton4";
        toolStripButton4.Size = new Size(26, 28);
        toolStripButton4.Text = "Add Geometry";
        toolStripButton4.Click += toolStripButton4_Click;
        // 
        // toolStripButton2
        // 
        toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
        toolStripButton2.ImageTransparentColor = Color.Magenta;
        toolStripButton2.Name = "toolStripButton2";
        toolStripButton2.Size = new Size(26, 28);
        toolStripButton2.Text = "Load";
        // 
        // toolStripButton6
        // 
        toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
        toolStripButton6.ImageTransparentColor = Color.Magenta;
        toolStripButton6.Name = "toolStripButton6";
        toolStripButton6.Size = new Size(26, 28);
        toolStripButton6.Text = "toolStripButton6";
        toolStripButton6.ToolTipText = "Materials";
        toolStripButton6.Click += toolStripButton6_Click;
        // 
        // toolStripButton5
        // 
        toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
        toolStripButton5.ImageTransparentColor = Color.Magenta;
        toolStripButton5.Name = "toolStripButton5";
        toolStripButton5.Size = new Size(26, 28);
        toolStripButton5.Text = "toolStripButton5";
        toolStripButton5.ToolTipText = "Solve";
        toolStripButton5.Click += toolStripButton5_Click;
        // 
        // statusStrip2
        // 
        statusStrip2.Dock = DockStyle.Top;
        statusStrip2.ImageScalingSize = new Size(24, 24);
        statusStrip2.Location = new Point(29, 55);
        statusStrip2.Name = "statusStrip2";
        statusStrip2.Size = new Size(1083, 22);
        statusStrip2.TabIndex = 17;
        statusStrip2.Text = "statusStrip2";
        // 
        // toolStrip2
        // 
        toolStrip2.BackColor = SystemColors.ControlLight;
        toolStrip2.Dock = DockStyle.Bottom;
        toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
        toolStrip2.ImageScalingSize = new Size(24, 24);
        toolStrip2.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripTextBox1, toolStripSeparator8, toolStripLabel2, toolStripTextBox2 });
        toolStrip2.Location = new Point(29, 591);
        toolStrip2.Name = "toolStrip2";
        toolStrip2.Size = new Size(1083, 25);
        toolStrip2.TabIndex = 18;
        toolStrip2.Text = "toolStrip2";
        // 
        // toolStripLabel1
        // 
        toolStripLabel1.Name = "toolStripLabel1";
        toolStripLabel1.Size = new Size(20, 22);
        toolStripLabel1.Text = "X :";
        // 
        // toolStripTextBox1
        // 
        toolStripTextBox1.Name = "toolStripTextBox1";
        toolStripTextBox1.Size = new Size(100, 25);
        // 
        // toolStripSeparator8
        // 
        toolStripSeparator8.Name = "toolStripSeparator8";
        toolStripSeparator8.Size = new Size(6, 25);
        // 
        // toolStripLabel2
        // 
        toolStripLabel2.Name = "toolStripLabel2";
        toolStripLabel2.Size = new Size(20, 22);
        toolStripLabel2.Text = "Y :";
        // 
        // toolStripTextBox2
        // 
        toolStripTextBox2.Name = "toolStripTextBox2";
        toolStripTextBox2.Size = new Size(100, 25);
        // 
        // panel1
        // 
        panel1.BorderStyle = BorderStyle.Fixed3D;
        panel1.Controls.Add(splitContainer2);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(29, 77);
        panel1.Name = "panel1";
        panel1.Size = new Size(1083, 514);
        panel1.TabIndex = 20;
        panel1.Paint += panel1_Paint_1;
        // 
        // splitContainer2
        // 
        splitContainer2.Dock = DockStyle.Right;
        splitContainer2.IsSplitterFixed = true;
        splitContainer2.Location = new Point(773, 0);
        splitContainer2.Name = "splitContainer2";
        splitContainer2.Orientation = Orientation.Horizontal;
        // 
        // splitContainer2.Panel1
        // 
        splitContainer2.Panel1.BackColor = SystemColors.ControlLight;
        // 
        // splitContainer2.Panel2
        // 
        splitContainer2.Panel2.BackColor = SystemColors.ControlLight;
        splitContainer2.Size = new Size(306, 510);
        splitContainer2.SplitterDistance = 107;
        splitContainer2.TabIndex = 0;
        // 
        // WaterTank
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        BackColor = SystemColors.Control;
        ClientSize = new Size(1112, 616);
        Controls.Add(panel1);
        Controls.Add(toolStrip2);
        Controls.Add(statusStrip2);
        Controls.Add(toolStrip3);
        Controls.Add(toolStrip1);
        Controls.Add(menuStrip1);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Icon = (Icon)resources.GetObject("$this.Icon");
        IsMdiContainer = true;
        MainMenuStrip = menuStrip1;
        Margin = new Padding(3, 4, 3, 4);
        Name = "WaterTank";
        RightToLeft = RightToLeft.No;
        Text = "Water Tank Design Tool";
        WindowState = FormWindowState.Maximized;
        Load += Form1_Load;
        Click += Form1_Click;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        toolStrip3.ResumeLayout(false);
        toolStrip3.PerformLayout();
        toolStrip2.ResumeLayout(false);
        toolStrip2.PerformLayout();
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
        splitContainer2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem toolToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem newToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem printToolStripMenuItem;
    private ToolStripMenuItem printPreviewToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem undoToolStripMenuItem;
    private ToolStripMenuItem redoToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem cutToolStripMenuItem;
    private ToolStripMenuItem copyToolStripMenuItem;
    private ToolStripMenuItem pasteToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem selectAllToolStripMenuItem;
    private ToolStripMenuItem toolsToolStripMenuItem;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private ToolStripButton newToolStripButton;
    private ToolStripButton openToolStripButton;
    private ToolStripButton saveToolStripButton;
    private ToolStripButton printToolStripButton;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripButton cutToolStripButton;
    private ToolStripButton copyToolStripButton;
    private ToolStripButton pasteToolStripButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton helpToolStripButton;
    private ToolStripMenuItem loadToolStripMenuItem;
    private ToolStripMenuItem materialToolStripMenuItem;
    private ToolStripMenuItem windLoadToolStripMenuItem;
    private ToolStripMenuItem seismicLoadToolStripMenuItem;
    private ToolStripMenuItem snowLoadToolStripMenuItem;
    private ToolStripMenuItem solveToolStripMenuItem;
    private ToolStrip toolStrip3;
    private ToolStripButton pasteToolStripButton1;
    private ToolStripButton toolStripButton1;
    private ToolStripButton toolStripButton2;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton toolStripButton3;
    private StatusStrip statusStrip2;
    private ToolStrip toolStrip2;
    private ToolStripLabel toolStripLabel1;
    private ToolStripTextBox toolStripTextBox1;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripLabel toolStripLabel2;
    private ToolStripTextBox toolStripTextBox2;
    private ToolStripButton toolStripButton4;
    private ToolStripMenuItem liveLoadToolStripMenuItem;
    private ToolStripMenuItem deadLoadToolStripMenuItem;
    private ToolStripMenuItem waterLoadToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripButton toolStripButton5;
    private ToolStripButton toolStripButton6;
    private Panel panel1;
    private SplitContainer splitContainer1;
    private SplitContainer splitContainer2;
}