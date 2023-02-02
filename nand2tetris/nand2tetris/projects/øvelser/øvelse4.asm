// Divide 

@3 // Used to calculation
M=0

@2
M=0 // Used to result

@i
M=0

(LOOP)

@3 // Calculation
D=M
@1 // Calculation and the next 
// Used to check if the next calculation is possible if not then return
D=D+M
@0 // RAM[0] is the first/biggest number
D=M-D
@END
D;JLT

@3
D=M
@1
D=D+M 

@3 // Save calculations into RAM[3]
M=D 

@i //i++
M=M+1

@LOOP
0;JMP

(END)
@i
D=M
@R2 
M=D

// Show modulus in RAM[5]
@R0
D=M
@R3
D=D-M
@R5
M=D
@END
0;JMP
