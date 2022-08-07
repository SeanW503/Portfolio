/* ********************************************************* *
 * p2_required_base.c:                                       *
 *                                                           *
 * My 3-digits: 337                                       *
 * Name: Sean Whitley                                                 *
 *                                                           *
 * plathome: os.cs.siue.edu                                  *
 * compile: "cc Project2.c"                                    *
 *                                                           *
 * 1:08 p.m., March 23, 2022                                 *
 * ********************************************************* */

/* header files ============================================ */

#include <stdio.h>
#include <stdlib.h> 
#include <sys/types.h>
#include <unistd.h>  

// the followings are for semaphores ----- 
#include <sys/sem.h>
#include <sys/ipc.h>

// the followings are for shared memory ----
#include <sys/shm.h>  
#include <errno.h>
#include <signal.h>
#include <string.h>

#define SHM_KEY            7170     // the shared memory key  
#define SEM_KEY_01         8170     //sem key
#define SEM_KEY_02         8171     //sem key 
#define SEM_KEY_03         8172     //sem key h1
#define SEM_KEY_04         8173     //sem key h2
#define SEM_KEY_05         8174     //sem key h3
#define SEM_KEY_06         8175     //sem key hpa
#define SEM_KEY_07         8176     //high prioirty starvation fix
#define SEM_KEY_08         8177     //sem key termination




#define DELAY_TIME     10000     // 10000 micro-seconds = 0.01 second

/* labels (pre-compoler declartives) ======================= */
#define NUM_REPEATS         20    // number of loops for high-priority processes
#define MAX_MSG_SIZE       512    // the max. msg size

/* the delay table ========================================= */
#define H1_TIME_IN      800000      //    800ms  = 0.8 seconds (STR)  
#define H1_TIME_OUT     100000      //    100ms  = 0.1 seconds

#define H2_TIME_IN      800000      //    800ms  = 0.8 seconds (STR)  
#define H2_TIME_OUT     100000      //    100ms  = 0.1 seconds

#define H3_TIME_IN      800000      //    800ms  = 0.8 seconds (STR)   
#define H3_TIME_OUT     100000      //    100ms  = 0.1 seconds

#define L1_TIME_IN     1000000      //   1000ms  = 1.0 seconds (STR) 
#define L1_TIME_OUT      50000      //     50ms  = 0.05 seconds (STR)

#define L2_TIME_IN     1500000      //   1500ms  = 1.5 seconds (STR)   
#define L2_TIME_OUT      50000      //     50ms  = 0.05 seconds (STR)

#define L3_TIME_IN     2000000      //   2000ms  = 2.0 seconds (STR)  
#define L3_TIME_OUT      50000      //     50ms  = 0.05 seconds (STR)
#define timeout        2000000 

/* function prototype --------------------------------------- */
void millisleep(unsigned ms);       // for random sleep time

// shared memory definition ------------------------------------------
struct my_mem {
   char     shared_msg[MAX_MSG_SIZE];   // the shared message
   int Hproccessalive; 
   int lcount;
   // you can have your own shared variables
};

//process functions
  void process_H1();
  void process_H2();
  void process_H3();
  void process_L1();
  void process_L2();
  void process_L3();
  


 pid_t  process_id;  
   int    i;                     // external loop counter  
   int    j;                     // internal loop counter  
   int    k = 0;                 // dumy integer  

   int    sem_id_01;             // the semaphore ID (1)
   int    sem_id_02;             // the semaphore ID (2)
   int    h1hasStarted;
   int    h2hasStarted;
   int    h3hasStarted;
   int    hpa;
   int    semstarv;
   int    semterm;
 


   struct sembuf operations[1];  // Define semaphore operations 
   int    ret_val;               // system-call return value    

   int    shm_id;                // the shared memory ID 
   int    shm_size;              // the size of the shared memoy  
   struct my_mem * p_shm;        // pointer to the attached shared memory 


//alarm handler for fixing starvation
volatile int alarm_triggered = 0;
void alarm_handler(int sig)
{
    alarm_triggered = 1;
}

