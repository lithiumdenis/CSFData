using System;

namespace Метод_Хука_Дживса
{
    class Program
    {
        public static double f(double x1, double x2)
        {
            return Math.Pow(x1, 2) - x1 * x2 + 3 * Math.Pow(x2, 2) - x1;
        }

        static void Main(string[] args)
        {
            // Шаг 1
            // Параметры
            double eps = 0.1;
            double h = 0.2;
            double d = 2;
            double m = 0.5;
            double n = 2;
            int i = 0;
            double fx1 = 0;
            double fxp = 0;
            double fz = 0;
            // Вектор нулевой
            Vect X0 = new Vect();
            X0.left = 0;
            X0.right = 0;
            // Вектор х1 инициализируем
            Vect X1 = new Vect();
            // Вектор Z инициализируем
            Vect Z = new Vect();
            // Вектор xp инициализируем
            Vect XP = new Vect();

            SHAG2:
            // Шаг 2
            X1.left = X0.left;
            X1.right = X0.right;
            Z.left = X0.left;
            Z.right = X0.right;
            fx1 = f(X1.left, X1.right);

            SHAG3:
            // Шаг 3
            i = 0;

            SHAG4:
            // Шаг 4
            if (i == 0)
            {
                X1.left += h;
                X1.right += 0;

            }
            else //i == 1
            {
                X1.left += 0;
                X1.right += h;
            }
            fx1 = f(X1.left, X1.right);

            // Шаг 5
            fz = f(Z.left, Z.right);
            if (fx1 < fz)
            {
                //Хороший шаг
                Z.left = X1.left;
                Z.right = X1.right;
                goto SHAG7;
            }
            else
            {
                //Шаг в обратном направлении
                if (i == 0)
                {
                    X1.left -= 2 * h;
                    X1.right += 0;

                }
                else //i == 1
                {
                    X1.left += 0;
                    X1.right -= 2 * h;
                }
                fx1 = f(X1.left, X1.right);
            }

            // Шаг 6
            if (fx1 < fz)
            {
                //Шаг в отрицательном направлении удачный
                Z.left = X1.left;
                Z.right = X1.right;
                fx1 = f(X1.left, X1.right);
            }
            else
            {
                //Все направления неудачны
                if (i == 0)
                {
                    X1.left += h;
                    X1.right += 0;

                }
                else //i == 1
                {
                    X1.left += 0;
                    X1.right += h;
                }
                Z.left = X1.left;
                Z.right = X1.right;
                fx1 = f(X1.left, X1.right);
            }

            SHAG7:
            // Шаг 7
            if (i < n - 1)
            {
                i++;
                goto SHAG4;
            }
            else
            {
                Z.left = X1.left;
                Z.right = X1.right;
            }

            // Шаг 8
            if (Math.Round(X1.left, 2) == Math.Round(X0.left, 2) && Math.Round(X1.right, 2) == Math.Round(X0.right, 2))
            {
                //Исследующий поиск был неудачным
                h = h / d;
                goto SHAG3;
            }
            else
            // Шаг 9
            {
                XP.left = X1.left + m * (X1.left - X0.left);
                XP.right = X1.right + m * (X1.right - X0.right);
                fxp = f(XP.left, XP.right);
            }

            // Шаг 10
            if (fxp < fx1)
            {
                //Поиск удачен
                X0.left = XP.left;
                X0.right = XP.right;
            }
            else
            {
                X0.left = X1.left;
                X0.right = X1.right;
            }

            // Шаг 11
            if (h < eps)
            {
                //end
                Console.WriteLine("Метод Хука Дживса для заданной функции с N = 2");
                Console.WriteLine("x* = {" + X1.left + ", " + X1.right + "}; fmin = " + f(X1.left, X1.right));
            }
            else
            {
                goto SHAG2;
            }
        }
    }

    class Vect
    {
        public double left;
        public double right;
    }
}