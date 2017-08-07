using System;

namespace Модель_Леонтьева
{
    class Program
    {
        /// <summary>
        /// Функция цветной печати строки
        /// </summary>
        /// <param name="str">Строка</param>
        static void prettyStringPrint(string str)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(str);
            Console.ResetColor();
        }

        /// <summary>
        /// Печать матрицы
        /// </summary>
        /// <param name="Data">Матрица</param>
        /// <param name="N">Размерность матрицы</param>
        static void print(double[,] Data, int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write("{0,-3}\t", Data[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Функция нахождения обратной матрицы
        /// </summary>
        /// <param name="A">Исходная матрица</param>
        /// <param name="N">Размерность матрицы</param>
        public static void InverseMatrix(double[,] A, int N)
        {
            double temp;

            double[,] E = new double[N, N];

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    E[i, j] = 0.0;

                    if (i == j)
                        E[i, j] = 1.0;
                }

            for (int k = 0; k < N; k++)
            {
                temp = A[k, k];

                for (int j = 0; j < N; j++)
                {
                    A[k, j] /= temp;
                    E[k, j] /= temp;
                }

                for (int i = k + 1; i < N; i++)
                {
                    temp = A[i, k];

                    for (int j = 0; j < N; j++)
                    {
                        A[i, j] -= A[k, j] * temp;
                        E[i, j] -= E[k, j] * temp;
                    }
                }
            }

            for (int k = N - 1; k > 0; k--)
            {
                for (int i = k - 1; i >= 0; i--)
                {
                    temp = A[i, k];

                    for (int j = 0; j < N; j++)
                    {
                        A[i, j] -= A[k, j] * temp;
                        E[i, j] -= E[k, j] * temp;
                    }
                }
            }

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    A[i, j] = E[i, j];
        }

        /// <summary>
        /// Функция перемножения двух матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static double[,] MatrixMultiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
                throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        private static double[,] GetMinor(double[,] matrix, int n)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result;
        }

        private static double DetRec(double[,] matrix)
        {
            if (matrix.Length == 4)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            double sign = 1, result = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                double[,] minor = GetMinor(matrix, i);
                result += sign * matrix[0, i] * DetRec(minor);
                sign = -sign;
            }
            return result;
        }

        /// <summary>
        /// Первый критерий продуктивности матрицы
        /// </summary>
        /// <param name="EA"></param>
        /// <returns></returns>
        public static bool firstCritOfProd(double[,] EA)
        {
            if (DetRec(EA) == 0)
                return false;

            for (int i = 0; i < EA.GetLength(0); i++)
                for (int j = 0; j < EA.GetLength(1); j++)
                    if (EA[i, j] < 0)
                        return false;
            return true;
        }

        /// <summary>
        /// Второй критерий продуктивности матрицы
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static bool secondCritOfProd(double[,] A)
        {
            //Все элементы матрицы А неотрицательны
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    if (A[i, j] < 0)
                        return false;

            double s;
            //Строки
            for (int i = 0; i < A.GetLength(0); i++)
            {
                s = 0;
                for (int j = 0; j < A.GetLength(1); j++)
                    s = s + A[i, j];
                Console.WriteLine("Сумма элементов А в {0} строке = {1}", i, s);
                if (s > 1)
                    return false;
            }
            //Столбцы
            for (int i = 0; i < A.GetLength(0); i++)
            {
                s = 0;
                for (int j = 0; j < A.GetLength(1); j++)
                    s = s + A[j, i];
                Console.WriteLine("Сумма элементов А в {0} стобце = {1}", i, s);
                if (s > 1)
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            int n = 2;
            //Вектор конечного потребления
            double[,] d = new double[n, 1];
            d[0, 0] = 150;
            d[1, 0] = 30;
            double[,] A = new double[n, n];
            double[,] EA = new double[n, n];
            A[0, 0] = 0.05;
            A[0, 1] = 0.40;
            A[1, 0] = 0.15;
            A[1, 1] = 0.10;
            prettyStringPrint("Матрица А:\n");
            print(A, n);
            prettyStringPrint("Матрица E - А:\n");
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        EA[i, j] = 1 - A[i, j];
                    else
                        EA[i, j] = -A[i, j];
                }
            print(EA, n);
            prettyStringPrint("Матрица H:\n");
            try
            {
                InverseMatrix(EA, n);
                print(EA, n);
            }
            catch
            {
                Console.WriteLine("Обратная матрица не существует, первый критерий продуктивности точно не выполнен");
                goto FINAL;
            }

            if (firstCritOfProd(EA) == true)
            {
                prettyStringPrint("1 критерий продуктивности выполнен\n");
            }
            else
            {
                prettyStringPrint("1 критерий продуктивности НЕ выполнен\n");
                goto FINAL;
            }

            if (secondCritOfProd(A) == true)
                prettyStringPrint("2 критерий продуктивности выполнен\n");
            else
            {
                prettyStringPrint("2 критерий продуктивности НЕ выполнен\n");
                goto FINAL;
            }

            prettyStringPrint("Валовый выпуск отраслей:\n");
            
            double[,] result = MatrixMultiplication(EA, d);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Выпуск {0} отрасли: {1}", i, result[i, 0]);
            }

            FINAL:
            return;
        }
    }
}
