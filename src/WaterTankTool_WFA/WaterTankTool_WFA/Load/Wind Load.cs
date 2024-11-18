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

namespace WaterTankTool_WFA.Load
{
    public partial class Wind_Load : Form
    {

        
        public Wind_Load()
        {
            InitializeComponent();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text !=null && textBox2.Text != null && textBox3.Text != null && textBox4.Text != null && textBox5.Text != null && textBox6.Text != null && textBox7.Text != null && textBox8.Text != null)
            {
                var Kzt = Double.Parse(textBox1.Text);
                var Kd = Double.Parse(textBox3.Text);
                var I = Double.Parse(textBox5.Text);
                var V = Double.Parse(textBox6.Text);

                var Q = Lambda.Y * 0.000256 * Kzt * Kd * I * V;
                richTextBox2.Text = "q = " + Q.ToString();
            } 
        }
    }
}
