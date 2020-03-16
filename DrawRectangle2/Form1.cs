using System.Drawing;
using System.Windows.Forms;

namespace DrawRectangle1
{
    public partial class Form1 : Form
    {
        bool isClicked = false;
        bool inBounds = false;

        int X = 0;
        int Y = 0;

        int X1 = 0;
        int Y1 = 0;

        int deltaX = 0;
        int deltaY = 0;


        Point[] points = new Point[8];

        Rectangle rect;

        Graphics g;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Cursor = Cursors.Cross;
            //pictureBox1.Image = DrawRectangle1.Properties.Resources.butterfly;


            //g = pictureBox1.CreateGraphics();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {


            isClicked = true;

            X = e.X;
            Y = e.Y;

            if ((e.X < rect.X + rect.Width) && (e.X > rect.X))
                if ((e.Y < rect.Y + rect.Height) && (e.Y > rect.Y))
                {
                    inBounds = true;

                    deltaX = e.X - rect.X;
                    deltaY = e.Y - rect.Y;
                }

            //if (!inBounds) { g.Clear(Color.White); } // очищаем рабочую поверхность
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (((e.X < rect.X + rect.Width) && (e.X > rect.X))
                && ((e.Y < rect.Y + rect.Height) && (e.Y > rect.Y)))

                pictureBox1.Cursor = Cursors.Default;
            else
                pictureBox1.Cursor = Cursors.Cross;

            if (isClicked)
            {
                X1 = e.X;
                Y1 = e.Y;
                pictureBox1.Invalidate();
            }

            if (inBounds)
            {
                rect.X = e.X - deltaX;
                rect.Y = e.Y - deltaY;

                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
            inBounds = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);

            points[0] = new Point(X, Y);
            points[1] = new Point(X1, Y);
            points[2] = new Point(X1, Y);
            points[3] = new Point(X1, Y1);
            points[4] = new Point(X1, Y1);
            points[5] = new Point(X, Y1);
            points[6] = new Point(X, Y1);
            points[7] = new Point(X, Y);

            if (!inBounds)
            {
                e.Graphics.DrawLines(pen, points);

                if (X < X1 && Y < Y1) rect = new Rectangle(X, Y, X1 - X, Y1 - Y);
                if (X1 < X && Y1 < Y) rect = new Rectangle(X1, Y1, X - X1, Y - Y1);
                if (X1 < X && Y < Y1) rect = new Rectangle(X1, Y, X - X1, Y1 - Y);
                if (X < X1 && Y1 < Y) rect = new Rectangle(X, Y1, X1 - X, Y - Y1);

            }
            else
                e.Graphics.DrawRectangle(pen, rect);
        }
    }
}
