
// Reverse fill screen
// Variable to save color
@color
M=0


(LOOP)


// If case - for key not pressed
@KBD
D=M
@WHITE
D;JEQ // If KBD is 0 no key is pressed

// DRAW Black
@color
M=-1



@DRAWING
0;JMP

(WHITE)
@color
M=0


@DRAWING
0;JMP

(DRAWING)
@i // Set i to 0 row
M=0

@SCREEN
D=A
@8191 // The last adrress for display memory 
D=D+A

@addr
M=D

(DRAWINGLOOP)
@i 
D=M
@8191 // n = rows
D=D-A
@LOOP
D;JGT

@color
D=M

// draw
@addr
A=M
M=D

@i
M=M+1 // i++

@addr
M=M-1

@DRAWINGLOOP
0;JMP

(DRAWINGEND)
@DRAWINGEND
0;JMP

(END)
@END
0;JMP