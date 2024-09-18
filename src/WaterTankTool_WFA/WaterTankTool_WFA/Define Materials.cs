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
        public Define_Materials()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Material_Property_Data dialog = new Material_Property_Data();

            dialog.ShowDialog();
        }
    }
}
