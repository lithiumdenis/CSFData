using System;

namespace Перколяция
{
    class Program
    {
        static int[] labels;
        static int n_labels = 0;

        //Вывод данных
        static void print(int[][] Data, int L)
        {
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < L; j++)
                {
                    if (Data[i][j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0,-3}", Data[i][j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void prettyStringPrint(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(str);
            Console.ResetColor();
        }

        static int[][] fill(int L, double P)
        {
            
            int[][] Data = new int[L][];
            for (int i = 0; i < L; i++)
                Data[i] = new int[L];
            //Заполним двумерный массив случайным образом, с зависимостью от Р
            Random rand = new Random();
            for (int i = 0; i < L; i++)
                for (int j = 0; j < L; j++)
                {
                    if (rand.NextDouble() <= P)
                        Data[i][j] = 1;
                    else
                        Data[i][j] = 0;
                }
            return Data;
        }

        //Подбираем общий номер для двух объединенных кластеров
        static int uf_find(int x)
        {
            //Значение элемента запоминаем
            int y = x;
            //Пока не найдем номер равный номеру из массива в названиях, присваиваем текущий
            while (labels[y] != y)
                y = labels[y];
            //Пока номер из массива не равен названию от номера из массива
            while (labels[x] != x)
            {
                //Запомним название от номера из массива
                int z = labels[x];
                //Заменим это название на у
                labels[x] = y;
                //Запишем в x запомненное ранее значение
                x = z;
            }
            return y;
        }

        //Объединяем два кластера и возвращаем их общий номер
        static int uf_union(int x, int y)
        {
            labels[uf_find(x)] = uf_find(y);
            return uf_find(y);
        }

        //Создает новый кластер и возвращает номер новой кластерной метки
        static int uf_make_set()
        {
            labels[0]++;
            if (labels[0] < n_labels)
            {
                labels[labels[0]] = labels[0];
                return labels[0];
            }
            else
                return 0;
        }

        //Инициализируем данные для работы
        static void uf_initialize(int max_labels)
        {
            n_labels = max_labels;
            labels = new int[n_labels];
            labels[0] = 0;
        }

        static int hoshen_kopelman(int[][] matrix, int m, int n)
        {
            //Инициализируем структуру для поиска
            uf_initialize(m * n / 2);

            //Проходим по всей матрице
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if(matrix[i][j] != 0) //Если в строке матрицы не 0
                    {                      
                        int up = (i == 0 ? 0 : matrix[i - 1][j]);    //  смотрим вверх 
                        int left = (j == 0 ? 0 : matrix[i][j - 1]);  //  смотрим влево

                        //Приводим наблюдения к виду true/false
                        bool upb = Convert.ToBoolean(up);
                        bool leftb = Convert.ToBoolean(left);
                        int counter = 0;
                        if (upb == true)
                            counter++;
                        if (leftb == true)
                            counter++;

                        //В зависимости от того, сколько условий было истинно, делаем вывод
                        switch (counter)
                        {
                            case 0:                             //Это новый кластер
                                matrix[i][j] = uf_make_set();      
                                break;
                            case 1:                             //Это часть существующего кластера (не знаем, слева или вверху. Просто нужный > 0)
                                matrix[i][j] = (int)Math.Max(up, left);
                                break;
                            case 2:                              //Это связывает два кластера
                                matrix[i][j] = uf_union(up, left);
                                break;
                        }
                    }
            
            //Переобозначим неточные элементы
            int[] new_labels = new int[n_labels];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (matrix[i][j] != 0) //Если элемент не нулевой
                    { 
                        //Находим место его названия в старом массиве названий
                        int x = uf_find(matrix[i][j]);
                        if (new_labels[x] == 0) //Если в новом массиве названий мы ещё его не использовали
                        {
                            new_labels[0] += 1; //Увеличиваем число кластеров
                            new_labels[x] = new_labels[0]; //Даём в новом массиве названий ему новое название по порядку
                        }
                        //присваиваем матрице это название
                        matrix[i][j] = new_labels[x];
                    }
            int total_clusters = new_labels[0];
            return total_clusters;
        }

        public static double getMeCountOfNonZero(int L, int[][] Data)
        {
            int counter = 0;
            for (int j = 0; j < L; j++)
                for (int k = 0; k < L; k++)
                    if (Data[j][k] != 0)
                        counter++;
            return counter;

        }

        public static double getLargestClusterSize(int L, int[][] Data, int clusters)
        {
            //Пробежимся по всем кластерам и найдем их длины
            int[] count = new int[clusters];
            for (int i = 0; i < clusters; i++)
                for (int j = 0; j < L; j++)
                    for (int k = 0; k < L; k++)
                        if (Data[j][k] == i)
                            count[i]++;
            int value = 0;

            for (int i = 1; i < clusters; i++)
                if (value < count[i])
                    value = count[i];
            return value;
        }

        /// <summary>
        /// Поиск наличия бесконечного цикла
        /// </summary>
        /// <param name="P">Вероятность</param>
        /// <param name="L">Размерность матрицы</param>
        /// <returns></returns>
        public static double funcInfiniteLoopFinder(double P, int L)
        {
            int[][] Data = fill(L, P);
            int clusters = hoshen_kopelman(Data, L, L);

            for(int i = 1; i < clusters; i++)
            {
                for (int j = 0; j < L; j++)
                    for (int k = 0; k < L; k++)
                        if (Data[0][j] == Data[L - 1][k] && Data[0][j] == i)
                            return 1;
            }
            return 0;
        }

        static void Main(string[] args)
        {
            //Введем исходные данные
            prettyStringPrint("Введите размер решетки L: ");
            int L = Convert.ToInt32(Console.ReadLine());
            prettyStringPrint("Введите вероятность заполнения P из промежутка [0, 1]: ");
            double P = Convert.ToDouble(Console.ReadLine());
            int[][] Data = fill(L, P);

            //Распечатаем исходный массив
            prettyStringPrint("Начальные данные: \n");
            print(Data, L);

            //Найдём кластеры по алгоритму Хошена Копельмана
            int clusters = hoshen_kopelman(Data, L, L);
            prettyStringPrint("Результат работы алгоритма Хошена-Копельмана: \n");
            print(Data, L);
            prettyStringPrint("Найдено " + clusters + " кластеров\n");

            //Меод деления отрезка пополам для поиска Pc
            double a = 0.4;
            double b = 1;
            double eps = 0.0001;

            double t, f1, f2, x;
            do
            {
                f1 = funcInfiniteLoopFinder(a, L);
                t = (a + b) / 2.0;
                f2 = funcInfiniteLoopFinder(t, L);
                //Console.WriteLine("f1 = {0}, f2 = {1}, a = {2}, t = {3}", f1, f2, a, t);

                if (f2 == 1) b = t;
                if (f1 == 0 && f2 == 0) a = t;
            }
            while (Math.Abs(b - a) > eps);

            x = (a + b) / 2.0;
            Console.WriteLine("Pc = " + x);

            double pc = x;

            double sxi = -1.0 / Math.Log(P);
            //It works by formula from Ukrainian Wikipedia
            double blackParts = getMeCountOfNonZero(L, Data);
            double df = Math.Log(blackParts) / Math.Log(L);
            Console.WriteLine("df = " + df);
            //Two dimentional lattice
            double d = 2;
            //True by Engilsh Wikipedia
            double tau = d / df + 1.0;
            Console.WriteLine("tau = " + tau);
            //Lections + Wikipedia
            double largestClusterSize = getLargestClusterSize(L, Data, clusters); //smax
            double omega = -Math.Log(Math.Abs(pc - P)) / Math.Log(largestClusterSize);
            Console.WriteLine("omega = " + omega);
            //alpha
            double alpha = 2 - (tau - 1) / omega;
            Console.WriteLine("alpha = " + alpha);
            //beta
            double beta = (tau - 2) / omega;
            Console.WriteLine("beta = " + beta);
            //gamma
            double gamma = (3 - tau) / omega;
            Console.WriteLine("gamma = " + gamma);
            //nu
            double nu = (tau - 1) / (omega * d);
            Console.WriteLine("nu = " + nu);
        }
    }
}
