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
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Migrations;
using WaterTankTool_WFA.Solver_Equation;
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

        private Label rtcLabel;

        public string Fy;

        public segmentGravityLoad segmentGravityLoad;

        public List<string> selfWeightData = new List<string>();

        public List<segmentGravityLoad> cummulativeLoadData = new List<segmentGravityLoad>();

        public List<WindTable> windLoadData = new List<WindTable>();

        public List<designTableData> segmentPropertiesTableData = new List<designTableData>();

        public List<tabelData2> tabelData2s = new List<tabelData2>();

        public Solver_Output()
        {
            InitializeComponent();

            var context = WaterTankDbContext.GetInstance();
            _context = context;

            LoadData();

            LoadAllowableCompressiveStress();


            LoadSegmentWeightData();

            LoadCummulativeWeightData();

            WindLoadPerSegment();

            LoadTable2();

            LoadCheckTableData();
        }

        private void WindLoadPerSegment()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            Segment_Cylinder_Equations segment_Cylinder_Equations = new Segment_Cylinder_Equations();
            Segment_Conical_Equations segment_Conical_Equations = new Segment_Conical_Equations();

            
            double cumulativeFwind = 0;

            foreach (var segment in segmentData)
            {
                double fwind = segment_Cylinder_Equations.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter);
                double loadlocation = segment_Cylinder_Equations.L(segment.HeightInitial, segment.HeightFinal);
                double baseElevation = segment.HeightInitial;
                double armLength = loadlocation - baseElevation;
                double farm = fwind * armLength;

                cumulativeFwind += fwind;  // cumulative addition

                windLoadData.Add(new WindTable
                {
                    Fwind = fwind.ToString(),
                    Vwind = cumulativeFwind.ToString(), // cumulative sum updated per iteration
                    BaseElevation = segment.HeightInitial.ToString(),
                    LoadLocation = loadlocation.ToString(),
                    ArmLength = armLength.ToString(),
                    FArm = farm.ToString(),
                    Mwind = segment_Cylinder_Equations.Mbase(segment.HeightInitial, segment.HeightFinal, segment.Diameter).ToString()
                });
            }


            dataGridView7.DataSource = windLoadData;


        }

        private void LoadSegmentWeightData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            string json = File.ReadAllText("../../../tanks.json");

            if (segmentData.Count > 0)
            {

                var tankCapacity = segmentData[0].SegmentName;

                TanksData data = JsonSerializer.Deserialize<TanksData>(json);
                Tank foundTank = data.tanks.Find(t => t.type == tankCapacity);



                waterWeight = foundTank.Weight_of_Water;
                snowWeight = _context.SnowLoadEntity.FirstOrDefault().Total_Load.ToString();
                selfWeight = foundTank.Weight_of_Steel;

                string numericPart = new string(selfWeight
                 .Where(c => char.IsDigit(c) || c == '.' || c == '-')
                 .ToArray());
                var result = double.Parse(numericPart, CultureInfo.InvariantCulture);

                Segment_Cylinder_Equations segment_Cylinder_Equations = new Segment_Cylinder_Equations();
                Segment_Conical_Equations segment_Conical_Equations = new Segment_Conical_Equations();

                var viewModelData = segmentData.FindAll(x => x.SegmentType == "Tanks").Select(segment => new segmentGravityLoad
                {

                    waterWeight = waterWeight,
                    snowWeight = snowWeight,
                    selfWeight = result.ToString(),


                }).ToList();



                var viewModelData1 = segmentData.FindAll(x => x.SegmentType == "Cylinder").Select(segment => new segmentGravityLoad
                {

                    waterWeight = "0",
                    snowWeight = "0",
                    selfWeight = segment_Cylinder_Equations.weightOfPedestal(
                                    segment.HeightInitial,
                                    segment.HeightFinal,
                                    segment.Diameter,
                                    segment.Thickness).ToString()


                }).ToList();

                var viewModelData2 = segmentData.FindAll(x => x.SegmentType == "Base").Select(segment => new segmentGravityLoad
                {

                    waterWeight = "0",
                    snowWeight = "0",
                    selfWeight = segment_Conical_Equations.weight(
                                    segment.HeightInitial,
                                    segment.HeightFinal,
                                    (double)segment.DiameterInitial,
                                    (double)segment.DiameterFinal,
                                    segment.Thickness).ToString()


                }).ToList();


                var combineViewModel = viewModelData.Concat(viewModelData1).Concat(viewModelData2).ToList();

                foreach (var item in combineViewModel)
                {
                    selfWeightData.Add(item.selfWeight);
                }

                dataGridView4.DataSource = combineViewModel;
            }
            else
            {
                //MessageBox.Show("Muji");
                Label label = new Label();
                label.Text = "No Segments Added. Please add the segments to see the output.";
                label.ForeColor = Color.Red;
                
                statusStrip2.Items.Add(label.Text);
                //label.Controls.Add(statusStrip2);
            }

        }

        private void LoadCummulativeWeightData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            string json = File.ReadAllText("../../../tanks.json");

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
                        snowWeight = snowWeight,
                        selfWeight = res[index], // Store the cumulative value

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
                var viewModelData = segmentData.Select((segment,index) => 
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
                        fa = (Double.Parse(cummulativeLoadData[index].waterWeight) + Double.Parse(cummulativeLoadData[index].snowWeight) + Double.Parse(cummulativeLoadData[index].selfWeight)) / segmentPropertiesTableData[index].A;
                        fb = (Double.Parse(windLoadData[index].Mwind) * 12) / segmentPropertiesTableData[index].S;

                        if((fa + fb) == 0)
                        {
                            check = "NA";
                        }
                        else
                        {
                            check = ((fa / tabelData2s[index].Fa) + (fb / tabelData2s[index].Fb)).ToString();

                        }
                    }


                    return new CheckTableData
                    {

                        Segment = segment.SegmentName,
                        fa = fa,
                        fb = fb,
                        check = check,


                        //A = Math.Round((Math.PI / 4) * (Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 2))),
                    };



                }).ToList();

                dataGridView1.DataSource = viewModelData;
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
                    var rt = (segment.Diameter / 2) / segment.Thickness;
                    var i = Math.Round((Math.PI / 64) * (Math.Pow(segment.Diameter, 4) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 4)), 4);
                    var a = Math.Round((Math.PI / 4) * (Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 2)));
                    var co = 1022 / (195 + rt);
                    var r = Math.Sqrt(i / a);
                    double Fl = 0;
                    if (rt <= Double.Parse(rtcLabel.Text))
                    {
                        Fl = (233 * Double.Parse(Fy)) / (2 * (166 + rt));
                    }
                    else if (rt > Double.Parse(rtcLabel.Text))
                    {
                        Fl = (co * 29000000) / (2 * rt);
                    }

                    var klr = (2.1 * 2124) / r;

                    var cc = Math.Sqrt((Math.Pow(Math.PI, 2) * 29000000) / Fl);

                    double kf = 0;

                    if (klr <= 25)
                    {
                        kf = 1;
                    }
                    else if (klr > 25 && klr <= cc)
                    {
                        kf = 1 - (0.5 * Math.Pow((klr / cc), 2));
                    }
                    else if (klr > cc)
                    {
                        kf = (0.5 * Math.Pow((cc / klr), 2));
                    }

                    var fa = Fl * kf;
                    var fb = Fl;

                    return new tabelData2
                    {
                        Segment = segment.SegmentName,
                        Radius = segment.Diameter / 2,
                        Thickness = segment.Thickness,
                        Rt = rt,
                        A = a,
                        I = i,
                        r = r,
                        Co = co,
                        Fl = Fl,
                        KLr = klr,
                        Cc = cc,
                        Kf = kf,
                        Fa = fa,
                        Fb = fb,
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
                rtc = "403";
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


            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            if (segmentData.Count > 0)
            {

                segmentPropertiesTableData = segmentData.Select(segment => new designTableData
                {
                    Segment = segment.SegmentName,
                    Diameter = segment.Diameter,
                    Thickness = segment.Thickness,

                    // Compute Eq as before
                    A = Math.Round((Math.PI / 4) * (Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 2))),

                    I = Math.Round((Math.PI / 64) * (Math.Pow(segment.Diameter, 4) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 4)), 4),

                    S = Math.Round(((Math.PI / 64) * (Math.Pow(segment.Diameter, 4) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 4))) / (2 * segment.Diameter), 4),

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

        private float PrintTableData(Graphics g, List<string> data, float xPos, float yPos, System.Drawing.Font printFont, PrintPageEventArgs e)
        {
            foreach (string item in data)
            {
                g.DrawString(item, printFont, Brushes.Black, xPos, yPos);
                yPos += printFont.GetHeight(); // Move to the next line
                if (yPos > e.MarginBounds.Bottom) // Check if we've reached the bottom of the page
                {
                    return yPos; // Return the current yPos to signal that we need more pages
                }
            }
            return yPos;
        }


        private List<string> GetTable1Data()
        {
            // ... your logic to retrieve data from table 1
            // Example:
            return new List<string> { "Row 1, Col 1", "Row 1, Col 2", "Row 2, Col 1", "Row 2, Col 2" };
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Graphics g = e.Graphics;
            //float yPos = e.MarginBounds.Top; // Starting Y position
            //float xPos = e.MarginBounds.Left; // Starting X position
            //System.Drawing.Font printFont = new System.Drawing.Font("Arial", 12);

            //// Example: Assuming you have lists of data for each table
            //List<string> table1Data = GetTable1Data(); // Your method to get table 1 data
            //                                           //List<string> table2Data = GetTable2Data(); 
            //                                           // ... and so on for other tables

            //// Print Table 1
            //yPos = PrintTableData(g, table1Data, xPos, yPos, printFont, e);

            //// Print Table 2 (move down a bit for spacing)
            ////yPos += 20;  
            ////yPos = PrintTableData(g, table2Data, xPos, yPos, printFont);
            //// ... print other tables

            //// Check if more pages needed (for long tables)
            //e.HasMorePages = (yPos < e.MarginBounds.Bottom); // Example condition
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocument.DocumentName = "My Print Job"; // Set a document name (optional)

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }


            // This is the working code to print each tables in separate page in PDF
            using (MemoryStream ms = new MemoryStream())
            using (Document doc = new Document(PageSize.A4, 25, 25, 30, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(doc, ms))
            {
                doc.Open();

                foreach (GroupBox groupBox in this.Controls.OfType<GroupBox>()) // Iterate through GroupBoxes on the form
                {
                    // 1. Calculate total content height within the GroupBox (for scrolling)
                    int totalContentHeight = 0;
                    foreach (Control c in groupBox.Controls)
                    {
                        totalContentHeight = Math.Max(totalContentHeight, c.Bottom);
                    }

                    int printableHeight = 700; // Adjust as needed
                    int numPages = (int)Math.Ceiling((double)totalContentHeight / printableHeight);

                    for (int i = 0; i < numPages; i++)
                    {
                        Bitmap bmp = new Bitmap(groupBox.Width, printableHeight);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            Rectangle clipRect = new Rectangle(0, i * printableHeight, bmp.Width, printableHeight);
                            g.SetClip(clipRect);

                            g.TranslateTransform(0, -i * printableHeight);
                            groupBox.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                            g.ResetTransform();
                        }

                        iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(bmp, ImageFormat.Png);
                        pdfImage.ScaleToFit(doc.PageSize.Width - doc.LeftMargin - doc.RightMargin, doc.PageSize.Height - doc.TopMargin - doc.BottomMargin);
                        pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                        doc.Add(pdfImage);

                        if (i < numPages - 1) doc.NewPage();
                    }
                    if (groupBox != this.Controls.OfType<GroupBox>().Last()) doc.NewPage(); // New page after each groupbox except the last one
                }


                doc.Close();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                    File.WriteAllBytes(sfd.FileName, ms.ToArray());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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
