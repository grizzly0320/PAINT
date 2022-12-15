using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAINT
{
    public class Erase
    {
        public Color _color;
        public int _size;
        public Point px, py;

        public Erase(Color color, int _size)
        {
            this._color = color;
            this._size = _size;
        }
        public void ToErase(MouseEventArgs e, Graphics g)
        {
            Pen p = new Pen(this._color);
            this.px = e.Location;
            g.DrawLine(p, this.px, this.py);
            this.py = this.px;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
    }
}
