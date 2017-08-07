using System;

namespace Транспортная_задача
{
    class FogelMethod
    {
        public void setColumn(int no) { no_of_columns = no; }
        public void setRow(int no) { no_of_rows = no; }
        int[,] data = new int[50, 50];
        int[] requered = new int[50];
        int[] capacity = new int[50];
        int[,] allocation = new int[50, 50];
        int no_of_rows, no_of_columns, no_of_allocation;
        void lcmethod()
        {
            for (int i = 0; i < 50; i++)
            {
                capacity[i] = 0;
                requered[i] = 0;
                for (int j = 0; j < 50; j++)
                {
                    data[i, j] = 0;
                    allocation[i, j] = 0;
                }
            }
            no_of_rows = no_of_columns = no_of_allocation = 0;
        }

        public int getPanalty(int[] array, int no)
        {
            int i, j, temp;
            for (i = 0; i < no; i++)
                for (j = i + 1; j < no; j++)
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
            return array[1] - array[0];
        }

        public int getMinVal(int[] array, int no)
        {
            int min = array[0];
            for (int i = 0; i < no; i++)
                if (array[i] < min)
                    min = array[i];
            return min;
        }

        public int getMinValsPos(int value, int[] temp_data, int no)
        {
            int k = 0;
            for (int i = 0; i < no; i++)
                if (temp_data[i] == value)
                    return i;
            return -1;
        }

        public int getTotalMinVal(int[] array, int n, int value)
        {
            int no = 0;
            for (int i = 0; i < n; i++)
                if (array[i] == value)
                    no++;
            return no;
        }

        public bool checkValue(int[] arr, int no)
        {
            for (int i = 0; i < no; i++)
                if (arr[i] != 0)
                    return false;
            return true;
        }

        public void arrayCopy(int start, int end, int[] array1, int start1, int[] array2)
        {
            for (int i = start, j = start1; i < end; i++, j++)
                array2[j] = array1[i];
        }

        public int getTotal(int[] array, int no)
        {
            int sum = 0;
            for (int i = 0; i < no; i++)
                sum += array[i];
            return sum;
        }

        public void copy2DArray(int startRow, int startCol, int endRow, int endCol, int[,] array, int start1Row, int start1Col, int[,] ans)
        {
            for (int i = startRow, k = start1Row; i < endRow; i++, k++)
                for (int j = startCol, l = start1Col; j < endCol; j++, l++)
                    ans[k, l] = array[i, j];
        }

        public int getMaxVal(int[] array, int no)
        {
            int max = 0;
            for (int i = 0; i < no; i++)
                if (array[i] > max)
                    max = array[i];
            return max;
        }

        public int getMaxValPos(int[] array, int no, int value)
        {
            for (int i = 0; i < no; i++)
                if (value == array[i])
                    return i;
            return -1;
        }

        public int[] return1Darray(int[,] arr, int curr)
        {
            int[] result = new int[50];
            for (int i = 0; i < 50; i++)
                result[i] = arr[curr, i];
            return result;
        }

