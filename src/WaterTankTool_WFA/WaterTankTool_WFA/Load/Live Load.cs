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
    public partial class Live_Load : Form
    {
        private WaterTankDbContext _context;


        public Live_Load()
        {
            InitializeComponent();
            var context = WaterTankDbContext.GetInstance();
            _context = context;

            var snowLoad = _context.SnowLoadEntity.FirstOrDefault();
            if (snowLoad != null)
                textBox2.Text = snowLoad.AreaSubjectedToSnow.ToString();

            // 2) Now load any existing LiveLoadEntity and populate the fields
            var existing = _context.LiveLoadEntity.FirstOrDefault();
            if (existing != null)
            {
                // Live load input
                textBox4.Text = existing.Live_Load.ToString();

                // Roof live load input
                textBox1.Text = existing.Roof_Live_Load.ToString();

                // Design roof live load (you could also let your TextChanged event recalc,
                // but to keep exactly what was saved, assign it directly)
                textBox3.Text = existing.Design_Roof_Live_Load.ToString();
            }

            richTextBox1.Text = NotesManager.Notes.LiveLoadNotes ?? "";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            var LiveLoad = new LiveLoadEntity
            {
                Live_Load = Double.Parse(textBox4.Text),
                Roof_Live_Load = Double.Parse(textBox1.Text),
                Design_Roof_Live_Load = Double.Parse(textBox3.Text)
            };

            AddOrUpdateLiveLoad(LiveLoad);
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);
        }

        public void AddOrUpdateLiveLoad(LiveLoadEntity LiveLoad)
        {
            //Check if any WindLoadEntity data already exists in the table
            var existingData = _context.LiveLoadEntity.FirstOrDefault();

            if (existingData == null)
            {
                // If no data exists, add the new WindLoadEntity to the table
                _context.LiveLoadEntity.Add(LiveLoad);
            }
            else
            {
                // If data exists, update the existing data with new values
                existingData.Live_Load = Double.Parse(textBox4.Text);

            }

            // Save changes to the database
            _context.SaveChanges();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.LiveLoadNotes = richTextBox1.Text;

            // Immediately save changes to the single JSON file
            NotesManager.SaveNotes();

        }

        private void Live_Load_Load(object sender, EventArgs e)
        {
            var snowLoad = _context.SnowLoadEntity.FirstOrDefault();

            if (snowLoad != null)
            {
                textBox2.Text = snowLoad.AreaSubjectedToSnow.ToString();

            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(textBox1.Text)))
            {
                var roof = double.Parse(textBox1.Text);
                var area = double.Parse(textBox2.Text);

                var result = Math.Round((roof * area) / 1000, 5);

                textBox3.Text = result.ToString();

            }
            else
            {
                textBox3.Text = string.Empty;
            }
        }
    }
}