// THE MODULE MAIN ---------------------------------------------------
int main (void)
{
//alarm signal
signal(SIGALRM, alarm_handler);




   // Semaphore control data structure --------
   union semun {
        int    val;  
        struct semid_ds  *buf;  
        ushort * arry;
   } argument; 
   argument.val = 1;   // the initial value of the semaphore     

   // find the shared memory size in bytes ----
   shm_size = sizeof(struct my_mem);   
   if (shm_size <= 0)
   {  
      printf("sizeof error in acquiring the shared memory size. Terminating ..\n");
      sleep(1);
      exit(0); 
   }    
 
    // create a new semaphore (1) ---------------
   sem_id_01 = semget(SEM_KEY_01, 1, 0666 | IPC_CREAT); 
   if (sem_id_01 < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
    else{
      printf("Semaphore 1 is created\n");
    }

   // initialzie the new semapahore (1) by 1  ----
   if (semctl(sem_id_01, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore by 1. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }

   // create a new semaphore (2) -----------------
   sem_id_02 = semget(SEM_KEY_02, 1, 0666 | IPC_CREAT); 
   if (sem_id_02 < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
    else{
      printf("Semaphore 2 is created\n");
    }

  //initialize sem 2
   if (semctl(sem_id_02, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (2) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }
  //create h1 sem
   h1hasStarted = semget(SEM_KEY_03, 1, 0666 | IPC_CREAT); 
   if (h1hasStarted < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
   else{
      printf("Semaphore h1hasStarted is created\n");
    }
     if (semctl(h1hasStarted, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (h1hasStarted) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }
  //create h2 sem
  h2hasStarted = semget(SEM_KEY_04, 1, 0666 | IPC_CREAT); 
   if (h2hasStarted < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
   else{
      printf("Semaphore h2hasStarted is created\n");
    }
     if (semctl(h2hasStarted, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (h2hasStarted) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }
  //create h3 sem
  h3hasStarted = semget(SEM_KEY_05, 1, 0666 | IPC_CREAT); 
   if (h3hasStarted < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
   else{
      printf("Semaphore h3hasStarted is created\n");
    }
    if (semctl(h3hasStarted, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (h3hasStarted) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }
  //crate hpa sem
   hpa = semget(SEM_KEY_06, 1, 0666 | IPC_CREAT); 
   if (hpa < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
   else{
      printf("Semaphore hpa is created\n");
    }
    if (semctl(hpa, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (hpa) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }

  //create staration sem
   semstarv = semget(SEM_KEY_07, 1, 0666 | IPC_CREAT); 
   if (semstarv < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
   else{
      printf("Semaphore starve is created\n");
    }
    argument.val = 1;
    if (semctl(semstarv, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (starv) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }

  //create termination sem 
   semterm = semget(SEM_KEY_08, 1, 0666 | IPC_CREAT); 
   if (semterm < 0)
   {
      printf("Failed to create a new semaphore. Terminating ..\n"); 
      sleep(1);
      exit(0);
   }
   else{
      printf("Semaphore term is created\n");
    }
    //initalize it at 6
    argument.val = 6;
    if (semctl(semterm, 0, SETVAL, argument) < 0)
   {
      printf("Failed to initialize the semaphore (term) by 0. Terminating ..\n"); 
      sleep(1);
      exit(0);  
   }



   // create a shared memory ----------------------
   shm_id = shmget(SHM_KEY, shm_size, 0666 | IPC_CREAT);         
   if (shm_id < 0) 
   {
      printf("Failed to create the shared memory. Terminating ..\n");  
      sleep(1);
      exit(0);  
   } 

   // attach the new shared memory ----------------
   p_shm = (struct my_mem *)shmat(shm_id, NULL, 0);     
   if (p_shm == (struct my_mem*) -1)
   {
      printf("Failed to attach the shared memory.  Terminating ..\n"); 
      sleep(1);
      exit(0);   
   }   

   // initialize the shared memory ----------------
   p_shm->lcount = 0;  
   p_shm->Hproccessalive = 3;
   

  //forks for parent process
   process_id = fork();
   if(process_id == 0){
     process_L1();
     exit(0);
   }
   process_id = fork();
     if(process_id == 0){
     process_L2();
     exit(0);
   }
   process_id = fork();
    if(process_id == 0){
     process_L3();
     exit(0);
    }
   process_id = fork();
   if(process_id == 0){
     process_H1();
     exit(0);
   }
   process_id = fork();
     if(process_id == 0){
     process_H2();
     exit(0);
   }

     process_H3();

        // after h3 then you have to wait for all 6 child processes to finish to be able to move on
        
        //wait to terminate
    operations[0].sem_num = 0;  // the first semapahore
    operations[0].sem_op  = 0; // "wait" on the semaphore
    operations[0].sem_flg = 0;  // make sure to block    
    ret_val = semop(semterm, operations, 1); 
    if (ret_val != 0)
    {
      printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
      sleep(1);
    } 
        // detach the shared memory ---
        ret_val = shmdt(p_shm);  
        if (ret_val != 0) 
        {
          printf ("shared memory detach failed ....\n");
          sleep(1);
        }

        ret_val = shmctl(shm_id, IPC_RMID, 0); 
        if (ret_val != 0)
        {
          printf("shared memory ID remove ID failed ... \n");
          sleep(1);
        } 

        ret_val = semctl(sem_id_01, IPC_RMID, 0);  
        if (ret_val != 0)
        {
          printf("semaphore remove ID failed (1) ... \n");
          sleep(1);
        }

        ret_val = semctl(sem_id_02, IPC_RMID, 0);  
        if (ret_val != 0)
        {
          printf("semaphore remove ID failed (2) ... \n");
          sleep(1);
        }
 
        exit(0);  
}
 
/* THE END OF MODULE MAIN ========================================== */

/* Process L1 ====================================================== */
void process_L1()
{


   // L1 starts -----------------------
   printf("L1 starts ....\n");
   printf("L1 starts waiting for three high prioirty process are active \n");


   //sem 3 wait(h1 started) 0 operation(all 3)3 dif 0 ops for 3 sems

   //semop -1 in h1 process(once)
   //h2 -1 for 2nd sem
   //h3 -1 for 3rd sem
   
    //h1 started
    operations[0].sem_num = 0;  // the first semapahore
    operations[0].sem_op  = 0; // "wait" on the semaphore
    operations[0].sem_flg = 0;  // make sure to block    
    ret_val = semop(h1hasStarted, operations, 1); 
    if (ret_val != 0)
    {
      printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
      sleep(1);
    } 
    //h2 has started
    operations[0].sem_num = 0;  // the first semapahore
    operations[0].sem_op  = 0; // "wait" on the semaphore
    operations[0].sem_flg = 0;  // make sure to block    
    ret_val = semop(h2hasStarted, operations, 1); 
    if (ret_val != 0)
    {
      printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
      sleep(1);
    } 
    //h3 has started
    operations[0].sem_num = 0;  // the first semapahore
    operations[0].sem_op  = 0; // "wait" on the semaphore
    operations[0].sem_flg = 0;  // make sure to block    
    ret_val = semop(h3hasStarted, operations, 1); 
    if (ret_val != 0)
    {
      printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
      sleep(1);
    } 


   // the main while loop -------------
   while(1)//infinite loop
   {
      //wait on sem Hproccesalive
        operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(hpa, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 
      //if pshm HPA == 0 then break out of loop
      if(p_shm->Hproccessalive == 0){
        break;
      }

     
      //signal for hpa
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
        //sem starv wait
        operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(semstarv, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 




      // the external random sleep -----------
      millisleep(L1_TIME_OUT);

      // wake-up shout ----------------
      printf("        L1 would like to read ...\n");

     
            //sem 2 wait
            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_02, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 
    
      p_shm->lcount = p_shm->lcount +1;
      if(p_shm->lcount == 1){
            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_01, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 

      }
            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_02, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }


      /* CRITICAL SECTION STARTS ================================ */
      printf("        L1 starts reading ...\n");

      // Read/show the shared message
      printf("                              L1 READ: %s\n", p_shm->shared_msg);

      // the internal sleep -----------
      millisleep(L1_TIME_IN);

      printf("        L1 finishes reading ...\n");
      /* CRITICAL SECTION ENDS ================================== */

      operations[0].sem_num = 0;  // the first semapahore
      operations[0].sem_op  = -1; // "wait" on the semaphore
      operations[0].sem_flg = 0;  // make sure to block    
      ret_val = semop(sem_id_02, operations, 1); 
      if (ret_val != 0)
      {
        printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
        sleep(1);
      } 
    p_shm->lcount = p_shm->lcount -1;
      if(p_shm->lcount ==0){
          operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_01, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }
      }
            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_02, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }


   }
   
   // terminating myself --------------
   printf("L1 is terminating ...\n");
   //signal for termination sem
operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(semterm, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 

}

void process_L2()
{
   // L2 starts -----------------------
   printf("L2 starts ....\n");
   printf("L2 starts waiting for three high prioirty process are active /n");

  //h1 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = 0; // "wait" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h1hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
  //h2 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = 0; // "wait" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h2hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
  //h3 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = 0; // "wait" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h3hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 


   // the main while loop -------------
   while(1)//infinite loop
   {
        //wait on sem Hproccesalive
        operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(hpa, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 
      //if pshm HPA == 0 then break
      if(p_shm->Hproccessalive == 0){
        break;
      }
      //signal for hpa
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }


        //wait on sem starv
         operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(semstarv, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 

      // the external random sleep -----------
      millisleep(L2_TIME_OUT);

      // wake-up shout ----------------
      printf("        L2 would like to read ...\n");


    

            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_02, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 
    
      p_shm->lcount = p_shm->lcount +1;
      if(p_shm->lcount == 1){
            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_01, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 

      }
            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_02, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }


      /* CRITICAL SECTION STARTS ================================ */
      printf("        L2 starts reading ...\n");

      // Read/show the shared message
      printf("                              L2 READ: %s\n", p_shm->shared_msg);

      // the internal sleep -----------
      millisleep(L2_TIME_IN);

      printf("        L2 finishes reading ...\n");
      /* CRITICAL SECTION ENDS ================================== */

      operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_02, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 
    p_shm->lcount = p_shm->lcount -1;
      if(p_shm->lcount ==0){
          operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_01, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }
      }
            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_02, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }


   }
  
   // terminating myself --------------
   printf("L2 is terminating ...\n");
   //sem signal for termination
 operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(semterm, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
}

void process_L3()
{
   // L3 starts -----------------------
   printf("L3 starts ....\n");
   printf("L3 starts waiting for three high prioirty process are active /n");


  //h1 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = 0; // "wait" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h1hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
  //h2 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = 0; // "wait" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h2hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
  //h3 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = 0; // "wait" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h3hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 




   // the main while loop -------------
   while(1)//infinite loop
   {
           //wait on sem Hproccesalive
        operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(hpa, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 
      //if pshm HPA == 0 then break
      if(p_shm->Hproccessalive == 0){
        break;
      }
   
      //signal for hpa
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }

        //wait on sem starv
        operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(semstarv, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 

      // the external random sleep -----------
      millisleep(L3_TIME_OUT);

      // wake-up shout ----------------
      printf("        L3 would like to read ...\n");

     

            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_02, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 
            
    
      p_shm->lcount = p_shm->lcount +1;
      if(p_shm->lcount == 1){
            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_01, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 

      }
            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_02, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }

      
      /* CRITICAL SECTION STARTS ================================ */
      printf("        L3 starts reading ...\n");

      // Read/show the shared message
      printf("                              L3 READ: %s\n", p_shm->shared_msg);

      // the internal sleep -----------
      millisleep(L3_TIME_IN);

      printf("        L3 finishes reading ...\n");
      /* CRITICAL SECTION ENDS ================================== */
   
      operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_02, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 
    p_shm->lcount = p_shm->lcount -1;
      if(p_shm->lcount ==0){
          operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_01, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }
      }
            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_02, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }


   }
   
   // terminating myself --------------
   printf("L3 is terminating ...\n");
   //sem signal for termination
operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(semterm, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  }
}

/* Process H1 ====================================================== */
void process_H1()
{

  
   // H1 starts -----------------------
   printf("H1 starts ....\n");

  
  //signal for h1
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(h1hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
  
    

   // the main while loop -------------
   for(i = 0; i < NUM_REPEATS; i++)
   {


    
      // the external sleep -----------
      millisleep(H1_TIME_OUT);

      // wake-up shout ----------------
      printf("H1 would like to update ...\n");

        
        
        //alarm for 4 to breakout of semop
        alarm(4);
        //signal for sem starv
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(semstarv, operations, 1);   
        if (ret_val != 0 && errno == EINTR)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
        //end alarm
        alarm(0);
        
            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_01, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 


      /* CRITICAL SECTION STARTS === */
      printf("H1 starts updating ...\n");

      // Update the shared message ----
      for (j = 0; j < MAX_MSG_SIZE; j ++)   // reset the msg
      {  p_shm->shared_msg[j] = '\0';  }
      strcpy(p_shm->shared_msg, "I am H1"); // the new msg

      // the internal sleep -----------
      millisleep(H1_TIME_IN);

      printf("H1 finishes updating ...\n");
      /* CRITICAL SECTION ENDS ===== */

            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_01, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }



   }

    //wait on HPA sem 
    // pshm HPA -- 1(minus equals one)
    // signal HPA sem
     //wait on sem Hproccesalive
        operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(hpa, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 
      
      p_shm->Hproccessalive--;
        
      
      //signal
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
    
   // terminating myself --------------
   printf("H1 is terminating ...\n" );
  //sem signal for termination
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(semterm, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
        //signal for sem starv
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(semstarv, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
        //signal for hpa
         operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }

}
        

void process_H2()
{
   // H2 starts -----------------------
   printf("H2 starts ....\n");

 

  //h2 has started
  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h2hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 


   // the main while loop -------------
   for(i = 0; i < NUM_REPEATS; i++)
   {

     
   
      // the external sleep -----------
      millisleep(H2_TIME_OUT);

      // wake-up shout ----------------
      printf("H2 would like to update ...\n");
      //alarm for 4 to breakout of semop
        alarm(4);
        //signal for sem starv
       operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(semstarv, operations, 1);   
        if (ret_val != 0 && errno == EINTR)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
        //end alarm
        alarm(0);

            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_01, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 

      /* CRITICAL SECTION STARTS === */
      printf("H2 starts updating ...\n");

      // Update the shared message ----
      for (j = 0; j < MAX_MSG_SIZE; j ++)   // reset the msg
      {  p_shm->shared_msg[j] = '\0';  }
      strcpy(p_shm->shared_msg, "I am H2"); // the new msg

      // the internal sleep -----------
      millisleep(H2_TIME_IN);

      printf("H2 finishes updating ...\n");
      /* CRITICAL SECTION ENDS ===== */

            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_01, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }
            

      

   }

      //wait on hpa sem
     operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(hpa, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 
    
    p_shm->Hproccessalive--;


      //signal hpa
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }

   // terminating myself --------------
   printf("H2 is terminating ...\n");
  //sem signal for terminating
   operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(semterm, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 
      //signal for sem starv
    operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(semstarv, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
      //signal for hpa
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }

}

void process_H3()
{
   // H3 starts -----------------------
   printf("H3 starts ....\n");

 

  //h3 has started 

  operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block    
  ret_val = semop(h3hasStarted, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 


   // the main while loop -------------
   for(i = 0; i < NUM_REPEATS; i++)
   {

     
    
      // the external sleep -----------
      millisleep(H3_TIME_OUT);

      // wake-up shout ----------------
      printf("H3 would like to update ...\n");
      //alarm at 4 for breaking out of semop
      alarm(4);
      //singal for semstarv
       operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(semstarv, operations, 1);   
        if (ret_val != 0 && errno == EINTR)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }
        //end alarm
       alarm(0);
          
            operations[0].sem_num = 0;  // the first semapahore
            operations[0].sem_op  = -1; // "wait" on the semaphore
            operations[0].sem_flg = 0;  // make sure to block    
            ret_val = semop(sem_id_01, operations, 1); 
            if (ret_val != 0)
            {
              printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
              sleep(1);
            } 


      printf("H3 would like to update ...\n");
 
      /* CRITICAL SECTION STARTS === */
      printf("H3 starts updating ...\n");

      // Update the shared message ----
      for (j = 0; j < MAX_MSG_SIZE; j ++)   // reset the msg
      {  p_shm->shared_msg[j] = '\0';  }
      strcpy(p_shm->shared_msg, "I am H3"); // the new msg

      // the internal sleep -----------
      millisleep(H3_TIME_IN);

      printf("H3 finishes updating ...\n");
      /* CRITICAL SECTION ENDS ===== */

            operations[0].sem_num = 0;  
            operations[0].sem_op  = 1; // SIGNAL   
            operations[0].sem_flg = 0;
            ret_val = semop(sem_id_01, operations, 1);   
            if (ret_val != 0)  
            {
              printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
              sleep(1);
            }

   }
    //wait on hpa
    operations[0].sem_num = 0;  // the first semapahore
        operations[0].sem_op  = -1; // "wait" on the semaphore
        operations[0].sem_flg = 0;  // make sure to block    
        ret_val = semop(hpa, operations, 1); 
        if (ret_val != 0)
        {
          printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
          sleep(1);
        } 
      
   p_shm->Hproccessalive--;

      //signal for hpa
        operations[0].sem_num = 0;  
        operations[0].sem_op  = 1; // SIGNAL   
        operations[0].sem_flg = 0;
        ret_val = semop(hpa, operations, 1);   
        if (ret_val != 0)  
        {
          printf("V-OP (signal) failed (parent) - semaphore (1) .... \n");
          sleep(1);
        }

   // terminating myself --------------
   printf("H3 is terminating ...\n");
   //sem signal for termination 
   operations[0].sem_num = 0;  // the first semapahore
  operations[0].sem_op  = -1; // "signal" on the semaphore
  operations[0].sem_flg = 0;  // make sure to block   
  ret_val = semop(semterm, operations, 1); 
  if (ret_val != 0)
  {
    printf("P-OP (wait) failed (parent) - semaphore (1) ....\n");
    sleep(1);
  } 

}

/* function "millisleep" ------------------------------------------ */
void millisleep(unsigned micro_seconds)
{ usleep(micro_seconds); }