using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterTankTool_WFA.Entity;

namespace WaterTankTool_WFA.Custom_Design_Control
{
    public partial class TankDesign : UserControl
    {
        
        public List<SegmentProperties> Segments {  get; set; }
        public TankDesign()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.Load += TankDesign_Load;

            InitializeComponent();
        }

        private void TankDesign_Load(object? sender, EventArgs e)
        {
            
        }

        public void Redraw()
        {
            this.PerformLayout();
            this.Invalidate(); 
            this.Update();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
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
                return;

            int width = this.Width;
            int height = this.Height;
            int centerX = width / 2;

            // Sort segments by HeightInitial to draw from bottom to top
            var sortedSegments = Segments.OrderBy(s => s.HeightInitial).ToList();

            // Scaling factors to convert real-world dimensions to pixels
            double scaleX = width / GetTotalWidth();
            double scaleY = height / GetTotalHeight();

            foreach (var segment in sortedSegments)
            {
                switch (segment.SegmentType)
                {
                    case "Base":
                        DrawBase(g, segment, centerX, scaleX, scaleY);
                        break;
                    case "Cylinder":
                        DrawCylinder(g, segment, centerX, scaleX, scaleY);
                        break;
                    case "Tanks":
                        DrawSpheroid(g, segment, centerX, scaleX, scaleY);
                        break;
                }
            }
        }

        private void DrawBase(Graphics g, SegmentProperties segment, int centerX, double scaleX, double scaleY)
        {
            // Calculate dimensions in pixels
            double baseWidth = segment.Diameter * scaleX;
            double baseHeight = (segment.HeightFinal - segment.HeightInitial) * scaleY;
            double baseY = this.Height - segment.HeightFinal * scaleY;

            // Define trapezoid points based on parameters
            Point[] basePoints = {
                new Point((int)(centerX - baseWidth / 2), (int)(baseY + baseHeight)),
                new Point((int)(centerX + baseWidth / 2), (int)(baseY + baseHeight)),
                new Point((int)(centerX + baseWidth * 0.7 / 2), (int)baseY),
                new Point((int)(centerX - baseWidth * 0.7 / 2), (int)baseY)
            };
            g.FillPolygon(Brushes.Black, basePoints);
        }

        private void DrawCylinder(Graphics g, SegmentProperties segment, int centerX, double scaleX, double scaleY)
        {
            double cylinderWidth = segment.Diameter * scaleX;
            double cylinderHeight = (segment.HeightFinal - segment.HeightInitial) * scaleY;
            double cylinderY = this.Height - segment.HeightFinal * scaleY;

            g.FillRectangle(
                Brushes.Gray,
                (float)(centerX - cylinderWidth / 2),
                (float)cylinderY,
                (float)cylinderWidth,
                (float)cylinderHeight
            );
        }

        private void DrawSpheroid(Graphics g, SegmentProperties segment, int centerX, double scaleX, double scaleY)
        {
            double spheroidWidth = segment.Diameter * scaleX;
            double spheroidHeight = (segment.HeightFinal - segment.HeightInitial) * scaleY;
            double spheroidY = this.Height - segment.HeightFinal * scaleY - spheroidHeight / 2;

            g.FillEllipse(
                Brushes.Blue,
                (float)(centerX - spheroidWidth / 2),
                (float)spheroidY,
                (float)spheroidWidth,
                (float)spheroidHeight
            );
        }

        private Brush GetSegmentBrush(string type)
        {
            switch (type)
            {
                case "Base": return Brushes.Black;
                case "Cylinder": return Brushes.Gray;
                case "Spheroid": return Brushes.Blue;
                default: return Brushes.Black;
            }
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
