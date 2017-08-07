using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Проекции
{
    public class TSide
    {
        public int[] p = new int[4];  // номера вершин
    }

    //Абстрактный класс
    public abstract class TBody
    {
        public double[][] Vertexs;  // массив начальных положений вершин всех тел
        public double[][] V;        // массив текущих положений вершин всех тел
        public TSide[] Sides;       // массив граней всех тел
        public abstract void CreateBody();
    }

    //Класс Тетраэдра, Реализация TBody 1
    public class BodyTetraedr : TBody
    {
        public override void CreateBody()
        {
            Vertexs = new double[4][];
            V = new double[4][];

            for (int i = 0; i < 4; i++)
            {
                Vertexs[i] = new double[4];
                V[i] = new double[4];
            }
            //Положение точки 0 в пространстве
            Vertexs[0][0] = 1;
            Vertexs[0][1] = -1;
            Vertexs[0][2] = -1;
            //Положение точки 1 в пространстве
            Vertexs[1][0] = 1;
            Vertexs[1][1] = 1;
            Vertexs[1][2] = 1;
            //Положение точки 2 в пространстве
            Vertexs[2][0] = -1;
            Vertexs[2][1] = -1;
            Vertexs[2][2] = 1;
            //Положение точки 3 в пространстве
            Vertexs[3][0] = -1;
            Vertexs[3][1] = 1;
            Vertexs[3][2] = -1;
            
            for (int i = 0; i < 4; i++)
                Vertexs[i][3] = 1;

            Sides = new TSide[4];
            for (int i = 0; i <= 3; i++)
                Sides[i] = new TSide();

            Sides[0].p[0] = 0; 
            Sides[0].p[1] = 1; 
            Sides[0].p[2] = 2;
            
            Sides[1].p[0] = 1; 
            Sides[1].p[1] = 3; 
            Sides[1].p[2] = 2;
            
            Sides[2].p[0] = 0; 
            Sides[2].p[1] = 2; 
            Sides[2].p[2] = 3;
           
            Sides[3].p[0] = 0; 
            Sides[3].p[1] = 3; 
            Sides[3].p[2] = 1;
        }
    }

    //Класс Гексаэдра, Реализация TBody 2
    public class BodyHexaedr : TBody
    {
        public override void CreateBody()
        {
            Vertexs = new double[8][];
            V = new double[8][];

            for (int i = 0; i < 8; i++)
            {
                Vertexs[i] = new double[4];
                V[i] = new double[4];
            }
            //Положение точки 0 в пространстве (x, y, z)
            Vertexs[0][0] = -1;
            Vertexs[0][1] = -1;
            Vertexs[0][2] = -1;
            //Положение точки 1 в пространстве
            Vertexs[1][0] = 1;
            Vertexs[1][1] = -1;
            Vertexs[1][2] = -1;
            //Положение точки 2 в пространстве
            Vertexs[2][0] = 1;
            Vertexs[2][1] = 1;
            Vertexs[2][2] = -1;
            //Положение точки 3 в пространстве
            Vertexs[3][0] = -1;
            Vertexs[3][1] = 1;
            Vertexs[3][2] = -1;
            //Положение точки 4 в пространстве
            Vertexs[4][0] = -1;
            Vertexs[4][1] = -1;
            Vertexs[4][2] = 1;
            //Положение точки 5 в пространстве
            Vertexs[5][0] = 1;
            Vertexs[5][1] = -1;
            Vertexs[5][2] = 1;
            //Положение точки 6 в пространстве
            Vertexs[6][0] = 1;
            Vertexs[6][1] = 1;
            Vertexs[6][2] = 1;
            //Положение точки 7 в пространстве
            Vertexs[7][0] = -1;
            Vertexs[7][1] = 1;
            Vertexs[7][2] = 1;

            for (int i = 0; i < 8; i++)
                Vertexs[i][3] = 1;

            Sides = new TSide[6];
            for (int i = 0; i <= 5; i++)
                Sides[i] = new TSide();

            Sides[0].p[0] = 3; 
            Sides[0].p[1] = 2;
            Sides[0].p[2] = 1; 
            Sides[0].p[3] = 0;
            
            Sides[1].p[0] = 4; 
            Sides[1].p[1] = 5;
            Sides[1].p[2] = 6; 
            Sides[1].p[3] = 7;
           
            Sides[2].p[0] = 5; 
            Sides[2].p[1] = 1;
            Sides[2].p[2] = 2; 
            Sides[2].p[3] = 6;
            
            Sides[3].p[0] = 6; 
            Sides[3].p[1] = 2;
            Sides[3].p[2] = 3; 
            Sides[3].p[3] = 7;
            
            Sides[4].p[0] = 3; 
            Sides[4].p[1] = 0; 
            Sides[4].p[2] = 4; 
            Sides[4].p[3] = 7;
            
            Sides[5].p[0] = 0; 
            Sides[5].p[1] = 1; 
            Sides[5].p[2] = 5; 
            Sides[5].p[3] = 4;
        }
    }

    //Класс Октаэдра, Реализация TBody 3
    public class BodyOctaedr : TBody
    {
        public override void CreateBody()
        {
            Vertexs = new double[6][];
            V = new double[6][];

            for (int i = 0; i < 6; i++)
            {
                Vertexs[i] = new double[4];
                V[i] = new double[4];
            }

            //Положение точки 0 в пространстве (x, y, z)
            Vertexs[0][0] = 0;
            Vertexs[0][1] = 1;
            Vertexs[0][2] = 0;
            //Положение точки 1 в пространстве
            Vertexs[1][0] = 1;
            Vertexs[1][1] = 0;
            Vertexs[1][2] = 0;
            //Положение точки 2 в пространстве
            Vertexs[2][0] = 0;
            Vertexs[2][1] = 0;
            Vertexs[2][2] = 1;

            for(int i = 3; i < 6; i++)
            {
                Vertexs[i][0] = -Vertexs[i - 3][0];
                Vertexs[i][1] = -Vertexs[i - 3][1];
                Vertexs[i][2] = -Vertexs[i - 3][2];
            }

            for (int i = 0; i < 3; i++)
                Vertexs[i][3] = 1;

            Sides = new TSide[9];
            for (int i = 0; i < 9; i++)
                Sides[i] = new TSide();

            Sides[0].p[0] = 1;
            Sides[0].p[1] = 0;
            Sides[0].p[2] = 2;
            
            Sides[1].p[0] = 2;
            Sides[1].p[1] = 0;
            Sides[1].p[2] = 4;

            Sides[2].p[0] = 4;
            Sides[2].p[1] = 0;
            Sides[2].p[2] = 5;

            Sides[3].p[0] = 5;
            Sides[3].p[1] = 0;
            Sides[3].p[2] = 1;

            for (int i = 4; i < 8; i++)
            {
                Sides[i].p[2] = 3;
            }

            Sides[4].p[0] = 1;
            Sides[4].p[1] = 2;

            Sides[5].p[0] = 1;
            Sides[5].p[1] = 1;

            Sides[6].p[0] = 4;
            Sides[6].p[1] = 5;
            
            Sides[7].p[0] = 5;
            Sides[7].p[1] = 1;

            for (int i = 0; i < 9; i++)
                Sides[i].p[3] = Sides[i].p[0];
        }
    }

    //Класс Икосаэдра, Реализация TBody 4
    public class BodyIcosahedron : TBody
    {
        public override void CreateBody()
        {
            Vertexs = new double[12][];
            V = new double[12][];

            for (int i = 0; i < 12; i++)
            {
                Vertexs[i] = new double[4];
                V[i] = new double[4];
            }

            double VV = .525731112119133606;
            double WW = .850650808352039932;

            //Положение точки 0 в пространстве (x, y, z)
            Vertexs[0][0] = -VV;
            Vertexs[0][1] = 0;
            Vertexs[0][2] = WW;
            //Положение точки 1 в пространстве
            Vertexs[1][0] = VV;
            Vertexs[1][1] = 0;
            Vertexs[1][2] = WW;
            //Положение точки 2 в пространстве
            Vertexs[2][0] = -VV;
            Vertexs[2][1] = 0;
            Vertexs[2][2] = -WW;

            //Положение точки 3 в пространстве (x, y, z)
            Vertexs[3][0] = VV;
            Vertexs[3][1] = 0;
            Vertexs[3][2] = -WW;
            //Положение точки 4 в пространстве
            Vertexs[4][0] = 0;
            Vertexs[4][1] = WW;
            Vertexs[4][2] = VV;
            //Положение точки 5 в пространстве
            Vertexs[5][0] = 0;
            Vertexs[5][1] = WW;
            Vertexs[5][2] = -VV;

            //Положение точки 6 в пространстве (x, y, z)
            Vertexs[6][0] = 0;
            Vertexs[6][1] = -WW;
            Vertexs[6][2] = VV;
            //Положение точки 7 в пространстве
            Vertexs[7][0] = 0;
            Vertexs[7][1] = -WW;
            Vertexs[7][2] = -VV;
            //Положение точки 8 в пространстве
            Vertexs[8][0] = WW;
            Vertexs[8][1] = VV;
            Vertexs[8][2] = 0;

            //Положение точки 9 в пространстве (x, y, z)
            Vertexs[9][0] = -WW;
            Vertexs[9][1] = VV;
            Vertexs[9][2] = 0;
            //Положение точки 10 в пространстве
            Vertexs[10][0] = WW;
            Vertexs[10][1] = -VV;
            Vertexs[10][2] = 0;
            //Положение точки 11 в пространстве
            Vertexs[11][0] = -WW;
            Vertexs[11][1] = -VV;
            Vertexs[11][2] = 0;

            Sides = new TSide[20];
            for (int i = 0; i < 20; i++)
                Sides[i] = new TSide();

            Sides[0].p[0] = 0;
            Sides[0].p[1] = 4;
            Sides[0].p[2] = 1;

            Sides[1].p[0] = 0;
            Sides[1].p[1] = 9;
            Sides[1].p[2] = 4;

            Sides[2].p[0] = 9;
            Sides[2].p[1] = 5;
            Sides[2].p[2] = 4;

            Sides[3].p[0] = 4;
            Sides[3].p[1] = 5;
            Sides[3].p[2] = 8;

            Sides[4].p[0] = 4;
            Sides[4].p[1] = 8;
            Sides[4].p[2] = 1;

            Sides[5].p[0] = 8;
            Sides[5].p[1] = 1;
            Sides[5].p[2] = 10;

            Sides[6].p[0] = 8;
            Sides[6].p[1] = 10;
            Sides[6].p[2] = 3;

            Sides[7].p[0] = 5;
            Sides[7].p[1] = 8;
            Sides[7].p[2] = 3;

            Sides[8].p[0] = 5;
            Sides[8].p[1] = 3;
            Sides[8].p[2] = 2;

            Sides[9].p[0] = 2;
            Sides[9].p[1] = 3;
            Sides[9].p[2] = 7;

            Sides[10].p[0] = 7;
            Sides[10].p[1] = 3;
            Sides[10].p[2] = 10;

            Sides[11].p[0] = 7;
            Sides[11].p[1] = 10;
            Sides[11].p[2] = 6;

            Sides[12].p[0] = 7;
            Sides[12].p[1] = 6;
            Sides[12].p[2] = 11;

            Sides[13].p[0] = 11;
            Sides[13].p[1] = 6;
            Sides[13].p[2] = 0;

            Sides[14].p[0] = 0;
            Sides[14].p[1] = 6;
            Sides[14].p[2] = 1;

            Sides[15].p[0] = 6;
            Sides[15].p[1] = 10;
            Sides[15].p[2] = 1;

            Sides[16].p[0] = 9;
            Sides[16].p[1] = 11;
            Sides[16].p[2] = 0;

            Sides[17].p[0] = 9;
            Sides[17].p[1] = 2;
            Sides[17].p[2] = 11;

            Sides[18].p[0] = 9;
            Sides[18].p[1] = 5;
            Sides[18].p[2] = 2;

            Sides[19].p[0] = 7;
            Sides[19].p[1] = 11;
            Sides[19].p[2] = 2;
        }
    }

    //Класс Тело
    public class Body
    {
        int I2, J2; //Высота и ширина запущенной формы
        double X1, X2, Y1, Y2; //Параметры размера тела

        public static Color clr = Color.Black;   //Цвет тела
        public static bool drawAxes = false;     //Рисовать оси
        public static double SizeBody = 4;       //Размер тела
        public static double Alf = Math.PI/6;    //Для Rotate fi
        public static double Bet = -Math.PI/6;   //Для Rotate fi
        public static byte flBody = 1;           //Текущее тело
        public static double Xs = 0;             //1-я точка схода
        public static double Zs = 0;             //2-я точка схода
        public TBody[] body = new TBody[4];      //Множество тел
        public static byte flRotate = 1;         //Поворачивать тело или оси
        public static double Alf1 = 0;           //Для Rotate fi двигаем тело
        public static double Bet1 = 0;           //Для Rotate fi

        //Конструктор класса
        public Body(Rectangle r)
        {
            body[0] = new BodyTetraedr();
            body[0].CreateBody();
            
            body[1] = new BodyHexaedr();
            body[1].CreateBody();

            body[2] = new BodyOctaedr();
            body[2].CreateBody();

            body[3] = new BodyIcosahedron();
            body[3].CreateBody();

            I2 = r.Width; 
            J2 = r.Height;
            
            X1 = -SizeBody; 
            Y1 = -SizeBody;
            
            X2 = SizeBody; 
            Y2 = SizeBody;
        }

        //JJ переход в бумажные координаты
        int JJ(double y)
        {
            return (int)Math.Round((y - Y2) * (J2 - 0) / (Y1 - Y2));
        }

        //II переход в бумажные координаты
        int II(double x)
        {
            return (int)Math.Round((x - X1) * (I2 - 0) / (X2 - X1));
        }

        //Преобразование для осей координат
        Point IJ(double[] Vt)
        {
            Vt = Rotate(Vt, 2, Bet, 0, 0);
            Vt = Rotate(Vt, 1, Alf, 0, 0);
            Vt = Rotate(Vt, 3, Alf + Bet, 0, 0);
            Point Result = new Point(II(Vt[0]), JJ(Vt[1]));
            return Result;
        }

        //Создать Матрицу
        public static double[,] Matr(int k, double fi, double p, double r)
        {
            double[,] M = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };

            switch (k)
            {
                case 1:  // Матрица поворота вокруг оси OX
                    M[1, 1] = Math.Cos(fi); 
                    M[1, 2] = Math.Sin(fi);
                    M[2, 1] = -Math.Sin(fi); 
                    M[2, 2] = Math.Cos(fi);
                    break;
                case 2:  // Матрица поворота вокруг оси OY
                    M[0, 0] = Math.Cos(fi); 
                    M[0, 2] = -Math.Sin(fi);
                    M[2, 0] = Math.Sin(fi); 
                    M[2, 2] = Math.Cos(fi);
                    break;
                case 3: // Матрица поворота вокруг оси OZ
                    M[0, 0] = Math.Cos(fi); 
                    M[0, 1] = Math.Sin(fi);
                    M[1, 0] = -Math.Sin(fi);
                    M[1, 1] = Math.Cos(fi);
                    break;
                case 4: // перспективное преобразование
                    M[1, 3] = p;
                    M[2, 3] = r;
                    break;
            }
            return M;
        }

        //Перемножить Матрицы
        static double[] VM_Mult(double[] A, double[,] B)
        {
            double[] Result = new double[4];
            
            for (int j = 0; j < 4; j++)
            {
                Result[j] = 0;
                for (int k = 0; k < 4; k++)
                    Result[j] += A[k] * B[k, j];
            }

            if (Result[3] != 0)
                for (int j = 0; j < 3; j++)
                    Result[j] /= Result[3];
            Result[3] = 1;
            return Result;
        }

        //Выполнение Поворота 
        public static double[] Rotate(double[] V, int k, double fi, double p, double r)
        {
            double[,] M = Matr(k, fi, p, r);
            double[] Result = VM_Mult(V, M);
            return Result;
        }

        //Выполнить Поворот
        void RotateBody()
        {
            byte k = flBody;
            for (int i = 0; i < body[k].Vertexs.Length; i++)
            {
                for (int j = 0; j <= 3; j++)
                    body[k].V[i][j] = body[k].Vertexs[i][j];

                body[k].V[i] = Rotate(body[k].V[i], 2, Bet1, 0, 0);
                body[k].V[i] = Rotate(body[k].V[i], 1, Alf1, 0, 0);
                body[k].V[i] = Rotate(body[k].V[i], 3, Alf1 + Bet1, 0, 0);
                body[k].V[i] = Rotate(body[k].V[i], 4, 0, Xs, Zs);
                body[k].V[i] = Rotate(body[k].V[i], 2, Bet, 0, 0);
                body[k].V[i] = Rotate(body[k].V[i], 1, Alf, 0, 0);
                body[k].V[i] = Rotate(body[k].V[i], 3, Alf + Bet, 0, 0);
            }
        }

        // Точки схода
        void DrawProspect(Graphics g) 
        {
            double[] V1 = new double[4];
            double[] V2 = new double[4];
            int u, v;
            if (Zs != 0)
            {
                V2[0] = 0; V2[1] = 0; V2[2] = 1 / Zs; V2[3] = 1;
                V2 = Rotate(V2, 2, Bet, 0, 0);
                V2 = Rotate(V2, 1, Alf, 0, 0);
                V2 = Rotate(V2, 3, Alf + Bet, 0, 0);
                u = II(V2[0]); v = JJ(V2[1]);
                for (int i = 0; i <= 3; i++)
                {
                    int a2 = II(body[flBody].V[i][0]);
                    int b2 = JJ(body[flBody].V[i][1]);
                    g.DrawLine(Pens.Silver, u, v, a2, b2);
                }
                g.DrawRectangle(Pens.Black, u - 2, v - 2, 4, 4);
            }
            if (Xs != 0)
            {
                V1[1] = 1 / Xs; V1[0] = 0; V1[2] = 0; V1[3] = 1;
                V1 = Rotate(V1, 2, Bet, 0, 0);
                V1 = Rotate(V1, 1, Alf, 0, 0);
                V1 = Rotate(V1, 3, Alf + Bet, 0, 0);
                u = II(V1[0]); v = JJ(V1[1]);
                for (int i = 0; i <= 3; i++)
                    if (i != 3)
                    {
                        int a2 = II(body[flBody].V[i][0]);
                        int b2 = JJ(body[flBody].V[i][1]);
                        g.DrawLine(Pens.Silver, u, v, a2, b2);
                    }
                g.DrawRectangle(Pens.Black, u - 2, v - 2, 4, 4);
            }
        }

        //Так как направление оси Z объекта прямо противоположно направлению визирования, 
        //можно рассматривать только проекцию вектора внешней нормали на ось Z. 
        //Если проекция отрицательна, грань нелицевая, не видна наблюдателю, и ее ребра не включаются в число видимых.

        // Вычисление z-проекции нормали к грани многогранника
        // NormalZ = (X2 - X1) * (Y3 - Y1) - (Y2 - Y1) * (X3 - X1)
        public double Norm(int k)
        {
            double vx = body[flBody].V[body[flBody].Sides[k].p[2]][0] - body[flBody].V[body[flBody].Sides[k].p[1]][0];
            double vy = body[flBody].V[body[flBody].Sides[k].p[2]][1] - body[flBody].V[body[flBody].Sides[k].p[1]][1];
            double wx = body[flBody].V[body[flBody].Sides[k].p[3]][0] - body[flBody].V[body[flBody].Sides[k].p[1]][0];
            double wy = body[flBody].V[body[flBody].Sides[k].p[3]][1] - body[flBody].V[body[flBody].Sides[k].p[1]][1];
            return vx * wy - vy * wx;
        }

        //Рисование Тела
        public void DrawBody(Graphics g)
        {
            Pen pen = new Pen(clr, 2);
            g.SmoothingMode = SmoothingMode.HighQuality; //Сглаживание

            for (int i = 0; i < body[flBody].Sides.Length; i++)
            {
                //точка 1 грани
                int a1 = II(body[flBody].V[body[flBody].Sides[i].p[0]][0]);
                int b1 = JJ(body[flBody].V[body[flBody].Sides[i].p[0]][1]);

                //точка 2 грани
                int a2 = II(body[flBody].V[body[flBody].Sides[i].p[1]][0]);
                int b2 = JJ(body[flBody].V[body[flBody].Sides[i].p[1]][1]);

                //точка 3 грани
                int a3 = II(body[flBody].V[body[flBody].Sides[i].p[2]][0]);
                int b3 = JJ(body[flBody].V[body[flBody].Sides[i].p[2]][1]);

                if (Norm(i) > 0)
                {
                    pen.Width = 3;
                    pen.DashStyle = DashStyle.Solid;
                }
                else
                {
                    pen.Width = 1;
                    pen.DashStyle = DashStyle.Dash;
                }

                if ((body[flBody].Sides.Length == 4) || (body[flBody].Sides.Length == 9) || (body[flBody].Sides.Length == 20)) // для тетраэдра и октаэдра и икосаэдра
                {
                    g.DrawLine(pen, a1, b1, a2, b2);
                    g.DrawLine(pen, a2, b2, a3, b3);
                    g.DrawLine(pen, a3, b3, a1, b1);
                }

                if (body[flBody].Sides.Length == 6) // для гексаэдра
                {
                    //точка 4 грани
                    int a4 = II(body[flBody].V[body[flBody].Sides[i].p[3]][0]);
                    int b4 = JJ(body[flBody].V[body[flBody].Sides[i].p[3]][1]);

                    g.DrawLine(pen, a1, b1, a2, b2);
                    g.DrawLine(pen, a2, b2, a3, b3);
                    g.DrawLine(pen, a3, b3, a4, b4);
                    g.DrawLine(pen, a4, b4, a1, b1);
                }
            }
            pen.Dispose();
        }

        double[] ToVector(double x, double y, double z)
        {
            double[] Result = new double[4];
            Result[0] = x;
            Result[1] = y;
            Result[2] = z;
            Result[3] = 1;
            return Result;
        }

        // Прорисовка осей
        void DrawAxes(Graphics g)   
        {
            if (drawAxes)
            {
                g.SmoothingMode = SmoothingMode.HighQuality; //Сглаживание

                Pen pen = new Pen(Color.Silver, 2);
                pen.DashStyle = DashStyle.Dash;
                double ax = 7;
                Point P0, P;
                Font font = new Font("Courier", 14);

                P0 = IJ(ToVector(-ax, 0, 0));
                P = IJ(ToVector(ax, 0, 0));
                g.DrawLine(pen, P0, P);
                g.DrawString("X", font, Brushes.Black, P.X, P.Y);

                P0 = IJ(ToVector(0, -ax, 0));
                P = IJ(ToVector(0, ax, 0));
                g.DrawLine(pen, P0, P);
                g.DrawString("Y", font, Brushes.Black, P.X, P.Y);

                P0 = IJ(ToVector(0, 0, -ax));
                P = IJ(ToVector(0, 0, ax));
                g.DrawLine(pen, P0, P);
                g.DrawString("Z", font, Brushes.Black, P.X, P.Y);

                font.Dispose();
                pen.Dispose();
            }
        }

        //Нарисовать
        public void Draw(Graphics g)
        {
            g.Clear(Color.White);
            RotateBody(); // изображение тела
            DrawBody(g);  // рисование тела
            DrawProspect(g); // точки схода
            if (drawAxes == true)
            {
                DrawAxes(g); // прорисовка осей
            }
        }
    }
}