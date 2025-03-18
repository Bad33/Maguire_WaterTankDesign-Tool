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

namespace WaterTankTool_WFA.Load
{
    public partial class Dead_Load : Form
    {
        public Dead_Load()
        {
            InitializeComponent();

            richTextBox1.Text = NotesManager.Notes.DeadLoadNotes ?? "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            NotesManager.Notes.DeadLoadNotes = richTextBox1.Text;


            NotesManager.SaveNotes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Data saved successfully!", "Confirmation", MessageBoxButtons.OK);

        }
    }
}
