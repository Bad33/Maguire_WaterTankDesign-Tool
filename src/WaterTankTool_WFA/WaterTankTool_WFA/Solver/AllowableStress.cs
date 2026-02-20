using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterTankTool_WFA.Solver
{
    public partial class AllowableStress : Form
    {
        public Label rtcLabel;                 // set in LoadAllowableCompressiveStress()
        public string Fy;


        public AllowableStress()
        {
            InitializeComponent();
            LoadAllowableCompressiveStress();
        }



        public void LoadAllowableCompressiveStress()
        {
            tableLayoutPanel1.Controls.Clear();

            // static labels
            string[] leftLabels = { "Fy", "K", "L", "(R/t)c", "E" };
            string[] rightLabels = { "psi", null, "in", null, "psi" };

            for (int i = 0; i < leftLabels.Length; i++)
                tableLayoutPanel1.Controls.Add(new Label { Text = leftLabels[i] }, 0, i);

            // Dropdown for Fy
            ComboBox fyBox = new ComboBox { Dock = DockStyle.Fill };
            fyBox.Items.AddRange(new[] { "30000", "32000", "34000", "36000", "38000", "40000" });
            fyBox.SelectedItem = AppState.Fy;

            tableLayoutPanel1.Controls.Add(fyBox, 1, 0);

            string kValue = "";

            if (AppState.CurrentTankType == TankType.SingleColumn)
            {
                kValue = "2";

            }
            else
                kValue = "1";


                // Fixed cells
            tableLayoutPanel1.Controls.Add(new Label { Text = kValue }, 1, 1);
            //tableLayoutPanel1.Controls.Add(new Label { Text = "2124" }, 1, 2);
            rtcLabel = new Label();
            tableLayoutPanel1.Controls.Add(rtcLabel, 1, 3);
            tableLayoutPanel1.Controls.Add(new Label { Text = "29000000" }, 1, 4);

            for (int i = 0; i < rightLabels.Length; i++)
                if (rightLabels[i] != null)
                    tableLayoutPanel1.Controls.Add(new Label { Text = rightLabels[i] }, 2, i);

            UpdateRtcLabel(AppState.Fy);

            fyBox.SelectedIndexChanged += (_, __) =>
            {
                var sel = fyBox.SelectedItem!.ToString()!;
                AppState.Fy = sel;                 // write back
                UpdateRtcLabel(sel);
                AppState.Rtc = int.Parse(rtcLabel.Text);

                // close the dialog to signal “OK”

            };
        }

        public void UpdateRtcLabel(string selectedFy)
        {
            rtcLabel.Text = selectedFy switch
            {
                "30000" => "420",
                "32000" => "377",
                "34000" => "354",
                "36000" => "334",
                "38000" => "316",
                "40000" => "299",
                _ => "0"
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
