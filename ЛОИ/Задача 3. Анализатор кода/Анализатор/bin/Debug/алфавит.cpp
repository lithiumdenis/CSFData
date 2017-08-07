#include <stdio>
#include <stdlib>


void InputMat(double* mas, int n, int m)
{
	int i,j;
	for ( i = 0; i < n; i++)			
		for ( j= 0; j < m; j++)	
        mas[i,j] = (i+1)+j;	
}

void PrintMat(double* mas, int n, int m)
{
	int i,j;
	for (i = 0; i < n; i++)
	{
		printf("\n");
		for (j = 0; j < n; j++)
		{
			printf("%d",mas[i,j]);
			printf(" ");
		}
	}
	printf("\n");
}


int main(int argn,char** args)
{
	int size;
	size = 10;
	int n1 = size - 1; 
	int m1 = size + 1;
	int n2 = size - 1;
	int m2 = size + 1;
	
	mat1 = malloc[size * size * sizeof ( double )];
    mat2 = malloc[size * size * sizeof ( double )];	
		
	Zaday( mat1, n1, m1);
	printf("Mat1: \n");
	PrintMas( mat1, n1, m1);
	
	Zaday( mat2, n1, m1);
	printf("Mat2: \n");
	PrintMas( mat2, n1, m1);
	
	return 0;
}
