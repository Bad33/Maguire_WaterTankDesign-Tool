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

            LoadInputBox();
            FillTextBox();
            FillExposure();

            TotalSnowLoad();
            richTextBox1.Text = NotesManager.Notes.SnowLoadNotes ?? "";
        }

        private void FillTextBox()
        {
            if (comboBox1?.Text == "II")
                textBox5.Text = SnowRiskCategoryII.Is.ToString();
            else if (comboBox1?.Text == "III")
                textBox5.Text = SnowRiskCategoryIII.Is.ToString();
            else if (comboBox1?.Text == "IV")
                textBox5.Text = SnowRiskCategoryIV.Is.ToString();
        }

        private void TotalSnowLoad()
        {
            if (IsTextBoxFilled(textBox1) && IsTextBoxFilled(textBox2) &&
                IsTextBoxFilled(textBox5) && IsTextBoxFilled(textBox6) && IsTextBoxFilled(textBox7))
            {
                if (double.TryParse(textBox1.Text, out double t1) &&
                    double.TryParse(textBox2.Text, out double t2) &&
                    double.TryParse(textBox5.Text, out double t5) &&
                    double.TryParse(textBox6.Text, out double t6) &&
                    double.TryParse(textBox7.Text, out double t7))
                {
                    var calc = Math.Round(((0.7 * t1 * t2 * t5 * t6 * t7) / 1000), 4);
                    textBox3.Text = calc.ToString();
                }
                else
                {
                    textBox3.Text = "";
                }
            }
        }

        private void FillExposure()
        {
            if (comboBox2?.Text == "C")
                textBox6.Text = SnowExposureC.Ce.ToString();
            else if (comboBox2?.Text == "D")
                textBox6.Text = SnowExposureD.Ce.ToString();
        }

        private void LoadInputBox()
        {
            var snowData = _context.SnowLoadEntity.FirstOrDefault();
            if (snowData != null)
            {
                textBox4.Text = snowData.HeightToConsider.ToString();
                textBox1.Text = snowData.GroundSnowLoad.ToString();
                comboBox1.Text = snowData.RiskCategory ?? "";
                textBox5.Text = snowData.ImportanceFactor.ToString();
                comboBox2.Text = snowData.Exposure ?? "";
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
            TotalSnowLoad();
        }

        private void TotalLoad()
        {
            if (IsTextBoxFilled(textBox1) && IsTextBoxFilled(textBox2))
            {
                if (double.TryParse(textBox1.Text, out double load) &&
                    double.TryParse(textBox2.Text, out double area))
                {
                    var total = (load * area) / 1000;
                    textBox3.Text = Math.Round(total, 4).ToString();
                }
                else
                {
                    textBox3.Text = "";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TotalLoad();
            TotalSnowLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AreAllRequiredFieldsFilled())
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox1.Text, out double live_Load) ||
                !double.TryParse(textBox2.Text, out double area) ||
                !double.TryParse(textBox3.Text, out double total) ||
                !double.TryParse(textBox4.Text, out double heightToConsider) ||
                !double.TryParse(textBox5.Text, out double importanceFactor) ||
                !double.TryParse(textBox6.Text, out double exposureFactor) ||
                !double.TryParse(textBox7.Text, out double areaSubjectedToSnow))
            {
                MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var snowLoad = new SnowLoadEntity
            {
                HeightToConsider = heightToConsider,
                GroundSnowLoad = live_Load,
                RiskCategory = comboBox1.Text,
                ImportanceFactor = importanceFactor,
                Exposure = comboBox2.Text,
                ExposureFactor = exposureFactor,
                AreaSubjectedToSnow = areaSubjectedToSnow,
                TotalSnowLoad = total
            };

            AddOrUpdateSnowLoad(snowLoad);
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);

            if(result == DialogResult.OK)
            {
                this.Close();
            }
        }

        public void AddOrUpdateSnowLoad(SnowLoadEntity snowLoad)
        {
            var existingData = _context.SnowLoadEntity.FirstOrDefault();

            if (existingData == null)
            {
                _context.SnowLoadEntity.Add(snowLoad);
            }
            else
            {
                existingData.HeightToConsider = snowLoad.HeightToConsider;
                existingData.GroundSnowLoad = snowLoad.GroundSnowLoad;
                existingData.RiskCategory = snowLoad.RiskCategory;
                existingData.ImportanceFactor = snowLoad.ImportanceFactor;
                existingData.Exposure = snowLoad.Exposure;
                existingData.ExposureFactor = snowLoad.ExposureFactor;
                existingData.AreaSubjectedToSnow = snowLoad.AreaSubjectedToSnow;
                existingData.TotalSnowLoad = snowLoad.TotalSnowLoad;
            }

            _context.SaveChanges();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.SnowLoadNotes = richTextBox1.Text;
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
            if (IsTextBoxFilled(textBox4))
            {
                var gg = _context.SegmentProperties.Where(z => z.SegmentType == "Tanks").ToList();
                if (gg != null && gg.Count > 0)
                {
                    var diameter = gg[0].Diameter;
                    if (double.TryParse(textBox4.Text, out double height))
                    {
                        var area = Math.Round((Math.Round(Math.PI, 4) * diameter * height), 4);
                        textBox7.Text = area.ToString();
                    }
                }
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

        // Helper: Check if a textbox is filled (not null, not empty, not whitespace)
        private bool IsTextBoxFilled(System.Windows.Forms.TextBox tb) =>
            tb != null && !string.IsNullOrWhiteSpace(tb.Text);

        // Helper: All required fields filled?
        private bool AreAllRequiredFieldsFilled()
        {
            return IsTextBoxFilled(textBox1) &&
                   IsTextBoxFilled(textBox2) &&
                   IsTextBoxFilled(textBox3) &&
                   IsTextBoxFilled(textBox4) &&
                   IsTextBoxFilled(textBox5) &&
                   IsTextBoxFilled(textBox6) &&
                   IsTextBoxFilled(textBox7);
        }
    }
}
