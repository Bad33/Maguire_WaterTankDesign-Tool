using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
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

namespace WaterTankTool_WFA.Solver
{


    public partial class Solver_Output : Form
    {

        private WaterTankDbContext _context;
        public string waterWeight;
        public string snowWeight;
        public string selfWeight;
        private WaterTank _waterTankForm;
        UnitsConverter inchToFtConverter = new UnitsConverter();

        private Label rtcLabel;

        public string Fy;

        public segmentGravityLoad segmentGravityLoad;

        public List<string> selfWeightData = new List<string>();

        public List<segmentGravityLoad> cummulativeLoadData = new List<segmentGravityLoad>();

        public List<WindTable> windLoadData = new List<WindTable>();

        public List<designTableData> segmentPropertiesTableData = new List<designTableData>();

        public List<tabelData2> tabelData2s = new List<tabelData2>();

       

        public Solver_Output(WaterTank waterTankForm)
        {
            InitializeComponent();



            var context = WaterTankDbContext.GetInstance();
            _context = context;
            _waterTankForm = waterTankForm;
            LoadData();

            LoadAllowableCompressiveStress();


            LoadSegmentWeightData();

            LoadCummulativeWeightData();

            WindLoadPerSegment();

            LoadTable2();

            LoadCheckTableData();
            _waterTankForm = waterTankForm;
        }

        private double calculateF(double qzi, double qzf, double projectedArea)
        {

            var result = (((qzi + qzf) / 2) * projectedArea) / 1000;

            return result;

        }
        public static double ExtractDoubleValue(string input)
        {

            string[] parts = input.Split();

            if (parts.Length == 0)
            {
                throw new FormatException("Input string is empty.");
            }

            if (double.TryParse(parts[0], out double result))
            {
                return result;
            }
            else
            {
                throw new FormatException($"Unable to parse '{parts[0]}' as a double.");
            }
        }


        private void WindLoadPerSegment()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            var segmentCylinderEquations = new Segment_Cylinder_Equations();
            var segmentConicalEquations = new Segment_Conical_Equations();
            var tankProperties = _context.TankProperties.FirstOrDefault();

            double projectedArea = 0;

            if (tankProperties == null)
            {
                MessageBox.Show("Please add segments first!", "No Segments added", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                projectedArea = ExtractDoubleValue(tankProperties.ProjectedArea);

            }
            double cumulativeFwind = 0;


            int index = 0;
            foreach (var segment in segmentData)
            {
                double fwind = 0;
                double loadLocation = 0;
                if (segment.SegmentType == "Tanks")
                {
                    fwind = calculateF(
                        segmentCylinderEquations.qzi(segment.HeightInitial),
                        segmentCylinderEquations.qzf(segment.HeightFinal),
                        projectedArea);
                    loadLocation = double.Parse(tankProperties.Centroid) + segment.HeightInitial;
                }
                else
                {
                    fwind = segmentCylinderEquations.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter);
                    loadLocation = segmentCylinderEquations.L(segment.HeightInitial, segment.HeightFinal);
                }

                double baseElevation = segment.HeightInitial;
                double armLength = loadLocation - baseElevation;
                double farm = fwind * armLength; 

                cumulativeFwind += fwind;

                double sumContributions = 0;
                for (int j = 0; j < windLoadData.Count; j++)
                {
                    double prevFwind = Double.Parse(windLoadData[j].Fwind);
                    double prevLoadLocation = Double.Parse(windLoadData[j].LoadLocation);
                    sumContributions += prevFwind * (prevLoadLocation - baseElevation);
                }

                double mwind = farm + sumContributions;

                windLoadData.Add(new WindTable
                {
                    Fwind = Math.Round(fwind, 4).ToString(),
                    Vwind = Math.Round(cumulativeFwind, 4).ToString(),
                    BaseElevation = Math.Round(segment.HeightInitial, 4).ToString(),
                    LoadLocation = Math.Round(loadLocation, 4).ToString(),
                    ArmLength = Math.Round(armLength, 4).ToString(),
                    FArm = Math.Round(farm, 4).ToString(),
                    Mwind = Math.Round(mwind, 4).ToString()
                });

                index++;
            }

            LoadSegmentWeightData();
            dataGridView7.DataSource = windLoadData;
        }


        private void LoadSegmentWeightData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            //string json = File.ReadAllText("../../../tanks.json");
            string jsonStringPath = Path.Combine(Application.StartupPath, "tanks.json");
            if (!File.Exists(jsonStringPath))
            {
                MessageBox.Show("Tanks File not Found");
            }
            string json = File.ReadAllText(jsonStringPath);
           

