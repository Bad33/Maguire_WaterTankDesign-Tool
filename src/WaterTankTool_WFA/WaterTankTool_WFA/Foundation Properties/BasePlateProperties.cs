using System;
using System.Linq;
using System.Windows.Forms;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Solver_Equation;

namespace WaterTankTool_WFA.Foundation_Properties
{
    public partial class BasePlateProperties : Form
    {
        private readonly WaterTankDbContext _context;

        public BasePlateProperties()
        {
            InitializeComponent();
            _context = WaterTankDbContext.GetInstance();
            this.Load += BasePlateProperties_Load;
        }

        private void BasePlateProperties_Load(object? sender, EventArgs e)
        {
            LoadBasePlateCalculatedValues();
        }

        private void LoadBasePlateCalculatedValues()
        {
            try
            {
                var basePlate = _context.BasePlateEntity.FirstOrDefault();

                if (basePlate == null)
                {
                    MessageBox.Show(
                        "No base plate data found. Please enter base plate parameters first.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                CalculateBasePlateValues(basePlate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load base plate values.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CalculateBasePlateValues(BasePlateEntity basePlate)
        {
            var eq = new FoundationEquations.BasePlateEquations();

            double grossArea = eq.GrossArea(basePlate.Ro, basePlate.Ri, basePlate.Theta);
            double netArea = eq.NetArea(basePlate.Ro, basePlate.Ri, basePlate.Theta, basePlate.Nh, basePlate.Dh);
            double volume = eq.Volume(basePlate.Ro, basePlate.Ri, basePlate.Theta, basePlate.T);
            double weightPerSegment = eq.WeightPerSegment(basePlate.Ro, basePlate.Ri, basePlate.Theta, basePlate.T, basePlate.Rs);
            double totalWeight = eq.TotalWeight(weightPerSegment, basePlate.N);

            double outerArcLength = eq.OuterArcLength(basePlate.Ro, basePlate.Theta);
            double innerArcLength = eq.InnerArcLength(basePlate.Ri, basePlate.Theta);
            double radialWidth = eq.RadialWidth(basePlate.Ro, basePlate.Ri);
            double centroidRadius = eq.CentroidRadius(basePlate.Ro, basePlate.Ri, basePlate.Theta);
            double centroidAngle = eq.CentroidAngle(basePlate.A ?? 0, basePlate.Theta);

            textBox1.Text = grossArea.ToString("F4");
            textBox2.Text = netArea.ToString("F4");
            textBox3.Text = volume.ToString("F4");
            textBox4.Text = weightPerSegment.ToString("F4");
            textBox5.Text = totalWeight.ToString("F4");
            textBox6.Text = outerArcLength.ToString("F4");
            textBox7.Text = innerArcLength.ToString("F4");
            textBox8.Text = radialWidth.ToString("F4");
            textBox9.Text = centroidRadius.ToString("F4");
            textBox10.Text = centroidAngle.ToString("F4");
        }
    }
}