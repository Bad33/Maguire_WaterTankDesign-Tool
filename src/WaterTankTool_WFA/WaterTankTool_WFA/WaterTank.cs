using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using WaterTankTool_WFA.Custom_Design_Control;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Load;
using WaterTankTool_WFA.Solver;

namespace WaterTankTool_WFA;

public partial class WaterTank : Form
{

    StructuralDrawingForm tankDesign = new StructuralDrawingForm();
    WaterTankDbContext context;
    private Image drawingImage;
    private Point lastMousePosition;
    private bool isDragging = false;
    private DataGridView dimensionGridView;
    private Button toggleButton;
    private Button toggleSolveButton;
    private bool isDimensionTableVisible = false;
    private bool isSolverVisible = false;

    private ToolStripStatusLabel appStatusLabel;
    private ToolStripStatusLabel selectedMaterialLabel;
    private ToolStripStatusLabel designDetailsLabel;
    private ToolStripStatusLabel noMaterialStatus;
    public WaterTank()
    {
        InitializeComponent();
        var _context = WaterTankDbContext.GetInstance();
        context = _context;
        InitializeStatusStrip2();
        InitializeLayout();
        panelDrawTankCapacity();
        LoadDimensionsToGrid();

        this.Paint += panel1_Paint_1;
        panel1.MouseWheel += panel1_MouseWheel;
        panel1.MouseDown += panel1_MouseDown;
        panel1.MouseMove += panel1_MouseMove;
        panel1.MouseUp += panel1_MouseUp;


        this.Resize += (s, e) => this.Invalidate();
    }

    private void InitializeStatusStrip2()
    {
        // Initialize labels
        appStatusLabel = new ToolStripStatusLabel
        {
            Text = "Status: Ready",
            Spring = false, // To avoid expanding unnecessarily
            TextAlign = ContentAlignment.MiddleLeft
        };

        selectedMaterialLabel = new ToolStripStatusLabel
        {
            Text = "Selected Material: None",
            Spring = false,
            TextAlign = ContentAlignment.MiddleLeft
        };

        noMaterialStatus = new ToolStripStatusLabel
        {
            Text = "Please Add the Material Type!",
            ForeColor = Color.Red,
            Spring = false, // Allow this label to stretch
            TextAlign = ContentAlignment.MiddleLeft
        };

        designDetailsLabel = new ToolStripStatusLabel
        {
            Text = "Tank: - | Total Weight: 0Kips | Area: 0ft²",
            Spring = true, // Allow this label to stretch
            TextAlign = ContentAlignment.MiddleRight
        };



        // Add labels to statusStrip2
        statusStrip2.Items.Add(appStatusLabel);
        statusStrip2.Items.Add(new ToolStripSeparator());
        statusStrip2.Items.Add(selectedMaterialLabel);
        statusStrip2.Items.Add(new ToolStripSeparator());
        statusStrip2.Items.Add(noMaterialStatus);

        statusStrip2.Items.Add(designDetailsLabel);
    }

    private void InitializeLayout()
    {
        // Configure SplitContainer2 and Panel1 (already created in your code)
        //splitContainer2.Panel1.BackColor = Color.White; // Drawing area background
        //splitContainer2.Panel1.Controls.Add(panel1);

        // Add the toggle button to show/hide dimensions



        toggleButton = new Button
        {
            Text = "Show Dimensions",
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = Color.Gray,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Popup

        };
        toggleButton.FlatAppearance.BorderSize = 0;
        toggleButton.Click += ToggleDimensionTableVisibility;
        splitContainer2.Panel1.Controls.Add(toggleButton);


        toggleSolveButton = new Button
        {
            Text = "Solve",
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = Color.Gray,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Popup
        };
        toggleSolveButton.FlatAppearance.BorderSize = 0;
        toggleSolveButton.Click += ToggleSolverVisibility;
        splitContainer2.Panel2.Controls.Add(toggleSolveButton);



        // Initialize the DataGridView for dimensions
        dimensionGridView = new DataGridView
        {
            Dock = DockStyle.Bottom,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false, // Disable the extra row
            RowHeadersVisible = false, // Hide the arrow column
            ColumnHeadersVisible = true, // Ensure column headers are visible
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.Fixed3D,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,

        };

        dimensionGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            Font = new Font("Segoe UI", 9, FontStyle.Bold), // Bold font for headers
            Alignment = DataGridViewContentAlignment.MiddleCenter, // Center alignment
            BackColor = Color.LightGray, // Optional: Background color for headers
            ForeColor = Color.Black      // Optional: Text color for headers
        };

