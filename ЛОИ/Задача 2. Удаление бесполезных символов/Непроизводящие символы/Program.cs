using System;

namespace Непроизводящие_символы
{
    class Program
    {
        public struct data
        {
            public string s;
            public int ind;
        }

        public static void markNeterminals(data[] rule, data[] neterm)
        {
            //Отметим нетерминалы единицей, если они слева в таких правилах, где справа нет нетерминалов
            for (int i = 0; i < rule.Length; i++)
                for (int j = 0; j < neterm.Length; j++)
                    if (rule[i].s[0] == Convert.ToChar(neterm[j].s) && rule[i].ind == 0)
                        neterm[j].ind = 1;
        }

        public static void printer(string status, data[] rule)
        {
            Console.WriteLine(status);
            for (int i = 0; i < rule.Length; i++)
                if (rule[i].ind == 0)
                    Console.WriteLine(rule[i].s);
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string[] basic_param = new string[3];
            basic_param = lines[0].Split(' ');

            data[] rule = new data[Convert.ToInt32(basic_param[2])];
            data[] term = new data[Convert.ToInt32(basic_param[0])];
            data[] neterm = new data[Convert.ToInt32(basic_param[1])];
            char startSymb = ' ';

            for (int i = 0; i < term.Length; i++)
            {
                term[i].s = lines[i + 1];
            }

            for (int i = 0; i < neterm.Length; i++)
            {
                neterm[i].s = lines[i + 1 + term.Length];
            }

            for (int i = 0; i < rule.Length; i++)
            {
                rule[i].s = lines[i + 1 + term.Length + neterm.Length];
            }

            startSymb = Convert.ToChar(lines[lines.Length - 1]);

            printer("Исходный набор правил:", rule);

            ///////////////////Удаляем непроизводящие символы//////////////////////////////////
            int curr = -1;
            //Ищем правила без нетерминалов справа, отмечаем их единичками в ind
            NEXT: curr++;
            for (; curr < rule.Length; curr++)
                for (int j = 3; j < rule[curr].s.Length; j++)
                    for (int k = 0; k < neterm.Length; k++)
                        if (rule[curr].s[j] == Convert.ToChar(neterm[k].s))
                        {
                            rule[curr].ind = 1; //В правиле справа есть нетерминал, отметим
                            goto NEXT;
                        }


            markNeterminals(rule, neterm);

            //Если найдено такое правило, что все 
            //нетерминалы, стоящие в его правой части, уже входят в множество, то добавить в множество нетерминалы, стоящие в его левой части. 

            curr = -1;
            NEXT2: curr++;
            for (; curr < rule.Length; rule[curr].ind = 0, curr = -1)
                if (curr == -1)
                {
                    markNeterminals(rule, neterm);
                    goto NEXT2;
                }
                else
                    for (int j = 3; j < rule[curr].s.Length; j++)
                        for (int k = 0; k < neterm.Length; k++)
                            if ((rule[curr].s[j] == Convert.ToChar(neterm[k].s) && neterm[k].ind != 1) || rule[curr].ind == 0) //если нашли справа нетерминал которого нет в множестве или в правиле типа нет нетерминала 100 процентов (ну, мы настаиваем на этом)
                            {
                                goto NEXT2;
                            }

            printer("Удалили непроизводящие:", rule);

            ///////////////////Удаляем недостижимые символы////////////////////////////////////////////
            //Зачистим значения индикаторов нетерминалов
            for (int i = 0; i < neterm.Length; i++)
                neterm[i].ind = 0;

            // Отмечаем начальный символ как 1 в списке нетерминалов
            for (int i = 0; i < neterm.Length; i++)
                if (startSymb == Convert.ToChar(neterm[i].s))
                    neterm[i].ind = 1;

            REPEAT:
            for (int m = 0; m < neterm.Length; m++)          //Переберем все известные нетерминалы
                for (int i = 0; i < rule.Length; i++)       //Для нетерминала просмотрим все правила
                    if (rule[i].s[0] == Convert.ToChar(neterm[m].s) && rule[i].ind == 0 && neterm[m].ind == 1) //Если 0 символ правила равен нетерминальному, правило это рассматривается и этот нетерминал уже достижимый
                        for (int j = 3; j < rule[i].s.Length; j++) //Посмотрим что в правой части правила
                            for (int k = 0; k < neterm.Length; k++) //Перебирая все нетерминалы
                                if (rule[i].s[j] == Convert.ToChar(neterm[k].s) && neterm[k].ind != 1) //Если что-то в правой части есть нетерминал, не отмеченный как достижимый
                                {
                                    neterm[k].ind = 1; //Отмечаем его достижимым
                                    m = 0; //Начинаем снова перебирать все нетерминалы, так как множество достижимых изменилось
                                    goto REPEAT;
                                }

            // Если среди правил какое-то начинается нетерминалом, которого нет в списке, то нам такое правило не нужно
            for (int i = 0; i < rule.Length; i++)
                for (int j = 0; j < neterm.Length; j++)
                    if (rule[i].s[0] == Convert.ToChar(neterm[j].s) && rule[i].ind == 0 && neterm[j].ind == 0)
                        rule[i].ind = 1;

            printer("Удалили недостижимые:", rule);
        }
    }
}
