using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public class TankTypeCard : UserControl
    {
        public event EventHandler CardClick;

        private readonly Panel _cardPanel;
        private readonly Panel _imageHost;
        private readonly PictureBox _picture;
        private readonly Label _textLabel;

        private bool _isHovering = false;

        public TankType CardTankType { get; set; }

        public string Label
        {
            get => _textLabel.Text;
            set => _textLabel.Text = value;
        }

        public Image CardImage
        {
            get => _picture.Image;
            set => _picture.Image = value;
        }

        public TankTypeCard()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            MinimumSize = new Size(300, 280);
            Padding = new Padding(0);
            Margin = new Padding(0);

            // Card container (this gets rounded)
            _cardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            // Image host (kept rectangular; clipped by _cardPanel region)
            _imageHost = new Panel
            {
                Dock = DockStyle.Top,
                Height = 205,
                BackColor = Color.White,
                Padding = new Padding(10, 10, 10, 0), // framed look
                Margin = new Padding(0)
            };

            _picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };

            _textLabel = new Label
            {
                Dock = DockStyle.Fill,
                Height = 58,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(28, 28, 28),
                Font = new Font("Segoe UI Semibold", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };

            _imageHost.Controls.Add(_picture);

            // Add in order (label fills remaining space)
            _cardPanel.Controls.Add(_textLabel);
            _cardPanel.Controls.Add(_imageHost);

            Controls.Add(_cardPanel);

            // Forward clicks from all child controls
            HookClicks(this);
            HookClicks(_cardPanel);
            HookClicks(_imageHost);
            HookClicks(_picture);
            HookClicks(_textLabel);

            // Hover effect
            HookHover(this);
            HookHover(_cardPanel);
            HookHover(_imageHost);
            HookHover(_picture);
            HookHover(_textLabel);

            Resize += (s, e) =>
            {
                ApplyCardRegion();
                Invalidate();
            };

            Load += (s, e) =>
            {
                ApplyCardRegion();
                Invalidate();
            };
        }

        private void HookClicks(Control c)
        {
            c.Click += (s, e) => CardClick?.Invoke(this, EventArgs.Empty);
        }

        private void HookHover(Control ctl)
        {
            ctl.MouseEnter += (s, e) =>
            {
                _isHovering = true;
                Invalidate();
            };

            ctl.MouseLeave += (s, e) =>
            {
                // Avoid flicker/false leave when moving between child controls
                Point p = PointToClient(Cursor.Position);
                _isHovering = ClientRectangle.Contains(p);
                Invalidate();
            };
        }

        private void ApplyCardRegion()
        {
            if (_cardPanel.Width <= 0 || _cardPanel.Height <= 0) return;

            using (var path = CreateRoundedRectPath(
                new Rectangle(0, 0, _cardPanel.Width, _cardPanel.Height), 18))
            {
                _cardPanel.Region = new Region(path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(4, 4, Width - 8, Height - 8);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            // Soft shadow
            var shadowRect = new Rectangle(rect.X + 3, rect.Y + 4, rect.Width, rect.Height);
            using (var shadowPath = CreateRoundedRectPath(shadowRect, 18))
            using (var shadowBrush = new SolidBrush(Color.FromArgb(_isHovering ? 50 : 30, 0, 0, 0)))
            {
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            // Card body + border
            using (var cardPath = CreateRoundedRectPath(rect, 18))
            using (var cardBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillPath(cardBrush, cardPath);

                var borderColor = _isHovering
                    ? Color.FromArgb(64, 128, 255)
                    : Color.FromArgb(220, 225, 230);

                var borderWidth = _isHovering ? 2f : 1.2f;

                using (var borderPen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawPath(borderPen, cardPath);
                }
            }

            // Divider line above label area
            int dividerY = rect.Top + _imageHost.Height;
            using (var dividerPen = new Pen(Color.FromArgb(235, 235, 235), 1))
            {
                e.Graphics.DrawLine(dividerPen, rect.Left + 12, dividerY, rect.Right - 12, dividerY);
            }
        }

        private GraphicsPath CreateRoundedRectPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}