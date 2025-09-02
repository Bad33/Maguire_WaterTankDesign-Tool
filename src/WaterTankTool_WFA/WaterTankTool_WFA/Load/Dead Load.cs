using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Designer_Notes;
using WaterTankTool_WFA.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WaterTankTool_WFA.Load
{
    public partial class Dead_Load : Form
    {

        private WaterTankDbContext _context;

        public Dead_Load()
        {
            InitializeComponent();
            var context = WaterTankDbContext.GetInstance();
            _context = context;

            var existingData = _context.DeadLoadEntity.FirstOrDefault();
            if(existingData != null)
            {
                textBox2.Text = existingData.Miscellaneous_Load.ToString();
            }
            richTextBox1.Text = NotesManager.Notes.DeadLoadNotes ?? "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.DeadLoadNotes = richTextBox1.Text;


            NotesManager.SaveNotes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var deadload = new DeadLoadEntity
            {
                Miscellaneous_Load = double.Parse(textBox2.Text)
            };
            AddOrUpdateDeadLoad(deadload);
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);
            if(result == DialogResult.OK)
            {
                this.Close();
            }

        }

        public void AddOrUpdateDeadLoad(DeadLoadEntity DeadLoad)
        {
            var existingData = _context.DeadLoadEntity.FirstOrDefault();

            if (existingData == null)
            {
                _context.DeadLoadEntity.Add(DeadLoad);
            }
            else
            {
                existingData.Miscellaneous_Load = Double.Parse(textBox2.Text);
            }

            _context.SaveChanges();
        }
    }
}
