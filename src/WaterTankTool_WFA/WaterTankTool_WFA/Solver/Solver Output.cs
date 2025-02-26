using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Migrations;
using Rectangle = System.Drawing.Rectangle;

namespace WaterTankTool_WFA.Solver
{


    public partial class Solver_Output : Form
    {

        private WaterTankDbContext _context;

        public Solver_Output()
        {
            InitializeComponent();

            var context = WaterTankDbContext.GetInstance();
            _context = context;

            LoadData();

            LoadAllowableCompressiveStress();

            LoadTable2();

            LoadCheckTableData();
        }

        private void LoadCheckTableData()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));
            var viewModelData = segmentData.Select(segment => new CheckTableData
            {
                Segment = segment.SegmentName,
                //fa = ,
                //fb = ,
                //check = ,
 

                //A = Math.Round((Math.PI / 4) * (Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 2))),



            }).ToList();

            dataGridView1.DataSource = viewModelData;

        }

        private void LoadTable2()
        {
            var segmentData = _context.SegmentProperties.ToList();
            segmentData.Sort((x, y) => y.HeightInitial.CompareTo(x.HeightInitial));

            var viewModelData = segmentData.Select(segment => new tabelData2
            {
                Segment = segment.SegmentName,
                Radius = segment.Diameter /2,
                Thickness = segment.Thickness,

                //Rt = ,


                // Compute Eq as before
                A = Math.Round((Math.PI / 4) * (Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 2))),

                I = Math.Round((Math.PI / 64) * (Math.Pow(segment.Diameter, 4) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 4)), 4),

                //r = ,
                //Co = ,
                //Fl = ,
                //KLr = ,
                //Cc = ,
                //Kf = ,
                //Fa = ,
                //Fb = ,

            }).ToList();

            dataGridView3.DataSource = viewModelData;

        }

        private Label rtcLabel;

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
                    UpdateRtcLabel(cb.SelectedItem.ToString());
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


            var viewModelData = segmentData.Select(segment => new designTableData
            {
                Segment = segment.SegmentName,
                Diameter = segment.Diameter,
                Thickness = segment.Thickness,

                // Compute Eq as before
                A = Math.Round((Math.PI / 4)*(Math.Pow(segment.Diameter, 2) - Math.Pow((segment.Diameter - (2 * segment.Thickness)),2))),

                I = Math.Round((Math.PI / 64) * (Math.Pow(segment.Diameter, 4) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 4)),4),

                S = Math.Round(((Math.PI / 64) * (Math.Pow(segment.Diameter, 4) - Math.Pow((segment.Diameter - (2 * segment.Thickness)), 4)) )/ (2*segment.Diameter),4),

            }).ToList();



            //Gravity Loads (per segment)
            //var segmentGravityLoadData = 


            dataGridView5.DataSource = viewModelData;


        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView5.DataSource = _context.SegmentProperties.ToList();

  
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
    }

    public class designTableData
    {
        public String Segment { get; set; }
        public Double Diameter { get; set; }
        public Double Thickness { get; set; }
        public Double A { get; set; }
        public Double I { get; set; }
        public Double S { get; set; }
    }

    public class segmentGravityLoad
    {
        public double water { get; set; }
        public double snow { get; set; }
        public double selfWeight { get; set; }
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
        public double check {  get; set; }
    }

    }
