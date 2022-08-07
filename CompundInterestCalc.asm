# Sean Whitley 800646337

.data
NewLine: .asciiz "\n"
prompt1: .asciiz "Enter the principal: "
prompt2: .asciiz "Enter the interest rate(0.0 < interest < 1.0, 001 for 1%): "
prompt3: .asciiz "Enter the target balance: "
prompt4: .asciiz "Number of the last years for display:  "
promptb: .asciiz "the balance at the end of a year "
prompt5: .asciiz "year "
prompt6: .asciiz ": " 
prompt7: .asciiz "It takes "
prompt8: .asciiz " years."
#######################################################
RLB:  .float 0.0
RHB:  .float 1.0
PLB:  .float 100.00
PHB:  .float 5000000.00
TLB:  .float 101.00 
THB:  .float 5000001.00
#######################################################
.text
.globl main
#######################################################
main:
#bounds
la $a0, RLB
lwc1 $f25, ($a0)

la $a0, RHB
lwc1 $f26, ($a0)

la $a0, PLB
lwc1 $f27, ($a0)

la $a0, PHB
lwc1 $f28, ($a0)

la $a0, TLB
lwc1 $f29, ($a0)

la $a0, THB
lwc1 $f30, ($a0)
#####################################################
loop1:
#prompt
li $v0, 4
la $a0, prompt1
syscall 

#get user input
li $v0, 6
syscall

#store result in $f0
mov.s $f4, $f0

#compare input to bounds
c.lt.s $f4, $f27
bc1t loop1

c.lt.s $f28, $f4
bc1t loop1

loop2:
#prompt
li $v0, 4
la $a0, prompt2
syscall 

#get user input
li $v0, 6
syscall

#store result in $f0
mov.s $f5, $f0

#compare input to bounds
c.lt.s $f5, $f25
bc1t loop2

c.lt.s $f26, $f5
bc1t loop2

loop3:
#prompt
li $v0, 4
la $a0, prompt3
syscall 

#get user input
li $v0, 6
syscall

#store result in $f0
mov.s $f6, $f0

#compare input to bounds
c.lt.s $f6, $f29
bc1t loop3

c.lt.s $f30, $f6
bc1t loop3

loop4:
#prompt
li $v0, 4
la $a0, prompt4
syscall 
li $v0, 5 
syscall
move  $t1, $v0
li $v0, 4
la $a0, promptb
syscall
li $v0, 4
la $a0, NewLine
syscall


#loopcounter 
li $t0, 0 
li $t6, 0
##########################################
sw $ra, ($sp)
##########################################
jal CPI_Rec
##########################################
lw $ra, ($sp)
##########################################
li $v0, 4
la $a0, NewLine
syscall
li $v0, 4 
la $a0, prompt7
syscall
li $v0, 1
move $a0, $t0
syscall
li $v0, 4 
la $a0, prompt8
syscall


##########################################

jr $31
#endmain

##########################################



CPI_Rec:
#create a stack

subu $sp, $sp, 12
sw $ra, 0($sp)

mul.s $f2, $f4, $f5
add.s $f4, $f4, $f2
add $t0, $t0, 1


s.s $f4, 4($sp)
sw $t0, 8($sp)



c.lt.s $f6, $f4
bc1t LStack

jal CPI_Rec

LStack:

l.s $f4, 4($sp)
lw $t5, 8($sp)


blt $t1, 0, Less0
bgt $t1, 0, Great0

Less0:
li $v0, 4
la $a0, prompt5
syscall

li $v0, 1
move $a0, $t5
syscall

li $v0, 4
la $a0, prompt6
syscall

li $v0, 2
mov.s $f12, $f4
syscall
li $v0, 4
la $a0, NewLine
syscall

j end

Great0:
add $t6, $t6, 1
bgt $t6, $t1, end



li $v0, 4
la $a0, prompt5
syscall

li $v0, 1
move $a0, $t5
syscall

li $v0, 4
la $a0, prompt6
syscall

li $v0, 2
mov.s $f12, $f4
syscall

li $v0, 4
la $a0, NewLine
syscall
end:

lw $ra, ($sp)
addu $sp, $sp, 12

jr $ra

###########################################




