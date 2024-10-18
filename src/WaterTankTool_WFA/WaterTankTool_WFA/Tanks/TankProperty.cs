using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Tanks;

namespace WaterTankTool_WFA
{
    public partial class TankProperty : Form
    {
        TankDataDimensions _properties = new TankDataDimensions();

        public TankProperty(TankDataDimensions properties)
        {
            _properties = properties;
            InitializeComponent();
            FillTextBoxValues();
        }

        private void FillTextBoxValues()
        {
            textBox4.Text = _properties.Type.ToString();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
