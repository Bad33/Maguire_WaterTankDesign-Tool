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
    public partial class Seismic : Form
    {
        public Seismic()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFaFvValues();
            Sd1_textchange();
            Sds_textChange();

        }

        private void setFaFvValues()
        {
            if (numericUpDown1 != null && numericUpDown2 != null)
            {
                double fvValue = SiteClassTable.GetFvValue(comboBox1.Text, double.Parse(numericUpDown1.Text));
                double faValue = SiteClassTable.GetFaValue(comboBox1.Text, double.Parse(numericUpDown2.Text));

                textBox4.Text = fvValue.ToString();
                textBox5.Text = faValue.ToString();
            }
        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            setFaFvValues();
            Sds_textChange();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            setFaFvValues();
            Sd1_textchange();

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Sds_textChange();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Sd1_textchange();
        }

        private void Sd1_textchange()
        {
            if (textBox4 != null && textBox4.Text != "NaN")
            {
                var Sm1 = double.Parse(textBox4.Text) * (double)numericUpDown1.Value;
                double Sd1 = Math.Round(((double)2 / 3) * Sm1, 4);

                textBox6.Text = Sd1.ToString();
            }
            else if (textBox4?.Text == "NaN")
            {
                textBox6.Text = "";
            }
        }

        private void Sds_textChange()
        {
            if (textBox5 != null && textBox5.Text != "NaN")
            {
                var Sms = double.Parse(textBox5.Text) * (double)numericUpDown2.Value;
                double Sds = Math.Round(((double)2 / 3) * Sms, 4);
                textBox7.Text = Sds.ToString();
                textBox9.Text = Sds.ToString();
            }
            else if (textBox5?.Text == "NaN")
            {
                textBox7.Text = "";
                textBox9.Text = "";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
