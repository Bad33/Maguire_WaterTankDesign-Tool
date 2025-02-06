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
using Rectangle = System.Drawing.Rectangle;

namespace WaterTankTool_WFA.Solver
{
    public partial class Solver_Output : Form
    {
        public Solver_Output()
        {
            InitializeComponent();
            //this.Load += Form1_Load;
            //this.dataGridView5.Paint += dataGridView5_Paint;
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    dataGridView5.ColumnCount = 6;
        //    dataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        //    dataGridView5.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        //    dataGridView5.ColumnHeadersHeight = 40;

        //    // Set the individual column headers (this part is the same)
        //    dataGridView5.Columns[0].HeaderCell.Value = "Sub 1";
        //    dataGridView5.Columns[1].HeaderCell.Value = "Sub 2";
        //    dataGridView5.Columns[2].HeaderCell.Value = "Sub 3";
        //    dataGridView5.Columns[3].HeaderCell.Value = "Sub 4";
        //    dataGridView5.Columns[4].HeaderCell.Value = "Sub 5";
        //    dataGridView5.Columns[5].HeaderCell.Value = "Sub 6";

        //    dataGridView5.AllowUserToAddRows = false;
        //    dataGridView5.AllowUserToDeleteRows = false;
        //    dataGridView5.ReadOnly = true;
        //}

        //private void dataGridView5_Paint(object sender, PaintEventArgs e)
        //{
        //    // Get the DataGridView's header rectangle
        //    Rectangle headerRect = this.dataGridView5.DisplayRectangle;
        //    headerRect.Height = this.dataGridView5.ColumnHeadersHeight; // Important!

        //    // Calculate the positions and sizes for the group headers
        //    int groupWidth = headerRect.Width / 3; // 3 groups
        //    int groupHeight = headerRect.Height / 2; // Two rows

        //    // Draw the group header rectangles and text
        //    using (Brush brush = new SolidBrush(this.dataGridView5.ColumnHeadersDefaultCellStyle.BackColor)) // Or a color you prefer
        //    using (Pen pen = new Pen(this.dataGridView5.GridColor)) // Or a color you prefer
        //    using (Font font = this.dataGridView5.ColumnHeadersDefaultCellStyle.Font)
        //    {
        //        // Group 1
        //        Rectangle rect1 = new Rectangle(headerRect.X, headerRect.Y, groupWidth, groupHeight); // Span 2 columns
        //        e.Graphics.FillRectangle(brush, rect1);
        //        e.Graphics.DrawRectangle(pen, rect1);
        //        TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
        //        TextRenderer.DrawText(e.Graphics, "", font, rect1, this.dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor, flags);


        //        // Group 2
        //        Rectangle rect2 = new Rectangle(headerRect.X + groupWidth, headerRect.Y, groupWidth, groupHeight); // Span 2 columns
        //        e.Graphics.FillRectangle(brush, rect2);
        //        e.Graphics.DrawRectangle(pen, rect2);
        //        TextRenderer.DrawText(e.Graphics, "Gravity Loads (per segment)", font, rect2, this.dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor, flags);

        //        // Group 3
        //        Rectangle rect3 = new Rectangle(headerRect.X + groupWidth, headerRect.Y, groupWidth, groupHeight); // Span 2 columns
        //        e.Graphics.FillRectangle(brush, rect3);
        //        e.Graphics.DrawRectangle(pen, rect3);
        //        TextRenderer.DrawText(e.Graphics, "Gravity Loads (Cumulative)", font, rect3, this.dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor, flags);

        //        Rectangle rect4 = new Rectangle(headerRect.X + groupWidth * 4, headerRect.Y, groupWidth * 2, groupHeight); // Span 2 columns
        //        e.Graphics.FillRectangle(brush, rect4);
        //        e.Graphics.DrawRectangle(pen, rect4);
        //        TextRenderer.DrawText(e.Graphics, "Wind Load (per segment)", font, rect4, this.dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor, flags);
        //    }

        //}

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Solver_Output_Load(object sender, EventArgs e)
        {

        }

        private float PrintTableData(Graphics g, List<string> data, float xPos, float yPos, System.Drawing.Font printFont,PrintPageEventArgs e)
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
            Graphics g = e.Graphics;
            float yPos = e.MarginBounds.Top; // Starting Y position
            float xPos = e.MarginBounds.Left; // Starting X position
            System.Drawing.Font printFont = new System.Drawing.Font("Arial", 12); 

            // Example: Assuming you have lists of data for each table
            List<string> table1Data = GetTable1Data(); // Your method to get table 1 data
            //List<string> table2Data = GetTable2Data(); 
                                                       // ... and so on for other tables

            // Print Table 1
            yPos = PrintTableData(g, table1Data, xPos, yPos, printFont,e);

            // Print Table 2 (move down a bit for spacing)
            //yPos += 20;  
            //yPos = PrintTableData(g, table2Data, xPos, yPos, printFont);
            // ... print other tables

            // Check if more pages needed (for long tables)
            e.HasMorePages = (yPos < e.MarginBounds.Bottom); // Example condition
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
            //using (MemoryStream ms = new MemoryStream())
            //using (Document doc = new Document(PageSize.A4, 25, 25, 30, 30))
            //using (PdfWriter writer = PdfWriter.GetInstance(doc, ms))
            //{
            //    doc.Open();

            //    foreach (GroupBox groupBox in this.Controls.OfType<GroupBox>()) // Iterate through GroupBoxes on the form
            //    {
            //        // 1. Calculate total content height within the GroupBox (for scrolling)
            //        int totalContentHeight = 0;
            //        foreach (Control c in groupBox.Controls)
            //        {
            //            totalContentHeight = Math.Max(totalContentHeight, c.Bottom);
            //        }

            //        int printableHeight = 700; // Adjust as needed
            //        int numPages = (int)Math.Ceiling((double)totalContentHeight / printableHeight);

            //        for (int i = 0; i < numPages; i++)
            //        {
            //            Bitmap bmp = new Bitmap(groupBox.Width, printableHeight);
            //            using (Graphics g = Graphics.FromImage(bmp))
            //            {
            //                Rectangle clipRect = new Rectangle(0, i * printableHeight, bmp.Width, printableHeight);
            //                g.SetClip(clipRect);

            //                g.TranslateTransform(0, -i * printableHeight);
            //                groupBox.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            //                g.ResetTransform();
            //            }

            //            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(bmp, ImageFormat.Png);
            //            pdfImage.ScaleToFit(doc.PageSize.Width - doc.LeftMargin - doc.RightMargin, doc.PageSize.Height - doc.TopMargin - doc.BottomMargin);
            //            pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            //            doc.Add(pdfImage);

            //            if (i < numPages - 1) doc.NewPage();
            //        }
            //        if (groupBox != this.Controls.OfType<GroupBox>().Last()) doc.NewPage(); // New page after each groupbox except the last one
            //    }


            //    doc.Close();

            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Filter = "PDF files (*.pdf)|*.pdf";
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //        File.WriteAllBytes(sfd.FileName, ms.ToArray());
            //}
        }


    }
}
