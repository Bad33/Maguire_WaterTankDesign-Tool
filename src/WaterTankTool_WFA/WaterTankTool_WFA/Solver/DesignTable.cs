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

namespace WaterTankTool_WFA.Solver
{
    public partial class DesignTable : Form
    {

        private readonly WaterTankDbContext _context;


        public string waterWeight;
        public string snowWeight;
        public string selfWeight;
        public List<tabelData2> tabelData2s = new List<tabelData2>();
        public event EventHandler<List<tabelData2>> Table2Loaded;

        public DesignTable()
        {

            InitializeComponent();

            _context = WaterTankDbContext.GetInstance();


            if (_context.SnowLoadEntity.FirstOrDefault() == null)
            {
                ShowError("Please add Snow Load first!");   // shows once
                return;                                     // skip the rest
            }

            try
            {
                LoadTable2();
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error while initialising Solver Output: {ex.Message}");
            }
        }

        private void ShowError(string msg, string title = "Error")
             => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void LoadTable2()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            AllowableStress allow = new AllowableStress();

            if (segmentData.Count == 0) return;

            try
            {
                tabelData2s = segmentData.Select(segment =>
                {
                    double dFinal = segment.DiameterFinal ?? segment.Diameter;
                    double radius = 12 * dFinal / 2.0;
                    double rt = Math.Round((12 * dFinal / 2.0) / segment.Thickness, 4);
                    double i = Math.Round((Math.PI / 64) *
                                  (Math.Pow(12 * dFinal, 4) -
                                   Math.Pow((12 * dFinal - 2 * segment.Thickness), 4)), 4);
                    double a = Math.Round((Math.PI / 4) *
                                  (Math.Pow(12 * dFinal, 2) -
                                   Math.Pow((12 * dFinal - 2 * segment.Thickness), 2)), 4);

                    double co = Math.Round(1022 / (195 + rt), 4);
                    double r = Math.Round(Math.Sqrt(i / a), 4);

                    double Fl;
                    double rtcValue = double.TryParse(allow.rtcLabel?.Text, out double rtc) ? rtc : 0;
                    double fyVal = double.TryParse(allow.Fy, out double fy) ? fy : 0;

                    if (rt < rtcValue)
                    {
                        double v1 = Math.Round((233 * fyVal) / (2 * (166 + rt)), 4);
                        double v2 = Math.Round(fyVal / 2, 4);
                        Fl = Math.Min(v1, v2);
                    }
                    else
                    {
                        Fl = Math.Round((co * 29000000) / (2 * rt), 4);
                    }

                    double klr = Math.Round((2.1 * 2124) / r, 4);
                    double Cc = Math.Round(Math.Sqrt((Math.Pow(Math.PI, 2) * 29000000) / Fl), 4);

                    double Kf = klr <= 25 ? 1
                              : klr <= Cc ? Math.Round(1 - 0.5 * Math.Pow(klr / Cc, 2), 4)
                              : Math.Round(0.5 * Math.Pow(Cc / klr, 2), 4);

                    double Fa = Math.Round((Fl / 1000) * Kf, 4);
                    double Fb = Math.Round(Fl / 1000, 4);

                    return new tabelData2
                    {
                        Segment = segment.SegmentName,
                        Radius = Math.Round(radius, 4),
                        Thickness = Math.Round(segment.Thickness, 4),
                        Rt = rt,
                        A = a,
                        I = i,
                        r = r,
                        Co = co,
                        Fl = Math.Round(Fl / 1000, 4),
                        KLr = klr,
                        Cc = Cc,
                        Kf = Kf,
                        Fa = Fa,
                        Fb = Fb
                    };
                }).ToList();

                
                dataGridView1.DataSource = tabelData2s;
            }
            catch (Exception ex)
            {
                ShowError($"Error while computing Table-2 values: {ex.Message}");
            }
        }

        public IReadOnlyList<tabelData2> TableData2Results
        {
            get { return tabelData2s; }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            AllowableStress allowable = new AllowableStress();
            var res = allowable.ShowDialog();

            if(res == DialogResult.OK || res == DialogResult.Cancel)
            {
                LoadTable2();
            }
        }


    }
    public class tabelData2
    {
        public string Segment { get; set; }
        public double Radius { get; set; }
        public double Thickness { get; set; }
        public double Rt { get; set; }
        public double A { get; set; }
        public double I { get; set; }
        public double r { get; set; }
        public double Co { get; set; }
        public double Fl { get; set; }
        public double KLr { get; set; }
        public double Cc { get; set; }
        public double Kf { get; set; }
        public double Fa { get; set; }
        public double Fb { get; set; }
    }





}


