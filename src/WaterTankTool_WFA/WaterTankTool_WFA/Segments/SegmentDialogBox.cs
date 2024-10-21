using Microsoft.EntityFrameworkCore;
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

namespace WaterTankTool_WFA
{
    public partial class SegmentDialogBox : Form
    {
        public String SegmentName { get; set; }
        public String SegmentType { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public double HeightInitial { get; set; }
        public double HeightFinal { get; set; }
        public double AverageHeight { get; set; }

        private string _segmentType;

        private int _segmentNumber;

        private string _dialogType;

        private double Ag;

        private double height;
        public SegmentDialogBox()
        {
            Ag = 0;
            height = 0;
            InitializeComponent();

            maskedTextBox1.TextChanged += InputFields_TextChanged;
            maskedTextBox2.TextChanged += InputFields_TextChanged;
            maskedTextBox3.TextChanged += InputFields_TextChanged;
            maskedTextBox4.TextChanged += InputFields_TextChanged;

            UpdateWeightLabel();

        }

        public SegmentDialogBox(string segmentType)
        {
            _segmentType = segmentType;
            InitializeComponent();
        }

        public SegmentDialogBox(int segmentNumber, string dialogType)
        {
            _dialogType = dialogType;
            _segmentNumber = segmentNumber;
            InitializeComponent();
            ModifyDialogBox();

        }

        private void InputFields_TextChanged(object sender, EventArgs e)
        {
            // Call the method to update the weight
            UpdateWeightLabel();
            UpdateProjectedArea();
        }

        private void ModifyDialogBox()
        {


            if (_dialogType == "Modify")
            {
                using (var context = new WaterTankDbContext())
                {
                    var segmentProperties = context.SegmentProperties.FirstOrDefault(item => item.SegmentNumber == _segmentNumber);
                    if (segmentProperties != null)
                    {
                        richTextBox1.Text = segmentProperties.SegmentName;
                        _segmentType = segmentProperties.SegmentType;
                        maskedTextBox2.Text = segmentProperties.Diameter.ToString();
                        maskedTextBox3.Text = segmentProperties.Thickness.ToString();
                        maskedTextBox1.Text = segmentProperties.HeightInitial.ToString();
                        maskedTextBox4.Text = segmentProperties.HeightFinal.ToString();

                    }
                }
            }
        }

        private void SegmentDialogBox_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            // Validate the input fields before proceeding
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) ||
                string.IsNullOrWhiteSpace(maskedTextBox2.Text) ||
                string.IsNullOrWhiteSpace(maskedTextBox3.Text) ||
                string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                string.IsNullOrWhiteSpace(maskedTextBox4.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try parsing numeric fields for Diameter, Thickness, and Heights
            if (!double.TryParse(maskedTextBox2.Text, out double diameter) ||
                !double.TryParse(maskedTextBox3.Text, out double thickness) ||
                !double.TryParse(maskedTextBox1.Text, out double heightInitial) ||
                !double.TryParse(maskedTextBox4.Text, out double heightFinal))
            {
                MessageBox.Show("Please enter valid numbers for Diameter, Thickness, and Heights.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new WaterTankDbContext())
            {
                SegmentProperties segmentProperties;

                if (_dialogType == "Modify")
                {
                    // Modify existing segment
                    segmentProperties = context.SegmentProperties.FirstOrDefault(item => item.SegmentNumber == _segmentNumber);

                    if (segmentProperties == null)
                    {
                        MessageBox.Show("Error: Segment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    segmentProperties.SegmentName = richTextBox1.Text;
                    segmentProperties.SegmentType = _segmentType;
                    segmentProperties.Diameter = diameter;
                    segmentProperties.Thickness = thickness;
                    segmentProperties.HeightInitial = heightInitial;
                    segmentProperties.HeightFinal = heightFinal;
                }
                else
                {
                    // Add new segment
                    segmentProperties = new SegmentProperties()
                    {
                        SegmentName = richTextBox1.Text,
                        SegmentType = _segmentType,
                        Diameter = diameter,
                        Thickness = thickness,
                        HeightInitial = heightInitial,
                        HeightFinal = heightFinal
                    };

                    context.SegmentProperties.Add(segmentProperties);
                }

                try
                {
                    int rowsAffected = context.SaveChanges();
                    successDialog(rowsAffected);
                    Form1 form1 = new Form1();
                    form1.OnSegmentAdded(segmentProperties);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void successDialog(int rowsAffected)
        {
            if (rowsAffected > 0)
            {
                DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    this.Close(); // Close the dialog on success
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Data might not have been saved!", "Confirmation", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    this.Close(); // Close the dialog even if no rows were affected
                }
            }
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            UpdateWeightLabel();
        }

        private void UpdateWeightLabel()
        {
            // Validate inputs and ensure all required fields are populated
            if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                !string.IsNullOrWhiteSpace(maskedTextBox2.Text) &&
                !string.IsNullOrWhiteSpace(maskedTextBox3.Text) &&
                !string.IsNullOrWhiteSpace(maskedTextBox4.Text))
            {
                if (double.TryParse(maskedTextBox2.Text, out double diameter) &&
                    double.TryParse(maskedTextBox3.Text, out double thickness) &&
                    double.TryParse(maskedTextBox1.Text, out double heightInitial) &&
                    double.TryParse(maskedTextBox4.Text, out double heightFinal))
                {
                    // Perform calculations if all inputs are valid
                    double Ag = CalculateCrossSectionArea(diameter, thickness);
                    double height = CalculateHeight(heightFinal, heightInitial);
                    double weight = Ag * height * 7850; // Using steel density (7850 kg/m^3)

                    // Update the weight textbox with the calculated weight
                    textBox1.Text = weight.ToString("F2"); // Format the result to two decimal places
                }
                else
                {
                    // Clear the weight label if any input is invalid
                    textBox1.Text = "Invalid input";
                }
            }
            else
            {
                // Clear the weight label if any input field is empty
                textBox1.Text = "N/A";
            }
        }




        private static double CalculateCrossSectionArea(double diameter, double thickness)
        {
            var crossSectionArea = (Math.PI / 4) * (Math.Pow(diameter, 2) - Math.Pow((diameter - 2 * thickness), 2));

            return crossSectionArea;
        }

        private static double CalculateHeight(double heightFinal, double heightInitial)
        {
            var height = heightFinal - heightInitial;

            return height;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateProjectedArea();
        }

        private void UpdateProjectedArea()
        {
            if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                    !string.IsNullOrWhiteSpace(maskedTextBox2.Text) &&
                    !string.IsNullOrWhiteSpace(maskedTextBox3.Text) &&
                    !string.IsNullOrWhiteSpace(maskedTextBox4.Text))
            {
                if (double.TryParse(maskedTextBox2.Text, out double diameter) &&
                    double.TryParse(maskedTextBox3.Text, out double thickness) &&
                    double.TryParse(maskedTextBox1.Text, out double heightInitial) &&
                    double.TryParse(maskedTextBox4.Text, out double heightFinal))
                {
                    // Perform calculations if all inputs are valid
                    double height = CalculateHeight(heightFinal, heightInitial);

                    var p_area = diameter * height;

                    // Update the weight textbox with the calculated weight
                    textBox2.Text = p_area.ToString("F2"); // Format the result to two decimal places
                }
                else
                {
                    // Clear the weight label if any input is invalid
                    textBox2.Text = "Invalid input";
                }
            }
            else
            {
                // Clear the weight label if any input field is empty
                textBox2.Text = "N/A";
            }
        }
    }
}
