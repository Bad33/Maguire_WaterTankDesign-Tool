using System.ComponentModel;
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

    public WaterTank()
    {
        InitializeComponent();
        var _context = WaterTankDbContext.GetInstance();
        context = _context;

        panelDrawTankCapacity();


        this.Paint += panel1_Paint_1;
        panel1.MouseWheel += panel1_MouseWheel;
        panel1.MouseDown += panel1_MouseDown;
        panel1.MouseMove += panel1_MouseMove;
        panel1.MouseUp += panel1_MouseUp;


        this.Resize += (s, e) => this.Invalidate();
    }



    private void panelDrawTankCapacity()
    {
        var tankCap = context.TankProperties?.FirstOrDefault();

        if (tankCap?.Capacity != null)
        {
            switch (tankCap.Capacity)
            {
                case "150,000 gallon":
                    drawingImage = Image.FromFile("../../../../icons/150K.png");
                    break;
                case "250,000 gallon":
                    drawingImage = Image.FromFile("../../../../icons/250K.png");
                    break;
                case "500,000 gallon":
                    drawingImage = Image.FromFile("../../../../icons/500K.png");
                    break;
                default:
                    drawingImage = Image.FromFile("../../../../icons/150K.png");
                    break;
            }
        }
        else
        {
            // Set drawingImage to null to indicate a blank panel
            drawingImage = null;
        }

        panel1.Invalidate();
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
        panel1.Invalidate();

    }

    public void OnSegmentDeleted()
    {
        panelDrawTankCapacity();
        panel1.Invalidate();


    }

    public List<SegmentProperties> GetSegmentsFromDatabase()
    {
        List<SegmentProperties> segments = new List<SegmentProperties>();

        using (var context = WaterTankDbContext.GetInstance())
        {
            segments = context.SegmentProperties.ToList();
        }

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
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

        // Clear the panel to avoid residual drawings
        g.Clear(Color.White);

        // Only draw the image if it exists
        if (drawingImage != null)
        {
            int scaledWidth = (int)(drawingImage.Width * zoomFactor);
            int scaledHeight = (int)(drawingImage.Height * zoomFactor);

            // Calculate the top-left corner to center the image
            int x = (panel1.Width - scaledWidth) / 2;
            int y = (panel1.Height - scaledHeight) / 2;

            // Apply rotation transformation around the image center
            g.TranslateTransform(panel1.Width / 2f, panel1.Height / 2f);
            g.RotateTransform(rotationAngle);
            g.TranslateTransform(-panel1.Width / 2f, -panel1.Height / 2f);

            // Draw the scaled and rotated image
            Rectangle destRect = new Rectangle(x, y, scaledWidth, scaledHeight);
            g.DrawImage(drawingImage, destRect);

            g.ResetTransform();
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
}