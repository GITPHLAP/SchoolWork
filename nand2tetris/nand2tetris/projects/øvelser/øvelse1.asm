@0 // n
D=M

@i
M=1

@sum
M=0

(LOOP)
@i 
D=M
@0
D=D-M
@STOP
D;JGT

@sum
M=M+D

@i
M=M+1

@LOOP
0;JMP

(STOP)

@END
0;JMP