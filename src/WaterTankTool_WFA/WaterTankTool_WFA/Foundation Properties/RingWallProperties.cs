using System;
using System.Drawing;
using System.Windows.Forms;
using WaterTankTool_WFA.Solver_Equation;

namespace WaterTankTool_WFA.Foundation_Properties
{
    public partial class RingWallProperties : Form
    {
        private readonly FoundationEquations.RingWallEquations _eq;

        public RingWallProperties()
        {
            InitializeComponent();

            _eq = new FoundationEquations.RingWallEquations();

            MakeOutputTextBoxesReadOnly();
        }

        public RingWallProperties(
            double trw,
            double B,
            double Rcl,
            double tedge,
            double cc,
            double vWater,
            double gammaW,
            double wTank,
            double wSup,
            double gammaC,
            double qAllowInput,
            double fc,
            double fy,
            double lambda,
            double Es,
            double As,
            double Mu,
            double Abar,
            double sProv,
            double Vu,
            double b0,
            double Vup,
            double Bu,
            double A1,
            double A2,
            bool bearingEnhancementPermitted,
            string columnLocation = "Interior",
            double phiShear = 0.75,
            double phiBearing = 0.65)
        {
            InitializeComponent();

            _eq = new FoundationEquations.RingWallEquations();

            MakeOutputTextBoxesReadOnly();

            CalculateAndDisplay(
                trw, B, Rcl, tedge, cc,
                vWater, gammaW, wTank, wSup, gammaC,
                qAllowInput, fc, fy, lambda, Es, As,
                Mu, Abar, sProv, Vu, b0, Vup,
                Bu, A1, A2, bearingEnhancementPermitted,
                columnLocation, phiShear, phiBearing);
        }

