using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Entity;

namespace WaterTankTool_WFA
{
    public partial class AddSegmentSection : Form
    {

        private PictureBox _selectedPictureBox;
        private String SegmentType;
        public AddSegmentSection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SegmentDialogBox segmentDialogBox = new SegmentDialogBox(SegmentType);
            this.Close();
            DialogResult result  = segmentDialogBox.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SegmentType = "Cylinder";

            if (_selectedPictureBox != null)
            {
                _selectedPictureBox.BorderStyle = BorderStyle.None;
            }

            // Select the clicked PictureBox
            _selectedPictureBox = (PictureBox)sender;
            _selectedPictureBox.BorderStyle = BorderStyle.Fixed3D;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SegmentType = "Base";

            if (_selectedPictureBox != null)
            {
                _selectedPictureBox.BorderStyle = BorderStyle.None;
            }

            _selectedPictureBox = (PictureBox)sender;
            _selectedPictureBox.BorderStyle = BorderStyle.Fixed3D;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SegmentType = "Tanks";

            if (_selectedPictureBox != null)
            {
                _selectedPictureBox.BorderStyle = BorderStyle.None;
            }

            _selectedPictureBox = (PictureBox)sender;
            _selectedPictureBox.BorderStyle = BorderStyle.Fixed3D;

            TanksList tanksList = new TanksList();
            DialogResult result = tanksList.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                this.Close();
            }


        }
    }
}
