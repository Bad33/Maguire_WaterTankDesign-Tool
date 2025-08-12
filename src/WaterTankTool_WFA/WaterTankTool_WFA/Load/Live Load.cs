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

            var existing = _context.LiveLoadEntity.FirstOrDefault();
            if (existing != null)
            {
                textBox4.Text = existing.Live_Load.ToString();
                textBox1.Text = existing.Roof_Live_Load.ToString();
                textBox3.Text = existing.Design_Roof_Live_Load.ToString();
            }

            richTextBox1.Text = NotesManager.Notes.LiveLoadNotes ?? "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AreAllRequiredFieldsFilled())
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox4.Text, out double liveLoad) ||
                !double.TryParse(textBox1.Text, out double roofLiveLoad) ||
                !double.TryParse(textBox3.Text, out double designRoofLiveLoad))
            {
                MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var LiveLoad = new LiveLoadEntity
            {
                Live_Load = liveLoad,
                Roof_Live_Load = roofLiveLoad,
                Design_Roof_Live_Load = designRoofLiveLoad
            };

            AddOrUpdateLiveLoad(LiveLoad);
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);
            if(result == DialogResult.OK)
            {
                this.Close();
            }
        }

        public void AddOrUpdateLiveLoad(LiveLoadEntity LiveLoad)
        {
            var existingData = _context.LiveLoadEntity.FirstOrDefault();

            if (existingData == null)
            {
                _context.LiveLoadEntity.Add(LiveLoad);
            }
            else
            {
                existingData.Live_Load = LiveLoad.Live_Load;
                existingData.Roof_Live_Load = LiveLoad.Roof_Live_Load;
                existingData.Design_Roof_Live_Load = LiveLoad.Design_Roof_Live_Load;
            }

            _context.SaveChanges();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.LiveLoadNotes = richTextBox1.Text;
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
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (double.TryParse(textBox1.Text, out double roof) &&
                    double.TryParse(textBox2.Text, out double area))
                {
                    var result = Math.Round((roof * area) / 1000, 5);
                    textBox3.Text = result.ToString();
                }
                else
                {
                    textBox3.Text = string.Empty;
                }
            }
            else
            {
                textBox3.Text = string.Empty;
            }
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
                   IsTextBoxFilled(textBox4);
        }
    }
}
