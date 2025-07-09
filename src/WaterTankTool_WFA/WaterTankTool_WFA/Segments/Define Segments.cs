using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public partial class Define_Segments : Form
    {
        private WaterTankDbContext _context;
        MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
        DialogResult result;
        private WaterTank _waterTankForm;
        private TankType _tankType;
        public Define_Segments(WaterTank waterTank, TankType tankType)
        {

            _waterTankForm = waterTank;
            _tankType = tankType;

            InitializeComponent();
            var context = WaterTankDbContext.GetInstance();

            _context = context;
            LoadData();
        }

        private void LoadData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            dataGridView1.DataSource = segmentData;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if(_tankType == TankType.SingleColumn)
            //{
            AddSegmentSection addSegmentSection = new AddSegmentSection(_waterTankForm, _tankType);
            DialogResult result = addSegmentSection.ShowDialog();
            //if (result == DialogResult.Cancel || result == DialogResult.OK)
            //{
            //    this.Close();
            //}
            LoadData();
            //}

            //else if(_tankType == TankType.MultiColumn)
            //{
            //    MultiColumn.Segments.AddNoOfColumns addNoOfColumns = new MultiColumn.Segments.AddNoOfColumns(_waterTankForm);
            //    DialogResult result = addNoOfColumns.ShowDialog();
            //    LoadData();
            //}



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = _context.SegmentProperties.ToList();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int segmentNumber = (int)selectedRow.Cells[0].Value;
                string segmentName = selectedRow.Cells[1].Value.ToString();

                result = MessageBox.Show($"Do you want to delete {segmentName}?", "Confirm Delete", buttons, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //using (var context = WaterTankDbContext.GetInstance())
                    //{
                        var segmentProperties = _context.SegmentProperties.FirstOrDefault(item => item.SegmentNumber == segmentNumber);
                        var tankProperties = _context.TankProperties.ToList();

                        if (segmentProperties != null)
                        {
                            _context.TankProperties.RemoveRange(tankProperties);

                            _context.SegmentProperties.Remove(segmentProperties);
                            _context.SaveChanges();
                            //MessageBox.Show($"Segment {segmentName} deleted successfully.");
                            LoadData();
                            
                            _waterTankForm.OnSegmentDeleted();
                        }
                        else
                        {
                            MessageBox.Show("Selected Segment not found");
                        }
                    //}
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int segmentNumber = (int)selectedRow.Cells[0].Value;

                SegmentDialogBox segmentDialogBox = new SegmentDialogBox(segmentNumber, "Modify",_waterTankForm);
                var result = segmentDialogBox.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to modify");
            }
        }
    }
}
