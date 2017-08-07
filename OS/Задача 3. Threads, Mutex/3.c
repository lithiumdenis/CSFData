#include <stdio.h>
#include <string.h>
#include <pthread.h>

#define STRBUFLEN 50
 
pthread_mutex_t shared_array_mutex = PTHREAD_MUTEX_INITIALIZER;
char shared_array[ 256 ] = {0};
char shared_array_changed = 0; // флаг говорит о том, что даные обновлены
int fileLong = 0; // длина файла
 
void * filler_thread( void * arg )
{
	// Открываем файл для работы
	FILE *f;
	char line[256];
	f = fopen((char*) arg, "r");
 
    	int i;
        // Для каждой строки
   
    	for( i = 0; i < fileLong; i++ )
    	{
        	//usleep( 1000000 ); // можно включить для повышения наглядности

		// считываем новую строку из файла
		fgets(line, sizeof(line), f);

        	// ждём пока поток вывода не пометит данные как устаревшие
        	for(;;)
        	{
           	pthread_mutex_lock( &shared_array_mutex );
      
           	if( shared_array_changed == 0 )
              		break;
      
           	pthread_mutex_unlock( &shared_array_mutex );
        }
      	
	// копируем в буфер новую строку
        strcpy( shared_array, line );
        shared_array_changed = 1;
        pthread_mutex_unlock( &shared_array_mutex );      
    }
    pthread_exit( 0 );
}
 
void * printer_thread( void * arg )
{
   int i;
   for( i = 0; i < fileLong; i++ )
   {
      // ждём пока поток заполнения не пометит данные как обновлённые
      for(;;)
      {
         pthread_mutex_lock( &shared_array_mutex );
      
         if( shared_array_changed == 1 )
            break;
      
         pthread_mutex_unlock( &shared_array_mutex );
      }

      if (strstr(shared_array, (char*) arg))
          puts( shared_array );

      shared_array_changed = 0;
      pthread_mutex_unlock( &shared_array_mutex );
   }
   pthread_exit( 0 );
}
 
int main( int argc, char **argv )
{
        // Вводим данные
        char fpath[STRBUFLEN], str[STRBUFLEN];

        if (argc < 3) 
        {
            printf("Please input like: ./a.out text.txt substring\n");
            return 1;
    	}

 	strncpy(fpath, argv[1], STRBUFLEN-1);
    	if (strlen(argv[1]) >= STRBUFLEN)
        	fpath[STRBUFLEN-1] = '\0';
    
    	strncpy(str, argv[2], STRBUFLEN-1);
    	if (strlen(argv[2]) >= STRBUFLEN)
        	str[STRBUFLEN-1] = '\0';

	// Считаем сколько строк в файле
	FILE *fNumOfStrings;
	char line[256];
	fNumOfStrings = fopen(fpath, "r");
	while(fgets(line, sizeof(line), fNumOfStrings) != NULL)
  		fileLong++;
	fclose(fNumOfStrings);

	// Создаём дочерний и родительский поток
   	pthread_t th_filler, th_printer;
   
        // Дочернему передали подстроку
   	pthread_create( &th_printer, 0, printer_thread, str );
        // Родительскому передали путь к файлу
   	pthread_create( &th_filler, 0, filler_thread, fpath );
   
        // Поток main ждёт завершение родительского и дочернего
   	pthread_join( th_printer, 0 );
   	pthread_join( th_filler, 0 );   
   
   	printf( "done.\n" );
 
   	return 0;
}