        public void CalculateAndDisplay(
            double trw,
            double B,
            double Rcl,
            double tedge,
            double cc,
            double vWater,
            double gammaW,
            double wTank,
            double wSup,
            double gammaC,
            double qAllowInput,
            double fc,
            double fy,
            double lambda,
            double Es,
            double As,
            double Mu,
            double Abar,
            double sProv,
            double Vu,
            double b0,
            double Vup,
            double Bu,
            double A1,
            double A2,
            bool bearingEnhancementPermitted,
            string columnLocation = "Interior",
            double phiShear = 0.75,
            double phiBearing = 0.65)
        {
            double stripWidth = _eq.StripWidth();

            // ==============================
            // Geometry
            // ==============================
            double Rin = _eq.InnerRadiusOfFooting(Rcl, B);
            double Rout = _eq.OuterRadiusOfFooting(Rcl, B);
            double A = _eq.FootingPlanArea(Rout, Rin);
            double Rrw = _eq.RingWallCenterlineRadius(Rcl, trw);

            textBox1.Text = F(Rin);
            textBox2.Text = F(Rout);
            textBox3.Text = F(A);
            textBox4.Text = F(Rrw);

            // ==============================
            // Service Load
            // ==============================
            double Ww = _eq.WaterWeight(vWater, gammaW);
            double Wf = _eq.FootingSelfWeight(A, tedge, gammaC);
            double Vs = _eq.TotalServiceVerticalLoad(Ww, wTank, wSup, Wf);
            double qAllow = _eq.AllowableSoilBearingPressure(qAllowInput);
            double qs = _eq.ServiceBearingPressure(Vs, A);
            double Ub = _eq.BearingUtilizationRatio(qs, qAllow);

            textBox5.Text = F(Wf);
            textBox6.Text = F(Vs);
            textBox7.Text = F(qAllow);
            textBox8.Text = F(qs);
            SetRatioBox(textBox9, Ub);

            // ==============================
            // Flexure
            // ==============================
            double d = _eq.EffectiveDepth(tedge, cc);
            double beta1 = _eq.BetaOneFactor(fc);
            double epsilonTy = _eq.YieldStrain(fy, Es);
            double a = _eq.CompressionBlockDepth(As, fy, fc, stripWidth);
            double c = _eq.NeutralAxisDepth(a, beta1);
            double epsilonT = _eq.NetTensileStrain(d, c);
            double phiFlexure = _eq.StrengthReductionFactor(epsilonT, epsilonTy);
            double Mn = _eq.NominalMomentCapacity(As, fy, d, a);
            double phiMn = _eq.DesignMomentStrength(phiFlexure, Mn);
            double Uf = _eq.FlexuralUtilizationRatio(Mu, phiMn);
            double MnReq = _eq.RequiredNominalMomentForSteelDesign(Mu);
            double AsReq = _eq.RequiredSteelArea(fy, d, fc, stripWidth, MnReq);
            double AsProv = _eq.ProvidedSteelArea(Abar, sProv);
            double sReq = _eq.RequiredBarSpacing(Abar, AsReq);

            textBox10.Text = F(d);
            textBox11.Text = F(beta1);
            textBox12.Text = F(epsilonTy);
            textBox13.Text = F(a);
            textBox14.Text = F(c);
            textBox15.Text = F(epsilonT);
            textBox16.Text = F(phiFlexure);
            textBox17.Text = F(Mn);
            textBox18.Text = F(phiMn);
            SetRatioBox(textBox19, Uf);
            textBox20.Text = F(MnReq);
            textBox21.Text = F(AsReq);
            textBox22.Text = F(AsProv);
            textBox23.Text = F(sReq);

            // ==============================
            // One-Way Shear
            // ==============================
            double h = _eq.TotalMemberThickness(tedge);
            double lambdaS = _eq.SizeEffectFactor(d);
            double rhoW = _eq.ReinforcementRatio(As, stripWidth, d);
            double Ag = _eq.GrossConcreteArea(stripWidth, h);

            // temporary equation until Excel logic for #37 is confirmed
            double Vn = _eq.NominalOneWayShearStrength(fc, stripWidth, d, lambda, lambdaS);

            double phiVn = _eq.DesignOneWayShearStrength(phiShear, Vn);
            double Uv = _eq.OneWayShearUtilizationRatio(Vu, phiVn);

            textBox24.Text = F(stripWidth);
            textBox25.Text = F(h);
            textBox26.Text = F(lambdaS);
            textBox27.Text = F(rhoW);
            textBox28.Text = F(Ag);
            textBox29.Text = F(Vn);
            textBox30.Text = F(phiVn);
            SetRatioBox(textBox31, Uv);

            // ==============================
            // Punching Shear
            // ==============================
            double beta = 1.0;
            double alphaS = _eq.ColumnLocationFactor(columnLocation);

            double vc1 = _eq.PunchingShearStressLimit1(lambda, fc);
            double vc2 = _eq.PunchingShearStressLimit2(beta, lambda, fc);
            double vc3 = _eq.PunchingShearStressLimit3(alphaS, d, b0, lambda, fc);
            double vc = _eq.GoverningPunchingShearStress(vc1, vc2, vc3);

            double Vnp = _eq.NominalPunchingShearStrength(vc, b0, d);
            double phiVnp = _eq.DesignPunchingShearStrength(phiShear, Vnp);
            double Up = _eq.PunchingShearUtilizationRatio(Vup, phiVnp);

            textBox32.Text = F(alphaS);
            textBox33.Text = $"vc1={F(vc1)}, vc2={F(vc2)}, vc3={F(vc3)}";
            textBox34.Text = F(vc);
            textBox35.Text = F(Vnp);
            textBox36.Text = F(phiVnp);
            SetRatioBox(textBox37, Up);

            // ==============================
            // Concrete Bearing
            // ==============================
            double Rn = _eq.NominalConcreteBearingStrength(fc, A1, A2, bearingEnhancementPermitted);
            double phiRn = _eq.DesignConcreteBearingStrength(phiBearing, Rn);
            double Ubr = _eq.ConcreteBearingUtilizationRatio(Bu, phiRn);

            textBox38.Text = F(Rn);
            textBox39.Text = F(phiRn);
            SetRatioBox(textBox40, Ubr);
        }

        private string F(double value)
        {
            return value.ToString("0.#####");
        }

        private void SetRatioBox(TextBox textBox, double ratio)
        {
            textBox.Text = F(ratio);

            if (ratio <= 1.0)
                textBox.BackColor = Color.LightGreen;
            else
                textBox.BackColor = Color.LightCoral;
        }

        private void MakeOutputTextBoxesReadOnly()
        {
            foreach (Control control in GetAllControls(this))
            {
                if (control is TextBox tb)
                {
                    tb.ReadOnly = true;
                    tb.TabStop = false;
                    tb.BackColor = Color.White;
                }
            }
        }

        private Control[] GetAllControls(Control parent)
        {
            var controls = new System.Collections.Generic.List<Control>();

            foreach (Control control in parent.Controls)
            {
                controls.Add(control);

                if (control.HasChildren)
                    controls.AddRange(GetAllControls(control));
            }

            return controls.ToArray();
        }
    }
}