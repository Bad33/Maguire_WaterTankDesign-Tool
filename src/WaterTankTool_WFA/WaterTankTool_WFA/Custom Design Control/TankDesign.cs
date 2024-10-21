using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WaterTankTool_WFA.Entity;

namespace WaterTankTool_WFA.Custom_Design_Control
{
    public partial class TankDesign : UserControl
    {
        public List<SegmentProperties> Segments { get; set; }
        private ToolTip toolTip;
        private SegmentProperties hoveredSegment;

        public TankDesign()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            Segments = new List<SegmentProperties>();
            toolTip = new ToolTip();
            hoveredSegment = null;

            this.Load += TankDesign_Load;
            this.MouseMove += TankDesign_MouseMove;

            InitializeComponent();
        }

        private void TankDesign_Load_1(object sender, EventArgs e)
        {

        }

        private void TankDesign_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("TankDesign_Load triggered");
            Redraw(); // Ensure initial drawing
        }

        public void Redraw()
        {
            Debug.WriteLine("Redraw called");
            this.PerformLayout();
            this.Invalidate();
            this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                Debug.WriteLine("OnPaint triggered");
                base.OnPaint(e);
                DrawTank(e.Graphics);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"OnPaint error: {ex.Message}");
            }
        }

        private void DrawTank(Graphics g)
        {
            if (Segments == null || Segments.Count == 0)
            {
                g.DrawString("No segments to draw", this.Font, Brushes.Black, new PointF(10, 10));
                return;
            }

            int width = this.Width;
            int height = this.Height;
            int centerX = width / 2;

            var sortedSegments = Segments.OrderBy(s => s.HeightInitial).ToList();

            double scaleX = width / GetTotalWidth();
            double scaleY = height / GetTotalHeight();

            foreach (var segment in sortedSegments)
            {
                switch (segment.SegmentType)
                {
                    case "Base":
                        DrawBaseOutline(g, segment, centerX, scaleX, scaleY);
                        break;
                    case "Cylinder":
                        DrawCylinderOutline(g, segment, centerX, scaleX, scaleY);
                        break;
                    case "Tanks":
                        DrawSpheroidOutline(g, segment, centerX, scaleX, scaleY);
                        break;
                }
            }
        }

        private void DrawBaseOutline(Graphics g, SegmentProperties segment, int centerX, double scaleX, double scaleY)
        {
            double baseWidth = segment.Diameter * scaleX;
            double baseHeight = (segment.HeightFinal - segment.HeightInitial) * scaleY;
            double baseY = this.Height - segment.HeightFinal * scaleY;

            Point[] basePoints = {
                new Point((int)(centerX - baseWidth / 2), (int)(baseY + baseHeight)),
                new Point((int)(centerX + baseWidth / 2), (int)(baseY + baseHeight)),
                new Point((int)(centerX + baseWidth * 0.7 / 2), (int)baseY),
                new Point((int)(centerX - baseWidth * 0.7 / 2), (int)baseY)
            };
            g.DrawPolygon(Pens.Black, basePoints);
        }

        private void DrawCylinderOutline(Graphics g, SegmentProperties segment, int centerX, double scaleX, double scaleY)
        {
            double cylinderWidth = segment.Diameter * scaleX;
            double cylinderHeight = (segment.HeightFinal - segment.HeightInitial) * scaleY;
            double cylinderY = this.Height - segment.HeightFinal * scaleY;

            g.DrawRectangle(
                Pens.Gray,
                (float)(centerX - cylinderWidth / 2),
                (float)cylinderY,
                (float)cylinderWidth,
                (float)cylinderHeight
            );
        }

        private void DrawSpheroidOutline(Graphics g, SegmentProperties segment, int centerX, double scaleX, double scaleY)
        {
            double spheroidWidth = segment.Diameter * scaleX;
            double spheroidHeight = (segment.HeightFinal - segment.HeightInitial) * scaleY;
            double spheroidY = this.Height - segment.HeightFinal * scaleY - spheroidHeight / 2;

            g.DrawEllipse(
                Pens.Blue,
                (float)(centerX - spheroidWidth / 2),
                (float)spheroidY,
                (float)spheroidWidth,
                (float)spheroidHeight
            );
        }

        private void TankDesign_MouseMove(object sender, MouseEventArgs e)
        {
            var segment = GetSegmentAtPosition(e.Location);

            if (segment != null && segment != hoveredSegment)
            {
                string tooltipText = $"Segment: {segment.SegmentType}\n" +
                                     $"Height: {segment.HeightFinal - segment.HeightInitial} meters\n" +
                                     $"Diameter: {segment.Diameter} meters\n" +
                                     $"Thickness: {segment.Thickness} meters";

                toolTip.SetToolTip(this, tooltipText);
                hoveredSegment = segment;
            }
            else if (segment == null)
            {
                toolTip.SetToolTip(this, string.Empty);
                hoveredSegment = null;
            }
        }

        private SegmentProperties GetSegmentAtPosition(Point mousePosition)
        {
            foreach (var segment in Segments)
            {
                double segmentWidth = segment.Diameter * (this.Width / GetTotalWidth());
                double segmentHeight = (segment.HeightFinal - segment.HeightInitial) * (this.Height / GetTotalHeight());
                double segmentY = this.Height - segment.HeightFinal * (this.Height / GetTotalHeight());
                int centerX = this.Width / 2;

                RectangleF segmentBounds = new RectangleF(
                    (float)(centerX - segmentWidth / 2),
                    (float)segmentY,
                    (float)segmentWidth,
                    (float)segmentHeight
                );

                if (segmentBounds.Contains(mousePosition))
                {
                    return segment;
                }
            }
            return null;
        }

        private double GetTotalHeight()
        {
            return Segments.Max(s => s.HeightFinal);
        }

        private double GetTotalWidth()
        {
            return Segments.Max(s => s.Diameter);
        }
    }
}
