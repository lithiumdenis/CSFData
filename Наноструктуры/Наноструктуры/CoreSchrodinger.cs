using System;
using System.Collections.Generic;

namespace Наноструктуры
{
    class CoreSchrodinger
    {
        public static void Work(double wellLength, double wellDepth, int N_max, double[] EigenEnergies, List<double> Wavefunction, List<double> Energy)
        {
            double Epsilon = 1e-10;
            int N_of_Divisions = 2 * (int)wellLength * 100;

            /* Положим для начала hbar = m = omega = a = 1 и переопределим их в конце. */

            double xRange = wellDepth; // глубина ямы
            double h_0 = xRange / N_of_Divisions;
            double[] E_pot = new double[N_of_Divisions + 1];
            double dist;

            for (int i = 0; i <= N_of_Divisions; ++i)
            {
                dist = i * h_0 - 0.5 * xRange;
                E_pot[i] = 0; //Отличие конечноразмерной ямы от осциллятора
            }

            /*Т.к. уравнение Шредингера линейное, амплитуда
            волновая определяется нормализацией. */

            double Psi_left = 1.0e-3; // левое граничное условие
            double Psi_right = 0.0; // правое граничное условие
            double[] Psi;// Массив хранящий результаты
            Psi = new double[N_of_Divisions + 1]; //N_of_Points = N_of_Divisions+1
            Psi[0] = Psi_left;
            Psi[1] = Psi_left + 1.0e-3; // Добавим произвольную малую величину
            int Nodes_plus; // Количество узлов (+1) в волновой функции
            double K_square;// Площадь волнового вектора
            double E_lowerLimit = 0.0;// Собственная энергия должна быть положительна
            double E_upperLimit = wellLength;
            int End_sign = -1;
            bool Limits_are_defined = false;
            double Normalization_coefficient;
            double E_trial;

            // Главный цикл:

            for (int N_quantum = 1; N_quantum <= N_max; ++N_quantum)
            {
                // Находим собственные значения для энергии
                Limits_are_defined = false;
                while (Limits_are_defined == false)
                { /* Во-первых, определим верхний предел для энергии, так что волновая функция psi[i] имеет один узел больше, чем физически необходимо. */
                    Nodes_plus = 0;
                    E_trial = E_upperLimit;
                    for (int i = 2; i <= N_of_Divisions; ++i)
                    {
                        K_square = 2.0 * (E_trial - E_pot[i]);
                        // начинаем использовать NUMEROV-equation для вычисления волновой функции
                        Psi[i] = 2.0 * Psi[i - 1] * (1.0 - (5.0 * h_0 * h_0 * K_square / 12.0))
                            / (1.0 + (h_0 * h_0 * K_square / 12.0)) - Psi[i - 2];
                        if (Psi[i] * Psi[i - 1] < 0) ++Nodes_plus;
                    }

                    if (E_upperLimit < E_lowerLimit)
                        E_upperLimit = Math.Max(2 * E_upperLimit, -2 * E_upperLimit);
                    if (Nodes_plus > N_quantum) E_upperLimit *= 0.7;
                    else if (Nodes_plus < N_quantum) E_upperLimit *= 2.0;
                    else Limits_are_defined = true;
                }
                // Уточним энергию, удовлетворяя правильным граничным условиям.
                End_sign = -End_sign;
                while ((E_upperLimit - E_lowerLimit) > Epsilon)
                {
                    E_trial = (E_upperLimit + E_lowerLimit) / 2.0;
                    for (int i = 2; i <= N_of_Divisions; ++i)
                    {
                        K_square = 2.0 * (E_trial - E_pot[i]);
                        Psi[i] = 2.0 * Psi[i - 1] * (1.0 - (5.0 * h_0 * h_0 * K_square / 12.0))
                            / (1.0 + (h_0 * h_0 * K_square / 12.0)) - Psi[i - 2];
                    }
                    if (End_sign * Psi[N_of_Divisions] > Psi_right) E_lowerLimit = E_trial;
                    else E_upperLimit = E_trial;
                } //  while ((E_upperLimit - E_lowerLimit) > Epsilon)
                  // Инициализация для следующей итерации главного цикла
                E_trial = (E_upperLimit + E_lowerLimit) / 2;
                EigenEnergies[N_quantum] = E_trial;
                E_upperLimit = E_trial;
                E_lowerLimit = E_trial;
                // Ищем коэффициент нормализации
                double Integral = 0.0;
                for (int i = 1; i <= N_of_Divisions; ++i)
                {
                    Integral += 0.5 * h_0 * (Psi[i - 1] * Psi[i - 1] + Psi[i] * Psi[i]);
                }
                Normalization_coefficient = Math.Sqrt(1.0 / Integral);
                // Выводим нормализованную безразмерную волновую функцию
                for (int i = 0; i <= N_of_Divisions; ++i)
                {
                    Wavefunction.Add(Normalization_coefficient * Psi[i]);
                }
                Wavefunction.Add(99999999999999999);
            }
        }
    }
    public abstract class AbstractEquation
    {
        public abstract double y(double x, bool nechotnie, double c);

        public double[] SolveFunc(double a, double b, double h, bool nechotnie, double c)
        {
            double[] res = { };
            for (double i = a; i < b; i += h)
                if (y(i, nechotnie, c) * y(i + h, nechotnie, c) < 0)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = i;
                }
            return res;
        }
    }

    public class SpecEquation : AbstractEquation
    {
        public override double y(double x, bool nechetnie, double c)
        {
            if (nechetnie == false)
                return Math.Pow(x, 2) * (Math.Pow(1.0 / Math.Tan(x), 2) + 1.0) - Math.Pow(c, 2); //четные
            else
                return Math.Pow(x, 2) * (Math.Pow(Math.Tan(x), 2) + 1.0) - Math.Pow(c, 2); //нечетные
        }
    }
}
