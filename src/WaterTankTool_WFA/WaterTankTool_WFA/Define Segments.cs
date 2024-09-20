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
        public Define_Segments()
        {
            InitializeComponent();

            _context = new WaterTankDbContext();
            dataGridView1.DataSource = _context.SegmentProperties.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SegmentDialogBox segmentDialogBox = new SegmentDialogBox();
            segmentDialogBox.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = _context.SegmentProperties.ToList();

            // Enable sorting for all columns
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

        }

    }
}
