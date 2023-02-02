@2
M=0

@i
M=0
(LOOP)
@i
D=M
@1
D=D-M
@END
D;JGE

@2
D=M
@0
D=D+M

@2 // Save result into RAM[2]
M=D 

@i //i++
M=M+1

@LOOP
0;JMP

(END)
@R2 
D=M
@R3 
M=D // Set the result from RAM[2] to RAM[3]
@END
0;JMP

