using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CircularProgressBar : Control
{
    private int progressValue = 0;
    private int maxValue = 100;

    public int ProgressValue
    {
        get { return progressValue; }
        set
        {
            progressValue = value;
            this.Invalidate(); // Redraw control when value changes
        }
    }

    public int MaxValue
    {
        get { return maxValue; }
        set
        {
            maxValue = value > 0 ? value : 1; // Prevent division by zero
            this.Invalidate();
        }
    }

    public CircularProgressBar()
    {
        //this.BackColor = Color.Transparent;
        this.DoubleBuffered = true; // Reduces flickering
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Do not paint the background to keep it transparent
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Draw the background circle
        using (Pen backgroundPen = new Pen(Color.FromArgb(100, 100, 100), 12))
        {
            e.Graphics.DrawArc(backgroundPen, 10, 10, this.Width - 20, this.Height - 20, 0, 360);
        }

        // Calculate the sweep angle for the progress
        float sweepAngle = 360f * progressValue / maxValue;

        // Draw the progress arc with a gradient effect
        using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
            new Rectangle(10, 10, this.Width - 20, this.Height - 20),
            Color.LightGreen, // Start color
            Color.Green, // End color
            LinearGradientMode.ForwardDiagonal)) // Direction of the gradient
        {
            using (Pen progressPen = new Pen(gradientBrush, 12))
            {
                e.Graphics.DrawArc(progressPen, 10, 10, this.Width - 20, this.Height - 20, -90, sweepAngle);
            }
        }

        // Draw the progress value text
        using (Font font = new Font("Segoe UI", 16, FontStyle.Bold))
        {
            string text = $"{progressValue}%";
            SizeF textSize = e.Graphics.MeasureString(text, font);
            PointF textPosition = new PointF((this.Width - textSize.Width) / 2, (this.Height - textSize.Height) / 2);
            e.Graphics.DrawString(text, font, Brushes.White, textPosition);
        }
    }
}
