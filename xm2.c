/* *************************************************** *
 * Sean Whitley                                           *
 * sewhitl@siue.edu
 * 337
 * xm2.c                                                     *
 * Febuary 14, 2022                                     *
 *                                                     *
 * *************************************************** */
#include <stdio.h>         // for printf
#include <time.h>          // for srand
#include <sys/types.h>     // for message queue
#include <sys/ipc.h>       // for message queue
#include <sys/msg.h>       // for message queue
#include <errno.h>         // checks queue
#define PERMS 0666         // key for OS
#define TRUE            1  // TRUE label
#define FALSE           0  // FALSE label
#define MSG_key_Q1   8375  // message queue key for Q1
#define MSG_key_Q3   9375  // message queue key for Q3
#define BUFFER_SIZE   256  // defines the buffer size

//main
int main(void)
{
//message struct 
struct message
{
        long mtype;
        unsigned int data;
};

//instaniating variables
int msqid_Q1;
int msqid_Q3;
key_t msgkey_Q1 = MSG_key_Q1; 
key_t msgkey_Q3 = MSG_key_Q3; 
int x2;
int  status_code;  
struct message message_struct;
int total;
int x2total;

//for accessing the Q1 message
msqid_Q1 = msgget(msgkey_Q1, 0666);

//for sending the Q3 message
msqid_Q3 = msgget(msgkey_Q3, 0666 | IPC_CREAT);

if (msqid_Q1 <= -1)
   { printf ("your new message queue (Q1) is not recieved ....\n");  }

else
   { printf("your new message queue (Q1) is successfully recieved ....\n"); }

if (msqid_Q3 <= -1)
   { printf ("your new message queue (Q3) is not created ....\n");  }

else
   { printf("your new message queue (Q3) is successfully created ....\n"); }

//for loop to recieve, check, create, deleting,  and sending queues
for(; ;)
{
status_code = msgrcv(msqid_Q1, &message_struct, sizeof(message_struct.data), 0, 0);
   //error for negative numbers
   if (status_code <= -1)
      {
        //reaching the end of the queue
        if(errno == EIDRM){
                //deleting the queue
               status_code = msgctl(msqid_Q3, IPC_RMID, NULL); 
               if (status_code <= -1)
                { printf("deleting Q3 failed ...\n\a");  } 
                   else
                { printf("xm2 is terminating (total: %d, x2 integers: %d) . . .\n", total, x2total); }
                break;
        }
        //recieving number from the queue failed
         printf("receiving a number to Q1 failed ... \n\a");          
        
      }
                else{
                        printf("xm2 received: %d\n", message_struct.data);
                        total++;
                        //check if integers are a multiple of 2
                        if(message_struct.data %2 == 0){
                                x2total++;
                                status_code = msgsnd(msqid_Q3, &message_struct, sizeof(message_struct.data), 0);
                                //error for sending  
                                if (status_code <= -1)
                                {
                                printf("sending a number to Q3 failed ... \n\a");          
                                }
                            }

                    }
}

}




