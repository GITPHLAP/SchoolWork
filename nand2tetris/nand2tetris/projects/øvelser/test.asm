// Power
// R0 is first number
// R1 is the power number
@2
M=0

@i
M=1

@R0
D=M
@R5
M=D

(LOOP)
@i
D=M
@1
D=D-A
@ENDPOWERONE
D;JEQ

@i
D=M
@1
D=D-M
@END
D;JEQ


@j
M=0
@2
M=0
(INNERLOOP)
@j
D=M
@0
D=D-M
@ENDINNER
D;JGE

@2
D=M
@R5
D=D+M

@2 // Save result into RAM[2]
M=D 

@j //j++
M=M+1


@INNERLOOP
0;JMP

(ENDINNER)

@R2
D=M
@R5
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

(ENDPOWERONE) // Set the result to 10 if its power 1
@10
D=A
@R2
M=D
@END
0;JMP