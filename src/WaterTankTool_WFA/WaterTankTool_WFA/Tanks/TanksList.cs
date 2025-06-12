using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Tanks;

namespace WaterTankTool_WFA
{
    public partial class TanksList : Form
    {
        String _selectedTankCapacity;
        TankData tankData = new TankData();
        TankDataDimensions dimensions = new TankDataDimensions();
        WaterTank _waterTankForm;
        public TanksList(WaterTank waterTankForm)
        {
            GetTanksJsonData();
            _waterTankForm = waterTankForm;
            InitializeComponent();
        }

        private void GetTanksJsonData()
        {
            try
            {
                string jsonStringPath = Path.Combine(Application.StartupPath, "tanks.json");
                if (!File.Exists(jsonStringPath))
                {
                    MessageBox.Show("Tanks File not Found");
                }
                string jsonString = File.ReadAllText(jsonStringPath);
                tankData = JsonSerializer.Deserialize<TankData>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dimensions != null)
            {

                TankProperty tankProperty = new TankProperty(dimensions,_waterTankForm);
                
                DialogResult result = tankProperty.ShowDialog();

                if (result == DialogResult.OK || result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedTankCapacity = comboBox1.SelectedItem.ToString();

            if (tankData?.Tanks != null)
            {
                dimensions = tankData.Tanks.FirstOrDefault(data => data.Type == _selectedTankCapacity);
            }
            else
            {
                Console.WriteLine("No tanks found in the JSON file.");
            }
        }

        private void TanksList_Load(object sender, EventArgs e)
        {

        }
    }
}
