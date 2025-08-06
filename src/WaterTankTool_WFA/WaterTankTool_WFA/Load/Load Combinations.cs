using iTextSharp.text.pdf.parser;
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
using WaterTankTool_WFA.Solver;
using WaterTankTool_WFA.Solver_Equation;

namespace WaterTankTool_WFA.Load
{
    public partial class Load_Combinations : Form
    {
        private WaterTankDbContext _context;

        double otherWeight = 0;
        double totalSegmentWeight = 0;

        double m = 0;

        double D = 0; // D


        double liveLoad = 0; //L
        double windLoad = 0;
        double snowLoad = 0;
        double seismicLoad = 0;
        double windBaseMoment = 0;  // W
        double seismicBaseMoment = 0; // E

        double roofLiveLoad = 0; // Lr

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
                    "D", "D + L", "D + (Lr or S)", "D + 0.75L + 0.75(Lr or S)",
                    "D + 0.6(W or E)", "D + 0.75L + 0.75(0.6W or 0.6E) + 0.75(Lr or S)",
                    "0.6D + 0.6(W or E)"
    };

            foreach (var val in predefinedValues)
            {
                int index = advancedDataGridView1.Rows.Add();
                advancedDataGridView1.Rows[index].Cells["Load Combination"].Value = val;
            }

            var _liveLoad = _context.LiveLoadEntity.FirstOrDefault();
            liveLoad = _liveLoad.Live_Load;
            var _snowLoad = _context.SnowLoadEntity.FirstOrDefault();
            snowLoad = _snowLoad.TotalSnowLoad;
            roofLiveLoad = _liveLoad.Roof_Live_Load;

            var _windLoad = _context.WindLoadEntity.FirstOrDefault();
            windLoad = _windLoad.Q;


            // Fill calculation data
            fillTableL1();
            fillTableL2();
            fillTableL3();
            fillTableL4();
            fillTableL5();
            fillTableL6();
            fillTableL7();


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
            var MiscLoad = _context.DeadLoadEntity.FirstOrDefault();
            List<SegmentProperties> segmentData = _context.SegmentProperties.ToList();


            var totalLoad = GetTotalSegmentLoad(segmentData);

