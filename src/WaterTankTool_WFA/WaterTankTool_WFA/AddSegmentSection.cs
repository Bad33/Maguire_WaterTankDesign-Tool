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
        public AddSegmentSection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SegmentDialogBox segmentDialogBox = new SegmentDialogBox();
            segmentDialogBox.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (_selectedPictureBox != null)
            {
                _selectedPictureBox.BorderStyle = BorderStyle.None;
            }

            // Select the clicked PictureBox
            _selectedPictureBox = (PictureBox)sender;
            _selectedPictureBox.BorderStyle = BorderStyle.Fixed3D;

        }
    }
}
