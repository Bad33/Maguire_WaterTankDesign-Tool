using System;
using System.Drawing;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public partial class TankTypeSelectionForm : Form
    {
        public TankType SelectedTankType { get; private set; } = TankType.None;

        public TankTypeSelectionForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Select Water Tank Type";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(760, 400);
            this.BackColor = ColorTranslator.FromHtml("#55959e");
           
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;

            var mainPanel = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, 0, 32),
                BackColor = Color.Transparent,
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            this.Controls.Add(mainPanel);

            var title = new Label
            {
                Text = "Select Water Tank Type",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(title, 0, 0);

            var cardsPanel = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Padding = new Padding(60, 18, 60, 0),
                BackColor = Color.Transparent
            };
            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainPanel.Controls.Add(cardsPanel, 0, 1);

            // Card 1: Single Column
            var cardSingle = new TankTypeCard
            {
                Label = "Single Column",
                CardImage = Properties.Resources.Sheldon_IA_New_Tank_Paint_2,
                CardTankType = TankType.SingleColumn,
                Margin = new Padding(26, 8, 26, 8),
            };
            cardSingle.CardClick += (s, e) =>
            {
                AppState.CurrentTankType = TankType.SingleColumn;   // ① set global
                SelectedTankType = TankType.SingleColumn;   // ② keep local return value
                this.DialogResult = DialogResult.OK;
            };
            cardsPanel.Controls.Add(cardSingle, 0, 0);

            // Card 2: Multi Column
            var cardMulti = new TankTypeCard
            {
                Label = "Multi-Column",
                CardImage = Properties.Resources.Katy,
                CardTankType = TankType.MultiColumn,
                Margin = new Padding(26, 8, 26, 8),
            };
            cardMulti.CardClick += (s, e) =>
            {
                AppState.CurrentTankType = TankType.MultiColumn;    // ①
                SelectedTankType = TankType.MultiColumn;    // ②
                this.DialogResult = DialogResult.OK;
            };
            cardsPanel.Controls.Add(cardMulti, 1, 0);

            // Footer
            var footer = new Label
            {
                Text = "© 2024 SDSU - Iron Maguire",
                Dock = DockStyle.Bottom,
                Height = 26,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(footer);
            footer.BringToFront();
        }
    }
}
