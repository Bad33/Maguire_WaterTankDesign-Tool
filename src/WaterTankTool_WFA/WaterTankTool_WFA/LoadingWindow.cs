using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public class ModernProgressBar : Control
    {
        private System.Windows.Forms.Timer timer;
        private int offset;
        private int barWidth = 60; // Wider moving bar for modern look

        public ModernProgressBar()
        {
            this.DoubleBuffered = true;
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 20; // Faster animation for smoothness
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
            this.offset = -barWidth;
            this.Height = 12; // Slightly thicker bar
            this.BackColor = Color.Coral;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            offset += 8;
            if (offset > this.Width)
                offset = -barWidth;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw modern glassy background
            Rectangle bgRect = this.ClientRectangle;
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(bgRect, Color.FromArgb(220, 220, 220), Color.FromArgb(250, 250, 250), LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(bgBrush, bgRect);
            }

            // Draw the animated gradient progress bar
            Rectangle movingRect = new Rectangle(offset, 0, barWidth, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(movingRect, Color.Cyan, Color.Blue, LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, movingRect);
            }

            // Add subtle glow effect
            using (Pen glowPen = new Pen(Color.FromArgb(120, Color.White), 1))
            {
                e.Graphics.DrawRectangle(glowPen, 0, 0, this.Width - 1, this.Height - 1);
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
            this.ClientSize = new Size(220, 80); // Slightly larger
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            // Enable Windows 10+ blur effect (optional)
            EnableBlurEffect();

            // Container panel
            Panel container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            this.Controls.Add(container);

            // "Loading..." label
            Label labelLoading = new Label
            {
                Font = new Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.DarkSlateGray,
                Text = "Please wait...",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            // Modern progress bar
            ModernProgressBar progressBar = new ModernProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 12,
                Margin = new Padding(10)
            };

            // Circular loading indicator (optional)
            //PictureBox loadingIndicator = new PictureBox
            //{
            //    Image = Properties.Resources.LoadingGif, // Use a small GIF for animation
            //    SizeMode = PictureBoxSizeMode.Zoom,
            //    Size = new Size(30, 30),
            //    Dock = DockStyle.Bottom
            //};

            //container.Controls.Add(loadingIndicator);
            container.Controls.Add(progressBar);
            container.Controls.Add(labelLoading);
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            SetRoundedRegion(16);
        }

        private void SetRoundedRegion(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        private void EnableBlurEffect()
        {
            if (Environment.OSVersion.Version.Major >= 10)
            {
                try
                {
                    int accentState = 3; // Enable blur
                    int policy = 2; // Apply to form
                    NativeMethods.ACCENT_POLICY accent = new NativeMethods.ACCENT_POLICY { AccentState = accentState };
                    NativeMethods.WINDOWCOMPOSITIONATTRIBDATA data = new NativeMethods.WINDOWCOMPOSITIONATTRIBDATA
                    {
                        Attribute = policy,
                        Data = System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(accent)),
                        SizeOfData = System.Runtime.InteropServices.Marshal.SizeOf(accent)
                    };
                    System.Runtime.InteropServices.Marshal.StructureToPtr(accent, data.Data, false);
                    NativeMethods.SetWindowCompositionAttribute(this.Handle, ref data);
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(data.Data);
                }
                catch { }
            }
        }
    }

    internal class NativeMethods
    {
        public struct ACCENT_POLICY
        {
            public int AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        public struct WINDOWCOMPOSITIONATTRIBDATA
        {
            public int Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WINDOWCOMPOSITIONATTRIBDATA data);
    }
}
