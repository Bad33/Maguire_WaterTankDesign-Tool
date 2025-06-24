using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    public class TankTypeCard : Control
    {
        public string Label { get; set; }
        public Image CardImage { get; set; }
        public event EventHandler CardClick;
        public TankType CardTankType { get; set; }

        private float scale = 1.0f;
        private System.Windows.Forms.Timer animTimer;
        private bool isHovering = false;
        private const float TargetScale = 1.06f;
        private const int CornerRadius = 22;

        public TankTypeCard()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(220, 220);
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            animTimer = new System.Windows.Forms.Timer();
            animTimer.Interval = 16; // ~60 FPS
            animTimer.Tick += (s, e) => Animate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovering = true;
            animTimer.Start();
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovering = false;
            animTimer.Start();
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            CardClick?.Invoke(this, e);
        }

        private void Animate()
        {
            float step = 0.07f;
            if (isHovering && scale < TargetScale)
                scale = Math.Min(TargetScale, scale + step);
            else if (!isHovering && scale > 1.0f)
                scale = Math.Max(1.0f, scale - step);

            Invalidate();

            if ((isHovering && Math.Abs(scale - TargetScale) < 0.01f) ||
                (!isHovering && Math.Abs(scale - 1.0f) < 0.01f))
            {
                animTimer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? ColorTranslator.FromHtml("#232532"));

            // Center & scale transform
            var cx = this.Width / 2f;
            var cy = this.Height / 2f;
            g.TranslateTransform(cx, cy);
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(-cx, -cy);

            // Card background with rounded corners and shadow on hover
            Rectangle cardRect = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath path = RoundedRect(cardRect, CornerRadius))
            {
                // Shadow
                if (scale > 1.01f)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(55, 40, 40, 56)))
                    {
                        g.FillPath(sb, RoundedRect(new Rectangle(cardRect.X + 4, cardRect.Y + 8, cardRect.Width - 8, cardRect.Height - 4), CornerRadius + 2));
                    }
                }

                // Main card background
                using (SolidBrush b = new SolidBrush(Color.White))
                    g.FillPath(b, path);

                // Card border
                using (Pen border = new Pen(Color.FromArgb(70, 60, 60, 60), 2))
                    g.DrawPath(border, path);
            }

            // Draw the image (rounded at top, clipped)
            Rectangle imgRect = new Rectangle(8, 8, Width - 16, Height - 60);
            using (GraphicsPath clip = RoundedRect(imgRect, CornerRadius, topOnly: true))
            {
                g.SetClip(clip);
                if (CardImage != null)
                    g.DrawImage(CardImage, imgRect);
                g.ResetClip();
            }

            // Draw the label (bottom, in card)
            Rectangle labelRect = new Rectangle(0, Height - 52, Width, 44);
            using (GraphicsPath labelPath = RoundedRect(labelRect, CornerRadius, bottomOnly: true))
            using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(245, 245, 247)))
            {
                g.FillPath(labelBrush, labelPath);
            }
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(38, 38, 60)))
            {
                g.DrawString(Label, this.Font, textBrush, labelRect, sf);
            }
        }

        // Helpers
        private static GraphicsPath RoundedRect(Rectangle r, int radius, bool topOnly = false, bool bottomOnly = false)
        {
            int d = radius * 2;
            GraphicsPath p = new GraphicsPath();
            if (topOnly)
            {
                p.AddArc(r.X, r.Y, d, d, 180, 90); // TL
                p.AddArc(r.Right - d, r.Y, d, d, 270, 90); // TR
                p.AddLine(r.Right, r.Y + radius, r.Right, r.Bottom);
                p.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
                p.AddLine(r.X, r.Bottom, r.X, r.Y + radius);
            }
            else if (bottomOnly)
            {
                p.AddLine(r.X, r.Y, r.Right, r.Y);
                p.AddLine(r.Right, r.Y, r.Right, r.Bottom - radius);
                p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); // BR
                p.AddArc(r.X, r.Bottom - d, d, d, 90, 90); // BL
                p.AddLine(r.X, r.Bottom - radius, r.X, r.Y);
            }
            else
            {
                p.AddArc(r.X, r.Y, d, d, 180, 90);
                p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                p.CloseFigure();
            }
            return p;
        }
    }
}
