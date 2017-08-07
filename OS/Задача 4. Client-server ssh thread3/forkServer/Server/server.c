#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <sys/wait.h>
#include <sys/socket.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <netinet/in.h>
#include <pthread.h>
#define MAXCLIENTS 10 //Максимальное число новых клиентов, которое можем подключить

int fileLong = 0;  //Длина файла вывода
char buf[2048];    //Буферная переменная
int currentCountOfClients = 0; //Текущее число новых подключенных клиентов

int main()
{
    //Инициализируем параметры сервера
	int serv_sock, clnt_sock;
	struct sockaddr_in serv_addr,clnt_addr;
	socklen_t clnt_len;
	pid_t pid;

	serv_sock = socket(AF_INET,SOCK_STREAM, 0);
	if(serv_sock==-1)
	{
		fprintf(stderr,"socket() error\n");
		exit(1);
	}
	memset((void*)&serv_addr, 0, sizeof(serv_addr));
	serv_addr.sin_family = AF_INET;
	serv_addr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_addr.sin_port = htons(8888); //Порт 8888

    // Преобразование DNS-имени в IP-адрес 
	if(bind(serv_sock,(struct sockaddr*)&serv_addr,sizeof(serv_addr))==-1)
	{
		fprintf(stderr,"bind() error\n");
		exit(1);
	}
    //Слушаем порт на предмет запроса подключения клиента
	if(listen(serv_sock,5)==-1)
	{
		fprintf(stderr,"lisen() error\n");
		exit(1);
	}
	puts("Listener on port 8888");
	puts("Waiting for connection...");
    //Основной цикл программы
	while(1)
	{
		clnt_len = sizeof(clnt_addr);
        //Если клиент появился - подключаем его и начинаем с ним работать
		clnt_sock = accept(serv_sock,(struct sockaddr*)&clnt_addr,&clnt_len);
		if(clnt_sock == -1)
		{
			fprintf(stderr,"accept() error\n");
			exit(1);
		}
        //Запоминаем идентификатор нового процесса
		pid = fork();
		if(pid == 0)    //Если дочерний процесс (Дочерний всегда = 0)
		{
			close(serv_sock);
			if((pid = fork()) == 0)    //Второй дочерний процесс
			{
                //Проверка на число новых подключенных клиентов
                currentCountOfClients++;
                if(currentCountOfClients == MAXCLIENTS)
                {
                    puts("Maximum of clients!");
                    goto endProgram;
                }
				char client_message[2048];
                int read_size;

                //Проверка присланного клиентом пароля
                int bytes_r;
                char user_name[2048], pass_name[2048], fname[50];
                bytes_r = recv(clnt_sock, user_name, 2048, 0);
                user_name[bytes_r] = '\0';
                bytes_r = recv(clnt_sock, pass_name, 2048, 0);
	            pass_name[bytes_r] = '\0';
                char chk[100],fi[100],fpass[100];
                int y = 0;
                //Считаем пароли для сверки из файла и проверяем
	            FILE *fp = fopen("pass.txt","r");
	            while(!feof(fp))
	            {	
                    fscanf(fp, "%s", fname);
                    fscanf(fp, "%s", fpass);
	                strcpy(fi, fpass);
	                if((strcmp(fi, pass_name) == 0) && (strcmp(fname, user_name) == 0))
	                {	
                        y++;
	                }
                }
                fclose(fp);
                if(y < 0 || y == 0)
                {
                    //Пароль неверный
                    write(clnt_sock , "adenied" , 7);
                }
                else
                {
                    //Пароль верный
                    write(clnt_sock , "agranted" , 8);
                    puts("Client connected");
          
                    chdir(user_name);

                    //Получаем сообщение от клиента
                    while( (read_size = recv(clnt_sock , client_message , 2048 , 0)) > 0 )
	                {
                        //Завершение работы сервера командой клиента
                        if(strstr(client_message, "serverclose"))
                        {
                            write(clnt_sock , "serverclose", 11);
                            goto endProgram;
                        }
                        //Команда напоминания пароля пользователю
                        else if(strstr(client_message, "remindpass"))
                        {
                            //Отправляем обратно клиенту
         	                write(clnt_sock , pass_name, 50);
                            //Зачистка от мусора прошлой присланной строки от этого клиента
                            memset(&client_message, ' ', 100);
                        }
                        //Команда напоминания команд
                        else if(strstr(client_message, "help"))
                        {
                            //Отправляем обратно клиенту
		                    write(clnt_sock , "help", 4);
                            //Зачистка от мусора прошлой присланной строки от этого клиента
                            memset(&client_message, ' ', 100);
                        }
                        //Команда закрытия клиента
                        else if(strstr(client_message, "clientclose"))
                        {
                            //Отправляем обратно клиенту
		                    write(clnt_sock , "clientclose", 11);
                            //Зачистка от мусора прошлой присланной строки от этого клиента
                            memset(&client_message, ' ', 100);
                        }
		                else
                        {          
                            //Зануляем буферную переменную, куда всё будем копировать
                            buf[0] = '\0';
                            //Копируем в буферную переменную сначала команду от клиента, а потом команду логгирования в файл
                            strcat(buf, client_message);
                            strcat(buf, " | tee logfile.txt");
                            //Выполняем команду, образовавшуюся в буферной переменной, на сервере
                            system(buf);

                            //Зануляем буферную переменную, куда всё будем копировать. Старое уже не надо
                            buf[0] = '\0';
                            //Соединяем файл в строку построчно
                            FILE *fsend;
                            char line[256];
                            fsend = fopen("logfile.txt", "r");
                            int first = 0;
                            while(fgets(line, sizeof(line), fsend) != NULL)
                            {  
                                if(first == 0)
                                {
                                    strcat(buf, "\n");
                                    strcat(buf, line);
                                    first = 1;
                                }
                                else
  		                            strcat(buf, line);
                            }
                            fclose(fsend);   
                            //Отправляем весь файл
                            write(clnt_sock, buf , 2048);                 
    
                            //Зачистка от мусора прошлой присланной строки от этого клиента
                            memset(&client_message, ' ', 100);
                            client_message[0] = '\0';
                            //Удаляем файл, чтобы в следующий раз начать лог сначала
                            system("rm logfile.txt");
                            fileLong = 0;
                        }
	                }
                    if(read_size == 0 || read_size == -1)
	                {
                        //Клиент отключился
		                puts("Client disconnected");
                        close(clnt_sock);
	                }
                }                
                exit(0); 
			}
			exit(0);             //Первый дочерний процесс завершен
		}
		else         //Родительский процесс
		{
			wait(NULL);           //Ждем первый дочерний процесс
			close(clnt_sock);
		}
	}
    endProgram:
        close(serv_sock);
        exit(EXIT_SUCCESS);
}
