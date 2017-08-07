#include <sys/types.h>
#include <unistd.h>
#include <stdio.h>

int main()
{
	pid_t pid, ppid, chpid, chpid2;
	int a = 0;
        chpid = fork();
	if(chpid == 0)
	{
		fork();
		fork();
	}
        
	pid = getpid();
	ppid = getppid();
	printf("PID = %d, PPID = %d, RES = %d\n", (int)pid, (int)ppid, a);
	for(;;)
        {}
	return 0;
}