        dimensionGridView.Columns.Add("Component", "Component");
        dimensionGridView.Columns.Add("Height", "Height");
        dimensionGridView.Columns.Add("Diameter", "Diameter");
        splitContainer2.Panel1.Controls.Add(dimensionGridView);
        dimensionGridView.Visible = false;

    }

    private void ToggleDimensionTableVisibility(object sender, EventArgs e)
    {

        isDimensionTableVisible = !isDimensionTableVisible;
        ((Button)sender).Text = isDimensionTableVisible ? "Hide Dimensions" : "Show Dimensions";
        dimensionGridView.Visible = isDimensionTableVisible;
    }

    private void ToggleSolverVisibility(object sender, EventArgs e)
    {
        isSolverVisible = !isSolverVisible;
        ((Button)sender).Text = isSolverVisible ? "Hide Solver" : "Show Solver";
    }

    private void LoadDimensionsToGrid()
    {
        // Clear existing columns and rows
        dimensionGridView.Columns.Clear();
        dimensionGridView.Rows.Clear();

        // Dynamically add columns based on the SegmentProperties class
        foreach (var property in typeof(SegmentProperties).GetProperties())
        {
            dimensionGridView.Columns.Add(property.Name, property.Name);
        }

        // Retrieve data from the database
        var segmentData = context.SegmentProperties.ToList();

        // Populate rows dynamically
        foreach (var segment in segmentData)
        {
            var values = typeof(SegmentProperties).GetProperties()
                .Select(property => property.GetValue(segment)?.ToString() ?? string.Empty)
                .ToArray();

            dimensionGridView.Rows.Add(values);
        }

        // Set column header style
        dimensionGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Alignment = DataGridViewContentAlignment.MiddleCenter,
        };

        // Additional grid configuration
        dimensionGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dimensionGridView.RowHeadersVisible = false; // Remove extra row header
        dimensionGridView.AllowUserToAddRows = false; // Disable the new row placeholder
    }






    private void panelDrawTankCapacity()
    {
        var tankCap = context.TankProperties?.FirstOrDefault();

        var gg = context.MaterialProperties.Select(x => x.MaterialName).ToList();
        if (gg.Count > 0)
        {

            UpdateMaterial(gg[0]);
            statusStrip2.Items.Remove(noMaterialStatus);
        }


        if (tankCap?.Capacity != null)
        {
            switch (tankCap.Capacity)
            {
                case "150,000 gallon":
                    drawingImage = Image.FromFile("../../../../icons/150K.png");
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails(tankCap.Capacity, tankCap.TotalWeight, tankCap.ProjectedArea);
                    break;
                case "250,000 gallon":
                    drawingImage = Image.FromFile("../../../../icons/250K.png");
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails(tankCap.Capacity, tankCap.TotalWeight, tankCap.ProjectedArea);

                    break;
                case "500,000 gallon":
                    drawingImage = Image.FromFile("../../../../icons/500K.png");
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails(tankCap.Capacity, tankCap.TotalWeight, tankCap.ProjectedArea);

                    break;
                default:
                    drawingImage = Image.FromFile("../../../../icons/150K.png");
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails("-", "0", "0");
                    break;
            }
        }
        else
        {
            // Set drawingImage to null to indicate a blank panel
            UpdateAppStatus("No Tank Loaded");
            UpdateDesignDetails("-", "0", "0");
            drawingImage = null;
        }

        panel1.Invalidate();
    }

    private void UpdateDesignDetails(string tank, string weight, string area)
    {
        designDetailsLabel.Text = $"Tank: {tank} | Total Weight: {weight} | Area: {area}";
    }

    private void UpdateAppStatus(string status)
    {
        appStatusLabel.Text = $"Status: {status}";
    }

    private void UpdateMaterial(string materialName)
    {
        selectedMaterialLabel.Text = $"Material: {materialName}";
    }



    private void panel1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = true;
            lastMousePosition = e.Location;
        }
    }

    private void panel1_MouseMove(object sender, MouseEventArgs e)
    {
        toolStripTextBox1.Text = $"{e.X}";
        toolStripTextBox2.Text = $"{e.Y}";

        if (isDragging)
        {
            int deltaX = e.X - lastMousePosition.X;
            int deltaY = e.Y - lastMousePosition.Y;

            rotationAngle += deltaX * 0.5f;

            lastMousePosition = e.Location;
            panel1.Invalidate();
        }
    }

    private void panel1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = false; // Stop dragging
        }
    }

    public void OnSegmentAdded()
    {
        panelDrawTankCapacity();
        LoadData();

        panel1.Invalidate();

    }

    public void OnSegmentDeleted()
    {
        panelDrawTankCapacity();
        LoadData();
        panel1.Invalidate();


    }

    private void LoadData()
    {
        dimensionGridView.Columns.Clear();

        dimensionGridView.DataSource = context.SegmentProperties.ToList();
        foreach (DataGridViewColumn column in dimensionGridView.Columns)
        {
            column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }
    }

    public List<SegmentProperties> GetSegmentsFromDatabase()
    {
        List<SegmentProperties> segments = new List<SegmentProperties>();

        //using (var context = WaterTankDbContext.GetInstance())
        //{
        segments = context.SegmentProperties.ToList();
        //}

        return segments;
    }

    private void Form1_Click(object sender, EventArgs e)
    {

    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {
        panel1.Invalidate();
    }

    private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
    {

    }

    private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
    {

    }

    private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void addSegmentToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SegmentDialogBox dialog = new SegmentDialogBox();

        dialog.ShowDialog();
    }

    private void materialToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Define_Materials dialog = new Define_Materials();

        dialog.ShowDialog();
    }

    private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {

    }

    private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Define_Segments define_Segments = new Define_Segments(this);
        define_Segments.ShowDialog();
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {

        Define_Segments define_Segments = new Define_Segments(this);
        define_Segments.ShowDialog();
    }

    private void windLoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Wind_Load wind_Load = new Wind_Load();
        wind_Load.ShowDialog();
    }

    private void liveLoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Live_Load live_Load = new Live_Load();
        live_Load.ShowDialog();
    }

    private void deadLoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Dead_Load dead_Load = new Dead_Load();
        dead_Load.ShowDialog();
    }

    private void waterLoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Water_Load water_Load = new Water_Load();
        water_Load.ShowDialog();
    }

    private void seismicLoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Seismic seismic = new Seismic();
        seismic.ShowDialog();
    }

    private void pasteToolStripButton1_Click(object sender, EventArgs e)
    {
        zoomFactor += 0.1f;

        panel1.Invalidate();



    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        About about = new About();
        about.ShowDialog();
    }

    private void solveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Solver_Output solver_Output = new Solver_Output();
        solver_Output.ShowDialog();
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CreateNewProject();
    }

    private void CreateNewProject()
    {
        // Open a SaveFileDialog to get the project file location and name
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "Project Files (*.proj)|*.proj";
            saveFileDialog.Title = "Create New Project";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string projectPath = saveFileDialog.FileName;

                try
                {
                    // Create a new project file with default content
                    File.WriteAllText(projectPath, "Default project content or structure.");

                    // Optionally, create a project folder and initialize project data
                    string projectFolder = Path.GetDirectoryName(projectPath);
                    Directory.CreateDirectory(Path.Combine(projectFolder, "Assets"));
                    Directory.CreateDirectory(Path.Combine(projectFolder, "Data"));

                    // Set up the UI for a new project
                    InitializeNewProjectUI();

                    // Display a message to confirm the project creation
                    MessageBox.Show("New project created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating new project: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private void InitializeNewProjectUI()
    {
        // Clear existing data or UI elements
        // For example, clear textboxes, grids, or project-specific fields
        // textBox1.Clear();
        // dataGridView1.Rows.Clear();
        // Reset any project-specific settings or UI components

        // Optionally, reset global variables or states for a new project
        //currentProjectPath = null; // Replace with your variable that tracks the current project path
        //isProjectSaved = false;    // Replace with your variable that tracks save state
    }


    private void toolStripButton5_Click(object sender, EventArgs e)
    {
        Solver_Output solver = new Solver_Output();
        solver.ShowDialog();
    }

    private void toolStripButton6_Click(object sender, EventArgs e)
    {
        Define_Materials define_Materials = new Define_Materials();
        define_Materials.ShowDialog();
    }

    private void toolStripStatusLabel1_Click(object sender, EventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }
    private float zoomFactor = 1.0f;
    private float rotationAngle = 0.0f; // Rotation angle in degrees

    private void panel1_Paint_1(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Enable high-quality rendering
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        // Clear the background
        g.Clear(Color.White);

        // Draw the tank image if it exists
        if (drawingImage != null)
        {
            int scaledWidth = (int)(drawingImage.Width * zoomFactor);
            int scaledHeight = (int)(drawingImage.Height * zoomFactor);

            // Calculate the top-left corner to center the image
            int x = (panel1.Width - scaledWidth) / 2;
            int y = (panel1.Height - scaledHeight) / 2;

            // Draw the scaled tank image
            Rectangle destRect = new Rectangle(x, y, scaledWidth, scaledHeight);
            g.DrawImage(drawingImage, destRect);

            // Draw dimensions with arrow lines and labels
            DrawTankWithArrowDimensions(g);
        }
    }


    private void DrawTankWithArrowDimensions(Graphics g)
    {
        // Enable high-quality rendering
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Ensure drawingImage is valid
        if (drawingImage == null) return;

        // Dynamically calculate tankImageHeight and bounds
        int scaledWidth = (int)(drawingImage.Width * zoomFactor);
        int scaledHeight = (int)(drawingImage.Height * zoomFactor);
        int tankImageHeight = scaledHeight;

        // Top-left corner of the image to center it
        int xOffset = (panel1.Width - scaledWidth) / 2;
        int yOffset = (panel1.Height - scaledHeight) / 2;

        // Vertical offset for labels
        int labelOffsetX = xOffset + scaledWidth + 20; // Position labels to the right of the image

        // Get the dimensions from the database
        var dimensions = GetDimensionsFromDatabase();

        foreach (var dimension in dimensions)
        {
            if (dimension.ComponentName == "Cylinder")
            {
                // Map cylinder segments proportionally to tank image
                int segmentTop = yOffset + (int)(tankImageHeight * (dimension.HeightInitial / 100.0));
                int segmentBottom = yOffset + (int)(tankImageHeight * (dimension.HeightFinal / 100.0));

                // Draw the arrow
                Point start = new Point(labelOffsetX - 30, segmentTop);
                Point end = new Point(labelOffsetX - 30, segmentBottom);
                DrawVerticalArrowLine(g, start, end, Color.Black);

                // Draw the label centered vertically relative to the arrow
                string label = $"{dimension.ComponentName}: H={dimension.HeightFinal - dimension.HeightInitial:F2}, D={dimension.Diameter:F2}";
                g.DrawString(label, new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, labelOffsetX, (segmentTop + segmentBottom) / 2 - 10);
            }
            else if (dimension.ComponentName == "Tanks" || dimension.ComponentName == "Base")
            {
                // Calculate arrow positions for Tanks and Base
                int segmentTop = yOffset + (dimension.ComponentName == "Tanks" ? 0 : tankImageHeight - 100);
                int segmentBottom = yOffset + (dimension.ComponentName == "Tanks" ? 150 : tankImageHeight);

                Point start = new Point(labelOffsetX - 30, segmentTop);
                Point end = new Point(labelOffsetX - 30, segmentBottom);
                DrawVerticalArrowLine(g, start, end, Color.Black);

                // Add label
                string label = $"{dimension.ComponentName}: H={dimension.HeightFinal - dimension.HeightInitial:F2}, D={dimension.Diameter:F2}";
                g.DrawString(label, new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, labelOffsetX, (segmentTop + segmentBottom) / 2 - 10);
            }
        }
    }



    private void DrawVerticalArrowLine(Graphics g, Point start, Point end, Color color)
    {
        using (Pen pen = new Pen(color, 2))
        {
            AdjustableArrowCap arrowCap = new AdjustableArrowCap(5, 5);
            pen.CustomEndCap = arrowCap;
            pen.CustomStartCap = arrowCap;

            // Draw the vertical line with arrows
            g.DrawLine(pen, start, end);
        }
    }



    private void DrawDimensions(Graphics g, Rectangle imageBounds)
    {
        var dimensions = GetDimensionsFromDatabase();
        var labelStartX = imageBounds.Right + 20; // Start X position for labels
        var labelYStep = 30; // Vertical space between labels
        var labelY = imageBounds.Top + 20; // Start Y position for labels

        foreach (var dimension in dimensions)
        {
            // Calculate the mid-point of the component (on the tank image)
            Point componentMidPoint = GetComponentMidPoint(dimension.ComponentName, imageBounds);

            // Set the label's position
            Point labelPosition = new Point(labelStartX, labelY);

            // Draw a connecting line (dashed)
            using (Pen dashedPen = new Pen(Color.Gray, 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            })
            {
                // Draw a straight horizontal line from component to label area
                g.DrawLine(dashedPen, componentMidPoint.X, componentMidPoint.Y, labelStartX - 10, componentMidPoint.Y);

                // Draw a vertical connecting line from label area to the text
                g.DrawLine(dashedPen, labelStartX - 10, componentMidPoint.Y, labelPosition.X, labelPosition.Y + 10);
            }

            // Draw the label background
            Rectangle labelRect = new Rectangle(labelPosition.X, labelPosition.Y, 200, labelYStep - 5);
            using (Brush labelBackgroundBrush = new SolidBrush(Color.LightGray))
            {
                g.FillRectangle(labelBackgroundBrush, labelRect);
            }

            // Draw the label text
            string labelText = $"{dimension.ComponentName}: H={(dimension.HeightFinal - dimension.HeightInitial):F2}, D={dimension.Diameter:F2}";
            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                g.DrawString(labelText, labelFont, Brushes.Black, labelPosition);
            }

            // Increment the Y position for the next label
            labelY += labelYStep;
        }
    }

    private Point GetComponentMidPoint(string componentName, Rectangle imageBounds)
    {
        // Dynamically calculate positions for each component based on their location in the tank
        return componentName switch
        {
            "Base" => new Point(imageBounds.Left + imageBounds.Width / 2, imageBounds.Bottom - 50),
            "Cylinder" => new Point(imageBounds.Left + imageBounds.Width / 2, imageBounds.Top + imageBounds.Height / 2),
            "Tanks" => new Point(imageBounds.Left + imageBounds.Width / 2, imageBounds.Top + 50),
            _ => new Point(imageBounds.Left + imageBounds.Width / 2, imageBounds.Top) // Default to the top center
        };
    }

    private void DrawStraightDottedLine(Graphics g, Point componentPosition, Point labelPosition)
    {
        using (Pen pen = new Pen(Color.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
        {
            // Horizontal line from component to the axis
            int midX = labelPosition.X - 20; // Vertical axis for all labels
            g.DrawLine(pen, componentPosition, new Point(midX, componentPosition.Y));

            // Vertical line from the axis to the label
            g.DrawLine(pen, new Point(midX, componentPosition.Y), new Point(midX, labelPosition.Y));

            // Horizontal line to the label
            g.DrawLine(pen, new Point(midX, labelPosition.Y), labelPosition);
        }
    }



    private void DrawLabelWithBackground(Graphics g, string text, Point position)
    {
        var font = new Font("Segoe UI", 10, FontStyle.Bold);
        var textSize = g.MeasureString(text, font);

        // Draw semi-transparent background rectangle
        using (var backgroundBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
        {
            g.FillRectangle(backgroundBrush, new Rectangle(position, textSize.ToSize()));
        }

        // Draw the label text
        g.DrawString(text, font, Brushes.Black, position);
    }



    //private Point GetLabelPosition(string componentName, Rectangle imageBounds)
    //{
    //    // Customize positions based on the component name and layout requirements
    //    return componentName switch
    //    {
    //        "Base" => new Point(imageBounds.Left + 20, imageBounds.Bottom - 50), // Bottom-left
    //        "Cylinder" => new Point(imageBounds.Left + imageBounds.Width / 2, imageBounds.Top + imageBounds.Height / 2), // Center
    //        "Tanks" => new Point(imageBounds.Right - 200, imageBounds.Top + 20), // Top-right
    //        _ => new Point(imageBounds.Left + 20, imageBounds.Top + 20) // Default position
    //    };
    //}

    private List<Dimension> GetDimensionsFromDatabase()
    {
        return context.SegmentProperties
            .Select(d => new Dimension
            {
                ComponentName = d.SegmentType,
                HeightFinal = d.HeightFinal,
                HeightInitial = d.HeightInitial,
                Diameter = d.Diameter
            })
            .ToList();
    }

    // Dimension class for holding dimension data
    private class Dimension
    {
        public string ComponentName { get; set; }
        public double HeightFinal { get; set; }
        public double Diameter { get; set; }

        public double HeightInitial { get; set; }
    }

    private Point AdjustPositionForOverlap(Point labelPosition, List<Point> existingPositions)
    {
        // Adjust positions dynamically to avoid overlap
        foreach (var existingPosition in existingPositions)
        {
            if (Math.Abs(existingPosition.X - labelPosition.X) < 100 &&
                Math.Abs(existingPosition.Y - labelPosition.Y) < 40)
            {
                // Shift label downwards and slightly to the right if overlapping
                labelPosition.Offset(0, 30);
            }
        }
        return labelPosition;
    }

    private Point GetSegmentPosition(string componentName, Rectangle imageBounds)
    {
        // Dynamically determine segment positions
        return componentName switch
        {
            "Base" => new Point(imageBounds.X + imageBounds.Width / 2, imageBounds.Bottom - 50),
            "Cylinder" => new Point(imageBounds.X + imageBounds.Width / 2, imageBounds.Y + imageBounds.Height / 2),
            "Tank" => new Point(imageBounds.X + imageBounds.Width / 2, imageBounds.Y + 30),
            _ => new Point(imageBounds.X + imageBounds.Width / 2, imageBounds.Y + 10)
        };
    }

    private Point GetLabelPosition(string componentName, Rectangle imageBounds, int labelIndex)
    {
        int verticalSpacing = 25; // Space between labels
        int labelX = imageBounds.Right + 50; // Fixed X position for labels
        int labelY = imageBounds.Top + (labelIndex * verticalSpacing); // Vertical alignment

        return new Point(labelX, labelY);
    }


    private void DrawConnectingLine(Graphics g, Point start, Point end)
    {
        using (var dashedPen = new Pen(Color.Black, 1)
        {
            DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        })
        {
            g.DrawLine(dashedPen, start, end);
        }
    }




    private void panel1_MouseWheel(object sender, MouseEventArgs e)
    {
        if (e.Delta > 0)
            zoomFactor += 0.1f;
        else if (zoomFactor > 0.1f)
            zoomFactor -= 0.1f;

        panel1.Invalidate();
    }


    private void rotateButton_Click(object sender, EventArgs e)
    {
        rotationAngle = (rotationAngle + 15) % 360;

        panel1.Invalidate();

    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
        zoomFactor -= 0.1f;

        panel1.Invalidate();
    }

    private void snowLoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Snow_Load snow_Load = new Snow_Load();
        snow_Load.ShowDialog();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
    {

    }

    private void newToolStripButton_Click(object sender, EventArgs e)
    {
        CreateNewProject();

    }

    private void splitContainer2_Panel2_Paint_1(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void toolStripTextBox1_Click(object sender, EventArgs e)
    {

    }

    private void toolStripTextBox2_Click(object sender, EventArgs e)
    {

    }

    private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }
}