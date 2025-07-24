#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Migrations;
using WaterTankTool_WFA.Solver_Equation;
using WaterTankTool_WFA.Tanks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Rectangle = System.Drawing.Rectangle;
#endregion

namespace WaterTankTool_WFA.Solver
{
    public partial class Solver_Output : Form
    {
        #region Fields / Properties
        private readonly WaterTankDbContext _context;
        private readonly WaterTank _waterTankForm;

        private readonly UnitsConverter inchToFtConverter = new UnitsConverter();   //  kept; not used here but left intact

        private Label rtcLabel;                 // set in LoadAllowableCompressiveStress()

        // Exposed to other members
        public string waterWeight;
        public string snowWeight;
        public string selfWeight;
        public string Fy;

        // Data caches
        public List<string> selfWeightData = new List<string>();
        public List<segmentGravityLoad> cummulativeLoadData = new List<segmentGravityLoad>();
        public List<WindTable> windLoadData = new List<WindTable>();
        public List<designTableData> segmentPropertiesTableData = new List<designTableData>();
        public List<tabelData2> tabelData2s = new List<tabelData2>();
        #endregion

        #region Constructor
        public Solver_Output(WaterTank waterTankForm)
        {
            InitializeComponent();

            // Context (singleton)
            _context = WaterTankDbContext.GetInstance();
            _waterTankForm = waterTankForm;

            if (_context.SnowLoadEntity.FirstOrDefault() == null)
            {
                ShowError("Please add Snow Load first!");   // shows once
                return;                                     // skip the rest
            }

            try
            {
                LoadData();
                LoadAllowableCompressiveStress();
                LoadSegmentWeightData();
                LoadCummulativeWeightData();
                WindLoadPerSegment();
                LoadTable2();
                LoadCheckTableData();
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error while initialising Solver Output: {ex.Message}");
            }
        }
        #endregion

        #region *** Utilities ***

        /// <summary> Small helper to show consistent error pop-ups. </summary>
        private void ShowError(string msg, string title = "Error")
            => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        /// <summary> Reads a JSON file safely. </summary>
        private bool TryReadJson(string path, out string json)
        {
            json = null!;
            try
            {
                json = File.ReadAllText(path);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Failed to read '{path}': {ex.Message}");
                return false;
            }
        }

        /// <summary> Converts a string that may contain units / text to a double. </summary>
        public static double ExtractDoubleValue(string input)
        {
            string[] parts = input?.Split() ?? Array.Empty<string>();
            if (parts.Length == 0 || !double.TryParse(parts[0], out double result))
                throw new FormatException($"Unable to parse '{input}' as a double.");
            return result;
        }

        /// <summary> Helper to add a red status label once. </summary>
        private void AddStatusLabel(string message)
        {
            if (statusStrip2.Items.OfType<ToolStripStatusLabel>().Any(l => l.Text == message)) return;

            statusStrip2.Items.Add(new ToolStripStatusLabel
            {
                ForeColor = Color.Red,
                Text = message
            });
        }
        #endregion

        #region Wind Load
        private void WindLoadPerSegment()
        {
            // Defensive fetch
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            var tankProperties = _context?.TankProperties?.FirstOrDefault();
            if (tankProperties == null)
            {
                ShowError("Please add segments first! (Tank Properties were not found)");
                return;
            }

            double projectedArea;
            try
            {
                projectedArea = ExtractDoubleValue(tankProperties.ProjectedArea);
            }
            catch (Exception ex)
            {
                ShowError($"Invalid projected area in Tank Properties: {ex.Message}");
                return;
            }

            var cylinderEq = new Segment_Cylinder_Equations();

            double cumulativeFwind = 0;
            foreach (var segment in segmentData)
            {
                double fwind, loadLocation;

                if (segment.SegmentType == "Tanks")
                {
                    fwind = calculateF(
                                        cylinderEq.qzi(segment.HeightInitial),
                                        cylinderEq.qzf(segment.HeightFinal),
                                        projectedArea);
                    loadLocation = ExtractDoubleValue(tankProperties.Centroid) + segment.HeightInitial;
                }
                else
                {
                    fwind = cylinderEq.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter);
                    loadLocation = cylinderEq.L(segment.HeightInitial, segment.HeightFinal);
                }

                double baseElevation = segment.HeightInitial;
                double armLength = loadLocation - baseElevation;
                double farm = fwind * armLength;
                cumulativeFwind += fwind;

                double prevContrib = windLoadData
                                     .Sum(row => Double.Parse(row.Fwind) *
                                                 (Double.Parse(row.LoadLocation) - baseElevation));

                double mwind = farm + prevContrib;

                windLoadData.Add(new WindTable
                {
                    Fwind = Math.Round(fwind, 4).ToString(),
                    Vwind = Math.Round(cumulativeFwind, 4).ToString(),
                    BaseElevation = Math.Round(baseElevation, 4).ToString(),
                    LoadLocation = Math.Round(loadLocation, 4).ToString(),
                    ArmLength = Math.Round(armLength, 4).ToString(),
                    FArm = Math.Round(farm, 4).ToString(),
                    Mwind = Math.Round(mwind, 4).ToString()
                });
            }

