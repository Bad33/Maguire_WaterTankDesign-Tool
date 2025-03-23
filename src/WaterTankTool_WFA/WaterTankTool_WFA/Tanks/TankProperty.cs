using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Tanks;

namespace WaterTankTool_WFA
{
    public partial class TankProperty : Form
    {
        TankDataDimensions _properties = new TankDataDimensions();
        private WaterTank _waterTankForm;
        private WaterTankDbContext _context;


        public TankProperty(TankDataDimensions properties, WaterTank waterTank)
        {
            _properties = properties;
            _waterTankForm = waterTank;

            InitializeComponent();

            var context = WaterTankDbContext.GetInstance();

            _context = context;
            FillTextBoxValues();
        }

        private void FillTextBoxValues()
        {
            textBox1.Text = _properties.Type.ToString();
            textBox4.Text = _properties.Type.ToString();
            textBox7.Text = _properties.Total_Weight.ToString();
            textBox6.Text = _properties.Weight_of_Steel.ToString();
            textBox5.Text = _properties.Weight_of_Water.ToString();
            textBox8.Text = _properties.Projected_Area.ToString();
            textBox9.Text = _properties.Centroid.ToString();

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = _properties.Type.ToString();
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

        public bool SaveTankProperties()
        {
            //using (var context = WaterTankDbContext.GetInstance())
            //{
                TankProperties properties = new TankProperties()
                {
                    Capacity = textBox4.Text,
                    WeightOfWater = textBox5.Text,
                    WeightOfSteel = textBox6.Text,
                    TotalWeight = textBox7.Text,
                    ProjectedArea = textBox8.Text,
                    Centroid = textBox9.Text
                };

                _context.TankProperties.Add(properties);


                try
                {
                    int rowsAffected = _context.SaveChanges();

                    return true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving Tank Properties data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                 string.IsNullOrWhiteSpace(textBox2.Text) ||
                 string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!SaveTankProperties())
            {
                return;
            }

            string diameterS = Regex.Match(_properties.Diameter, @"\d+(\.\d+)?").Value;
            string thichnessS = Regex.Match(_properties.Thickness, @"\d+(\.\d+)?").Value;

            double diameter = double.Parse(diameterS);
            double thickness = double.Parse(thichnessS);

            // Try parsing numeric fields for Diameter, Thickness, and Heights
            if (
                !double.TryParse(textBox2.Text, out double heightInitial) ||
                !double.TryParse(textBox3.Text, out double heightFinal))
            {
                MessageBox.Show("Please enter valid numbers for Diameter, Thickness, and Heights.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //using (var context = WaterTankDbContext.GetInstance())
            //{
                SegmentProperties segmentProperties;

                if (_context.SegmentProperties.ToList().Any(x => x.SegmentType == "Tanks"))
                {
                    MessageBox.Show("You Have to delete the existing tank to add new one.Alternatively you can modify the existing tank", "Error");
                    return;
                }
                else
                {
                    segmentProperties = new SegmentProperties()
                    {
                        SegmentName = textBox1.Text,
                        SegmentType = "Tanks",
                        Diameter = diameter,
                        Thickness = thickness,
                        HeightInitial = heightInitial,
                        HeightFinal = heightFinal
                    };

                    _context.SegmentProperties.Add(segmentProperties);


                    try
                    {
                        int rowsAffected = _context.SaveChanges();
                        successDialog(rowsAffected);
                        _waterTankForm.OnSegmentAdded();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            //}
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
