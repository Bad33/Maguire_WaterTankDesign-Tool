using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterTankTool_WFA.MultiColumn.Segments
{
    public partial class AddNoOfColumns : Form
    {
        private WaterTank _waterTankForm;
        private String SegmentType;


        public AddNoOfColumns(WaterTank waterTankForm,String _segmentType)
        {
            InitializeComponent();
            _waterTankForm = waterTankForm;
            SegmentType = _segmentType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AddSegmentSection addSegmentSection = new AddSegmentSection(_waterTankForm,TankType.MultiColumn);
            //DialogResult result = addSegmentSection.ShowDialog();

            SegmentDialogBox segmentDialogBox = new SegmentDialogBox(SegmentType, _waterTankForm, TankType.MultiColumn);
            this.Close();
            DialogResult result1 = segmentDialogBox.ShowDialog();
            if (result1 == DialogResult.OK || result1 == DialogResult.Cancel)
            {
                this.Close();
            }
        }
    }
}
