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
        private WaterTank _waterTankForm;
        private WaterTankDbContext _context;

        List<string> ggwp = new List<string>();

        private TankType _type;
        private int _noOfCol;
        public AddSegmentSection(WaterTank waterTankForm)
        {

            _waterTankForm = waterTankForm;
            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();

            setComboboxItems();

        }

        public AddSegmentSection(WaterTank waterTankForm, TankType tankType)
        {
            _type = tankType;
            _waterTankForm = waterTankForm;
            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();

            setComboboxItems();

            if (_type == TankType.MultiColumn)
            {
                label2.Text = "Columns";
                label3.Visible = false;
                pictureBox2.Visible = false;
            }

        }




        private void setComboboxItems()
        {
            var gg = _context.MaterialProperties.Select(x=> x.MaterialName).ToList();

            if(gg == null || gg.Count<=0)
            {
                comboBox1.Text = "Select the material type.";
            }
            else
            {
                comboBox1.Text = gg[0].ToString();
                foreach (var g in gg)
                {
                    comboBox1.Items.Add(g);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var columnCount = _context.SegmentProperties.Where(x => x.SegmentType == "Cylinder").ToList();


            if(_type == TankType.MultiColumn && SegmentType == "Cylinder" && columnCount.Count() == 0)
            {
                MultiColumn.Segments.AddNoOfColumns addNoOfColumns = new MultiColumn.Segments.AddNoOfColumns(_waterTankForm,SegmentType);
                DialogResult result2 = addNoOfColumns.ShowDialog();
                if (result2 == DialogResult.OK || result2 == DialogResult.Cancel)
                {

                    this.Close();
                }

            }
            else if (_type == TankType.MultiColumn && SegmentType == "Cylinder" && columnCount.Count() > 0)
            {
                SegmentDialogBox segmentDialogBox = new SegmentDialogBox(SegmentType, _waterTankForm);

                DialogResult result2 = segmentDialogBox.ShowDialog();
                if (result2 == DialogResult.OK)
                {
                    SegmentDialogBox segmentDialogBoxRizor = new SegmentDialogBox("Riser", _waterTankForm, "Add Riser");
                    this.Close();
                    DialogResult result1 = segmentDialogBoxRizor.ShowDialog();
                    this.Close();
                }
                else if (result2 == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            else if (_type == TankType.MultiColumn && SegmentType == "Tanks" )
            {
                SegmentDialogBox segmentDialogBox = new SegmentDialogBox(SegmentType, _waterTankForm);
                this.Close();
                DialogResult result1 = segmentDialogBox.ShowDialog();
                if (result1 == DialogResult.OK || result1 == DialogResult.Cancel)
                {

                    this.Close();
                }
         
            }

            else
            {
                SegmentDialogBox segmentDialogBox = new SegmentDialogBox(SegmentType, _waterTankForm);
                this.Close();
                DialogResult result = segmentDialogBox.ShowDialog();
                if (result == DialogResult.OK || result == DialogResult.Cancel)
                {
                    this.Close();
                }

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

            //TanksList tanksList = new TanksList(_waterTankForm);
            //DialogResult result = tanksList.ShowDialog();
            //if (result == DialogResult.OK || result == DialogResult.Cancel)
            //{
            //    this.Close();
            //}


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //comboBox1.Items = ggwp.ToList();
        }
    }
}
