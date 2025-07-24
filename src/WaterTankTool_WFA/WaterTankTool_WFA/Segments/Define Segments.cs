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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete");
                return;
            }

            DataGridViewRow selRow = dataGridView1.SelectedRows[0];
            int segNumber = (int)selRow.Cells[0].Value;
            string segName = selRow.Cells[1].Value.ToString();

            // ---------------------------------------------
            // MULTILEG: get the base part before last '_n'
            // ---------------------------------------------
            List<SegmentProperties> segmentsToDelete;
            string confirmMessage;

            if (_tankType == TankType.MultiColumn)
            {
                int idx = segName.LastIndexOf('_');
                string baseName = (idx > 0 && int.TryParse(segName[(idx + 1)..], out _))
                                ? segName[..idx]
                                : segName;

                // SQL-translateable predicate (StartsWith / ==)
                segmentsToDelete = _context.SegmentProperties
                                           .Where(s => s.SegmentName == baseName
                                                   || s.SegmentName.StartsWith(baseName + "_"))
                                           .ToList();

                confirmMessage = segmentsToDelete.Count == 1
                    ? $"Do you want to delete {segName}?"
                    : $"Do you want to delete ALL {segmentsToDelete.Count} columns of '{baseName}'?";
            }
            else
            {
                var seg = _context.SegmentProperties
                                  .FirstOrDefault(s => s.SegmentNumber == segNumber);

                if (seg == null)
                {
                    MessageBox.Show("Selected segment not found");
                    return;
                }

                segmentsToDelete = new() { seg };
                confirmMessage = $"Do you want to delete {segName}?";
            }

            // ---------------------------------------------
            // Confirm & delete
            // ---------------------------------------------
            if (MessageBox.Show(confirmMessage, "Confirm Delete",
                                buttons, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // If you still want to wipe TankProperties like before:
            _context.TankProperties.RemoveRange(_context.TankProperties);

            _context.SegmentProperties.RemoveRange(segmentsToDelete);
            _context.SaveChanges();

            LoadData();
            if (segmentsToDelete[0].SegmentType == "Tanks")
            {
                _waterTankForm.OnSegmentDeleted();  // notify parent form
            }// refresh grid
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
