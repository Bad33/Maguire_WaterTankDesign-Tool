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

namespace WaterTankTool_WFA.Load
{

    public partial class Wind_Load : Form
    {
        private WaterTankDbContext _context;
        private Double Q;

        public Wind_Load()
        {
            InitializeComponent();
            var context = WaterTankDbContext.GetInstance();
            _context = context;

            FillTextBox();
            FillCalculatedValue();
        }

        private void FillTextBox()
        {
            if (comboBox1.Text != null && comboBox1.Text == "C")
            {
                textBox7.Text = WindLoadExposure_C.Zg.ToString();
                textBox8.Text = WindLoadExposure_C.Alpha.ToString();
            }
            else if (comboBox1.Text != null && comboBox1.Text == "D")
            {
                textBox7.Text = WindLoadExposure_D.Zg.ToString();
                textBox8.Text = WindLoadExposure_D.Alpha.ToString();
            }
        }

        private void FillCalculatedValue()
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty && textBox6.Text != string.Empty && textBox7.Text != string.Empty && textBox8.Text != string.Empty)
            {
                var Kzt = Double.Parse(textBox1.Text);
                var Kd = Double.Parse(textBox3.Text);
                var I = Double.Parse(textBox5.Text);
                var V = Double.Parse(textBox6.Text);

                Q = Lambda.Y * 0.00256 * Kzt * Kd * I * Math.Pow(V, 2);
                richTextBox2.Text = "q = " + Q.ToString("F5");
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTextBox();
            FillCalculatedValue();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var windLoad = new WindLoadEntity
            {
                Exposure = comboBox1.Text,
                Kzt = double.Parse(textBox1.Text),
                Ke = double.Parse(textBox2.Text),
                Kd = double.Parse(textBox3.Text),
                G = double.Parse(textBox4.Text),
                I = double.Parse(textBox5.Text),
                V = double.Parse(textBox6.Text),
                Zg = double.Parse(textBox7.Text),
                alpha = double.Parse(textBox8.Text),
                lambda = double.Parse(textBox9.Text),
                Cf = double.Parse(textBox11.Text),
                Q = Q
            };


            AddOrUpdateWindLoad(windLoad);
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);

        }

        public void AddOrUpdateWindLoad(WindLoadEntity windLoad)
        {
            //Check if any WindLoadEntity data already exists in the table
            var existingData = _context.WindLoadEntity.FirstOrDefault();

            if (existingData == null)
            {
                // If no data exists, add the new WindLoadEntity to the table
                _context.WindLoadEntity.Add(windLoad);
            }
            else
            {
                // If data exists, update the existing data with new values
                existingData.Exposure = windLoad.Exposure;
                existingData.Kzt = windLoad.Kzt;
                existingData.Ke = windLoad.Ke;
                existingData.Kd = windLoad.Kd;
                existingData.G = windLoad.G;
                existingData.I = windLoad.I;
                existingData.V = windLoad.V;
                existingData.Zg = windLoad.Zg;
                existingData.alpha = windLoad.alpha;
                existingData.lambda = windLoad.lambda;
                existingData.Cf = windLoad.Cf;
                existingData.Q = windLoad.Q;
            }

            // Save changes to the database
            _context.SaveChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && textBox3.Text != null && textBox4.Text != null && textBox5.Text != null && textBox6.Text != null && textBox7.Text != null && textBox8.Text != null)
            {
                var Kzt = Double.Parse(textBox1.Text);
                var Kd = Double.Parse(textBox3.Text);
                var I = Double.Parse(textBox5.Text);
                var V = Double.Parse(textBox6.Text);

                Q = Lambda.Y * 0.00256 * Kzt * Kd * I * Math.Pow(V, 2);
                richTextBox2.Text = "q = " + Q.ToString("F5");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            FillCalculatedValue();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillCalculatedValue();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FillCalculatedValue();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            FillCalculatedValue();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            FillCalculatedValue();

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            FillCalculatedValue();

        }
    }
}
