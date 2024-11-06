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
    }
}
