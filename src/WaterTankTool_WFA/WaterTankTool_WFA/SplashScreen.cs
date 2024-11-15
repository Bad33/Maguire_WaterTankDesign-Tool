using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public partial class SplashScreen : Form
    {
        private CircularProgressBar circularProgressBar;
        private System.Windows.Forms.Timer timer;
        private int progress = 0;

        public SplashScreen()
        {
            this.BackColor = Color.FromArgb(40, 40, 40); // Modern dark gray background
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 300);

            // Add rounded corners to the form
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, 20, 20, 180, 90);
                path.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
                path.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
                path.AddArc(0, this.Height - 20, 20, 20, 90, 90);
                path.CloseAllFigures();
                this.Region = new Region(path);
            }

            // Add gradient background to the form
            this.Paint += (s, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                    Color.FromArgb(40, 40, 40), Color.FromArgb(60, 60, 60), LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            };

            // Initialize and add the custom circular progress bar
            circularProgressBar = new CircularProgressBar
            {
                Location = new Point((this.Width - 150) / 2, 80),
                Size = new Size(150, 150),
                ProgressValue = 0,
                MaxValue = 100
            };
            this.Controls.Add(circularProgressBar);

            // Timer for simulating progress
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50; // Adjust for desired speed
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progress < 100)
            {
                progress++;
                circularProgressBar.ProgressValue = progress;
            }
            else
            {
                timer.Stop();
                this.Close(); // Close the splash screen when done
            }
        }
    }
}