            // ensure segment weights are loaded once (no duplicates)
            if (selfWeightData.Count == 0)
                LoadSegmentWeightData();

            dataGridView7.DataSource = windLoadData;
        }

        private double calculateF(double qzi, double qzf, double projectedArea)
            => (((qzi + qzf) / 2) * projectedArea) / 1000;
        #endregion

        #region Segment Gravity Loads
        private void LoadSegmentWeightData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            var snowEntity = _context?.SnowLoadEntity?.FirstOrDefault();
            if (snowEntity == null)
            {
                ShowError("Please add Snow Load first!");
                return;
            }

            if (segmentData.Count == 0)
            {
                AddStatusLabel("No Segments Added. Please add the segments to see the output.");
                return;
            }

            // Choose the correct file depending on tank type
            string fileName = AppState.CurrentTankType == TankType.MultiColumn
                              ? "MultiLeg-Tanks.json"
                              : "tanks.json";

            string jsonPath = Path.Combine(Application.StartupPath, fileName);

            if (!File.Exists(jsonPath))
            {
                ShowError($"{fileName} file not found.");
                return;
            }

            if (!TryReadJson(jsonPath, out string json)) return;

            TanksData data;
            try
            {
                data = JsonSerializer.Deserialize<TanksData>(json);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to parse {fileName}: {ex.Message}");
                return;
            }

            // Find matching tank record
            string tankCapacity = segmentData[0].SegmentName;
            Tank foundTank = data?.tanks?.Find(t => t.type == tankCapacity);

            if (foundTank == null)
            {
                ShowError($"Tank type '{tankCapacity}' not found in {fileName}!");
                return;
            }

            // … proceed with foundTank …


            string numericPart = new(foundTank.Weight_of_Water
                                      .Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());

            if (!double.TryParse(numericPart, NumberStyles.Any, CultureInfo.InvariantCulture,
                                 out double waterWeightDbl))
            {
                ShowError("Failed to parse Weight_of_Water.");
                return;
            }
            waterWeight = numericPart;

            string snowWeightStr = snowEntity.TotalSnowLoad.ToString();
            snowWeight = snowWeightStr;
            selfWeight = foundTank.Weight_of_Steel;

            double miscLoad = 15;

            var cylinderEq = new Segment_Cylinder_Equations();
            var conicalEq = new Segment_Conical_Equations();

            // Tanks
            var viewModelData = segmentData
                .Where(x => x.SegmentType == "Tanks")
                .Select(_ => new segmentGravityLoad
                {
                    waterWeight = waterWeight,
                    snowWeight = (double.Parse(snowWeightStr) + miscLoad).ToString(),
                    selfWeight = Math.Round(waterWeightDbl, 4).ToString()
                }).ToList();

            // Cylinder
            var viewModelData1 = segmentData
                .Where(x => x.SegmentType == "Cylinder")
                .Select(segment => new segmentGravityLoad
                {
                    waterWeight = "0",
                    snowWeight = "0",
                    selfWeight = Math.Round(cylinderEq.weightOfPedestal(
                                    segment.HeightInitial,
                                    segment.HeightFinal,
                                    segment.Diameter,
                                    segment.Thickness), 4).ToString()
                }).ToList();

            // Base
            var viewModelData2 = segmentData
                .Where(x => x.SegmentType == "Base")
                .Select(segment => new segmentGravityLoad
                {
                    waterWeight = "0",
                    snowWeight = "0",
                    selfWeight = Math.Round(conicalEq.weight(
                                    segment.HeightInitial,
                                    segment.HeightFinal,
                                    (double)segment.DiameterInitial,
                                    (double)segment.DiameterFinal,
                                    segment.Thickness), 4).ToString()
                }).ToList();

