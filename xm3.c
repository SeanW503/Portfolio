/* *************************************************** *
 * Sean Whitley                                           *
 * sewhitl@siue.edu
 * 337
 * xm3.c                                                     *
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
#define MSG_key_Q2   8875  // message queue key for Q2
#define MSG_key_Q4   9675  // message queue key for Q4
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
int msqid_Q2;
int msqid_Q4;
key_t msgkey_Q2 = MSG_key_Q2; 
key_t msgkey_Q4 = MSG_key_Q4; 
int x3;
int  status_code;  
struct message message_struct;
int total;
int x3total;

//for accessing the Q2 message
msqid_Q2 = msgget(msgkey_Q2, 0666);

//for sending the Q4 message
msqid_Q4 = msgget(msgkey_Q4, 0666 | IPC_CREAT);

if (msqid_Q2 <= -1)
   { printf ("your new message queue (Q2) is not recieved ....\n");  }

else
   { printf("your new message queue (Q2) is successfully recieved ....\n"); }

if (msqid_Q4 <= -1)
   { printf ("your new message queue (Q4) is not created ....\n");  }

else
   { printf("your new message queue (Q4) is successfully created ....\n"); }

//for loop to recieve, check, create, deleting,  and sending queues
for(; ;)
{
status_code = msgrcv(msqid_Q2, &message_struct, sizeof(message_struct.data), 0, 0);
   //error for negative numbers
   if (status_code <= -1)
      {
        //reaching the end of the queue
        if(errno == EIDRM){
                //deleting the queue
               status_code = msgctl(msqid_Q4, IPC_RMID, NULL); 
               if (status_code <= -1)
                { printf("deleting Q4 failed ...\n\a");  } 
                   else
                { printf("xm2 is terminating (total: %d, x3 integers: %d) . . .\n", total, x3total); }
                break;
        }
        //recieving number from the queue failed
         printf("receiving a number to Q2 failed ... \n\a");          
        
      }
                else{
                        printf("xm2 received: %d\n", message_struct.data);
                        total++;
                        //check if integers are a multiple of 2
                        if(message_struct.data %3 == 0){
                                x3total++;
                                status_code = msgsnd(msqid_Q4, &message_struct, sizeof(message_struct.data), 0);
                                //error for sending  
                                if (status_code <= -1)
                                {
                                printf("sending a number to Q4 failed ... \n\a");          
                                }
                            }

                    }
}

}


