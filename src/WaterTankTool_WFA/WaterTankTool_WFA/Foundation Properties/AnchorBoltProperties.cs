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
using WaterTankTool_WFA.Foundation_Design;
using WaterTankTool_WFA.Solver_Equation;

namespace WaterTankTool_WFA.Foundation_Properties
{
    public partial class AnchorBoltProperties : Form
    {
        private readonly WaterTankDbContext _context;

        public AnchorBoltProperties()
        {
            InitializeComponent();

            _context = WaterTankDbContext.GetInstance();

            this.Load += AnchorBoltProperties_Load;

            // optional: better for long angle/coordinate text
            textBox4.Multiline = true;
            textBox4.Height = 45;
            textBox4.ScrollBars = ScrollBars.Vertical;

            textBox5.Multiline = true;
            textBox5.Height = 45;
            textBox5.ScrollBars = ScrollBars.Vertical;


        }

        private void AnchorBoltProperties_Load(object? sender, EventArgs e)
        {
            LoadAnchorBoltCalculatedValues();
        }

        private void LoadAnchorBoltCalculatedValues()
        {
            try
            {
                var anchorBolt = _context.AnchorBoltEntity.FirstOrDefault();

                if (anchorBolt == null)
                {
                    MessageBox.Show(
                        "No anchor bolt data found. Please enter anchor bolt parameters first.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                CalculateAnchorBoltValues(anchorBolt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load anchor bolt values.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CalculateAnchorBoltValues(AnchorBoltEntity anchorBolt)
        {
            var eq = new FoundationEquations.AnchorBoltEquations();

            double area = eq.CrossSectionalArea(anchorBolt.Db);
            double holeArea = eq.HoleArea(anchorBolt.Dh);
            double angularSpacing = eq.BoltAngularSpacing(anchorBolt.Nb);
            double arcSpacing = eq.ArcSpacing(anchorBolt.Rb, anchorBolt.Nb);
            double chordSpacing = eq.ChordSpacing(anchorBolt.Rb, anchorBolt.Nb);

            double? boltsPerSegment = null;
            bool boltsPerSegmentIsInteger = true;
            if (anchorBolt.Ns.HasValue && anchorBolt.Ns.Value > 0)
            {
                boltsPerSegment = eq.BoltsPerSegment(anchorBolt.Nb, anchorBolt.Ns.Value);
                boltsPerSegmentIsInteger = eq.BoltsPerSegmentIsInteger(anchorBolt.Nb, anchorBolt.Ns.Value);
            }

            double? clearEdgeDistance = null;
            if (anchorBolt.E.HasValue)
            {
                clearEdgeDistance = eq.ClearEdgeDistance(anchorBolt.E.Value, anchorBolt.Dh);
            }

            double tensionPerBolt;
            if (anchorBolt.Nbs.HasValue && anchorBolt.Nbs.Value > 0)
            {
                tensionPerBolt = eq.TensionDemandPerBolt_Effective(anchorBolt.Tu, anchorBolt.Nbs.Value);
            }
            else
            {
                tensionPerBolt = eq.TensionDemandPerBolt_Equal(anchorBolt.Tu, anchorBolt.Nb);
            }

            double shearPerBolt = eq.ShearDemandPerBolt(anchorBolt.Vu, anchorBolt.Nb);

            double tensileCapacity = 0;
            if (anchorBolt.Fu.HasValue)
            {
                tensileCapacity = eq.TensileDesignStrengthUltimate(
                    anchorBolt.Db,
                    anchorBolt.Fu.Value,
                    anchorBolt.Phi ?? 1.0);
            }
            else if (anchorBolt.Fy.HasValue)
            {
                tensileCapacity = eq.TensileDesignStrength(
                    anchorBolt.Db,
                    anchorBolt.Fy.Value,
                    anchorBolt.Phi ?? 1.0);
            }

            double shearCapacity = 0;
            if (anchorBolt.Fy.HasValue)
            {
                shearCapacity = eq.ShearDesignStrength(
                    anchorBolt.Db,
                    anchorBolt.Fy.Value,
                    anchorBolt.Phi ?? 1.0);
            }

            double interaction = 0;
            bool interactionPass = false;

            if (tensileCapacity > 0 && shearCapacity > 0)
            {
                interaction = eq.InteractionCheck(
                    tensionPerBolt,
                    tensileCapacity,
                    shearPerBolt,
                    shearCapacity);

                interactionPass = eq.InteractionPass(interaction);
            }

            textBox1.Text = area.ToString("F4");
            textBox2.Text = holeArea.ToString("F4");
            textBox3.Text = angularSpacing.ToString("F4");
            textBox4.Text = BuildBoltAnglesText(eq, anchorBolt);
            textBox5.Text = BuildBoltCoordinatesText(eq, anchorBolt);
            textBox6.Text = arcSpacing.ToString("F4");
            textBox7.Text = chordSpacing.ToString("F4");
            textBox8.Text = boltsPerSegment?.ToString("F4") ?? "";
            textBox9.Text = clearEdgeDistance?.ToString("F4") ?? "";
            textBox10.Text = tensionPerBolt.ToString("F4");
            textBox11.Text = shearPerBolt.ToString("F4");
            textBox12.Text = tensileCapacity > 0 ? tensileCapacity.ToString("F4") : "";
            textBox13.Text = shearCapacity > 0 ? shearCapacity.ToString("F4") : "";

            if (tensileCapacity > 0 && shearCapacity > 0)
            {
                SetInteractionStatus(interaction, interactionPass, false);
            }
            else
            {
                SetInteractionStatus(null, null, true);
            }

            if (boltsPerSegment.HasValue && !boltsPerSegmentIsInteger)
            {
                textBox8.BackColor = Color.Khaki;
            }
            else
            {
                textBox8.BackColor = Color.White;
            }

            textBox9.BackColor = clearEdgeDistance.HasValue ? Color.White : Color.LightYellow;
        }

        private void SetInteractionStatus(double? interactionValue, bool? interactionPass, bool isWarning = false)
        {
            textBox14.BackColor = Color.White;
            textBox14.ForeColor = Color.Black;
            labelStatus.Text = "";
            labelStatus.ForeColor = Color.Black;

            if (!interactionValue.HasValue)
            {
                textBox14.Text = "";
                textBox14.BackColor = Color.LightYellow;
                labelStatus.Text = "WARNING";
                labelStatus.ForeColor = Color.Goldenrod;
                return;
            }

            textBox14.Text = interactionValue.Value.ToString("F4");

            if (isWarning)
            {
                textBox14.BackColor = Color.Khaki;
                labelStatus.Text = "WARNING";
                labelStatus.ForeColor = Color.Goldenrod;
            }
            else if (interactionPass == true)
            {
                textBox14.BackColor = Color.LightGreen;
                labelStatus.Text = "PASS";
                labelStatus.ForeColor = Color.Green;
            }
            else
            {
                textBox14.BackColor = Color.LightCoral;
                labelStatus.Text = "FAIL";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private string BuildBoltAnglesText(FoundationEquations.AnchorBoltEquations eq, AnchorBoltEntity anchorBolt)
        {
            var sb = new StringBuilder();

            for (int i = 1; i <= anchorBolt.Nb; i++)
            {
                double angle = eq.BoltAngle(anchorBolt.Ab, i, anchorBolt.Nb);

                if (i > 1)
                    sb.Append(" ; ");

                sb.Append($"B{i}={angle:F2}");
            }

            return sb.ToString();
        }

        private string BuildBoltCoordinatesText(FoundationEquations.AnchorBoltEquations eq, AnchorBoltEntity anchorBolt)
        {
            var sb = new StringBuilder();

            for (int i = 1; i <= anchorBolt.Nb; i++)
            {
                double angle = eq.BoltAngle(anchorBolt.Ab, i, anchorBolt.Nb);
                double x = eq.BoltXCoordinate(anchorBolt.Rb, angle);
                double y = eq.BoltYCoordinate(anchorBolt.Rb, angle);

                if (i > 1)
                    sb.Append(" ; ");

                sb.Append($"B{i}=({x:F2},{y:F2})");
            }

            return sb.ToString();
        }

        private void textBox14_DoubleClick(object sender, EventArgs e)
        {
            AnchorBoltParameters anchorBoltParameters = new AnchorBoltParameters();
            anchorBoltParameters.ShowDialog();
        }
    }
}
