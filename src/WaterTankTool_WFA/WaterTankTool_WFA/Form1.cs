using System.Windows.Forms;
using WaterTankTool_WFA.Custom_Design_Control;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Load;
using WaterTankTool_WFA.Solver;

namespace WaterTankTool_WFA;

public partial class Form1 : Form
{

     TankDesign tankDesign = new TankDesign();

    public Form1()
    {
        InitializeComponent();

        tankDesign.Dock = DockStyle.None;
        tankDesign.Anchor = AnchorStyles.None;
        tankDesign.Size = new Size(150, 150); // Adjust as needed

        tableLayoutPanel1.SetCellPosition(tankDesign, new TableLayoutPanelCellPosition(0, 0));
        tableLayoutPanel1.SetColumnSpan(tankDesign, tableLayoutPanel1.ColumnCount);
        tableLayoutPanel1.SetRowSpan(tankDesign, tableLayoutPanel1.RowCount);
        tableLayoutPanel1.Controls.Add(tankDesign,0,0);
        tankDesign.BringToFront();
        tankDesign.Segments = GetSegmentsFromDatabase();

        tankDesign.Invalidate();


    }

    public void OnSegmentAdded(SegmentProperties newSegment)
    {
        if (InvokeRequired)
        {
            this.Invoke(new Action(() => OnSegmentAdded(newSegment)));
            return;
        }
        tankDesign.Segments = GetSegmentsFromDatabase();

        tableLayoutPanel1.PerformLayout();
        tankDesign.PerformLayout();

        tankDesign.Redraw();
    }

    public void OnSegmentDeleted()
    {
        tankDesign.Segments = GetSegmentsFromDatabase();

        tableLayoutPanel1.PerformLayout();
        tankDesign.PerformLayout();

        tankDesign.Redraw();

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
}