            var combined = viewModelData.Concat(viewModelData1).Concat(viewModelData2).ToList();

            selfWeightData.AddRange(combined.Select(x => x.selfWeight));

            dataGridView4.DataSource = combined;

            AddStatusLabel("Double-click a cell in the Segment Check Table to change thickness. (It’s recommended to change T by ±25 % each time) | For more info open Help.");
        }
        #endregion

        #region Cumulative Gravity Loads
        private void LoadCummulativeWeightData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            if (segmentData.Count == 0)
            {
                ShowError("No segment data found. Please add segments before proceeding.");
                return;
            }
            // Choose the correct file depending on tank type
            string fileName = AppState.CurrentTankType == TankType.MultiColumn
                              ? "MultiLeg-Tanks.json"
                              : "tanks.json";

            string jsonPath = Path.Combine(Application.StartupPath, fileName);

            if (!File.Exists(jsonPath))
            {
                ShowError($"{fileName} file not found.");
                return;
            }

            if (!TryReadJson(jsonPath, out string json)) return;

            TanksData data;
            try
            {
                data = JsonSerializer.Deserialize<TanksData>(json);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to parse {fileName}: {ex.Message}");
                return;
            }

            // Find matching tank record
            string tankCapacity = segmentData[0].SegmentName;
            Tank foundTank = data?.tanks?.Find(t => t.type == tankCapacity);

            if (foundTank == null)
            {
                ShowError($"Tank type '{tankCapacity}' not found in {fileName}!");
                return;
            }

            // … proceed with foundTank …


            string numericPart = new(foundTank.Weight_of_Water
                                      .Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());
            string waterWeightStr = numericPart;

            var snowEntity = _context?.SnowLoadEntity?.FirstOrDefault();
            if (snowEntity == null)
            {
                ShowError("Please add Snow Load first!");
                return;
            }
            string snowWeightStr = snowEntity.TotalSnowLoad.ToString();

            double miscLoad = 15;

            // Build cumulative self-weight list (res[i] holds Σ selfWeight[0..i])
            List<string> cumulative = new();
            for (int i = 0; i < selfWeightData.Count; i++)
            {
                if (!double.TryParse(selfWeightData[i], out double thisWeight))
                {
                    ShowError($"Invalid selfWeight '{selfWeightData[i]}' at index {i}.");
                    return;
                }
                double previous = i == 0 ? 0
                                         : double.Parse(cumulative[i - 1]);
                cumulative.Add((thisWeight + previous).ToString());
            }

            cummulativeLoadData = segmentData.Select((segment, index) =>
            {
                string cumSelf = index < cumulative.Count
                               ? Math.Round(double.Parse(cumulative[index]), 4).ToString()
                               : "0";

                double snowDbl = double.TryParse(snowWeightStr, out double sn) ? sn : 0;
                string snowSum = (snowDbl + miscLoad).ToString();

                return new segmentGravityLoad
                {
                    waterWeight = waterWeightStr,
                    snowWeight = snowSum,
                    selfWeight = cumSelf
                };
            }).ToList();

            dataGridView6.DataSource = cummulativeLoadData;
        }
        #endregion

        #region Check Table (fa / fb / check)
        private void LoadCheckTableData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            if (segmentData.Count == 0) return;

            var viewModel = segmentData.Select((segment, i) =>
            {
                double fa = 0, fb = 0;
                string chk = "NA";

                if (segment.SegmentType != "Tanks")
                {
                    fa = Math.Round(
                            (Double.Parse(cummulativeLoadData[i].waterWeight) +
                             Double.Parse(cummulativeLoadData[i].snowWeight) +
                             Double.Parse(cummulativeLoadData[i].selfWeight)) /
                            segmentPropertiesTableData[i].A, 4);

                    fb = Math.Round(
                            (Double.Parse(windLoadData[i].Mwind) * 12) /
                             segmentPropertiesTableData[i].S, 4);

                    if ((fa + fb) != 0)
                        chk = Math.Round(
                                (fa / tabelData2s[i].Fa) +
                                (fb / tabelData2s[i].Fb), 4).ToString();
                }

                return new CheckTableData
                {
                    SegmentID = segment.SegmentNumber,
                    Segment = segment.SegmentName,
                    fa = fa,
                    fb = fb,
                    check = chk
                };
            }).ToList();

            dataGridView1.DataSource = viewModel;
            dataGridView1.Columns["SegmentID"].Visible = false;
        }
        #endregion

        #region Table-2 (Design parameters)
        private void LoadTable2()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

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
                    double rtcValue = double.TryParse(rtcLabel?.Text, out double rtc) ? rtc : 0;
                    double fyVal = double.TryParse(Fy, out double fy) ? fy : 0;

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

                dataGridView3.DataSource = tabelData2s;
            }
            catch (Exception ex)
            {
                ShowError($"Error while computing Table-2 values: {ex.Message}");
            }
        }
        #endregion

        #region Allowable Compressive Stress (TableLayout initial setup)
        private void LoadAllowableCompressiveStress()
        {
            tableLayoutPanel1.Controls.Clear();

            // static labels
            string[] leftLabels = { "Fv", "k", "l", "(R/t)c", "E" };
            string[] rightLabels = { "psi", null, "in", null, "psi" };

            for (int i = 0; i < leftLabels.Length; i++)
                tableLayoutPanel1.Controls.Add(new Label { Text = leftLabels[i] }, 0, i);

            // Dropdown for Fy
            ComboBox fyBox = new ComboBox { Dock = DockStyle.Fill };
            fyBox.Items.AddRange(new[] { "30000", "32000", "34000", "36000", "38000", "40000" });
            fyBox.SelectedItem = "36000";
            Fy = "36000";
            tableLayoutPanel1.Controls.Add(fyBox, 1, 0);

            // Fixed cells
            tableLayoutPanel1.Controls.Add(new Label { Text = "2.1" }, 1, 1);
            tableLayoutPanel1.Controls.Add(new Label { Text = "2124" }, 1, 2);
            rtcLabel = new Label();
            tableLayoutPanel1.Controls.Add(rtcLabel, 1, 3);
            tableLayoutPanel1.Controls.Add(new Label { Text = "29000000" }, 1, 4);

            for (int i = 0; i < rightLabels.Length; i++)
                if (rightLabels[i] != null)
                    tableLayoutPanel1.Controls.Add(new Label { Text = rightLabels[i] }, 2, i);

            UpdateRtcLabel(Fy);

            fyBox.SelectedIndexChanged += (_, __) =>
            {
                Fy = fyBox.SelectedItem!.ToString();
                UpdateRtcLabel(Fy);
                LoadTable2();               // refresh design table
            };
        }

        private void UpdateRtcLabel(string selectedFy)
        {
            rtcLabel.Text = selectedFy switch
            {
                "30000" => "420",
                "32000" => "377",
                "34000" => "354",
                "36000" => "334",
                "38000" => "316",
                "40000" => "299",
                _ => "0"
            };
        }
        #endregion

        #region Segment Properties Table (dataGridView5)
        private void LoadData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            if (segmentData.Count == 0) return;

            segmentPropertiesTableData = segmentData.Select(segment =>
            {
                double dFinal = segment.DiameterFinal ?? segment.Diameter;
                double A = Math.Round((Math.PI / 4) *
                         (Math.Pow(12 * dFinal, 2) -
                          Math.Pow((12 * dFinal - 2 * segment.Thickness), 2)), 4);

                double I = Math.Round((Math.PI / 64) *
                         (Math.Pow(12 * dFinal, 4) -
                          Math.Pow((12 * dFinal - 2 * segment.Thickness), 4)), 4);

                double S = Math.Round((I * 2) / (12 * dFinal), 4);

                return new designTableData
                {
                    Segment = segment.SegmentName,
                    Diameter = 12 * dFinal,
                    Thickness = segment.Thickness,
                    A = A,
                    I = I,
                    S = S
                };
            }).ToList();

            dataGridView5.DataSource = segmentPropertiesTableData;
        }
        #endregion

        #region --- Printing ---

        private float PrintDataGridView(
            Graphics g, DataGridView dgv, float xPos, float yPos,
            System.Drawing.Font printFont, string title)
        {
            float startX = xPos;
            float cellHeight = printFont.GetHeight() + 10;
            float colWidth = 100;

            g.DrawString(title, new System.Drawing.Font("Arial", 12, FontStyle.Bold), Brushes.Black, xPos, yPos);
            yPos += cellHeight + 5;

            Pen pen = Pens.Black;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                g.DrawRectangle(pen, xPos, yPos, colWidth, cellHeight);
                g.DrawString(col.HeaderText, printFont, Brushes.Black,
                             new RectangleF(xPos, yPos, colWidth, cellHeight));
                xPos += colWidth;
            }
            yPos += cellHeight;
            xPos = startX;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    g.DrawRectangle(pen, xPos, yPos, colWidth, cellHeight);
                    g.DrawString(Convert.ToString(cell.Value), printFont, Brushes.Black,
                                 new RectangleF(xPos, yPos, colWidth, cellHeight));
                    xPos += colWidth;
                }
                yPos += cellHeight;
                xPos = startX;
            }

            return yPos + 20;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = e.MarginBounds.Top;
            float x = e.MarginBounds.Left;
            var f = new System.Drawing.Font("Arial", 10);

            y = PrintDataGridView(e.Graphics, dataGridView1, x, y, f, "Check Table Data") + 40;
            PrintDataGridView(e.Graphics, dataGridView5, x, y, f, "Segment Properties Data");

            e.HasMorePages = false;
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += PrintDocument_PrintPage;
                doc.DocumentName = "Output Data";

                PrintDialog dlg = new PrintDialog { Document = doc };
                if (dlg.ShowDialog() == DialogResult.OK) doc.Print();
            }
            catch (Exception ex)
            {
                ShowError($"Printing failed: {ex.Message}");
            }
        }
        #endregion

        #region --- Event Handlers (grid formatting / double-click) ---

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name != "Check" || e.Value == null) return;

            if (double.TryParse(e.Value.ToString(), out double val))
                e.CellStyle.ForeColor = (val >= 0.95 || val <= 0.75) ? Color.Red : Color.Black;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
            {
                ShowError("Please select a valid row to modify.");
                return;
            }

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is not CheckTableData check) return;

            SegmentDialogBox dlg = new SegmentDialogBox(check.SegmentID, "Modify", _waterTankForm);
            if (dlg.ShowDialog() != DialogResult.OK) return;

            // refresh all dependent tables
            try
            {
                selfWeightData.Clear();
                windLoadData.Clear();
                LoadData();
                LoadSegmentWeightData();
                LoadCummulativeWeightData();
                WindLoadPerSegment();
                LoadTable2();
                LoadCheckTableData();
            }
            catch (Exception ex)
            {
                ShowError($"Error while refreshing data after modification: {ex.Message}");
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            using Help h = new Help();
            h.ShowDialog();
        }
        #endregion
    }

    #region --- DTO / ViewModel classes (unchanged) ---

    public class designTableData
    {
        public string Segment { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public double A { get; set; }
        public double I { get; set; }
        public double S { get; set; }
    }

    public class segmentGravityLoad
    {
        public string waterWeight { get; set; }
        public string snowWeight { get; set; }
        public string selfWeight { get; set; }
    }

    public class CummulativeGravityLoad
    {
        public double water { get; set; }
        public double snow { get; set; }
        public double selfWeight { get; set; }
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

    public class CheckTableData
    {
        public int SegmentID { get; set; }
        public string Segment { get; set; }
        public double fa { get; set; }
        public double fb { get; set; }
        public string check { get; set; }
    }

    public class TanksData
    {
        public List<Tank> tanks { get; set; }
    }

    public class Tank
    {
        public string type { get; set; }
        public string Weight_of_Water { get; set; }
        public string Weight_of_Steel { get; set; }
        public string Total_Weight { get; set; }
        public string Projected_Area { get; set; }
        public string height { get; set; }
        public string diameter { get; set; }
        public string thickness { get; set; }
    }

    public class WindTable
    {
        public string Fwind { get; set; }
        public string Vwind { get; set; }
        public string BaseElevation { get; set; }
        public string LoadLocation { get; set; }
        public string ArmLength { get; set; }
        public string FArm { get; set; }
        public string Mwind { get; set; }

        // ==================================================================
        //  Designer-generated event hooks – keep these methods even if empty
        // ==================================================================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No action needed – left for Designer compatibility
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No action needed – left for Designer compatibility
        }

        private void Solver_Output_Load(object sender, EventArgs e)
        {
            // Form-load logic is already handled in the constructor.
            // Keep this stub so the Designer remains happy.
        }

    }



    #endregion
}
