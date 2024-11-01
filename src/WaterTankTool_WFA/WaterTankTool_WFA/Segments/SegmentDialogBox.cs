using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Constants;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Solver_Equation;

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
            //UpdateWeightLabel();
            ValidateLabels();
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
                    WaterTank form1 = new WaterTank();
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
            ValidateLabels();
        }


        private void ValidateLabels()
        {
            Segment_Cylinder_Equations cylinder_Equations = new Segment_Cylinder_Equations();
            Segment_Conical_Equations conical_Equations = new Segment_Conical_Equations();

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

                    if (_segmentType == "Cylinder")
                    {
                        textBox1.Text = cylinder_Equations.weightOfPedestal(heightInitial,heightFinal,diameter,thickness).ToString();
                        textBox2.Text = cylinder_Equations.ProjectedArea(heightInitial,heightFinal,diameter).ToString();
                        textBox3.Text = cylinder_Equations.Centroid(heightInitial,heightFinal).ToString();
                        textBox4.Text = cylinder_Equations.kzi(heightInitial).ToString();
                        textBox5.Text = cylinder_Equations.kzf(heightFinal).ToString();
                        textBox6.Text = cylinder_Equations.qzi(heightInitial).ToString();
                        textBox7.Text = cylinder_Equations.qzf(heightFinal).ToString();
                        textBox8.Text = cylinder_Equations.F(heightInitial,heightFinal,diameter).ToString();
                        textBox9.Text = cylinder_Equations.L(heightInitial,heightFinal).ToString();
                        textBox10.Text = cylinder_Equations.Mbase(heightInitial,heightFinal,diameter).ToString();

                    }

                    else if (_segmentType == "Base")
                    {
                        //change the diameter to diameter initial and final 
                        textBox1.Text = conical_Equations.weight(heightInitial, heightFinal,diameter ,diameter, thickness).ToString();
                        textBox2.Text = cylinder_Equations.ProjectedArea(heightInitial, heightFinal, diameter).ToString();
                        textBox3.Text = cylinder_Equations.Centroid(heightInitial, heightFinal).ToString();
                        textBox4.Text = cylinder_Equations.kzi(heightInitial).ToString();
                        textBox5.Text = cylinder_Equations.kzf(heightFinal).ToString();
                        textBox6.Text = cylinder_Equations.qzi(heightInitial).ToString();
                        textBox7.Text = cylinder_Equations.qzf(heightFinal).ToString();
                        textBox8.Text = cylinder_Equations.F(heightInitial, heightFinal, diameter).ToString();
                        textBox9.Text = cylinder_Equations.L(heightInitial, heightFinal).ToString();
                        textBox10.Text = cylinder_Equations.Mbase(heightInitial, heightFinal, diameter).ToString();

                    }

                }
                else
                {
                    setTextboxvalues("Invalid Input");
                }
            }
            else
            {
                setTextboxvalues("-");
            }
        }

        public void setTextboxvalues(string value)
        {
            textBox1.Text = value;
            textBox2.Text = value;
            textBox3.Text = value;
            textBox4.Text = value;
            textBox5.Text = value;
            textBox6.Text = value;
            textBox7.Text = value;
            textBox8.Text = value;
            textBox9.Text = value;
            textBox10.Text = value; 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //UpdateProjectedArea();
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
                    //double height = CalculateHeight(heightFinal, heightInitial);

                    //var p_area = diameter * height;

                    // Update the weight textbox with the calculated weight
                    textBox2.Text = "sfsdf"; 
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
                textBox2.Text = "-";
            }
        }
    }
}
