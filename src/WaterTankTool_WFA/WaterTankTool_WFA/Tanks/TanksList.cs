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
    public partial class TanksList : Form
    {
        public TanksList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TankProperty tankProperty = new TankProperty();
            tankProperty.ShowDialog();
        }
    }
}
