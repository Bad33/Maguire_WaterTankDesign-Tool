using System.Windows.Forms;
using WaterTankTool_WFA.Load;
using WaterTankTool_WFA.Solver;

namespace WaterTankTool_WFA;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
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
}