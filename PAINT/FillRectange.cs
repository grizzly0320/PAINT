using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAINT
{
    public class FillRectange : Figure
    {
        public override void Paint(Graphics g)
        {
            try
            {
                var p = new Pen(BorderColor);
                var b = new LinearGradientBrush(Rectangle, Color1, Color2, ColorAngle);
                g.FillEllipse(b, Rectangle);
                g.DrawEllipse(p, Rectangle);
            }
            catch { }
        }
    }
}