            if (MiscLoad != null)
            {
                D = Math.Round(MiscLoad.Miscellaneous_Load + totalSegmentWeight, 5);
                calculatedP.Add(D);
                calculatedM.Add(m);
            }

        }

        private void fillTableL2()
        {

            var result = Math.Round(liveLoad + D, 5);

            calculatedP.Add(result);
            calculatedM.Add(m);
        }

        private void fillTableL3()
        {

            var result = Math.Round((D + Math.Max(snowLoad, roofLiveLoad)), 5);

            calculatedP.Add(result);
            calculatedM.Add(m);
        }

        private void fillTableL4()
        {
            var result = Math.Round((D + (0.75 * (liveLoad + snowLoad))), 5);

            calculatedP.Add(result);
            calculatedM.Add(m);
        }

        private void fillTableL5()
        {
            List<SegmentProperties> segmentData = _context.SegmentProperties.ToList();
            if (segmentData != null)
            {
                GetBaseMoment(segmentData);

            }

            var resultP = D;
            var resultM = Math.Round((0.6 * (Math.Max(windBaseMoment, seismicBaseMoment))), 5);

            calculatedP.Add(Math.Round(resultP, 5));
            calculatedM.Add(resultM);
        }

        private void fillTableL6()
        {
            var resP = Math.Round((D + (0.75 * liveLoad) + (0.75 * Math.Max(roofLiveLoad, snowLoad))), 5);
            var resM = Math.Round((0.75 * (Math.Max(0.6 * windBaseMoment, 0.6 * seismicBaseMoment))), 5);

            calculatedP.Add(resP);
            calculatedM.Add(resM);
        }

        private void fillTableL7()
        {
            var resP = Math.Round(0.6 * D, 5);
            var resM = Math.Round((0.6 * Math.Max(windBaseMoment, seismicBaseMoment)), 5);

            calculatedP.Add(resP);
            calculatedM.Add(resM);
        }


        public void GetBaseMoment(List<SegmentProperties> segment)
        {
            Segment_Cylinder_Equations segment_Cylinder_Equations = new Segment_Cylinder_Equations();
            Segment_Conical_Equations segment_Conical_Equations = new Segment_Conical_Equations();
            Multileg_Cylinders multi_leg_equations = new Multileg_Cylinders();

            segment.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));


            List<SegmentProperties> tankSegment = segment.FindAll(x => x.SegmentType == "Tanks");


            List<SegmentProperties> cylinderSegment = segment.FindAll(x => x.SegmentType == "Cylinder");

            List<SegmentProperties> baseSegment = segment.FindAll(x => x.SegmentType == "Base");

            var tankProperties = _context?.TankProperties?.FirstOrDefault();
            var seismicLoad = _context?.SeismicLoadEntity?.FirstOrDefault();
            if (tankProperties == null)
            {
                ShowError("Please add segments first! (Tank Properties were not found)");

            }
            double comXweight = 0;
            comXweight += double.Parse(tankProperties.TotalWeight) * double.Parse(tankProperties.Centroid);



            windBaseMoment += multi_leg_equations.F_Tank(tankSegment[0].HeightInitial, tankSegment[0].HeightFinal, tankSegment[0].Diameter, Double.Parse(tankProperties.ProjectedArea)) * Double.Parse(tankProperties.Centroid);




            foreach (var item in cylinderSegment)
            {
                windBaseMoment += segment_Cylinder_Equations.Mbase(item.HeightInitial, item.HeightFinal, item.Diameter);
                var weightC = segment_Cylinder_Equations.weightOfPedestal(item.HeightInitial, item.HeightFinal, item.Diameter, item.Thickness);
                var comC = segment_Cylinder_Equations.Centroid(item.HeightInitial, item.HeightFinal);
                comXweight += weightC * comC;


            }


            foreach (var item in baseSegment)
            {
                var diameter = ((double)item.DiameterFinal + (double)item.DiameterInitial) / 2;

                windBaseMoment += segment_Conical_Equations.Mbase(item.HeightInitial, item.HeightFinal, diameter);

                var weight = segment_Conical_Equations.weight(item.HeightInitial, item.HeightFinal, (double)item.DiameterInitial, (double)item.DiameterFinal, item.Thickness);
                var com = segment_Conical_Equations.Centroid(item.HeightInitial, item.HeightFinal, (double)item.DiameterInitial, (double)item.DiameterFinal);
                comXweight += weight * com;

            }
            var misc = _context?.DeadLoadEntity.FirstOrDefault();

            var res = double.Parse(tankProperties.TotalWeight) + misc.Miscellaneous_Load;

            if(seismicLoad != null)
            {

                seismicBaseMoment = (comXweight / res) * seismicLoad.V;
            }
        }

        public double GetTotalSegmentLoad(List<SegmentProperties> segment)
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


        private void button3_Click_1(object sender, EventArgs e)
        {
            Solver_Output solver = new Solver_Output("A", "D + 0.6(W or E)"); // A -> D + 0.6(W or E) , B -> D + 0.75L + 0.75(0.6W , C -> 0.6D + 0.6 (W pr E)
            solver.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Solver_Output solver = new Solver_Output("B", "D + 0.75L + 0.75(0.6W or 0.6E)"); // A -> D + 0.6(W or E) , B -> D + 0.75L + 0.75(0.6W , C -> 0.6D + 0.6 (W pr E)
            solver.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solver_Output solver = new Solver_Output("C", "0.6D + 0.6 (W or E)"); // A -> D + 0.6(W or E) , B -> D + 0.75L + 0.75(0.6W , C -> 0.6D + 0.6 (W pr E)
            solver.ShowDialog();
        }
    }
}
