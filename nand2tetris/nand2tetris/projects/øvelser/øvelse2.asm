// @0 // n = RAM[0]
// D=M
// @n
// M=D

@i // Set i to 0
M=0

@SCREEN
D=A

@addr
M=D

(LOOP)
@i 
D=M
@0 // n run 
D=D-M
@END
D;JGT

// draw
@addr
A=M
M=-1

@i
M=M+1 // i++

@32
D=A
@addr
M=M+D

@LOOP
0;JMP

(END)
@END
0;JMP