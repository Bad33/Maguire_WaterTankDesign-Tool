using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public partial class Define_Materials : Form
    {
        private WaterTankDbContext _context;
        public Define_Materials()
        {
            InitializeComponent();
            _context = new WaterTankDbContext();
            MaterialListView();
        }

        public void MaterialListView()
        {
            if(_context.MaterialProperties != null)
            {
                foreach (var property in _context.MaterialProperties)
                {
                    listBox1.Items.Add(property.MaterialName);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Material_Property_Data dialog = new Material_Property_Data();

            dialog.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
