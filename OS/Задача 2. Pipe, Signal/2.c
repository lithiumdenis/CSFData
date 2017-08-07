#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <unistd.h>
#include <fcntl.h>
#include <sys/ioctl.h>

#define STRBUFLEN 50
#define BUFLEN 500

int read_line(int fd, char *dest, int len) {
    char *ptr = dest;
    int n = 0;

    while (read(fd, ptr, 1) == 1 && n <= len-1) {
        n++;
        if (*ptr == '\n') {
            ptr[1] = '\0';
            break;
        }
        ptr++;
    }
    
    return n;
}


void io_handler(int sig)
{
}

int main(int argc, char **argv) {
    int pfds[2];
    char fpath[STRBUFLEN], str[STRBUFLEN];
    char buf[BUFLEN];
    char line[256];
    FILE *f;
    int n;
    pid_t cpid;

    if (argc < 3) {
        printf("Please input like: ./a.out text.txt substring\n");
        exit(1);
    }
    
    strncpy(fpath, argv[1], STRBUFLEN-1);
    if (strlen(argv[1]) >= STRBUFLEN)
        fpath[STRBUFLEN-1] = '\0';
    
    strncpy(str, argv[2], STRBUFLEN-1);
    if (strlen(argv[2]) >= STRBUFLEN)
        str[STRBUFLEN-1] = '\0';
    
    pipe(pfds);
    cpid = fork();

    if (cpid == 0) {
        close(pfds[1]);

      signal(SIGUSR1, io_handler);

      while (read_line(pfds[0], buf, BUFLEN)) 
      {            
          if (strstr(buf, str))
              printf("%s", buf);
      }
       close(pfds[0]);
    } 
     else {
        close(pfds[0]);
        f = fopen(fpath, "r");
            
            fgets(line, sizeof(line), f);
            write(pfds[1], line, strlen(line));
            sleep(5);
 

        while(fgets(line, sizeof(line), f) != NULL)
            	write(pfds[1], line, strlen(line));

        kill(0, SIGUSR1);
         
        close(pfds[1]);
        fclose(f);
    }
}