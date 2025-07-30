using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Designer_Notes;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Solver_Equation;

namespace WaterTankTool_WFA.Load
{
    public partial class Load_Combinations : Form
    {
        private WaterTankDbContext _context;

        double otherWeight = 0;
        double totalSegmentWeight = 0;

        double m = 0;

        List<double> calculatedP = new List<double> { };
        List<double> calculatedM = new List<double> { };
        private void ShowError(string msg, string title = "Error")
    => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public Load_Combinations()
        {
            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();

            // Setup columns first
            advancedDataGridView1.Columns.Add("Load Combination", "Load Combination");
            advancedDataGridView1.Columns.Add("P", "P");
            advancedDataGridView1.Columns.Add("M", "M");

            advancedDataGridView1.Columns["Load Combination"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            advancedDataGridView1.Columns["P"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            advancedDataGridView1.Columns["M"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Set FillWeight: 50%, 25%, 25%
            advancedDataGridView1.Columns["Load Combination"].FillWeight = 50;
            advancedDataGridView1.Columns["P"].FillWeight = 25;
            advancedDataGridView1.Columns["M"].FillWeight = 25;

            // Add predefined load combinations to column A
            string[] predefinedValues = {
                    "D", "D + L", "D + S", "D + 0.75(L + S)",
                    "D + 0.6(W or E)", "D + 0.75(L+S) + 0.75(0.6W or 0.6E)",
                    "0.6D + 0.6(W or E)"
    };

            foreach (var val in predefinedValues)
            {
                int index = advancedDataGridView1.Rows.Add();
                advancedDataGridView1.Rows[index].Cells["Load Combination"].Value = val;
            }

            // Fill calculation data
            fillTableL1();
            fillTableL2();
            fillTableL3();
            fillTableL4();
            fillTableL5();
            fillTableL6();
            fillTableL7();
            fillTableL8();

            // Fill B and C columns without adding new rows
            addRowsValues();
        }


        private void addRowsValues()
        {
            for (int i = 0; i < Math.Min(calculatedP.Count, advancedDataGridView1.Rows.Count); i++)
            {
                advancedDataGridView1.Rows[i].Cells["P"].Value = calculatedP[i];
                advancedDataGridView1.Rows[i].Cells["M"].Value = calculatedM[i];
            }
        }


        public void fillTableL1()
        {
            var MiscLoad = _context.LiveLoadEntity.FirstOrDefault();
            List<SegmentProperties> segmentData = _context.SegmentProperties.ToList();
            var miscLoad = _context.LiveLoadEntity.FirstOrDefault();

            var totalLoad = GetTotalSegmentLoad(segmentData);

            if (miscLoad != null)
            {
                var result = Math.Round(otherWeight + miscLoad.Live_Load + totalSegmentWeight, 5);
                calculatedP.Add(result);
                calculatedM.Add(m);
            }

        }

        private void fillTableL2()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private void fillTableL3()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private void fillTableL4()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private void fillTableL5()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private void fillTableL6()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private void fillTableL7()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private void fillTableL8()
        {
            calculatedP.Add(m);
            calculatedM.Add(m);
        }

        private double GetTotalSegmentLoad(List<SegmentProperties> segment)
        {
            Segment_Cylinder_Equations segment_Cylinder_Equations = new Segment_Cylinder_Equations();
            Segment_Conical_Equations segment_Conical_Equations = new Segment_Conical_Equations();

            segment.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            var tankProperties = _context?.TankProperties?.FirstOrDefault();
            if (tankProperties == null)
            {
                ShowError("Please add segments first! (Tank Properties were not found)");

            }

            List<SegmentProperties> cylinderSegment = segment.FindAll(x => x.SegmentType == "Cylinder");

            List<SegmentProperties> baseSegment = segment.FindAll(x => x.SegmentType == "Base");



            totalSegmentWeight = double.Parse(tankProperties.TotalWeight);

            foreach (var item in cylinderSegment)
            {
                totalSegmentWeight += segment_Cylinder_Equations.weightOfPedestal(item.HeightInitial, item.HeightFinal, item.Diameter, item.Thickness);
            }

            foreach (var item in baseSegment)
            {
                totalSegmentWeight += segment_Conical_Equations.weight(item.HeightInitial, item.HeightFinal, (double)item.DiameterInitial, (double)item.DiameterFinal, item.Thickness);
            }



            return totalSegmentWeight;
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Load_Combinations_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double value))
            {
                otherWeight = value;
                RecalculateTable();
            }
            else
            {
                ShowError("Please enter a valid number for other weight.");
            }


        }

        private void RecalculateTable()
        {
            // Clear old data
            calculatedP.Clear();
            calculatedM.Clear();

            // Recalculate
            fillTableL1();
            fillTableL2();
            fillTableL3();
            fillTableL4();
            fillTableL5();
            fillTableL6();
            fillTableL7();
            fillTableL8();

            // Update DataGridView
            for (int i = 0; i < calculatedP.Count && i < advancedDataGridView1.Rows.Count; i++)
            {
                advancedDataGridView1.Rows[i].Cells["P"].Value = calculatedP[i];
                advancedDataGridView1.Rows[i].Cells["M"].Value = calculatedM[i];
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.LiveLoadNotes = richTextBox1.Text;

            // Immediately save changes to the single JSON file
            NotesManager.SaveNotes();
        }

        private void Load_Combinations_Shown(object sender, EventArgs e)
        {
            advancedDataGridView1.ClearSelection();
        }
    }
}
