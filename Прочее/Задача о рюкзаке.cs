using System;

namespace Рюкзак
{
    class Program
    {
        /// <summary>
        /// Печать матрицы
        /// </summary>
        /// <param name = "Data" > Матрица </ param >
        /// < param name="N">Размерность матрицы</param>
        static void print(int[,] Data)
        {
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    Console.Write("{0,-3}  ", Data[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void ItemsInKnapsack(int s, int n, int[,] dp, int[] wi)
        {
            if (dp[s, n] == 0) // максимальный рюкзак для параметров (s,n)
                return;        // имеет нулевую ценность, 
                               // поэтому ничего не выводим
            else if (dp[s - 1, n] == dp[s, n])
                ItemsInKnapsack(s - 1, n, dp, wi);  // можно составить рюкзак без предмета s
            else
            {
                ItemsInKnapsack(s - 1, n - wi[s], dp, wi); // Предмет s должен обязательно 
                Console.WriteLine(s);                      // войти в рюкзак
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Автоматический вычислитель задачи о рюкзаке");
            Console.Write("Введите вместимость рюкзака W: ");
            int W = Convert.ToInt32(Console.ReadLine().Trim());
            Console.Write("Введите количество предметов K: ");
            int k = Convert.ToInt32(Console.ReadLine().Trim());
            Console.Write("Введите множество весов w(i) через пробел: ");
            string prew = "0 " + Console.ReadLine().Trim();
            string[] preww = prew.Split(' ');
            int[] w = Array.ConvertAll(preww, Convert.ToInt32);
            Console.Write("Введите множество цен v(i) через пробел: ");
            string prev = "0 " + Console.ReadLine().Trim();
            string[] prevv = prev.Split(' ');
            int[] p = Array.ConvertAll(prevv, Convert.ToInt32);

            int[,] A = new int[k + 1, W + 1];
            for(int n=0; n <= W; ++n)       // Заполняем нулевую строчку
               A[0, n] = 0;
            for (int s = 1; s <= k; ++s)       // s - максимальный номер предмета, 
            {                       // который можно использовать
                for (int n = 0; n <= W; ++n)   // n - вместимости рюкзака
                {
                    A[s, n] = A[s - 1, n];
                    if (n >= w[s] && (A[s - 1, n - w[s]] + p[s] > A[s, n]))
                        A[s, n] = A[s - 1, n - w[s]] + p[s];
                }
            }

            print(A);
            Console.WriteLine("Нужно положить внутрь предметы:");
            ItemsInKnapsack(k, W, A, w);
            Console.Write("Для завершения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}