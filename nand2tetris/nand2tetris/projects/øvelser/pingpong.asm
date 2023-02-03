// Begin screen = SCREEN


// Up - 131
// Down - 133
// Left - 130
// Right - 132

// Last number of screen 24575
// First number of screen 16384

@24575
D=A
@16
D=D-A
@position // Save the position
M=D
A=D
M=-1

@Ti
M=0

(LOOP)

@KBD
D=M
@131
D=D-A
@DRAW_LEFT_UP
D;JEQ

@KBD
D=M
@133
D=D-A
@DRAW_LEFT_DOWN
D;JEQ

@LOOP
0;JMP


(DRAW_LEFT_UP)
// When left substrakt 1 from number 

// Check if absolut position
@16384
D=A
@position
D=D-M
@LOOP
D;JEQ


// First draw the new line
@position
D=M
@32
D=D-A
@pixel
A=D
M=1
M=1
M=1
M=1
M=1

// Then remove the last drawing
@position
D=M
@pixel
A=D
M=0

// New position
@32
D=A
@position 
M=D-M

@TIMER
0;JMP


(DRAW_LEFT_DOWN)
// When right add to the number 

@24575
D=A
@position
D=D-M
@LOOP
D;JEQ

@position
D=M
@pixel
A=D+1 // First draw the new line
M=-1
A=D // Then remove the last drawing
M=0
@position // New position
M=D+1

@TIMER
0;JMP


(TIMER)
@200
D=A
@Ti
D=D-M
@TIMER_RESET
D;JEQ

(INNER_TIMER)
@40
D=A
@ITi
D=D-M
@INNER_TIMER_END
D;JEQ

@SUM
D=M+D
M=D

@ITi
M=M+1
@INNER_TIMER
0;JMP

@SUM
D=M+D
M=D

(INNER_TIMER_END)
@ITi
M=0
@Ti
M=M+1
@TIMER
0;JMP

(TIMER_RESET)
@Ti
M=0
@ITi
M=0

@LOOP
0;JMP