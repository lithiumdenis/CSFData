#include "stdafx.h"
#include <stdio.h>
#include <string.h>
#include <vector>
#include <string>

int _tmain(int argc, _TCHAR* argv[])
{
	FILE *fp;
	fopen_s(&fp, "file.txt", "r");
	if (fp == NULL)
	{
		printf("No file");
		return 1; // выход по ошибке, код ошибки 1
	}

	char mystring1[100];
	char mystring2[100];

	fgets(mystring1, 100, fp);
	fgets(mystring2, 100, fp);

	fclose(fp);

	puts(mystring1);
	puts(mystring2);
	printf("\n");

	std::vector<std::string> lines1;
	std::vector<std::string> lines2;

	char *token1;
	token1 = strtok(mystring1, " ");

	while (token1 != NULL)
	{
		lines1.push_back(token1);
		printf("%s\n", token1);
		token1 = strtok(NULL, " \n");
	}

	printf("\n");

	char *token2;
	token2 = strtok(mystring2, " ");

	while (token2 != NULL)
	{
		lines2.push_back(token2);
		printf("%s\n", token2);
		token2 = strtok(NULL, " \n");
	}

	std::string buf = "";

	for (int i = 0; i < lines2.size(); i++) 
	{
		buf = "";
		for (int j = 0; j < lines1.size(); j++)
		{
			int pos = lines2[i].find(lines1[j]);
			if (pos != std::string::npos)
			{
				//нет ли там уже такой подстроки
				int pos2 = buf.find(lines1[j]);
				if (pos2 == std::string::npos)
					buf += lines1[j];
				if (buf.size() == lines2[i].size())
				{
					break;
				}

			}
		}
		if (lines2[i].size() > buf.size())
		{
			printf("\nlines2[%d] is not builds \n", i);
			return 0;
		}
		
	}

	printf("\nAll words builds! \n");


	return 0;
}