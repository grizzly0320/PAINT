using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PAINT
{
    public class Pencil
    {
        public Color _colorMain;
        public Color _colorBack;
        public int _size;
        public Point px, py;

        public Pencil(Color color1, Color color2, int _size)
        {
            this._colorMain = color1;
            this._colorBack = color2;
            this._size = _size;
        }
        public void ToDraw(MouseEventArgs e, Graphics g)
        {
            Pen p = new Pen(this._colorMain);
            this.px = e.Location;
            g.DrawLine(p, this.px, this.py);
            this.py = this.px;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
        public void ToDrawBack(MouseEventArgs e, Graphics g)
        {
            Pen p = new Pen(this._colorBack);
            this.px = e.Location;
            g.DrawLine(p, this.px, this.py);
            this.py = this.px;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
    }
}
