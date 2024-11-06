using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            maskedTextBox5.TextChanged += InputFields_TextChanged;

        }

        public SegmentDialogBox(string segmentType)
        {
            _segmentType = segmentType;


            InitializeComponent();
            showInputFieldsOnType();
        }


        public SegmentDialogBox(int segmentNumber, string dialogType)
        {
            _dialogType = dialogType;
            _segmentNumber = segmentNumber;
            InitializeComponent();
            ModifyDialogBox();

        }

        public void showInputFieldsOnType()
        {
            if (_segmentType == "Base")
            {
                label3.Text = "DiameterInitial";
                label4.Text = "DiameterFinal";
                label17.Text = "ft";
                label25.Visible = true;
                maskedTextBox5.Visible = true;
                label26.Visible = true;
            }
            else if (_segmentType == "Cylinder")
            {
                label3.Text = "Diameter";
                label17.Text = "in";
                label25.Visible = false;
                maskedTextBox5.Visible = false;
                label26.Visible = false;
            }
        }

        private void InputFields_TextChanged(object sender, EventArgs e)
        {

            DoCalculations();
        }

        private void ModifyDialogBox()
        {


            if (_dialogType == "Modify")
            {
                using (var context = new WaterTankDbContext())
                {
                    var segmentProperties = context.SegmentProperties.FirstOrDefault(item => item.SegmentNumber == _segmentNumber);
                    if (segmentProperties != null && segmentProperties.SegmentType == "Base")
                    {
                        _segmentType = segmentProperties.SegmentType;

                        showInputFieldsOnType();

                        richTextBox1.Text = segmentProperties.SegmentName;
                        maskedTextBox2.Text = segmentProperties.DiameterInitial.ToString();
                        maskedTextBox3.Text = segmentProperties.DiameterFinal.ToString();
                        maskedTextBox1.Text = segmentProperties.HeightInitial.ToString();
                        maskedTextBox4.Text = segmentProperties.HeightFinal.ToString();
                        maskedTextBox5.Text = segmentProperties.Thickness.ToString();

                        DoCalculations();

                    }
                    else if(segmentProperties != null && segmentProperties.SegmentType == "Cylinder")
                    {
                        _segmentType = segmentProperties.SegmentType;

                        showInputFieldsOnType();


                        richTextBox1.Text = segmentProperties.SegmentName;
                        maskedTextBox2.Text = segmentProperties.Diameter.ToString();
                        maskedTextBox3.Text = segmentProperties.Thickness.ToString();
                        maskedTextBox1.Text = segmentProperties.HeightInitial.ToString();
                        maskedTextBox4.Text = segmentProperties.HeightFinal.ToString();

                        DoCalculations();

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

        private void Save_ClickBase(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) ||
             string.IsNullOrWhiteSpace(maskedTextBox2.Text) ||
             string.IsNullOrWhiteSpace(maskedTextBox3.Text) ||
             string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
             string.IsNullOrWhiteSpace(maskedTextBox4.Text) || string.IsNullOrWhiteSpace(maskedTextBox5.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(maskedTextBox2.Text, out double diameterInitial) ||
                !double.TryParse(maskedTextBox3.Text, out double diameterFinal) ||
                !double.TryParse(maskedTextBox1.Text, out double heightInitial) ||
                !double.TryParse(maskedTextBox4.Text, out double heightFinal) || !double.TryParse(maskedTextBox5.Text, out double thickness))
            {
                MessageBox.Show("Please enter valid numbers for Diameter, Thickness, and Heights.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var diameter = (double)(diameterFinal - diameterInitial);

            using (var context = new WaterTankDbContext())
            {
                SegmentProperties segmentProperties;




                if (_dialogType == "Modify")
                {
                    segmentProperties = context.SegmentProperties.FirstOrDefault(item => item.SegmentNumber == _segmentNumber);

                    ValidateSegment(segmentProperties);

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
                    segmentProperties.DiameterInitial = diameterInitial;
                    segmentProperties.DiameterFinal = diameterFinal;
                }
                else
                {
                    segmentProperties = new SegmentProperties()
                    {
                        SegmentName = richTextBox1.Text,
                        SegmentType = _segmentType,
                        Diameter = diameter,
                        Thickness = thickness,
                        HeightInitial = heightInitial,
                        HeightFinal = heightFinal,
                        DiameterInitial = diameterInitial,
                        DiameterFinal = diameterFinal

                    };

                    ValidateSegment(segmentProperties);


                    context.SegmentProperties.Add(segmentProperties);
                }

                try
                {
                    int rowsAffected = context.SaveChanges();
                    successDialog(rowsAffected);
                    //WaterTank form1 = new WaterTank();
                    //form1.OnSegmentAdded();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Save_ClickCylinder(object sender, EventArgs e)
        {
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

                    ValidateSegment(segmentProperties);

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

                    ValidateSegment(segmentProperties);


                    context.SegmentProperties.Add(segmentProperties);
                }

                try
                {
                    int rowsAffected = context.SaveChanges();
                    successDialog(rowsAffected);
                    //WaterTank form1 = new WaterTank();
                    //form1.OnSegmentAdded();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (_segmentType == "Base")
            {
                Save_ClickBase(sender, e);
            }
            else if (_segmentType == "Cylinder")
            {
                Save_ClickCylinder(sender, e);
            }

        }

        private void ValidateSegment(SegmentProperties segment)
        {
            if (segment.SegmentType == "Base" && (segment.DiameterInitial == null || segment.DiameterFinal == null))
            {
                throw new ValidationException("DiameterInitial and DiameterFinal must be specified for 'base' segment type.");
            }
            else if (segment.SegmentType != "Base" && (segment.DiameterInitial != null || segment.DiameterFinal != null))
            {
                throw new ValidationException("DiameterInitial and DiameterFinal should be null for non-base segment types.");
            }
            else if (segment.SegmentType == "Base" && (segment.DiameterInitial != null && segment.DiameterFinal != null && segment.Diameter == null))
            {
                segment.Diameter = (double)(segment.DiameterFinal - segment.DiameterInitial);
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
                    this.Close(); 
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
            DoCalculations();
        }


        private void DoCalculations()
        {
            if (string.IsNullOrWhiteSpace(_segmentType))
            {
                setTextboxvalues("Unknown Segment Type");
                return;
            }

            bool validInput = ValidateAndParseInput(out double diameter, out double diameterFinal, out double thickness, out double heightInitial, out double heightFinal);

            if (!validInput)
            {
                setTextboxvalues("");
                return;
            }

            if (_segmentType == "Cylinder")
            {
                CalculateCylinderValues(heightInitial, heightFinal, diameter, thickness);
            }
            else if (_segmentType == "Base")
            {
                CalculateBaseValues(heightInitial, heightFinal, diameter, diameterFinal, thickness);
            }
            else
            {
                setTextboxvalues("Unknown Segment Type");
            }
        }

        private bool ValidateAndParseInput(out double diameter, out double diameterFinal, out double thickness, out double heightInitial, out double heightFinal)
        {
            diameter = diameterFinal = thickness = heightInitial = heightFinal = 0.0;

            bool isCylinderValid = _segmentType == "Cylinder" &&
                                   double.TryParse(maskedTextBox2.Text, out diameter) &&
                                   double.TryParse(maskedTextBox3.Text, out thickness) &&
                                   double.TryParse(maskedTextBox1.Text, out heightInitial) &&
                                   double.TryParse(maskedTextBox4.Text, out heightFinal);

            bool isBaseValid = _segmentType == "Base" &&
                               double.TryParse(maskedTextBox2.Text, out diameter) &&
                               double.TryParse(maskedTextBox3.Text, out diameterFinal) &&
                               double.TryParse(maskedTextBox1.Text, out heightInitial) &&
                               double.TryParse(maskedTextBox4.Text, out heightFinal) &&
                               double.TryParse(maskedTextBox5.Text, out thickness);

            return isCylinderValid || isBaseValid;
        }

        private void CalculateCylinderValues(double heightInitial, double heightFinal, double diameter, double thickness)
        {
            Segment_Cylinder_Equations cylinder_Equations = new Segment_Cylinder_Equations();

            textBox1.Text = cylinder_Equations.weightOfPedestal(heightInitial, heightFinal, diameter, thickness).ToString();
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

        private void CalculateBaseValues(double heightInitial, double heightFinal, double diameterInitial, double diameterFinal, double thickness)
        {
            Segment_Conical_Equations conical_Equations = new Segment_Conical_Equations();

            textBox1.Text = conical_Equations.weight(heightInitial, heightFinal, diameterInitial, diameterFinal, thickness).ToString();
            textBox2.Text = conical_Equations.ProjectedArea(heightInitial, heightFinal, diameterInitial).ToString();
            textBox3.Text = conical_Equations.Centroid(heightInitial, heightFinal).ToString();
            textBox4.Text = conical_Equations.kzi(heightInitial).ToString();
            textBox5.Text = conical_Equations.kzf(heightFinal).ToString();
            textBox6.Text = conical_Equations.qzi(heightInitial).ToString();
            textBox7.Text = conical_Equations.qzf(heightFinal).ToString();
            textBox8.Text = conical_Equations.F(heightInitial, heightFinal, diameterInitial).ToString();
            textBox9.Text = conical_Equations.L(heightInitial, heightFinal).ToString();
            textBox10.Text = conical_Equations.Mbase(heightInitial, heightFinal, diameterInitial).ToString();
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





        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox5_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
