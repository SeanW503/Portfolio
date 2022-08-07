#################################################################################################		
.data
msg1: .asciiz "Enter an IP address: \n"
first: .asciiz "First: "
second: .asciiz "Second: "
third: .asciiz "Third: "
fourth: .asciiz "Fourth: "
error: .asciiz "The entered number is larger than 255. \n"
error1: .asciiz "The First byte shouldn't be less than 1.\n"
class_a: .asciiz "\n\nClass A address" 
class_b: .asciiz "\n\nClass B address"
class_c: .asciiz "\n\nClass C address"
IP: .asciiz "IP "
dot: .asciiz "."
newline: .asciiz "\n"
nodom: .asciiz "Matching domain not found "
dom: .asciiz "Matching domain found "
class_d:.asciiz "\n\nClass D address"
#################################################################################################			
	IP_ROUTING_TABLE_SIZE:
		.word	10
	IP_ROUTING_TABLE:
		# line #, x.x.x.x -------------------------------------
		.byte 0, 146, 92, 255, 255 # 146.92.255.255
		.byte 1, 147, 163, 255, 255 # 147.163.255.255
		.byte 2, 201, 88, 88, 90 # 201.88.88.90
		.byte 3, 182, 151, 44, 56 # 182.151.44.56
		.byte 4, 24, 125, 100, 100 # 24.125.100.100
		.byte 5, 146, 163, 140, 80 # 146.163.170.80
		.byte 6, 146, 163, 147, 80 # 146.163.147.80
		.byte 10, 201, 88, 102, 80 # 201.88.102.1
		.byte 11, 148, 163, 170, 80 # 146.163.170.80
		.byte 12, 193, 77, 77, 10 # 193.77.77.10
#################################################################################################		
.text
.globl main
#################################################################################################		
main:
   li $v0, 4 
   la $a0, msg1 
   syscall
#################################################################################################		
first1:
   li $v0, 4 
   la $a0, first 
   syscall
  
   li $v0, 5 
   syscall 
   move $t0, $v0 
   blt $t0,1,first_error1 
   
   bgt $t0,255,first_error 
#################################################################################################		
second1:
   li $v0, 4 
   la $a0, second 
   syscall
  
   li $v0, 5 
   syscall 
   move $t1, $v0 
   bgt $t1,255,second_error 
   blt $t1, 1, fifth2
#################################################################################################		  
third1:   
   li $v0, 4 
   la $a0, third 
   syscall
  
   li $v0, 5 
   syscall 
   move $t2, $v0 
   bgt $t2,255,third_error 
   blt $t2, 1, fifth3
#################################################################################################		 
forth1: 
   li $v0, 4 
   la $a0, fourth 
   syscall
  
   li $v0, 5 
   syscall 
   move $t3, $v0 
   bgt $t3,255,fourth_error 
   blt $t3,1,fifth4
#################################################################################################		
#######finding class#######

   blt $t0,128,class_aa
 
   blt $t0,192,class_bb
  
   blt $t0,224,class_cc 
#################################################################################################		
class_aa: 
	
   li $v0, 4 
   la $a0, class_a
   syscall
  

	li $v0, 4
	la $a0, newline
	syscall
	
	add $s6, $s6, 1
	
j load
#################################################################################################		
class_bb: 

   li $v0, 4    
   la $a0, class_b
   syscall
   
 
   li $v0, 4
   la $a0, newline
   syscall
	
   add $s6, $s6, 2
	
j load
#################################################################################################		  
class_cc: 
   li $v0, 4    
   la $a0, class_c
   syscall
   
   
   li $v0, 4
   la $a0, newline
   syscall
	
   add $s6, $s6, 3
   
j load
#################################################################################################			
class_dd:
   li $v0, 4    
   la $a0, class_d
   syscall
   
   
   li $v0, 4
   la $a0, newline
   syscall	
	
   add $s6, $s6, 4
   
j nomatch
#################################################################################################		  
   
   
nomatch: 
	li $v0, 4
	la, $a0, nodom
	syscall
jr $31
#################################################################################################		
match:
	move $s3, $t5
	beq $t4,$s1, out
j loadl
#################################################################################################			  
load: 
	la $s2, IP_ROUTING_TABLE
	lw $s1, IP_ROUTING_TABLE_SIZE
#################################################################################################		
loadl:

	lbu $t5,  ($s2)
	lbu $t6, 1($s2)
	lbu $t7, 2($s2)
	lbu $t8, 3($s2)
	lbu $t9, 4($s2)

	li $v0, 4
	la $a0, IP
	syscall
	
	li $v0, 1
	move $a0, $t6
	syscall
	
	li $v0, 4
	la $a0, dot
	syscall
	
	li $v0, 1
	move $a0, $t7
	syscall
	
	li $v0, 4
	la $a0, dot
	syscall
	
	li $v0, 1
	move $a0, $t8
	syscall
    	
	li $v0, 4
	la $a0, dot
	syscall
	
	li $v0, 1
	move $a0, $t9
	syscall
	
	li $v0, 4
	la $a0, newline
	syscall
	
	addi $s2, $s2, 5
	add $t4,$t4, 1
	
	
	beq $s6, 1, class_a_check
	beq $s6, 2, class_b_check
	beq $s6, 3, class_c_check
	beq $s6, 4, nomatch
##############################CLASS A CHECK######################################################			
class_a_check:
		beq $t6, $t0,match
		blt $t4, $s1, loadl
		beq $s3, 0, nomatch
		
	j out
##############################CLASS B CHECK######################################################				
class_b_check:

	beq $t0,$t6, b_second
	blt $t4, $s1, loadl
		b_second:
			beq $t1,$t7, match
			blt $t4, $s1, loadl
			beq $s3, 0, nomatch
	j out
##############################CLASS C CHECK######################################################				
class_c_check:
	beq $t0, $t6, c_second
	blt $t4, $s1 loadl
		c_second: 
			beq $t1,$t7, c_third
			blt $t4, $s1, loadl
				c_third:
					beq $t2, $t8, match
					blt $t4, $s1 loadl
					beq $s3, 0, nomatch
	j out
#################################################################################################			
###### Errors ######
first_error:
   li $v0, 4 
   la $a0, error 
   syscall
j first1 
 

first_error1:
   li $v0, 4 
   la $a0, error1 
   syscall
j first1

second_error:
   li $v0, 4 
   la $a0, error 
   syscall
j second1 


third_error:
   li $v0, 4 
   la $a0, error
   syscall
j third1 

fourth_error:
   li $v0, 4 
   la $a0, error 
   syscall
j forth1 
#################################################################################################				  
out:
	li $v0,4
	la $a0,dom
	syscall
	
	li $v0, 1
	move $a0, $s3
	syscall
  
######ending of program######
jr $31
#################################################################################################		
fifth11:

	li $v0, 4
	la $a0, error1
	syscall
	
j first1

fifth2:

	li $v0, 4
	la $a0, error1
	syscall
	
j second1

fifth3:

	li $v0, 4
	la $a0, error1
	syscall
	
j third1

fifth4:

	li $v0, 4
	la $a0, error1
	syscall
	
j forth1
#################################################################################################		