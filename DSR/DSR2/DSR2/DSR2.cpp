#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <vector>

using namespace std;

#define N 120

int _tmain(int argc, _TCHAR* argv[])
{
	vector<string> text1;
	vector<string> text2;
	
	FILE *file1;
	char arr[N];

	fopen_s(&file1, "file1.txt", "r");

	while (fgets(arr, N, file1) != NULL)
	{
		text1.push_back(arr);
		printf("%s", arr);
	}
	text1[text1.size() - 1] += "\n";


	printf("\n");
	printf("\n");
	fclose(file1);

	FILE *file2;
	char arr2[N];
	

	fopen_s(&file2, "file2.txt", "r");

	while (fgets(arr2, N, file2) != NULL)
	{
		text2.push_back(arr2);
		printf("%s", arr2);
	}
	text2[text2.size() - 1] += "\n";

	printf("\n");
	fclose(file2);

	for (int i = 0; i < text2.size(); i++)
	{
		for (int j = 0; j < text1.size(); j++)
		{
			if (text2[i] == text1[j])
				break;
			if (j == text1.size() - 1)
			{
				printf("\nNo, wrong\n");
				return 0;
			}
		}
	}
	printf("\nOkey, all good\n");
	return 0;
}