            Segment_Cylinder_Equations segment_Cylinder_Equations = new Segment_Cylinder_Equations();
            Segment_Conical_Equations segment_Conical_Equations = new Segment_Conical_Equations();

            double miscLoad = 15;

            if (segmentData.Count > 0)
            {

                var tankCapacity = segmentData[0].SegmentName;

                double fWindData = 0;

                TanksData data = JsonSerializer.Deserialize<TanksData>(json);
                Tank foundTank = data.tanks.Find(t => t.type == tankCapacity);


                waterWeight = foundTank.Weight_of_Water;

                snowWeight = (_context.SnowLoadEntity?.FirstOrDefault().Total_Load ).ToString();
                selfWeight = foundTank.Weight_of_Steel;

                string numericPart = new string(selfWeight
                 .Where(c => char.IsDigit(c) || c == '.' || c == '-')
                 .ToArray());
                var result = double.Parse(numericPart, CultureInfo.InvariantCulture);



                var viewModelData = segmentData.FindAll(x => x.SegmentType == "Tanks").Select(segment => new segmentGravityLoad
                {

                    waterWeight = waterWeight,
                    snowWeight = (Double.Parse(snowWeight) + miscLoad).ToString(),
                    selfWeight = Math.Round(result, 4).ToString(),


                }).ToList();



                var viewModelData1 = segmentData.FindAll(x => x.SegmentType == "Cylinder").Select(segment => new segmentGravityLoad
                {

                    waterWeight = "0",
                    snowWeight = "0",
                    selfWeight = Math.Round(segment_Cylinder_Equations.weightOfPedestal(
                                    segment.HeightInitial,
                                    segment.HeightFinal,
                                    segment.Diameter,
                                    segment.Thickness), 4).ToString()


                }).ToList();

                var viewModelData2 = segmentData.FindAll(x => x.SegmentType == "Base").Select(segment => new segmentGravityLoad
                {

                    waterWeight = "0",
                    snowWeight = "0",
                    selfWeight = Math.Round(segment_Conical_Equations.weight(
                                    segment.HeightInitial,
                                    segment.HeightFinal,
                                    (double)segment.DiameterInitial,
                                    (double)segment.DiameterFinal,
                                    segment.Thickness), 4).ToString()


                }).ToList();


                var combineViewModel = viewModelData.Concat(viewModelData1).Concat(viewModelData2).ToList();

                foreach (var item in combineViewModel)
                {
                    selfWeightData.Add(item.selfWeight);
                }

                dataGridView4.DataSource = combineViewModel;


                ToolStripStatusLabel toolStripStatusLabel = new ToolStripStatusLabel();
                toolStripStatusLabel.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Double Click on the cell in Segment Check Table to change the thickness of segments. (It is recommended to gradually increase/decrease (T) by 25% each time) | For more info go to Help window. ";

                statusStrip2.Items.Add(toolStripStatusLabel);


            }
            else
            {


                ToolStripStatusLabel toolStripStatusLabel = new ToolStripStatusLabel();
                toolStripStatusLabel.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "No Segments Added. Please add the segments to see the output.";

                statusStrip2.Items.Add(toolStripStatusLabel);


            }

        }

        private void LoadCummulativeWeightData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));
            double miscLoad = 15;

            string jsonStringPath = Path.Combine(Application.StartupPath, "tanks.json");
            if (!File.Exists(jsonStringPath))
            {
                MessageBox.Show("Tanks File not Found");
            }
            string json = File.ReadAllText(jsonStringPath);



            if (segmentData.Count > 0)
            {
                var tankCapacity = segmentData[0].SegmentName;

                TanksData data = JsonSerializer.Deserialize<TanksData>(json);
                Tank foundTank = data.tanks.Find(t => t.type == tankCapacity);





                string numericPart = new string(foundTank.Weight_of_Water
                         .Where(c => char.IsDigit(c) || c == '.' || c == '-')
                         .ToArray());
                waterWeight = numericPart;

                snowWeight = _context.SnowLoadEntity.FirstOrDefault().Total_Load.ToString();
                selfWeight = foundTank.Weight_of_Steel;
                int cumulativeIndex = 0;


                Segment_Cylinder_Equations segment_Cylinder_Equations = new Segment_Cylinder_Equations();
                Segment_Conical_Equations segment_Conical_Equations = new Segment_Conical_Equations();

                List<string> res = new List<string>();
                for (int i = 0; i < selfWeightData.Count; i++)
                {
                    if (i == 0)
                    {
                        res.Add(selfWeightData[i]);

                    }
                    else
                    {
                        var gg = Double.Parse(selfWeightData[i]) + Double.Parse(res[i - 1]);
                        res.Add(gg.ToString());
                    }
                }

                cummulativeLoadData = segmentData.Select((segment, index) =>
                {
                    // Accumulate selfWeight in each iteration
                    return new segmentGravityLoad
                    {
                        waterWeight = waterWeight,
                        snowWeight = (Double.Parse(snowWeight) + miscLoad).ToString(),
                        selfWeight = Math.Round(Double.Parse(res[index]), 4).ToString(), // Store the cumulative value

                    };
                }).ToList();

                dataGridView6.DataSource = cummulativeLoadData;

            }


        }

        private void LoadCheckTableData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));



            if (segmentData.Count > 0)
            {
                var viewModelData = segmentData.Select((segment, index) =>
                {

                    double fa = 0;
                    double fb = 0;
                    string check = null;

                    if (segmentData[index].SegmentType == "Tanks")
                    {
                        fa = 0;
                        fb = 0;
                        check = "NA";
                    }
                    else
                    {
                        fa = Math.Round((Double.Parse(cummulativeLoadData[index].waterWeight) + Double.Parse(cummulativeLoadData[index].snowWeight) + Double.Parse(cummulativeLoadData[index].selfWeight)) / segmentPropertiesTableData[index].A, 4);
                        fb = Math.Round((Double.Parse(windLoadData[index].Mwind) * 12) / segmentPropertiesTableData[index].S, 4);

                        if ((fa + fb) == 0)
                        {
                            check = "NA";
                        }
                        else
                        {
                            check = Math.Round(((fa / tabelData2s[index].Fa) + (fb / tabelData2s[index].Fb)), 4).ToString();


                        }
                    }


                    return new CheckTableData
                    {
                        SegmentID = segment.SegmentNumber,
                        Segment = segment.SegmentName,
                        fa = fa,
                        fb = fb,
                        check = check,


                        //A = Math.Round((Math.PI / 4) * (Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 2))),
                    };



                }).ToList();


                dataGridView1.DataSource = viewModelData;
                dataGridView1.Columns["SegmentID"].Visible = false;
            }

        }

        private void LoadTable2()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            if (segmentData.Count > 0)
            {

                tabelData2s = segmentData.Select(segment =>
                {
                    var rt = Math.Round(((12 * (segment.DiameterFinal ?? segment.Diameter) / 2) / segment.Thickness), 4);
                    var i = Math.Round((Math.PI / 64) * (Math.Pow(12 * (segment.DiameterFinal ?? segment.Diameter), 4) - Math.Pow((12 * (segment.DiameterFinal ?? segment.Diameter) - (2 * segment.Thickness)), 4)), 4);
                    var a = Math.Round((Math.PI / 4) * (Math.Pow(12 * (segment.DiameterFinal ?? segment.Diameter), 2) - Math.Pow((12 * (segment.DiameterFinal ?? segment.Diameter) - (2 * segment.Thickness)), 2)), 4);
                    var co = Math.Round(1022 / (195 + rt), 4);
                    var r = Math.Round(Math.Sqrt(i / a), 4);
                    double Fl = 0;
                    if (rt < Double.Parse(rtcLabel.Text))
                    {
                        var value1 = Math.Round((233 * Double.Parse(Fy)) / (2 * (166 + rt)), 4);
                        var value2 = Math.Round((Double.Parse(Fy) / 2), 4);
                        Fl = double.Min(value1, value2);
                    }
                    else if (rt >= Double.Parse(rtcLabel.Text))
                    {
                        Fl = Math.Round((co * 29000000) / (2 * rt), 4);
                    }

                    var klr = Math.Round((2.1 * 2124) / r, 4);

                    var cc = Math.Round(Math.Sqrt((Math.Pow(Math.PI, 2) * 29000000) / Fl), 4);

                    double kf = 0;

                    if (klr <= 25)
                    {
                        kf = 1;
                    }
                    else if (klr > 25 && klr <= cc)
                    {
                        kf = Math.Round(1 - (0.5 * Math.Pow((klr / cc), 2)), 4);
                    }
                    else if (klr > cc)
                    {
                        kf = Math.Round((0.5 * Math.Pow((cc / klr), 2)), 4);
                    }
                    Fl = Fl / 1000;
                    var fa = Fl * kf;
                    var fb = Fl;

                    return new tabelData2
                    {
                        Segment = segment.SegmentName,
                        Radius = Math.Round((12 * ((segment.DiameterFinal ?? segment.Diameter) / 2)),4),
                        Thickness = Math.Round(segment.Thickness,4),
                        Rt = Math.Round(rt,4),
                        A = Math.Round(a,4),
                        I = Math.Round(i,4),
                        r = Math.Round(r,4),
                        Co = co,
                        Fl = Math.Round(Fl,4),
                        KLr = Math.Round(klr,4),
                        Cc = Math.Round(cc,4),
                        Kf = Math.Round(kf,4),
                        Fa = Math.Round(fa,4),
                        Fb = Math.Round(fb,4),
                    };


                }).ToList();

                dataGridView3.DataSource = tabelData2s;
            }

        }


        private void LoadAllowableCompressiveStress()
        {
            // Clear any existing controls if needed
            tableLayoutPanel1.Controls.Clear();

            // Left column labels (static text)
            tableLayoutPanel1.Controls.Add(new Label { Text = "Fv" }, 0, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "k" }, 0, 1);
            tableLayoutPanel1.Controls.Add(new Label { Text = "l" }, 0, 2);
            tableLayoutPanel1.Controls.Add(new Label { Text = "(R/t)c" }, 0, 3);
            tableLayoutPanel1.Controls.Add(new Label { Text = "E" }, 0, 4);

            // ComboBox for the first row, second column
            ComboBox dropdown = new ComboBox();
            dropdown.Dock = DockStyle.Fill;
            dropdown.Margin = new Padding(0);
            dropdown.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            tableLayoutPanel1.Controls.Add(dropdown, 1, 0);
            dropdown.Items.Add("30000");
            dropdown.Items.Add("32000");
            dropdown.Items.Add("34000");
            dropdown.Items.Add("36000");
            dropdown.Items.Add("38000");
            dropdown.Items.Add("40000");
            dropdown.SelectedItem = dropdown.Items[3]; // Set default to "36000"
            Fy = dropdown.SelectedItem.ToString();
            // Other static labels
            tableLayoutPanel1.Controls.Add(new Label { Text = "2.1" }, 1, 1);
            tableLayoutPanel1.Controls.Add(new Label { Text = "2124" }, 1, 2);

            // Create the rtc label that will be updated dynamically
            rtcLabel = new Label();
            tableLayoutPanel1.Controls.Add(rtcLabel, 1, 3);
            UpdateRtcLabel(dropdown.SelectedItem.ToString()); // Set initial value

            tableLayoutPanel1.Controls.Add(new Label { Text = "29000000" }, 1, 4);

            // Right column (unit labels)
            tableLayoutPanel1.Controls.Add(new Label { Text = "psi" }, 2, 0);
            tableLayoutPanel1.Controls.Add(new Label { Text = "in" }, 2, 2);
            tableLayoutPanel1.Controls.Add(new Label { Text = "psi" }, 2, 4);

            // Attach event handler so that when the dropdown value changes, the rtc label updates
            dropdown.SelectedIndexChanged += (sender, e) =>
            {
                ComboBox cb = sender as ComboBox;
                if (cb?.SelectedItem != null)
                {
                    Fy = cb.SelectedItem.ToString();
                    UpdateRtcLabel(cb.SelectedItem.ToString());
                    LoadTable2();

                }
            };
        }

        private void UpdateRtcLabel(string selectedValue)
        {
            string rtc;
            // Map the selected value to the appropriate computed string
            if (selectedValue == "36000")
            {
                rtc = "334";
            }
            else if (selectedValue == "32000")
            {
                rtc = "377";
            }
            else if (selectedValue == "34000")
            {
                rtc = "354";
            }
            else if (selectedValue == "38000")
            {
                rtc = "316";
            }
            else if (selectedValue == "40000")
            {
                rtc = "299";
            }
            else if (selectedValue == "30000")
            {
                // Provide a value for "30000", for example:
                rtc = "420";  // Change this value as needed
            }
            else
            {
                rtc = "0";
            }
            rtcLabel.Text = rtc;
        }


        private void LoadData()
        {
            var segmentData = _context.SegmentProperties.ToList();

            var pi = Math.PI;

            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            if (segmentData.Count > 0)
            {

                segmentPropertiesTableData = segmentData.Select(segment => new designTableData
                {
                    Segment = segment.SegmentName,
                    Diameter = (12 * (segment.DiameterFinal ?? segment.Diameter)),
                    Thickness = segment.Thickness,

                    // Compute Eq as before
                    A = Math.Round((Math.PI / 4) * (Math.Pow(12 * (segment.DiameterFinal ?? segment.Diameter), 2) - Math.Pow(((12 * (segment.DiameterFinal ?? segment.Diameter)) - (2 * (segment.Thickness))), 2)),4),

                    I = Math.Round((Math.PI / 64) * (Math.Pow(12 * (segment.DiameterFinal ?? segment.Diameter), 4) - Math.Pow(((12 * (segment.DiameterFinal ?? segment.Diameter)) - (2 * (segment.Thickness ))), 4)), 4),

                    S = Math.Round(((Math.PI / 64) * 2 *(Math.Pow(12 * (segment.DiameterFinal ?? segment.Diameter), 4) - Math.Pow(((12 * (segment.DiameterFinal ?? segment.Diameter)) - (2 * (segment.Thickness))), 4))) / (12 * (segment.DiameterFinal ?? segment.Diameter)), 4),

                }).ToList();

                dataGridView5.DataSource = segmentPropertiesTableData;
            }

        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void Solver_Output_Load(object sender, EventArgs e)
        {

        }

        private float PrintDataGridView(Graphics g, DataGridView dgv, float xPos, float yPos, System.Drawing.Font printFont, string tableName)
        {

            float startX = xPos;
            float startY = yPos;
            float cellHeight = printFont.GetHeight() + 10; // Row height with spacing
            float colWidth = 100; // Column width (adjust as needed)

            // Draw table name/title
            g.DrawString(tableName, new System.Drawing.Font("Arial", 12, FontStyle.Bold), Brushes.Black, xPos, yPos);
            yPos += cellHeight + 5; // Move down for table

            Pen borderPen = new Pen(Color.Black, 1); // Table border lines

            // Draw column headers with borders
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                RectangleF headerRect = new RectangleF(xPos, yPos, colWidth, cellHeight);
                g.DrawRectangle(borderPen, xPos, yPos, colWidth, cellHeight);
                g.DrawString(col.HeaderText, printFont, Brushes.Black, headerRect);
                xPos += colWidth;
            }
            yPos += cellHeight;
            xPos = startX; // Reset X position

            // Draw each row with borders
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow) // Skip empty last row
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        RectangleF cellRect = new RectangleF(xPos, yPos, colWidth, cellHeight);
                        g.DrawRectangle(borderPen, xPos, yPos, colWidth, cellHeight);
                        g.DrawString(Convert.ToString(cell.Value), printFont, Brushes.Black, cellRect);
                        xPos += colWidth;
                    }
                    yPos += cellHeight;
                    xPos = startX; // Reset X position for next row
                }
            }

            return yPos + 20; // Return new Y position (add space after table)
        }


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = e.MarginBounds.Top; // Start position for printing
            float xPos = e.MarginBounds.Left; // Left margin position
            System.Drawing.Font printFont = new System.Drawing.Font("Arial", 10);

            // Print First Table (e.g., dataGridView1)
            yPos = PrintDataGridView(e.Graphics, dataGridView1, xPos, yPos, printFont, "Check Table Data");
            yPos += 40; // Add space between tables

            // Print Second Table (e.g., dataGridView5)
            yPos = PrintDataGridView(e.Graphics, dataGridView5, xPos, yPos, printFont, "Segment Properties Data");

            e.HasMorePages = false; // Only one page
        }



        private void printToolStripButton_Click(object sender, EventArgs e)
        {

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocument.DocumentName = "Output Data"; //

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Check" && e.Value != null)
            {
                // Try converting the cell's value to a number.
                if (double.TryParse(e.Value.ToString(), out double cellValue))
                {
                    if (cellValue >= 0.95 || cellValue <= 0.75)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black; // or your default color
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Convert the row's DataBoundItem to our CheckTableData object
                CheckTableData checkData = row.DataBoundItem as CheckTableData;

                if (checkData != null)
                {
                    // We can now retrieve the unique ID
                    int segmentNumber = checkData.SegmentID;

                    // Open your dialog with that unique ID
                    SegmentDialogBox segmentDialogBox = new SegmentDialogBox(segmentNumber, "Modify", _waterTankForm);
                    var result = segmentDialogBox.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        LoadData();

                        LoadSegmentWeightData();
                        LoadCummulativeWeightData();
                        WindLoadPerSegment();
                        LoadTable2();
                        LoadCheckTableData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a valid row to modify.");
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
    }

    public class designTableData
    {
        public string Segment { get; set; }
        public Double Diameter { get; set; }
        public Double Thickness { get; set; }
        public Double A { get; set; }
        public Double I { get; set; }
        public Double S { get; set; }
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
        public string check {  get; set; }
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

    }


}