        public void makeAllocation()
        {
            int i = 0, j = 0, min;
            int[] temp_requered = new int[50];
            int[] temp_capacity = new int[50];
            int[,] temp_data = new int[50, 50];
            int[] position = new int[50];
            int[] dataPos = new int[50];
            int sum_of_cap, sum_of_req;
            sum_of_cap = getTotal(capacity, no_of_rows);
            sum_of_req = getTotal(requered, no_of_columns);
            if (sum_of_cap != sum_of_req)
            {
                if (sum_of_cap > sum_of_req)
                {
                    for (j = 0; j < no_of_rows; j++)
                        data[j, no_of_columns] = 0;
                    requered[no_of_columns] = sum_of_cap - sum_of_req;
                    no_of_columns++;
                }
                else
                {
                    for (j = 0; j < no_of_columns; j++)
                        data[no_of_rows, j] = 0;
                    capacity[no_of_rows] = sum_of_req - sum_of_cap;
                    no_of_rows++;
                }
            }
            i = j = 0;
            arrayCopy(0, no_of_rows, capacity, 0, temp_capacity);
            arrayCopy(0, no_of_columns, requered, 0, temp_requered);
            copy2DArray(0, 0, no_of_rows, no_of_columns, data, 0, 0, temp_data);
            int[] rowPanalty = new int[50];
            int[] colPanalty = new int[50];
            int[] panaltyData = new int[50];
            int n = 0; 
            while (!checkValue(temp_capacity, no_of_rows) || !checkValue(temp_requered, no_of_columns))
            {

                for (i = 0; i < no_of_rows; i++)
                {
                    arrayCopy(0, no_of_columns, return1Darray(temp_data, i)/*temp_data[i]*/, 0, panaltyData);
                    if (temp_capacity[i] != 0)
                        rowPanalty[i] = getPanalty(panaltyData, no_of_columns);
                    else
                        rowPanalty[i] = 0;
                }
                for (i = 0; i < no_of_columns; i++)
                {
                    for (j = 0; j < no_of_rows; j++)
                        panaltyData[j] = temp_data[j, i];
                    if (requered[i] != 0)
                        colPanalty[i] = getPanalty(panaltyData, no_of_rows);
                    else
                        colPanalty[i] = 0;
                }
                int maxRowPanalty = getMaxVal(rowPanalty, no_of_rows);
                int maxColPanalty = getMaxVal(colPanalty, no_of_columns);
                int[] maxPanRow = new int[50];
                int[] maxPanCol = new int[50];
                if (maxRowPanalty > maxColPanalty)
                {
                    i = getMaxValPos(rowPanalty, no_of_rows, maxRowPanalty);
                    for (j = 0; j < no_of_columns; j++)
                        maxPanRow[j] = temp_data[i, j];
                    min = getMinVal(maxPanRow, no_of_columns);
                    j = getMinValsPos(min, maxPanRow, no_of_columns);
                }
                else
                {
                    j = getMaxValPos(colPanalty, no_of_columns, maxColPanalty);
                    for (i = 0; i < no_of_rows; i++)
                        maxPanCol[i] = temp_data[i, j];
                    min = getMinVal(maxPanCol, no_of_rows);
                    i = getMinValsPos(min, maxPanCol, no_of_rows);
                }

                if (temp_capacity[i] > temp_requered[j])
                {
                    allocation[i, j] = temp_requered[j];
                    for (int k = 0; k < no_of_rows; k++)
                        temp_data[k, j] = 9999;
                    temp_capacity[i] -= temp_requered[j];
                    temp_requered[j] = 0;
                }
                else if (temp_capacity[i] < temp_requered[j])
                {
                    allocation[i, j] = temp_capacity[i];
                    for (int k = 0; k < no_of_columns; k++)
                        temp_data[i, k] = 9999;
                    temp_requered[j] -= temp_capacity[i];
                    temp_capacity[i] = 0;
                }
                else
                {
                    int k;
                    allocation[i, j] = temp_capacity[i];
                    for (k = 0; k < no_of_rows; k++)
                        temp_data[k, j] = 9999;
                    for (k = 0; k < no_of_columns; k++)
                        temp_data[i, k] = 9999;
                    temp_requered[j] = temp_capacity[i] = 0;
                }
                n++;
            }
            no_of_allocation = n;
        }

        public void getCapacity(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                capacity[i] = a[i];
        }

        public void getRequiredValue(int[] b)
        {
            for (int i = 0; i < b.Length; i++)
                requered[i] = b[i];
        }

        public void display(int m , int n)
        {
            //выводим массив на экран
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (allocation[i, j] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("{0}", data[i, j]);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("({0}) \t", allocation[i, j]); Console.ResetColor();
                    }
                    else
                        Console.Write("{0}({1}) \t", data[i, j], allocation[i, j]);
                }
                Console.WriteLine();
            }

            int ResultFunction = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    ResultFunction += (data[i, j] * allocation[i, j]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Результирующая функция = {0}", ResultFunction);
            Console.ResetColor();
        }

        public void getData(Program.Element[,] C, int m, int n)
        {
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    data[i, j] = C[i, j].Value;
        }
    }
}
