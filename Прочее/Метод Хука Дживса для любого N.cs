using System;

namespace Метод_Хука_Дживса
{
    class Program
    {
        static double f(double[] x)
        {
            return Math.Pow(x[0], 2) - x[0] * x[1] + 3 * Math.Pow(x[1], 2) - x[0];
        }

        static void Main(string[] args)
        {
            // Шаг 1
            // Параметры
            double eps = 0.1;
            double h = 0.2;
            double d = 2;
            double m = 0.5;
            int n = 2;
            int i = 0;
            double fx1 = 0;
            double fxp = 0;
            double fz = 0;
            // Вектор нулевой
            double[] x0 = new double[n];
            // Вектор х1 инициализируем
            double[] x1 = new double[n];
            // Вектор Z инициализируем
            double[] z = new double[n];
            // Вектор xp инициализируем
            double[] xp = new double[n];

            SHAG2:
            // Шаг 2
            x1 = (double[])x0.Clone();
            z = (double[])x0.Clone();
            fx1 = f(x1);

            SHAG3:
            // Шаг 3
            i = 0;

            SHAG4:
            // Шаг 4
            x1[i] += h;
            fx1 = f(x1);

            // Шаг 5
            fz = f(z);
            if (fx1 < fz)
            {
                //Хороший шаг
                z = (double[])x1.Clone();
                goto SHAG7;
            }
            else
            {
                //Шаг в обратном направлении
                x1[i] -= 2 * h;
                fx1 = f(x1);
            }

            // Шаг 6
            if (fx1 < fz)
            {
                //Шаг в отрицательном направлении удачный
                z = (double[])x1.Clone();
                fx1 = f(x1);
            }
            else
            {
                //Все направления неудачны
                x1[i] += h;
                z = (double[])x1.Clone();
                fx1 = f(x1);
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
                z = (double[])x1.Clone();
            }

            // Шаг 8
            //Если хоть чем-то отличаются, то prov > 0
            double prov = 0;
            for (int k = 0; k < n; k++)
                if (Math.Round(x1[k], 2) != Math.Round(x0[k], 2))
                    prov++;

            if (prov == 0)
            {
                //Исследующий поиск был неудачным
                h = h / d;
                goto SHAG3;
            }
            else
            // Шаг 9
            {
                for (int k = 0; k < n; k++)
                    xp[k] = x1[k] + m * (x1[k] - x0[k]);
                fxp = f(xp);
            }

            // Шаг 10
            if (fxp < fx1)
            {
                //Поиск удачен
                x0 = (double[])xp.Clone();
            }
            else
            {
                x0 = (double[])x1.Clone();
            }

            // Шаг 11
            if (h < eps)
            {
                //Соберем строчку для вывода
                string output = "";
                for (int k = 0; k < n; k++)
                {
                    if(k == n - 1)
                        output += x1[k] + "";
                    else
                        output += x1[k] + ", ";
                }

                //end
                Console.WriteLine("Метод Хука Дживса для заданной функции с любым N");
                Console.WriteLine("x* = {" + output + "}; fmin = " + f(x1));
            }
            else
            {
                goto SHAG2;
            }
        }
    }
}