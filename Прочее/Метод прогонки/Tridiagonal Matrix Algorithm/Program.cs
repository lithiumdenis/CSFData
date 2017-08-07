using System;

namespace Tridiagonal_Matrix_Algorithm
{
    class Program
    {
        public static double func(double x)
        {
            return (((Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(1) - Math.Exp(-1))) - 2 * x);
        }

        //n - число уравнений (строк матрицы)
        //b - диагональ, лежащая над главной (нумеруется: [0;n-2])
        //a - главная диагональ матрицы A (нумеруется: [0;n-1])
        //c - диагональ, лежащая под главной (нумеруется: [1;n-1])
        //d - правая часть (столбец)

        public static double[] TridiagonalMatrixAlgorithm(int n, double[] a, double[] c, double[] b, double[] d, double h)
        {
            double[] y = new double[n];
            double[] x = new double[n];
            for (int i = 1; i < x.Length; i++)
            {
                x[i] = i * h;
            }

            double[] v = new double[n];
            double[] u = new double[n];

            v[0] = 0;
            u[0] = 0;

            for (int i = 1; i < x.Length; i++)
            {
                v[i] = -b[i] / (a[i] + c[i] * v[i - 1]);
                u[i] = (d[i] - c[i] * u[i - 1]) / (a[i] + c[i] * v[i - 1]);
            }
            
            y[n - 1] = u[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                y[i] = v[i] * y[i + 1] + u[i];
            }

            return y;
        }

        static void Main(string[] args)
        {
            int N;
            
            REPEAT:
            try
            {
                Console.Write("Введите N: ");
                 N = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ошибка. Попробуйте снова.");
                goto REPEAT;
            }
         
            double h = 1.0 / (N - 1.0);

            double[] A = new double[N];
            double[] B = new double[N];
            double[] C = new double[N];
            double[] D = new double[N];

            A[0] = 1;
            A[N - 1] = 1;

            for (int i = 1; i < N - 1; i++)
                A[i] = 2 + Math.Pow(h, 2);

            B[0] = 0;
            B[N - 2] = -1;

            for (int i = 1; i < N - 2; i++)
                B[i] = -1;

            C[0] = -1;
            C[N - 1] = 0;

            for (int i = 1; i < N - 1; i++)
                C[i] = -1;

            D[0] = 0;
            D[N - 1] = -1;

            for (int i = 1; i < N - 1; i++)
                D[i] = -2 * Math.Pow(h, 2) * i * h;

            double[] result = TridiagonalMatrixAlgorithm(N, A, C, B, D, h);

            for(int i = 0; i < result.Length; i++)
                Console.WriteLine(i + "  " + result[i] + " " + func(i * h));
        }
    }
}
