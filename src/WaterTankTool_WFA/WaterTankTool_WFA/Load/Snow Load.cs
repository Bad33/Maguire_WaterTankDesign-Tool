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
using WaterTankTool_WFA.Designer_Notes;
using WaterTankTool_WFA.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WaterTankTool_WFA.Load
{
    public partial class Snow_Load : Form
    {
        private WaterTankDbContext _context;

        public Snow_Load()
        {
            InitializeComponent();
            var context = WaterTankDbContext.GetInstance();
            _context = context;
            //ShowInputField();

            LoadInputBox();
            FillTextBox();
            FillExposure();

            TotalSnowLoad();
            richTextBox1.Text = NotesManager.Notes.SnowLoadNotes ?? "";
        }


        private void FillTextBox()
        {
            if (comboBox1.Text != null && comboBox1.Text == "II")
            {
                textBox5.Text = SnowRiskCategoryII.Is.ToString();

            }
            else if (comboBox1.Text != null && comboBox1.Text == "III")
            {
                textBox5.Text = SnowRiskCategoryIII.Is.ToString();

            }
            else if (comboBox1.Text != null && comboBox1.Text == "IV")
            {
                textBox5.Text = SnowRiskCategoryIV.Is.ToString();

            }
        }

        private void TotalSnowLoad()
        {
            if (textBox1 != null && textBox1.Text != "" && textBox2 != null && textBox2.Text != "" && textBox5 != null && textBox5.Text != "" && textBox6 != null && textBox6.Text != "" && textBox7 != null && textBox7.Text != "")
            {

                var calc = Math.Round(((0.7 * Double.Parse(textBox1.Text) * Double.Parse(textBox2.Text) * Double.Parse(textBox5.Text) * Double.Parse(textBox6.Text) * Double.Parse(textBox7.Text))/1000), 4);

                textBox3.Text = calc.ToString();
            }
        }

        private void FillExposure()
        {
            if (comboBox2.Text != null && comboBox2.Text == "C")
            {
                textBox6.Text = SnowExposureC.Ce.ToString();

            }
            else if (comboBox2.Text != null && comboBox2.Text == "D")
            {
                textBox6.Text = SnowExposureD.Ce.ToString();


            }
        }

        private void LoadInputBox()
        {

            var snowData = _context.SnowLoadEntity.FirstOrDefault();
            if (snowData != null)
            {
                textBox4.Text = snowData.HeightToConsider.ToString();
                textBox1.Text = snowData.GroundSnowLoad.ToString();
                comboBox1.Text = snowData.RiskCategory.ToString();
                textBox5.Text = snowData.ImportanceFactor.ToString();
                comboBox2.Text = snowData.Exposure.ToString();
                textBox6.Text = snowData.ExposureFactor.ToString();
                textBox7.Text = snowData.AreaSubjectedToSnow.ToString();
                textBox3.Text = snowData.TotalSnowLoad.ToString();


            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TotalLoad();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1 != null && textBox1.Text != "" && textBox2 != null && textBox2.Text != ""  && textBox5 != null && textBox5.Text != "" && textBox6 != null && textBox6.Text != "" && textBox7 != null && textBox7.Text != "")
            {

                var calc = Math.Round(((0.7 * Double.Parse(textBox1.Text) * Double.Parse(textBox2.Text) * Double.Parse(textBox5.Text) * Double.Parse(textBox6.Text) * Double.Parse(textBox7.Text))/1000), 4);

                textBox3.Text = calc.ToString();
            }

        }

        private void TotalLoad()
        {
            if (textBox1 != null && textBox1.Text != "" && textBox2 != null && textBox2.Text != "")
            {
                var load = double.Parse(textBox1.Text);
                var area = double.Parse(textBox2.Text);
                var total = (load * area) / 1000;
                textBox3.Text = Math.Round(total, 4).ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TotalLoad();
            TotalSnowLoad();

        }

        private void ShowInputField()
        {
            //var existingData = _context.SnowLoadEntity.FirstOrDefault();
            //if (existingData != null)
            //{
            //    textBox1.Text = existingData.Snow_Pressure.ToString();
            //    textBox2.Text = existingData.Area_Subjected.ToString();
            //    textBox3.Text = existingData.Total_Load.ToString();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
             string.IsNullOrWhiteSpace(textBox2.Text) ||
             string.IsNullOrWhiteSpace(textBox3.Text) ||
             string.IsNullOrWhiteSpace(textBox4.Text) ||
             string.IsNullOrWhiteSpace(textBox5.Text)
              ||
             string.IsNullOrWhiteSpace(textBox6.Text)
              ||
             string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox1.Text, out double live_Load) ||
                !double.TryParse(textBox2.Text, out double area) ||
                !double.TryParse(textBox3.Text, out double total))
            {
                MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var snowLoad = new SnowLoadEntity
            {
                HeightToConsider = double.Parse(textBox4.Text),
                GroundSnowLoad = double.Parse(textBox1.Text),
                RiskCategory = comboBox1.Text,
                ImportanceFactor = double.Parse(textBox5.Text),
                Exposure = comboBox2.Text,
                ExposureFactor = double.Parse(textBox6.Text),
                AreaSubjectedToSnow = double.Parse(textBox7.Text),
                TotalSnowLoad = double.Parse(textBox3.Text)

            };


            AddOrUpdateSnowLoad(snowLoad);
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);
        }

        public void AddOrUpdateSnowLoad(SnowLoadEntity snowLoad)
        {
            //Check if any WindLoadEntity data already exists in the table
            var existingData = _context.SnowLoadEntity.FirstOrDefault();

            if (existingData == null)
            {
                // If no data exists, add the new WindLoadEntity to the table
                _context.SnowLoadEntity.Add(snowLoad);
            }
            else
            {
                // If data exists, update the existing data with new values
                existingData.HeightToConsider = snowLoad.HeightToConsider;
                existingData.GroundSnowLoad = snowLoad.GroundSnowLoad;
                existingData.RiskCategory = snowLoad.RiskCategory;
                existingData.ImportanceFactor = snowLoad.ImportanceFactor;
                existingData.Exposure = snowLoad.Exposure;
                existingData.ExposureFactor = snowLoad.ExposureFactor;
                existingData.AreaSubjectedToSnow = snowLoad.AreaSubjectedToSnow;
                existingData.TotalSnowLoad = snowLoad.TotalSnowLoad;
         

            }

            // Save changes to the database
            _context.SaveChanges();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.SnowLoadNotes = richTextBox1.Text;

            // Immediately save changes to the single JSON file
            NotesManager.SaveNotes();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTextBox();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExposure();

        }


        private void textBox4_TextChanged_2(object sender, EventArgs e)
        {
            if (textBox4.Text != string.Empty)
            {
                var gg = _context.SegmentProperties.Where(z => z.SegmentType == "Tanks").ToList();
                var diameter = gg[0].Diameter;

                var height = double.Parse(textBox4.Text);

                var area = Math.Round((Math.Round(Math.PI, 4) * diameter * height), 4);

                textBox7.Text = area.ToString();
            }
            TotalSnowLoad();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TotalSnowLoad();

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TotalSnowLoad();

        }
    }
}
