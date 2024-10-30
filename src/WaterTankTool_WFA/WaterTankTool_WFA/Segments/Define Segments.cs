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
        WaterTank form1 = new WaterTank();
        public Define_Segments()
        {
            InitializeComponent();

            _context = new WaterTankDbContext();
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = _context.SegmentProperties.ToList();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSegmentSection addSegmentSection = new AddSegmentSection();
            var result = addSegmentSection.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadData();
            }

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
                    using (var context = new WaterTankDbContext())
                    {
                        var segmentProperties = context.SegmentProperties.FirstOrDefault(item => item.SegmentNumber == segmentNumber);

                        if (segmentProperties != null)
                        {
                            context.SegmentProperties.Remove(segmentProperties);
                            context.SaveChanges();
                            MessageBox.Show($"Segment {segmentName} deleted successfully.");
                            LoadData();
                            form1.OnSegmentDeleted();
                        }
                        else
                        {
                            MessageBox.Show("Selected Segment not found");
                        }
                    }
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

                SegmentDialogBox segmentDialogBox = new SegmentDialogBox(segmentNumber, "Modify");
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
