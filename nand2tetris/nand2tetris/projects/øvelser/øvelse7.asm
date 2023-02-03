// ENTER = 128
// Numbers begin from 48


// Use R0 to ciffer
// Use R1 to the multi number e.g. 100 
// Use R3 to result 
// From R10-R15 the multi numbers starting from 1 to 
@R3
M=0 // Clear the result register

@ciffers
M=0
@i
M=0

(LOOP)

// Check if key is Enter
@128
D=A
@KBD
D=M-D
@SHOWLOOP
D;JEQ

// Check if pressed key is less then numberpad keys 
// Then restart
@48
D=A
@KBD
D=M-D
@LOOP
D;JLT

// Check if pressed key is not bigger then numberpad keys 
// Then restart
@57
D=A
@KBD
D=M-D
@LOOP
D;JGT

// Else save key

// Generate index starting from 50
@50
D=A
@key
M=D
@ciffers
D=M
@key
M=M+D

@KBD
D=M
@48 // Translate the result to the right number
D=D-A
@key
A=M
M=D


// Count ciffers
@ciffers
M=M+1


// Wait for press is released
(UPPRESS)
@KBD
D=M
@UPPRESS
D;JNE

@LOOP
0;JMP

(SHOWLOOP)
@i
D=M
@ciffers
D=M-D
@END
D;JLE

// Get the ciffer 
@50
D=A
@i
D=D+M
@ciffer
A=D
D=M
@R0
M=D

// Set the power number
@ciffers
D=M
@i
D=D-M
D=D-1
@powerNumber
M=D

@TenPower
0;JMP

(TenPowerEnd)


// // Get the multi number
// @ciffers
// D=M
// @i
// D=D-M
// @10
// D=D+A
// D=D-1
// @multicif
// A=D
// D=M
// @R1
// M=D

@i //i++
M=M+1

@MULTI
0;JMP


@SHOWLOOP
0;JMP

(END)
@END
0;JMP


// The ciffer
// And position number - like 100, 10, 1
// Use R0 to ciffer
// Use R1 to the multi number e.g. 100 
// USe R2 to calculations
// Use R3 to result 
(MULTI)
@R2
M=0
@mi
M=0
(MULTILOOP)
@mi
D=M
@0 // Use R0 to ciffer
D=D-M
@STOPMULTI
D;JGE

@2
D=M
@1
D=D+M

@2
M=D

@mi
M=M+1

@MULTILOOP
0;JMP

(STOPMULTI)
// Save the result to R3
@R2
D=M
@R3
M=D+M
@SHOWLOOP
0;JMP



// Power
// powerNumber is the power number
// powerCalc is Calculation
// Result -> R1
// innerCalc is inner calculation 
(TenPower)
@powerCalc
M=0

@10
D=A
@ten
M=D
@innerCalc
M=1

(POWERLOOP)

@powerNumber
D=M
@ENDPOWERLOOP
D;JLE

@powerj
M=0
@powerCalc
M=0
(INNERPOWERLOOP)
@powerj
D=M
@ten
D=D-M
@ENDINNERPOWERLOOP
D;JGE

@powerCalc
D=M
@innerCalc
D=D+M

@powerCalc
M=D 

@powerj //j++
M=M+1


@INNERPOWERLOOP
0;JMP

(ENDINNERPOWERLOOP)

@powerCalc
D=M
@innerCalc
M=D

@powerNumber //i++
M=M-1

@POWERLOOP
0;JMP

(ENDPOWERLOOP)

// Else return the calculation
@innerCalc
D=M
@R1 // Power result set to the multiciffer 
M=D // Set the result from RAM[2] to RAM[3]
@TenPowerEnd
0;JMP