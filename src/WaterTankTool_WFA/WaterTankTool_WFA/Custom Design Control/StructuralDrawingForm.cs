using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterTankTool_WFA.Custom_Design_Control
{
    public partial class StructuralDrawingForm : Form
    {
        private Image drawingImage;

        public StructuralDrawingForm()
        {
            this.Width = 800;
            this.Height = 600;
            this.Paint += StructuralDrawingForm_Paint;

            // Load the PNG structural drawing
            drawingImage = Properties.Resources._150k;
        }

        private void StructuralDrawingForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            // Calculate center alignment
            var x = (this.ClientSize.Width - drawingImage.Width) / 2;
            var y = (this.ClientSize.Height - drawingImage.Height) / 2;

            // Draw the PNG image on the canvas
            g.DrawImage(drawingImage, x, y);
        }
    }
}
