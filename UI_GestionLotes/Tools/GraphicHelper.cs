using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_GestionLotes.Tools
{
    internal class GraphicHelper
    {
        public GraphicsPath GetGraphicsPath(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rectangle.X, rectangle.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rectangle.Right - curveSize, rectangle.Y, curveSize, curveSize, 270, 90);
            path.AddArc((rectangle.Right - curveSize), (rectangle.Bottom - curveSize), curveSize, curveSize, 0, 90);
            path.AddArc(rectangle.X, (rectangle.Bottom - curveSize), curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
       
    }
}
