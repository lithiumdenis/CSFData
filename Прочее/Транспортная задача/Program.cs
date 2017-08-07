using System;

namespace Транспортная_задача
{
    class Program
    {
        public struct Element
        {
            public int Delivery { get; set; }
            public int Value { get; set; }
            public static int FindMinElement(int a, int b)
            {
                if (a > b) return b;
                if (a == b) { return a; }
                else return a;
            }
        }

        public static void celevayaFunction(int m, int n, Element[,] C)
        {
            //считаем целевую функцию
            int ResultFunction = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    ResultFunction += (C[i, j].Value * C[i, j].Delivery);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Результирующая функция = {0}", ResultFunction);
            Console.ResetColor();
        }

        public static void printResult(int m, int n, Element[,] C)
        {
            //выводим массив на экран
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (C[i, j].Delivery != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("{0}", C[i, j].Value);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("({0}) \t", C[i, j].Delivery); Console.ResetColor();
                    }
                    else
                        Console.Write("{0}({1}) \t", C[i, j].Value, C[i, j].Delivery);
                }
                Console.WriteLine();
            }
        }

        public static void methodFogel(int m, int n, int[] a, int[] b, Element[,] C)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Метод Фогеля:");
            Console.ResetColor();
            FogelMethod fm = new FogelMethod();
            fm.setColumn(m);
            fm.setRow(n);

            fm.getData(C, m, n);
            fm.getCapacity(a);
            fm.getRequiredValue(b);
            fm.makeAllocation();
            fm.display(m, n);
            Console.WriteLine("");
        }

        public static void methodMinCosts(int m, int n, int[] a, int[] b, Element[,] C)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Метод наименьшей стоимости:");
            Console.ResetColor();
            //Пусть -1 говорит о том, что клетка не рассматривалась
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    C[i, j].Delivery = -1;
                }
            }

            while (true)
            {
                int minI = -1, minJ = -1;
                
                //Найдём первый элемент с -1 (его i и j)
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (C[i, j].Delivery == -1)
                        {
                            minI = i;
                            minJ = j;
                            goto NEXT1;
                        }
                        if (i == n - 1 && j == m - 1)
                            goto FINAL; //Всё уже точно рассмотрено, выходим из цикла

                    }
                }

                // найдём минимальный элемент из нерассмотренных с -1 (его i и j)
                NEXT1:
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (C[i, j].Delivery == -1 && C[i, j].Value < C[minI, minJ].Value)
                        {
                            minI = i;
                            minJ = j;
                        }
                    }
                }

                //Нашли минимальный нерассмотренный, рассматриваем его
                C[minI, minJ].Delivery = Element.FindMinElement(a[minI], b[minJ]);
                a[minI] -= C[minI, minJ].Delivery;
                b[minJ] -= C[minI, minJ].Delivery;

                //вычеркнем строку или столбец, если 0 в а или в
                if (a[minI] == 0 && b[minJ] == 0)
                {//то вычеркнем что-нибудь одно
                    for (int k = 0; k < a.Length; k++)
                        if (k != minI && C[k, minJ].Delivery == -1)
                            C[k, minJ].Delivery = 0;
                }
                else if (a[minI] == 0) //вычеркнем строку
                {
                    for (int j = 0; j < b.Length; j++)
                        if (j != minJ && C[minI, j].Delivery == -1)
                            C[minI, j].Delivery = 0;
                }
                else if (b[minJ] == 0) //вычеркнем столбец
                {
                    
                    for (int i = 0; i < a.Length; i++)
                        if (i != minI && C[i, minJ].Delivery == -1)
                            C[i, minJ].Delivery = 0;
                }
            }

            FINAL:
            printResult(m, n, C);
            celevayaFunction(m, n, C);
            Console.WriteLine("");
        }

        public static void methodNorthWestCorner(int m, int n, int[] a, int[] b, Element[,] C)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Метод Северо-западного угла:");
            Console.ResetColor();
            int i = 0;
            int j = 0;
  
            // идём с северо-западного элемента 
            // если a[i] = 0 i++
            // если b[j] = 0 j++
            //  если a[i],b[j] = 0 то i++,j++;
            // доходим до последнего i , j
            while (i < n && j < m)
            {

                try
                {
                    if (a[i] == 0) { i++; }
                    if (b[j] == 0) { j++; }
                    if (a[i] == 0 && b[j] == 0) { i++; j++; }
                    C[i, j].Delivery = Element.FindMinElement(a[i], b[j]);
                    a[i] -= C[i, j].Delivery;
                    b[j] -= C[i, j].Delivery;
                }
                catch { }
            }

            printResult(m, n, C);
            celevayaFunction(m, n, C);
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            int n = 5;//3; // Кол-во а
            int m = 5;//4; // Кол-во b
            int[] a = {16,21,13,43,27 };//{15, 8, 18, 11, 20 };//{ 30, 190, 250 };
            int[] b = {37,19,29,22,13 };//{19, 10, 11, 14, 18 };//{ 70, 120, 150, 130 };
            //Заполним стоимость
            Element[,] C = new Element[n, m];
            int[] inpC = {1,9,5,2,1,8,3,9,4,3,3,5,2,9,5,1,5,1,7,4,3,11,4,9,8 };//{1,7,3,3,5,2,4,9,11,1,9,3,2,5,8,5,7,5,3,4,2,4,1,7,2 };//{ 4, 7, 2, 3, 3, 1, 2, 4, 5, 6, 3, 7 };
            int curr = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    C[i, j].Value = inpC[curr];
                    curr++;
                }
            }

            //Продублируем для каждого метода данные, а то они затираются

            int[] a2 = (int[])a.Clone(); 
            int[] b2 = (int[])b.Clone(); 
            Element[,] C2 = (Element[,])C.Clone(); 

            int[] a3 = (int[])a.Clone();
            int[] b3 = (int[])b.Clone();
            Element[,] C3 = (Element[,])C.Clone();

            methodNorthWestCorner(m, n, a, b, C);
            methodMinCosts(m, n, a2, b2, C2);
            methodFogel(m, n, a3, b3, C3);
        }
    }
}