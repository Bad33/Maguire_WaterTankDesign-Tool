using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    // Custom modern progress bar control
    public class ModernProgressBar : Control
    {
        private System.Windows.Forms.Timer timer;
        private int offset;
        private int barWidth = 40; // width of the moving indicator

        public ModernProgressBar()
        {
            this.DoubleBuffered = true;
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 30; // adjust speed (ms)
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
            this.offset = -barWidth;
            this.Height = 10; // modern thin bar
            this.BackColor = Color.Snow;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            offset += 5;
            if (offset > this.Width)
                offset = -barWidth;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw background bar
            Rectangle bgRect = this.ClientRectangle;
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(240, 240, 240))) // subtle light gray
            {
                e.Graphics.FillRectangle(bgBrush, bgRect);
            }

            // Draw the moving indicator with a gradient effect
            Rectangle movingRect = new Rectangle(offset, 0, barWidth, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(movingRect, Color.LightGreen, Color.ForestGreen, LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, movingRect);
            }
        }
    }

    public partial class LoadingWindow : Form
    {
        public LoadingWindow()
        {
            InitializeComponent();
            InitFunc();
            this.Load += LoadingForm_Load;
        }

        private void InitFunc()
        {
            // Set the form to a modern, reduced size (approximately 25% smaller than original)
            this.ClientSize = new Size(190, 60);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            // Container panel (optional for additional styling)
            Panel container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(container);

            // "Loading..." label with modern styling
            Label labelLoading = new Label();
            labelLoading.Font = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Point);
            labelLoading.ForeColor = Color.Gray;
            labelLoading.Text = "Loading...";
            labelLoading.TextAlign = ContentAlignment.MiddleCenter;
            labelLoading.Dock = DockStyle.Top;
            labelLoading.Height = 40;
            labelLoading.Padding = new Padding(0, 10, 0, 0);

            // Modern progress bar (custom control)
            ModernProgressBar progressBar = new ModernProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 10,
                Margin = new Padding(20)
            };

            // Optional separator (a thin line) between the label and progress bar
            Panel separator = new Panel
            {
                Height = 1,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray
            };

            // Add controls to the container panel
            container.Controls.Add(progressBar);
            container.Controls.Add(separator);
            container.Controls.Add(labelLoading);
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            // Apply rounded corners for a refined, modern look
            SetRoundedRegion(12);
        }

        private void SetRoundedRegion(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            // Create a rounded rectangle path
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }
    }
}
