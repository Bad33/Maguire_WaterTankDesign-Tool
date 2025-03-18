using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Designer_Notes;

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
            ShowInputField();
            richTextBox1.Text = NotesManager.Notes.SnowLoadNotes ?? "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TotalLoad();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TotalLoad();
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

        }

        private void ShowInputField()
        {
            var existingData = _context.SnowLoadEntity.FirstOrDefault();
            if (existingData != null)
            {
                textBox1.Text = existingData.Snow_Pressure.ToString();
                textBox2.Text = existingData.Area_Subjected.ToString();
                textBox3.Text = existingData.Total_Load.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
             string.IsNullOrWhiteSpace(textBox2.Text) ||
             string.IsNullOrWhiteSpace(textBox3.Text))
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
                Snow_Pressure = double.Parse(textBox1.Text),
                Area_Subjected = double.Parse(textBox2.Text),
                Total_Load = double.Parse(textBox3.Text),

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
                existingData.Snow_Pressure = snowLoad.Snow_Pressure;
                existingData.Area_Subjected = snowLoad.Area_Subjected;
                existingData.Total_Load = snowLoad.Total_Load;

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
    }
}
