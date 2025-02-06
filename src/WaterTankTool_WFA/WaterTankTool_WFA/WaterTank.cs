using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
    private ToolStripStatusLabel noLoadStatus;
    private StartupForm _startupForm;
    public WaterTank(StartupForm startupForm)
    {
        InitializeComponent();
        var _context = WaterTankDbContext.GetInstance();
        context = _context;
        _startupForm = startupForm;

        InitializeUIComponents();
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

        noLoadStatus = new ToolStripStatusLabel
        {
            Text = "Please add the Load!",
            ForeColor = Color.Red,
            Spring = true, // Allow this label to stretch
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
        if (statusStrip2.Items.Contains(noMaterialStatus))
        {
            Console.WriteLine("muji");
        }

        statusStrip2.Items.Add(noLoadStatus);
        statusStrip2.Items.Add(designDetailsLabel);
    }

    private void InitializeLayout()
    {

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

        Button exportButton = new Button
        {
            Text = "Export Diagram",
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = Color.Gray,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Popup
        };
        exportButton.Click += ExportDiagram;
        splitContainer2.Panel2.Controls.Add(exportButton);


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

    private void InitializeUIComponents()
    {
        // Export Button

    }
    private void ExportDiagram(object sender, EventArgs e)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
            saveFileDialog.Title = "Export Diagram";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Create a bitmap with the panel's dimensions
                using (Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        // Draw the tank image and dimensions as in the Panel_Paint event
                        if (drawingImage != null)
                        {
                            int scaledWidth = (int)(drawingImage.Width * zoomFactor);
                            int scaledHeight = (int)(drawingImage.Height * zoomFactor);
                            int x = (panel1.Width - scaledWidth) / 2;
                            int y = (panel1.Height - scaledHeight) / 2;
                            Rectangle destRect = new Rectangle(x, y, scaledWidth, scaledHeight);

                            g.TranslateTransform(destRect.X + scaledWidth / 2, destRect.Y + scaledHeight / 2);
                            g.RotateTransform(rotationAngle);
                            g.TranslateTransform(-(destRect.X + scaledWidth / 2), -(destRect.Y + scaledHeight / 2));

                            g.DrawImage(drawingImage, destRect);

                            // Draw the dimensions
                            DrawDimensions(g, destRect);
                        }
                    }

                    // Save the bitmap to the selected file
                    bitmap.Save(saveFileDialog.FileName, saveFileDialog.FilterIndex == 1 ? ImageFormat.Png : ImageFormat.Jpeg);
                }

                MessageBox.Show("Diagram exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }





    private void panelDrawTankCapacity()
    {
        var tankCap = context.TankProperties?.FirstOrDefault();

        //var gg = context.MaterialProperties.Select(x => x.MaterialName).ToList();
        //if (gg.Count > 0)
        //{

        UpdateMaterial();
        UpdateLoadStatus();
        //}


        if (tankCap?.Capacity != null)
        {
            switch (tankCap.Capacity)
            {
                case "150,000 gallon":
                    drawingImage = Properties.Resources._150k;
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails(tankCap.Capacity, tankCap.TotalWeight, tankCap.ProjectedArea);
                    break;
                case "250,000 gallon":
                    drawingImage = Properties.Resources._250k;
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails(tankCap.Capacity, tankCap.TotalWeight, tankCap.ProjectedArea);

                    break;
                case "500,000 gallon":
                    drawingImage = Properties.Resources._500k;
                    UpdateAppStatus("Loading Tank...");
                    UpdateDesignDetails(tankCap.Capacity, tankCap.TotalWeight, tankCap.ProjectedArea);

                    break;
                default:
                    drawingImage = Properties.Resources._150k;
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

    private void UpdateMaterial()
    {

        var material = context.MaterialProperties.FirstOrDefault();
        if (material != null)
        {

            selectedMaterialLabel.Text = $"Material: {material.MaterialName}";
            statusStrip2.Items.Remove(noMaterialStatus);
            

        }
        else
        {
            selectedMaterialLabel.Text = $"Material: None";

        }


    }

    private void UpdateLoadStatus()
    {
        var liveLoad = context.LiveLoadEntity.FirstOrDefault();
        var seismicLoad = context.SeismicLoadEntity.FirstOrDefault();
        var snowLoad = context.SnowLoadEntity.FirstOrDefault();
        var windLoad = context.WindLoadEntity.FirstOrDefault();

        if (liveLoad != null && seismicLoad != null && snowLoad !=null && windLoad !=null)
        {

            statusStrip2.Items.Remove(noLoadStatus);


        }
        //else
        //{
        //    selectedMaterialLabel.Text = $"Material: None";

        //}
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

    public void OnmaterialAdded()
    {
        UpdateMaterial();

    }

    public void OnMaterialDeleted()
    {
        UpdateMaterial();
        //statusStrip2.Items.Add(noMaterialStatus);


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



    private void addSegmentToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SegmentDialogBox dialog = new SegmentDialogBox();

        dialog.ShowDialog();
    }

    private void materialToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Define_Materials dialog = new Define_Materials(this);

        dialog.ShowDialog();
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

                    _startupForm.OpenProject(projectPath);
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

    private void OpenProject(string projectPath)
    {
        //try
        //{
        //    // Load project content
        //    string projectContent = File.ReadAllText(projectPath);

        //    // Initialize the UI or application state with the loaded project
        //    InitializeProjectUI(projectContent);

        //    // Optionally, set the current project path for future use
        //    CurrentProjectPath = projectPath; // Make sure you have this variable declared
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show($"Error opening project: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
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
        Define_Materials define_Materials = new Define_Materials(this);
        define_Materials.ShowDialog();
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

        // Apply transformations for scaling and rotation
        g.TranslateTransform(panel1.Width / 2, panel1.Height / 2); // Move origin to center
        g.RotateTransform(rotationAngle);                         // Apply rotation
        g.ScaleTransform(zoomFactor, zoomFactor);                  // Apply scaling
        g.TranslateTransform(-panel1.Width / 2, -panel1.Height / 2); // Move origin back

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

            // Draw the dimensions
            DrawDimensions(g, destRect);
        }

        // Reset transformations
        g.ResetTransform();
    }






    private void DrawTankWithArrowDimensions(Graphics g)
    {
        if (drawingImage == null) return;

        int scaledWidth = (int)(drawingImage.Width * zoomFactor);
        int scaledHeight = (int)(drawingImage.Height * zoomFactor);

        int xOffset = (panel1.Width - scaledWidth) / 2; // Center the tank horizontally
        int yOffset = (panel1.Height - scaledHeight) / 2; // Center the tank vertically

        int arrowOffsetX = xOffset + scaledWidth + 40; // Space for arrows to the right
        int labelOffsetX = arrowOffsetX + 30; // Space for labels to the right of arrows

        List<Rectangle> existingLabels = new List<Rectangle>(); // To prevent overlap

        var dimensions = GetDimensionsFromDatabase();

        foreach (var dimension in dimensions)
        {
            int segmentTop = yOffset + (int)(scaledHeight * (dimension.HeightInitial / 100.0));
            int segmentBottom = yOffset + (int)(scaledHeight * (dimension.HeightFinal / 100.0));

            Point arrowStart, arrowEnd, labelPosition;

            if (dimension.ComponentName == "Tank") // Tank section at the very top
            {
                arrowStart = new Point(arrowOffsetX, segmentTop);
                arrowEnd = new Point(arrowOffsetX, segmentTop + 40); // Short arrow for tank
                labelPosition = new Point(labelOffsetX, segmentTop + 5); // Close to the top
            }
            else if (dimension.ComponentName == "Base") // Base section at the bottom
            {
                arrowStart = new Point(arrowOffsetX, segmentBottom - 40); // Short arrow for base
                arrowEnd = new Point(arrowOffsetX, segmentBottom);
                labelPosition = new Point(labelOffsetX, segmentBottom - 20); // Close to the bottom
            }
            else // Cylinder sections dynamically positioned
            {
                arrowStart = new Point(arrowOffsetX, segmentTop);
                arrowEnd = new Point(arrowOffsetX, segmentBottom);
                int labelY = (segmentTop + segmentBottom) / 2; // Center label vertically
                //labelPosition = AdjustLabelPosition(new Point(labelOffsetX, labelY), existingLabels);
            }

            // Draw arrows
            DrawVerticalArrowLine(g, arrowStart, arrowEnd, Color.Black);

            // Draw labels
            string label = $"{dimension.ComponentName}: H={dimension.HeightFinal - dimension.HeightInitial:F2}, D={dimension.Diameter:F2}";
            SizeF textSize = g.MeasureString(label, new Font("Segoe UI", 10, FontStyle.Bold));
            //Rectangle labelRect = new Rectangle(labelPosition, textSize.ToSize());
            //existingLabels.Add(labelRect); // Track label positions to avoid overlap
            //g.DrawString(label, new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, labelPosition);
        }
    }








    private Point AdjustLabelPosition(RectangleF labelBounds, List<RectangleF> existingLabelBounds, ref RectangleF adjustedBounds)
    {
        int verticalSpacing = 5; // Space between labels in pixels
        bool overlap;

        // Initialize the adjusted position with the original label bounds
        Point adjustedPosition = new Point((int)labelBounds.X, (int)labelBounds.Y);

        do
        {
            overlap = false;

            foreach (var existingBound in existingLabelBounds)
            {
                if (adjustedBounds.IntersectsWith(existingBound))
                {
                    // If overlapping, move the label downward by verticalSpacing
                    adjustedPosition.Y += verticalSpacing;
                    adjustedBounds.Y += verticalSpacing;
                    overlap = true;
                    break; // Exit the loop to re-check with updated position
                }
            }

        } while (overlap); // Repeat until no overlap is detected

        return adjustedPosition;
    }











    private void DrawVerticalArrowLine(Graphics g, Point start, Point end, Color color)
    {
        using (Pen pen = new Pen(color, 2))
        {
            AdjustableArrowCap arrowCap = new AdjustableArrowCap(5, 5);
            pen.CustomStartCap = arrowCap;
            pen.CustomEndCap = arrowCap;

            g.DrawLine(pen, start, end);
        }
    }






    private void DrawDimensions(Graphics g, Rectangle imageBounds)
    {
        // Retrieve dimension data from the database
        var dimensions = GetDimensionsFromDatabase();

        if (dimensions == null || dimensions.Count == 0)
            return;

        // Calculate the total tank height in feet
        double totalTankHeight = dimensions.Max(d => d.HeightFinal) - dimensions.Min(d => d.HeightInitial);

        if (totalTankHeight <= 0)
            return; // Prevent division by zero or negative scaling

        // Calculate scaling factor: pixels per foot
        double scaleFactor = imageBounds.Height / 746;

        // Define offsets for lines and labels
        int lineXOffset = imageBounds.Right + 40; // 40 pixels to the right of the image
        int labelXOffset = lineXOffset + 20;     // 20 pixels further for the label

        // Length multiplier for the vertical line based on segment height
        double verticalLineLengthMultiplier = 1.0; // Adjust as needed for visibility

        // List to keep track of existing label bounds to prevent overlaps
        List<RectangleF> existingLabelBounds = new List<RectangleF>();

        int designTankHeight = 243;
        int designCylinderHeight = 350;
        int designBaseHeight = 153;
        int totalDesignHeight = designTankHeight + designCylinderHeight + designBaseHeight; //746

        int tankHeight = (int)(designTankHeight * scaleFactor);
        int cylinderHeight = (int)(designCylinderHeight * scaleFactor);
        int baseHeight = (int)(designBaseHeight * scaleFactor);

        int tankTop = imageBounds.Top;
        int tankBottom = tankTop + tankHeight;
        int cylinderTop = tankBottom;
        int cylinderBottom = cylinderTop + cylinderHeight;
        int baseTop = cylinderBottom;
        // Ideally, baseBottom should be imageBounds.Bottom
        int baseBottom = imageBounds.Bottom;

        foreach (var dimension in dimensions)
        {
            // Calculate the average height of the segment
            double averageHeight = (dimension.HeightFinal + dimension.HeightInitial) / 2.0;
            int segmentY = 0;
            int flag = 0;
            if (dimension.ComponentName == "Base")
            {
                double ratio = dimension.HeightInitial / (double)designBaseHeight;
                segmentY = baseBottom;
                flag = designBaseHeight;

            }
            else if(dimension.ComponentName == "Cylinder")
            {
                double ratio = dimension.HeightInitial / (double)designCylinderHeight;
                segmentY = cylinderBottom;
                flag = designBaseHeight;

            }
            else if( dimension.ComponentName == "Tanks")
            {
                double ratio = dimension.HeightInitial / (double)designTankHeight;
                segmentY = tankBottom;
                flag = designTankHeight;
            }

            // Calculate the Y position on the image (tank at top, base at bottom)
            //segmentY = imageBounds.Top + imageBounds.Height - (int)(averageHeight * scaleFactor);

            // Calculate the height of the vertical line based on segment height
            //int verticalLineHeight = (int)((dimension.HeightFinal - dimension.HeightInitial) * scaleFactor * verticalLineLengthMultiplier);

            // Define start and end points for the vertical line (upward)
            Point lineStart = new Point(lineXOffset, segmentY);
            Point lineEnd = new Point(lineXOffset, segmentY-flag); // Line goes upward

            // Draw the vertical line with arrows at both ends
            DrawDoubleArrowVerticalLine(g, lineStart, lineEnd, Color.Black);

            // Prepare label text
            string labelText = $"{dimension.ComponentName}: H={dimension.HeightFinal - dimension.HeightInitial:F2} ft, D={dimension.Diameter:F2} ft";

            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                // Measure the size of the label text
                SizeF textSize = g.MeasureString(labelText, labelFont);

                // Define the initial label position (to the right of the vertical line)
                Point labelPosition = new Point(labelXOffset, lineEnd.Y - (int)(textSize.Height / 2));

                // Define the background rectangle for the label
                RectangleF labelBackground = new RectangleF(labelPosition, textSize);

                // Adjust label position to prevent overlap
                labelPosition = AdjustLabelPosition(labelBackground, existingLabelBounds, ref labelBackground);

                // Add the adjusted label bounds to the list
                existingLabelBounds.Add(labelBackground);

                // Draw the label background
                using (Brush backgroundBrush = new SolidBrush(Color.LightYellow))
                {
                    g.FillRectangle(backgroundBrush, labelBackground);
                }

                // Optionally, draw a border around the label
                using (Pen borderPen = new Pen(Color.Gray, 1))
                {
                    g.DrawRectangle(borderPen, labelBackground.X, labelBackground.Y, labelBackground.Width, labelBackground.Height);
                }

                // Draw the label text
                g.DrawString(labelText, labelFont, Brushes.Black, labelPosition);
            }
        }
    }





    private void DrawDoubleArrowVerticalLine(Graphics g, Point start, Point end, Color color)
    {
        using (Pen pen = new Pen(color, 2))
        {
            // Define arrow caps for both ends
            AdjustableArrowCap arrowCap = new AdjustableArrowCap(5, 5);
            pen.CustomStartCap = arrowCap;
            pen.CustomEndCap = arrowCap;

            // Draw the vertical line with arrows at both ends
            g.DrawLine(pen, start, end);
        }
    }



    private void DrawHorizontalArrowLine(Graphics g, Point start, Point end, Color color)
    {
        using (Pen pen = new Pen(color, 2))
        {
            AdjustableArrowCap arrowCap = new AdjustableArrowCap(5, 5);
            pen.CustomEndCap = arrowCap;
            g.DrawLine(pen, start, end);
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

    private Point GetSegmentPosition(string componentName, Rectangle bounds)
    {
        return componentName switch
        {
            "Base" => new Point(bounds.X + bounds.Width / 2, bounds.Bottom - (bounds.Height / 10)), // Near bottom
            "Cylinder" => new Point(bounds.X + bounds.Width / 2, bounds.Top + (bounds.Height / 2)), // Center
            "Tank" => new Point(bounds.X + bounds.Width / 2, bounds.Top + 50), // Near top
            _ => new Point(bounds.X + bounds.Width / 2, bounds.Top) // Default top position
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

    private void openToolStripMenuItem_Click(object sender, EventArgs e) { }

    private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e) { }

    private void newToolStripButton_Click(object sender, EventArgs e)
    {
        CreateNewProject();

    }

    private void splitContainer2_Panel2_Paint_1(object sender, PaintEventArgs e) { }

    private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e) { }

    private void toolStripTextBox1_Click(object sender, EventArgs e) { }

    private void toolStripTextBox2_Click(object sender, EventArgs e) { }

    private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

    private void Form1_Click(object sender, EventArgs e) { }

    private void toolStripMenuItem1_Click(object sender, EventArgs e) { }

    private void toolStripMenuItem2_Click(object sender, EventArgs e) { }

    private void Form1_Load(object sender, EventArgs e)
    {
        panel1.Invalidate();
    }

    private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) { }
    private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e) { }

    private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e) { }

    private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e) { }

    private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e) { }

    private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }

    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }

    private void toolStripStatusLabel1_Click(object sender, EventArgs e) { }

    private void panel1_Paint(object sender, PaintEventArgs e) { }
    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

    private void toolsToolStripMenuItem_Click(object sender, EventArgs e) { }

    private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

    private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

    private void toolStripButton1_Click(object sender, EventArgs e) { }

    private void helpToolStripButton_Click(object sender, EventArgs e)
    {
        Help help = new Help();
        help.ShowDialog();
    }
}