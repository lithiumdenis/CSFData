using System;

namespace Lobachevsky_method
{
    class Program
    {
        static int Lobachevsky(int flag, int iteration, int iteration_max, double upper_limit, double lower_limit)
        {
            Console.Write("Введите порядок полинома: ");
            int n = Convert.ToInt32(Console.ReadLine());
            double root, sum, val;
            double[] a = new double[n + 1];
            double[] b = new double[n + 1];
            double[] c = new double[n + 1];

            for (int i = 0; i <= n; i++)
            {
                Console.Write("Введите a[{0}]: ", i);
                a[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Введите eps (#.##): ");
            double eps = Convert.ToDouble(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                a[i] /= a[0];
                c[i] = a[i];
            }

            a[0] = 1.0;
            b[0] = 1.0;
            c[0] = 1.0;

            do
            {
                iteration++;
                for (int i = 1; i <= n; i++)
                {
                    sum = 0.0;
                    for (int k = 1; k <= i; k++)
                    {
                        if ((i + k) <= n)
                        {
                            sum += Math.Pow(-1.0, k) * c[i + k] * c[i - k];
                        }
                    }
                    b[i] = Math.Pow(-1.0, i) * (c[i] * c[i] + 2 * sum);
                }

                for (int i = 1; i <= n; i++)
                {
                    if (b[i] != 0)
                    {
                        if ((Math.Abs(b[i]) > upper_limit) || (Math.Abs(b[i]) < lower_limit))
                            flag = 1;
                    }
                }

                if (flag != 1)
                {
                    for (int i = 1; i <= n; i++)
                    {
                        c[i] = b[i];
                    }
                }

                if (iteration > iteration_max)
                {
                    Console.WriteLine("Не найден корень среди {0} итераций. ", iteration_max);
                    return 0;
                }
            } while (flag == 0);

            Console.WriteLine("Номер итерации = {0}", iteration);

            for (int i = 1; i <= n; i++)
            {
                if (b[i] == 0)
                {
                    Console.WriteLine("Ошибка");
                    return 0;
                }
            }

            for (int i = 1; i <= n; i++)
            {
                root = Math.Pow(Math.Abs(b[i] / b[i - 1]), Math.Pow(2.0, (-iteration)));
                val = Math.Abs((b[i] / b[i - 1]) - Math.Pow(c[i] / c[i - 1], 2));
                if (val > eps)
                    Console.WriteLine("Найден корень: {0}", root);
                else
                    Console.WriteLine("Неточный корень: {0}", root);
            }
            return 0;
        }

        static void Main(string[] args)
        {
            Lobachevsky(0, 0, 100, 1e35, 1e-35);
        }
    }
}