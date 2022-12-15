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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.pencil = new Pencil(MainColorButton.BackColor, BackgroundСolorButton.BackColor, size);
            this.erase2 = new Erase(BackgroundСolorButton.BackColor, size);
            Width = pictureBox2.Width;
            this.Height = pictureBox2.Height;
            bm = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pictureBox2.Image = bm;
            colorButtons.Push(1);
        }

        Stack<int> toolButtons = new Stack<int>();
        Stack<int> colorButtons = new Stack<int>();

        Pencil pencil;
        Erase erase2;


        static int size = 1;
        Bitmap bm;
        Graphics g;
        //Pen p = new Pen(Color.Black, size);
        //Pen back_p = new Pen(Color.White, size);
        //Pen erase = new Pen(Color.White, size);
        bool paint = false;
        Point px, py;
        int index;
        int x, y, sX, sY, cX, cY;

        ColorDialog cd = new ColorDialog();
        Color new_color;
        Color back_new_color;

        private void RectangleButtom_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            //index = 5;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }
                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }
                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
                if (index == 8)
                {
                    try
                    {
                        Rectangle r = new Rectangle(cX, cY, sX, sY);
                        g.DrawEllipse(p, cX, cY, sX, sY);
                        var b = new LinearGradientBrush(r, Color.Red, Color.Red, 90);
                        g.FillEllipse(b, r);
                        g.DrawEllipse(p, r);
                    }
                    catch { }
                }
            }
        }

        private void ClearButtom_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox2.Image = bm;
            index = 0;
        }

        private void ColorDialogButton_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            if (colorButtons.Peek() == 1)
            {
                MainColorButton.BackColor = cd.Color;
                pencil._colorBack = cd.Color;
            }
            else if (colorButtons.Peek() == 2)
            {
                BackgroundСolorButton.BackColor = cd.Color;
                pencil._colorBack = cd.Color;
            }

        }

        private void EllipseBottom_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pencil.py = e.Location;
            erase2.py = e.Location;
            paint = true;
            py = e.Location;
            cX = e.X;
            cY = e.Y;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private Point SetPoint(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Width / pb.Width;
            float pY = 1f * pb.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }

        private void FillButtom_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (index == 7)
            {
                Point point = SetPoint(pictureBox2, e.Location);
                Fill(bm, point.X, point.Y, new_color);
            }
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg| Bitmap files(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pencil._size = trackBar1.Value;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void RedColorButtom_Click(object sender, EventArgs e)
        {
            if (colorButtons.Peek() == 1)
            {
                MainColorButton.BackColor = Color.Red;
            }
            else if (colorButtons.Peek() == 2)
            {
                BackgroundСolorButton.BackColor = Color.Red;
            }
        }

        private void YellowColorButton_Click(object sender, EventArgs e)
        {
            if (colorButtons.Peek() == 1)
            {
                MainColorButton.BackColor = Color.Yellow;
            }
            else if (colorButtons.Peek() == 2)
            {
                BackgroundСolorButton.BackColor = Color.Yellow;
            }
        }

        private void GreenColorButton_Click(object sender, EventArgs e)
        {
            p.Color= Color.Green;
            new_color = Color.Green;
        }

        private void BlueColorButton_Click(object sender, EventArgs e)
        {
                p.Color = Color.Blue;
                new_color = Color.Blue;
        }

        private void OrangeColorButton_Click(object sender, EventArgs e)
        {
            p.Color = Color.Orange;
            new_color = Color.Orange;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ихображениеВФорматеPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "PNG(*.png)|*.png";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void изображениеВФорматеJPEGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "JPG(*.jpg)|*.jpg";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void изображениеВФорматеBMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "BMP(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void изображениеВФорматеPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "PDF(*.pdf)|*.pdf";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "PNG(*.png)|*.png|JPG(*.jpg)|*.jpg|BTM(*.bmp)|*.bmp|PDF(*.pdf)|*.pdf";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox2_SizeChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {

        }

        private void button43_Click(object sender, EventArgs e)
        {
            colorButtons.Push(2);
            //back_p.Color = BackgroundСolorButton.BackColor;
            //back_new_color = BackgroundСolorButton.BackColor;
        }

        private void button42_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click(object sender, EventArgs e)
        {

        }

        private void pic_color_Click(object sender, EventArgs e)
        {
            colorButtons.Push(1);
            p.Color = MainColorButton.BackColor;
            new_color = MainColorButton.BackColor;
        }

        private void button45_Click(object sender, EventArgs e)
        {

        }

        private void TextWriteButton_Click(object sender, EventArgs e)
        {
            index = 123;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}

        private void FillEllipse_Click(object sender, EventArgs e)
        {
            index = 8;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, e);
            ClearButtom_Click(sender, e);

        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            sX = x - cX;
            sY = y - cY;

            if (index == 3)
            {
                if (e.Button == MouseButtons.Left)
                    g.DrawEllipse(p, cX, cY, sX, sY);
                else if (e.Button == MouseButtons.Right)
                    //Создать PEN для фонового цвета
                    g.DrawEllipse(p, cX, cY, sX, sY);
            }
            if (index == 4)
            {
                if (e.Button == MouseButtons.Left)
                    g.DrawRectangle(p, cX, cY, sX, sY);
                else if ((e.Button == MouseButtons.Right))
                {

                }
            }
            if (index == 5)
            {
                if (e.Button == MouseButtons.Left)
                    g.DrawLine(p, cX, cY, x, y);
                else if (e.Button == MouseButtons.Right)
                {

                }
            }
            if (index == 8)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Rectangle r = new Rectangle(cX, cY, sX, sY);
                    g.DrawEllipse(p, cX, cY, sX, sY);
                    var b = new LinearGradientBrush(r, Color.Red, Color.Red,  90);
                    g.FillEllipse(b, r);
                    g.DrawEllipse(p, r);

                }
                else if (e.Button == MouseButtons.Right)
                    //Создать PEN для фонового цвета
                    g.DrawEllipse(p, cX, cY, sX, sY);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void RubberButton_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 1)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        //px = e.Location;
                        //g.DrawLine(p, px, py);
                        //py = px;
                        //p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        //p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        pencil.ToDraw(e, g);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        //px = e.Location;
                        //g.DrawLine(back_p, px, py);
                        //py = px;
                        //back_p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        //back_p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        pencil.ToDrawBack(e, g);
                    }
                }
                if (index == 2)
                {
                    erase2.ToErase(e, g);
                }
                if (index == 123)
                {
                }
            }
            pictureBox2.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }

        
        private void Validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color)
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color new_clr)
        {
            Color old_color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, new_clr);
            if (old_color == new_clr) return;
            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width-1 && pt.Y < bm.Height - 1)
                {
                    Validate(bm, pixel, pt.X-1, pt.Y, old_color, new_clr);
                    Validate(bm, pixel, pt.X, pt.Y-1, old_color, new_clr);
                    Validate(bm, pixel, pt.X+1, pt.Y, old_color, new_clr);
                    Validate(bm, pixel, pt.X, pt.Y+1, old_color, new_clr);
                }
            }
        }
    }
}

// Левая кнопка мыши - фигура основного цвета,
// Правая кнопка мыши - фигура фонового цвета.
