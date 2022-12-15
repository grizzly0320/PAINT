using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAINT
{
    public abstract class Figure
    {
        private int? _x1 = null, _y1 = null, _x2 = null, _y2 = null;
        public bool IsExsist => _x1 != null && _y1 != null && _x2 != null && _y2 != null;
        public int X => (_x1 != null && _x2 != null) ? Math.Min(_x1 ?? 0, _x2 ?? 0) : 0;
        public int Y => _y1 != null && _y2 != null ? Math.Min(_y1 ?? 0, _y2 ?? 0) : 0;
        public int Width => _x1 != null && _x2 != null ? Math.Abs((_x1 ?? 0) - (_x2 ?? 0)) : 1;
        public int Height => _y1 != null && _y2 != null ? Math.Abs((_y1 ?? 0) - (_y2 ?? 0)) : 1;
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public Color BorderColor { get; set; }
        public float ColorAngle { get; set; }
        public Rectangle Rectangle => new Rectangle(X, Y, Width, Height);
        public virtual void AddPoint(Point p)
        {
            if (_x1 == null)
            {
                _x1 = p.X;
                _y2 = p.Y;
            }
            else
            {
                if (p.X == _x1 && p.Y == _y1)
                {
                    _x2 = null;
                    _y2 = null;
                }
                else
                {
                    _x2 = p.X;
                    _y2 = p.Y;
                }
            }
        }
        public abstract void Paint(Graphics g);
        public virtual bool ContainsPoint(Point p) =>
            p.X >= X &&
            p.X <= X + Width &&
            p.Y >= Y &&
            p.Y <= Y + Height;
        public virtual void Move(int dx, int dy)
        {
            _x1 += dx;
            _y1 += dy;
            _x2 = dx;
            _y2 = dy;
        }
    }
}
