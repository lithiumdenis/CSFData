#include <stdio.h>
#include <pthread.h>
#include <unistd.h>
#include <string.h>

pthread_mutex_t shared_array_mutex = PTHREAD_MUTEX_INITIALIZER;
char shared_array_changed = 0;
int  a[] = {1, 2, 3, 4, 5, 6, 7, 8};
int  b[] = {1, 3, 5, 7, 11, 13, 17, 19, 23};
int  c[] = {-9, -7, -5, -3, -1, 0, 1, 2};


void * thread_func1 (void * arg)
{
	int i;
	for(i = 0; i < 8; i++)
	{
		for(;;)
      		{
         		pthread_mutex_lock( &shared_array_mutex );
         		if( shared_array_changed == 1 )
            			break;
         		pthread_mutex_unlock( &shared_array_mutex );
      		}	
		
        	a[i] = a[i] + b[i];
                shared_array_changed = 0;   
                pthread_mutex_unlock( &shared_array_mutex );  
        }
	pthread_exit( 0 );
}

void * thread_func2 (void * arg)
{
	//sleep (5);
	int i;
	for(i = 0; i < 8; i++)
        {
		for(;;)
      		{
         		pthread_mutex_lock( &shared_array_mutex );
         		if( shared_array_changed == 0 )
            			break;
         		pthread_mutex_unlock( &shared_array_mutex );
      		}

        	c[i] = a[i] + c[i];
		shared_array_changed = 1;
		pthread_mutex_unlock( &shared_array_mutex );   
	}
	pthread_exit( 0 );
}

int main (void)
{
	int i;
	
        fprintf (stderr, "Before work\n");
        fprintf (stderr, " a \t b \t c\n");
		for(i = 0; i < 8; i++)
			fprintf (stderr, " %d \t %d \t %d\n", a[i], b[i], c[i]);

	pthread_t thread1, thread2;

	pthread_create (&thread1, NULL, &thread_func1, NULL); 
	pthread_create (&thread2, NULL, &thread_func2, NULL);

	fprintf (stderr, "After work\n");
        
	pthread_join( thread1, 0 );
   	pthread_join( thread2, 0 );  

	fprintf (stderr, " a \t b \t c\n");
		for(i = 0; i < 8; i++)
			fprintf (stderr, " %d \t %d \t %d\n", a[i], b[i], c[i]);

	return 0;
}
