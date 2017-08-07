using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuadraticSurfaces
{
    public struct TVector
    {
        public double x, y, z;
        public TVector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class TSides
    {
        public TVector[] P = new TVector[4];
        public double N;
        public double Zmin;
    }

    public class TDraw3D
    {
        public static int SurfaceNumber = 0;    // Фигура
        public static int sg = 0;               // Надо ли фигуре 2х точек
        public static int L = 0;                // Количество точек
        public static bool drawAxis = false;    // Показывать ли оси
        public static bool typeFace = false;    // true - Грани, false - Сетка

        public static bool clrBlue = true;      // Синий
        public static bool clrPurple = false;   // Фиолетовый
        public static bool clrYellow = false;   // Желтый
        public static bool clrRed = false;      // Красный
        public static bool clrGreen = false;    // Зеленый

        public static double Xmin;    // прямоугольник
        public static double Xmax;
        public static double Ymin;
        public static double Ymax;

        public static double fX1;     // куб в мировой СК
        public static double fX2;
        public static double fY1;
        public static double fY2;

        public static double FA;

        public static int fn;          // число разбиений по X
        public int n
        {
            get { return fn; }
            set { fn = value; SetV(); }
        }

        public static int fm;          // число разбиений по Y
        public int m
        {
            get { return fm; }
            set { fm = value; SetV(); }
        }

        public static double Alf;
        public static double Bet;
        public static double Gam;

        public static TSides[] Sides;
        public static TVector[,] V;
        public static Bitmap bitmap;
        public SolidBrush myBrush;

        public TDraw3D(int VW, int VH) //Конструктор
        {
            fX1 = 0;
            fX2 = Math.PI;
            fY1 = -Math.PI;
            fY2 = Math.PI;

            Xmin = -3; //Размеры
            Xmax = 3;
            Ymin = -3;
            Ymax = 3;

            fn = 36;
            fm = 36;

            Alf = 4.31;
            Bet = 4.92;
            Gam = Alf + Bet;

            FA = -0.1;
            bitmap = new Bitmap(VW, VH);
            SetV();
            myBrush = new SolidBrush(Color.White);
        }

        public static void SetV()
        {
            double hx = (fX2 - fX1) / (fm - 1);
            double hy = (fY2 - fY1) / (fn - 1);

            if (sg == 0)
                L = fn * fm;
            else
                L = 2 * fn * fm;

            Sides = new TSides[L];
            int q = -1;
            double t1 = 0;
            double t2 = 0;

            for (int j = 0; j < fn; j++)
                for (int i = 0; i < fm; i++)
                {
                    q++;
                    Sides[q] = new TSides();

                    t1 = fX1 + hx * (j + 0);
                    t2 = fY1 + hy * (i + 0);
                    Sides[q].P[0] = Rotate(Alf, Bet, Gam, 0, XYZtoVector(SurfaceNumber, t1, t2, 0));

                    t1 = fX1 + hx * (j + 0);
                    t2 = fY1 + hy * (i + 1);
                    Sides[q].P[1] = Rotate(Alf, Bet, Gam, 0, XYZtoVector(SurfaceNumber, t1, t2, 0));

                    t1 = fX1 + hx * (j + 1);
                    t2 = fY1 + hy * (i + 1);
                    Sides[q].P[2] = Rotate(Alf, Bet, Gam, 0, XYZtoVector(SurfaceNumber, t1, t2, 0));

                    t1 = fX1 + hx * (j + 1);
                    t2 = fY1 + hy * (i + 0);
                    Sides[q].P[3] = Rotate(Alf, Bet, Gam, 0, XYZtoVector(SurfaceNumber, t1, t2, 0));

                    if (sg != 0)
                    {
                        Sides[q + fn * fm] = new TSides();

                        t1 = fX1 + hx * (j + 0);
                        t2 = fY1 + hy * (i + 0);
                        Sides[q + fn * fm].P[0] = Rotate(Alf, Bet, Gam, 1, XYZtoVector(SurfaceNumber, t1, t2, 0));

                        t1 = fX1 + hx * (j + 0);
                        t2 = fY1 + hy * (i + 1);
                        Sides[q + fn * fm].P[1] = Rotate(Alf, Bet, Gam, 1, XYZtoVector(SurfaceNumber, t1, t2, 0));

                        t1 = fX1 + hx * (j + 1);
                        t2 = fY1 + hy * (i + 1);
                        Sides[q + fn * fm].P[2] = Rotate(Alf, Bet, Gam, 1, XYZtoVector(SurfaceNumber, t1, t2, 0));

                        t1 = fX1 + hx * (j + 1);
                        t2 = fY1 + hy * (i + 0);
                        Sides[q + fn * fm].P[3] = Rotate(Alf, Bet, Gam, 1, XYZtoVector(SurfaceNumber, t1, t2, 0));
                    }
                }
        }

        private double XX(int I)
        {
            return Xmin + I * (Xmax - Xmin) / bitmap.Width;
        }

        private double YY(int J)
        {
            return Ymax + J * (Ymin - Ymax) / bitmap.Height;
        }

        void DrawGrid(Graphics g)
        {
            SetV();

            Pen p1 = new Pen(Color.Black);
            Point[] w = new Point[4];

            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j <= 3; j++)
                    w[j] = VectorToPoint(Sides[i].P[j]);
                g.DrawPolygon(p1, w);
            }
        }

        void DrawSides(Graphics g)
        {
            SetV();

            for (int i = 0; i < L; i++)
            {
                Sides[i].N = Norm(Sides[i].P[0], Sides[i].P[1], Sides[i].P[2]);
                Sides[i].Zmin = Z_Min(Sides[i]);
            }

            Sort();

            for (int i = 0; i < L; i++)
            {
                Point[] w = new Point[4];
                for (int j = 0; j <= 3; j++)
                    w[j] = VectorToPoint(Sides[i].P[j]);

                byte R = 0;
                byte G = (byte)(255 * Math.Abs(Sides[i].N));
                byte B = (byte)(255 * Math.Abs(Sides[i].N));

                //Синий
                if (clrBlue == true)
                {
                    R = 0;
                    G = (byte)(255 * Math.Abs(Sides[i].N));
                    B = (byte)(255 * Math.Abs(Sides[i].N));
                }

                //Фиолетовый
                if (clrPurple == true)
                {
                    R = (byte)(255 * Math.Abs(Sides[i].N));
                    G = 0;
                    B = (byte)(255 * Math.Abs(Sides[i].N));
                }

                //Желтый
                if (clrYellow == true)
                {
                    R = (byte)(255 * Math.Abs(Sides[i].N));
                    G = (byte)(255 * Math.Abs(Sides[i].N));
                    B = 0;
                }

                //Красный
                if (clrRed == true)
                {
                    R = (byte)(255 * Math.Abs(Sides[i].N));
                    G = 0;
                    B = 0;
                }

                //Зеленый
                if (clrGreen == true)
                {
                    R = 0;
                    G = (byte)(255 * Math.Abs(Sides[i].N));
                    B = 0;
                }

                myBrush.Color = Color.FromArgb(R, G, B);
                g.FillPolygon(myBrush, w);
            }
        }

        public void Draw()
        {
            using (Graphics g  = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Color cl = Color.FromArgb(255, 255, 255);
                g.Clear(cl);
                if (typeFace == false)
                    DrawGrid(g);
                else
                    DrawSides(g);
                if (drawAxis == true)
                    Axis(g);
            }
        }

        public void SetAngle(double x, double y) //задание угла левой кнопкой
        {
            Alf = Math.Atan2(y, x);
            Bet = Math.Sqrt((x / 10) * (x / 10) + (y / 10) * (y / 10));
            Gam = Alf + Bet;
        }

        public void SetDelta(MouseEventArgs e0, MouseEventArgs e) //передвижение правой кнопкой
        {
            double dx = XX(e.X) - XX(e0.X);
            double dy = YY(e.Y) - YY(e0.Y);
            Xmin -= dx; Ymin -= dy; Xmax -= dx; Ymax -= dy;
        }

        public static TVector Rotate(double alf, double bet, double gam, byte fl, TVector P)
        {
            if (fl != 0)
                P.y = -P.y;

            double x = P.x * Math.Cos(gam) - P.y * Math.Sin(gam);
            double y = P.x * Math.Sin(gam) + P.y * Math.Cos(gam);
            double z = P.z;

            double t = x * Math.Cos(bet) - z * Math.Sin(bet);
            z = x * Math.Sin(bet) + z * Math.Cos(bet);
            return new TVector(t, y * Math.Cos(alf) - z * Math.Sin(alf), y * Math.Sin(alf) + z * Math.Cos(alf));
        }

        public static double a = 1;
        public static double b = 1;
        public static double c = 1;

        public static TVector XYZtoVector(int n, double t1, double t2, double t3) //Встраивание 2
        {
            switch (n)
            {
                case 0:
                    // эллипсоид
                    return new TVector(a * Math.Sin(t1) * Math.Sin(t2), b * Math.Cos(t1), c * Math.Sin(t1) * Math.Cos(t2));
                case 1:
                    // однополостной гиперболоид
                    return new TVector(a * Ch(t1) * Math.Sin(t2), b * Sh(t1), c * Ch(t1) * Math.Cos(t2));
                case 2:
                    // двуполостной гиперболоид
                    return new TVector(a * Sh(t1) * Math.Sin(t2), b * Ch(t1), c * Sh(t1) * Math.Cos(t2));
                case 3:
                    // эллиптический конус
                    return new TVector(a * t1 * Math.Sin(t2), b * t1, c * t1 * Math.Cos(t2));
                case 4:
                    // гиперболический параболоид
                    return new TVector(a * t1 * Math.Sin(t2), -b * t1 * t1 * Math.Cos(2 * t2), c * t1 * Math.Cos(t2));
                case 5:
                    // эллиптический параболоид
                    return new TVector(a * t1 * Math.Sin(t2), b * t1 * t1, c * t1 * Math.Cos(t2));
                case 6:
                    // эллиптический цилиндр
                    return new TVector(a * Math.Sin(t2), b * t1, c * Math.Cos(t2));
                case 7:
                    // параболический цилиндр
                    return new TVector(a * t2, b * t1, c * t2 * t2);
                case 8:
                    // гиперболический цилиндр
                    return new TVector(a * Sh(t2), c * Ch(t2), b * t1);
                case 9:
                    // тор
                    return new TVector((a + b * Math.Cos(t1)) * Math.Cos(t2), b * Math.Sin(t1), (a + b * Math.Cos(t1)) * Math.Sin(t2));
                default:
                    return new TVector(t1, t2, t3);
            }
        }

        public static double Ch(double x)
        {
            return (Math.Exp(x) + Math.Exp(-x)) / 2.0;
        }

        public static double Sh(double x)
        {
            return (Math.Exp(x) - Math.Exp(-x)) / 2.0;
        }

        public Point VectorToPoint(TVector P)
        {
            Point result = new Point();
            result.X = Convert.ToInt32(bitmap.Width * (P.x - Xmin) / (Xmax - Xmin));
            result.Y = Convert.ToInt32(bitmap.Height * (P.y - Ymax) / (Ymin - Ymax));
            return result;
        }

        public void Axis(Graphics g) // Оси координат
        {
            PointF P0 = new PointF();
            PointF P1 = new PointF();
            Pen p = new Pen(Color.Black, 1);
            Font font = new Font("Courier", 10);

            P0 = VectorToPoint(Rotate(Alf, Bet, Gam, 0, XYZtoVector(10, 0, 0, 0)));
            
            P1 = VectorToPoint(Rotate(Alf, Bet, Gam, 0, XYZtoVector(10, 2, 0, 0)));
            g.DrawLine(p, P0.X, P0.Y, P1.X, P1.Y);
            g.DrawString("X", font, Brushes.Black, P1.X + 3, P1.Y);

            P1 = VectorToPoint(Rotate(Alf, Bet, Gam, 0, XYZtoVector(10, 0, 2, 0)));
            g.DrawLine(p, P0.X, P0.Y, P1.X, P1.Y);
            g.DrawString("Y", font, Brushes.Black, P1.X + 3, P1.Y);

            P1 = VectorToPoint(Rotate(Alf, Bet, Gam, 0, XYZtoVector(10, 0, 0, 2)));
            g.DrawLine(p, P0.X, P0.Y, P1.X, P1.Y);
            g.DrawString("Z", font, Brushes.Black, P1.X + 3, P1.Y);
        }

        public static double Norm(TVector V1, TVector V2, TVector V3)
        {
            TVector A = new TVector();
            TVector B = new TVector();
            double result;

            A.x = V2.x - V1.x; B.x = V3.x - V1.x;
            A.y = V2.y - V1.y; B.y = V3.y - V1.y;
            A.z = V2.z - V1.z; B.z = V3.z - V1.z;

            double u = A.y * B.z - A.z * B.y;
            double v = -A.x * B.z + A.z * B.x;
            double w = A.x * B.y - A.y * B.x;

            double d = Math.Sqrt(u * u + v * v + w * w);
            if (d != 0)
                result = w / d;
            else
                result = 0;
            return result;
        }

        public static double Z_Min(TSides Sides)
        {
            double result = 0;
            for (int i = 0; i <= 3; i++)
                result = result + Sides.P[i].z / 4.0;
            return result;
        }

        public static void Sort()
        {
            int i, j, L = Sides.Length;
            TSides T;
            for (i = 1; i < L; i++)
            {
                for (j = L - 1; j >= i; j--)
                {
                    if (Sides[j - 1].Zmin > Sides[j].Zmin)
                    {
                        T = Sides[j];
                        Sides[j] = Sides[j - 1];
                        Sides[j - 1] = T;
                    }
                }
            }
        }
    }
}