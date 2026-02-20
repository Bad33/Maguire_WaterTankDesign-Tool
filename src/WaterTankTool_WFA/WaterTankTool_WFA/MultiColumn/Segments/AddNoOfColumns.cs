using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterTankTool_WFA.MultiColumn.Segments
{
    public partial class AddNoOfColumns : Form
    {
        private WaterTank _waterTankForm;
        private String SegmentType;
        private int noOfCol;

        public AddNoOfColumns(WaterTank waterTankForm, String _segmentType)
        {
            InitializeComponent();
            _waterTankForm = waterTankForm;
            SegmentType = _segmentType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1) Store globally
            AppState.NoOfColumns = (int)numericUpDown1.Value;

            AppState.NoOfSegment = (int)numericUpDown2.Value;

            AppState.struts = Double.Parse(textBox1.Text);

            AppState.crossBracing = Double.Parse(textBox2.Text);

            AppState.Save();

            // 2) Launch the dialog – pass it only what’s unique (segment type)
            var dlg = new SegmentDialogBox(SegmentType, _waterTankForm);

            DialogResult dr = dlg.ShowDialog();
            if (dr == DialogResult.OK || dr == DialogResult.Cancel)
            {
                SegmentDialogBox segmentDialogBoxRizor = new SegmentDialogBox("Riser", _waterTankForm, "Add Riser");
                this.Close();
                DialogResult result1 = segmentDialogBoxRizor.ShowDialog();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
