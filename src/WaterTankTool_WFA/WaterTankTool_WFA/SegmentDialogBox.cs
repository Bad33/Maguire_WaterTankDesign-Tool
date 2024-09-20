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
        public SegmentDialogBox()
        {
            InitializeComponent();
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

            using (var context = new WaterTankDbContext()) 
            {
                var segmentProperties = new SegmentProperties()
                {
                    SegmentName = richTextBox1.Text,
                    SegmentType = comboBox1.Text,
                    Diameter = Double.Parse(maskedTextBox2.Text),
                    Thickness = Double.Parse(maskedTextBox3.Text),
                    HeightInitial = Double.Parse(maskedTextBox1.Text),
                    HeightFinal = Double.Parse(maskedTextBox4.Text)
                };

                context.SegmentProperties.Add(segmentProperties);
                int rowsAffected = context.SaveChanges();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data saved successfully!");
                }
                else
                {
                    MessageBox.Show("Data might not have been saved.");
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
