using System.Windows.Forms;
using WaterTankTool_WFA.Custom_Design_Control;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Load;
using WaterTankTool_WFA.Solver;

namespace WaterTankTool_WFA;

public partial class Form1 : Form
{

    StructuralDrawingForm tankDesign = new StructuralDrawingForm();
    private Image drawingImage;
    private Point lastMousePosition;
    private bool isDragging = false;
    public Form1()
    {
        InitializeComponent();

        drawingImage = Image.FromFile("../../../../icons/150K.png");

        this.Paint += panel1_Paint_1;
        panel1.MouseWheel += panel1_MouseWheel;
        panel1.MouseDown += panel1_MouseDown;
        panel1.MouseMove += panel1_MouseMove;
        panel1.MouseUp += panel1_MouseUp;


        this.Resize += (s, e) => this.Invalidate();

    }

    private void panel1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = true;
            lastMousePosition = e.Location; // Store the initial mouse position
        }
    }

    // Mouse Move - Handle the dragging and update the rotation angle
    private void panel1_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            // Calculate the difference in mouse movement
            int deltaX = e.X - lastMousePosition.X;
            int deltaY = e.Y - lastMousePosition.Y;

            // Adjust the rotation angle based on horizontal movement
            rotationAngle += deltaX * 0.5f; // Scale the angle change for smoother rotation

            // Store the current mouse position for the next movement calculation
            lastMousePosition = e.Location;

            // Force the panel to redraw with the new rotation angle
            panel1.Invalidate();
        }
    }

    // Mouse Up - Stop the drag operation
    private void panel1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = false; // Stop dragging
        }
    }

    public void OnSegmentAdded(SegmentProperties newSegment)
    {
        //if (InvokeRequired)
        //{
        //    this.Invoke(new Action(() => OnSegmentAdded(newSegment)));
        //    return;
        //}
        //tankDesign.Segments = GetSegmentsFromDatabase();

        //tableLayoutPanel1.PerformLayout();
        //tankDesign.PerformLayout();

        //tankDesign.Redraw();
    }

    public void OnSegmentDeleted()
    {
        //tankDesign.Segments = GetSegmentsFromDatabase();

        //tableLayoutPanel1.PerformLayout();
        //tankDesign.PerformLayout();

        //tankDesign.Redraw();

    }


    public List<SegmentProperties> GetSegmentsFromDatabase()
    {
        List<SegmentProperties> segments = new List<SegmentProperties>();

        using (var context = new WaterTankDbContext())
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
        Define_Segments define_Segments = new Define_Segments();
        define_Segments.ShowDialog();
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {

        Define_Segments define_Segments = new Define_Segments();
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
        //tableLayoutPanel1.ZoomFactor += 0.1f;
        //tableLayoutPanel1.Refresh();
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
    private float zoomFactor = 1.0f; // Initial zoom factor
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

        // Calculate the scaled dimensions of the image
        int scaledWidth = (int)(drawingImage.Width * zoomFactor);
        int scaledHeight = (int)(drawingImage.Height * zoomFactor);

        // Calculate the top-left corner to center the image
        int x = (panel1.Width - scaledWidth) / 2;
        int y = (panel1.Height - scaledHeight) / 2;

        // Apply rotation transformation around the image center
        g.TranslateTransform(panel1.Width / 2f, panel1.Height / 2f); // Move to center
        g.RotateTransform(rotationAngle); // Rotate by the specified angle
        g.TranslateTransform(-panel1.Width / 2f, -panel1.Height / 2f); // Move back

        // Draw the scaled and rotated image
        Rectangle destRect = new Rectangle(x, y, scaledWidth, scaledHeight);
        g.DrawImage(drawingImage, destRect);

        // Reset transformations to avoid affecting future drawings
        g.ResetTransform();
    }



    private void panel1_MouseWheel(object sender, MouseEventArgs e)
    {
        // Adjust the zoom factor based on the mouse wheel scroll direction
        if (e.Delta > 0)
            zoomFactor += 0.1f; // Zoom in
        else if (zoomFactor > 0.1f)
            zoomFactor -= 0.1f; // Zoom out

        panel1.Invalidate(); // Force redraw to apply zoom
    }


    private void rotateButton_Click(object sender, EventArgs e)
    {
        // Increase the rotation angle by 15 degrees (or any desired value)
        rotationAngle = (rotationAngle + 15) % 360;

        panel1.Invalidate(); // Force redraw to apply rotation
    }






}