using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    /// <summary>Clickable card used on the tank-type selection screen.</summary>
    public class TankTypeCard : Control
    {
        public string Label { get; set; }
        public Image CardImage { get; set; }
        public TankType CardTankType { get; set; }
        public event EventHandler CardClick;

        private const float TARGET_SCALE = 1.035f;  // ≈ 3 % zoom
        private const int CORNER_RADIUS = 24;
        private const int FPS = 60;

        private float _scale = 1f;
        private bool _hover = false;
        private readonly System.Windows.Forms.Timer _animTimer;

        public TankTypeCard()
        {
            DoubleBuffered = true;
            Size = new Size(260, 260);
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 13, FontStyle.Bold);

            _animTimer = new System.Windows.Forms.Timer { Interval = 1000 / FPS };
            _animTimer.Tick += (s, e) => AnimateStep();
        }

        /*────────── mouse / animation ─────────*/
        protected override void OnMouseEnter(EventArgs e) { _hover = true; _animTimer.Start(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _animTimer.Start(); base.OnMouseLeave(e); }
        protected override void OnClick(EventArgs e) { CardClick?.Invoke(this, e); base.OnClick(e); }

        private void AnimateStep()
        {
            const float STEP = 0.075f;
            float target = _hover ? TARGET_SCALE : 1f;

            if (Math.Abs(_scale - target) < 0.003f) { _scale = target; _animTimer.Stop(); }
            else { _scale += (target - _scale) * STEP; }

            Invalidate();
        }

        /*────────── painting ─────────*/
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? ColorTranslator.FromHtml("#232532"));

            Rectangle cardRect = new Rectangle(0, 0, Width, Height);
            Rectangle imageRect = new Rectangle(8, 8, Width - 16, Height - 64);
            Rectangle labelRect = new Rectangle(0, Height - 56, Width, 48);

            using GraphicsPath cardPath = Rounded(cardRect, CORNER_RADIUS);

            /*──── 1) clip EVERYTHING to the rounded card ────*/
            Region previousClip = g.Clip;
            g.SetClip(cardPath);   //  ←-- THIS is what removes the “ears”

            /*──── 2) optional drop shadow ────*/
            if (_scale > 1.002f)
            {
                Rectangle sh = new Rectangle(cardRect.X + 4, cardRect.Y + 6,
                                             cardRect.Width - 8, cardRect.Height - 6);
                using GraphicsPath shPath = Rounded(sh, CORNER_RADIUS);
                using SolidBrush shBr = new SolidBrush(Color.FromArgb(55, 0, 0, 0));
                g.FillPath(shBr, shPath);
            }

            /*──── 3) white card background & border ────*/
            using (SolidBrush white = new SolidBrush(Color.White))
                g.FillPath(white, cardPath);
            using (Pen border = new Pen(Color.FromArgb(80, 60, 60, 60), 2f))
                g.DrawPath(border, cardPath);

            /*──── 4) image (with zoom) ────*/
            if (CardImage != null)
            {
                GraphicsState st = g.Save();

                // further clip to rounded-top only
                using (GraphicsPath topClip = Rounded(imageRect, CORNER_RADIUS, topOnly: true))
                    g.SetClip(topClip, CombineMode.Intersect);

                float cx = imageRect.Left + imageRect.Width / 2f;
                float cy = imageRect.Top + imageRect.Height / 2f;
                g.TranslateTransform(cx, cy);
                g.ScaleTransform(_scale, _scale);
                g.TranslateTransform(-cx, -cy);

                g.DrawImage(CardImage, imageRect);
                g.Restore(st);
            }

            /*──── 5) caption strip ────*/
            using (GraphicsPath barPath = Rounded(labelRect, CORNER_RADIUS, bottomOnly: true))
            using (SolidBrush barBr = new SolidBrush(Color.FromArgb(246, 246, 248)))
                g.FillPath(barBr, barPath);

            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (SolidBrush txt = new SolidBrush(Color.FromArgb(35, 35, 50)))
                g.DrawString(Label ?? "", Font, txt, labelRect, sf);

            /*──── restore original clip ────*/
            g.Clip = previousClip;
        }

        /*────────── helper: rounded rectangle ─────────*/
        private static GraphicsPath Rounded(Rectangle r, int radius,
                                            bool topOnly = false, bool bottomOnly = false)
        {
            int d = radius * 2;
            GraphicsPath p = new GraphicsPath();

            if (topOnly)
            {
                p.AddArc(r.X, r.Y, d, d, 180, 90);
                p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                p.AddLine(r.Right, r.Y + radius, r.Right, r.Bottom);
                p.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
                p.AddLine(r.X, r.Bottom, r.X, r.Y + radius);
            }
            else if (bottomOnly)
            {
                p.AddLine(r.X, r.Y, r.Right, r.Y);
                p.AddLine(r.Right, r.Y, r.Right, r.Bottom - radius);
                p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
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
