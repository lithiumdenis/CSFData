//Решение СЛУ вида Ах = В методом Гаусса 

#include "stdafx.h"
#include <stdio.h>
#include <process.h>
float **a, *b, *x;
int N;
FILE* InFile = NULL;

//количество уравнений в системе 
void count_num_lines()
{
	int nonEmptyLine = 0;
	do
	{
		nonEmptyLine = 0;
		while (fgetc(InFile) != '\n' && !feof(InFile)) 
			nonEmptyLine = 1;
		if (nonEmptyLine)
			N++;
	} while (!feof(InFile));
}

void freematrix()
{
	for (int i = 0; i < N; i++)
		delete[] a[i];
	delete[] a;
	delete[] b;
	delete[] x;
}

void allocmatrix()
{
	x = new float[N];
	b = new float[N];
	a = new float*[N];
	
	if (x == NULL || b == NULL || a == NULL)
	{
		printf("\nNot enough memory to allocate for %d equations.\n", N);
		exit(-1);
	}

	for (int i = 0; i < N; i++)
	{
		a[i] = new float[N];
		if (a[i] == NULL)
			printf("\nNot enough memory to allocate for %d equations.\n", N);
	}

	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < N; j++)
			a[i][j] = 0;
		b[i] = 0;
		x[i] = 0;
	}
}

void readmatrix()
{
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < N; j++)
			fscanf_s(InFile, "%f", &a[i][j]);
		fscanf_s(InFile, "%f", &b[i]);
	}
}

void printmatrix()
{
	printf("\n");
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < N; j++)
			printf(" %+f*X%d \t", a[i][j], j);
		printf(" =  %f\n", b[i]);
	}
}

void printresult()
{
	int i = 0;
	printf("\n");
	for (i = 0; i < N; i++)
		printf("X%d = %f\n", i, x[i]);
}

//делает чтобы на главной диагонали не было нулей
void diagonal()
{
	float temp = 0;
	for (int i = 0; i < N; i++)
	{
		if (a[i][i] == 0)
		{
			for (int j = 0; j < N; j++)
			{
				if (j == i) 
					continue;
				if (a[j][i] != 0 && a[i][j] != 0)
				{
					for (int k = 0; k < N; k++)
					{
						temp = a[j][k];
						a[j][k] = a[i][k];
						a[i][k] = temp;
					}
					temp = b[j];
					b[j] = b[i];
					b[i] = temp;
					break;
				}
			}
		}
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	fopen_s(&InFile, "data.txt", "rt");
	
	count_num_lines();
	allocmatrix();
	rewind(InFile);
	readmatrix();
	diagonal();
	
	fclose(InFile);
	
	printmatrix();
	
	//process
	for (int k = 0; k < N; k++)
	{
		for (int i = k + 1; i < N; i++)
		{
			if (a[k][k] == 0)
			{
				printf("\nSolution is not exist.\n");
				return 0;
			}
			float M = a[i][k] / a[k][k];
			for (int j = k; j < N; j++)
				a[i][j] -= M * a[k][j];
			b[i] -= M * b[k];
		}
	}

	printmatrix();
	
	for (int i = N - 1; i >= 0; i--)
	{
		float s = 0;
		for (int j = i; j < N; j++)
			s = s + a[i][j] * x[j];
		x[i] = (b[i] - s) / a[i][i];
	}

	printmatrix();
	printresult();
	freematrix();
	return 0;
}