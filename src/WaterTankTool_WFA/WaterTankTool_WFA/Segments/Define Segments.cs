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
using WaterTankTool_WFA.Entity;

namespace WaterTankTool_WFA
{
    public partial class Define_Segments : Form
    {
        private WaterTankDbContext _context;
        MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
        DialogResult result;
        private WaterTank _waterTankForm;
        private TankType _tankType;
        public Define_Segments(WaterTank waterTank)
        {

            _waterTankForm = waterTank;
            _tankType = AppState.CurrentTankType;

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

        // ──────────────────────────────────────────────────────────────
        //  Utility: return the part before "_n" when suffix is numeric
        //  "TankWall_3" → "TankWall"   ;   "TopRing" → "TopRing"
        // ──────────────────────────────────────────────────────────────
        private static string BaseName(string name)
        {
            int idx = name.LastIndexOf('_');
            return (idx > 0 && int.TryParse(name[(idx + 1)..], out _))
                   ? name[..idx]          // strip numeric suffix
                   : name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete");
                return;
            }

            // Read key values from the grid safely
            DataGridViewRow selRow = dataGridView1.SelectedRows[0];

            if (selRow.Cells[0].Value == null || selRow.Cells[1].Value == null)
            {
                MessageBox.Show("Selected row is invalid.");
                return;
            }

            int segNumber;
            if (!int.TryParse(selRow.Cells[0].Value.ToString(), out segNumber))
            {
                MessageBox.Show("Unable to read segment number.");
                return;
            }

            string segName = selRow.Cells[1].Value.ToString();

            // Fetch the segment from DB
            var seg = _context.SegmentProperties
                              .FirstOrDefault(s => s.SegmentNumber == segNumber);

            if (seg == null)
            {
                MessageBox.Show("Selected segment not found.");
                return;
            }

            // Confirm deletion (single row only)
            string confirmMessage = $"Do you want to delete {segName}?";
            var confirm = MessageBox.Show(confirmMessage, "Confirm Delete",
                                          buttons, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                // If you still intend to clear TankProperties like before,
                // leave this as-is; otherwise remove this line.
                _context.TankProperties.RemoveRange(_context.TankProperties);

                // Delete ONLY the selected segment
                _context.SegmentProperties.Remove(seg);

                int rows = _context.SaveChanges();

                if (rows > 0)
                {
                    // Refresh grid
                    LoadData();

                    // Notify parent form if a tank was deleted (so it can refresh drawing/state)
                    if (seg.SegmentType == "Tanks")
                    {
                        _waterTankForm?.OnSegmentDeleted();
                    }
                }
                else
                {
                    MessageBox.Show("Nothing was deleted. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int segmentNumber = (int)selectedRow.Cells[0].Value;

                SegmentDialogBox segmentDialogBox = new SegmentDialogBox(segmentNumber, "Modify", _waterTankForm);
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

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "SegmentType"
                && e.Value?.ToString() == "Cylinder"
                && AppState.CurrentTankType == TankType.MultiColumn)
            {
                e.Value = "Column";
            }
        }
    }
}
