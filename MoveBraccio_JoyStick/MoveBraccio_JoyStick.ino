#include "BraccioV2.h"
Braccio arm;


int led = 13;
int pauseLed = 8;

//Variables to joystick inputs
int joy_1_Y_pin = A3; //connectd to analog 1 for joystick X value for joystick 1
int joy_1_X_pin = A2; //connectd to analog 2 for joystick Y value for joystick 1
int joy_1_switch_pin = 2; //variable connectd to digital 2 for joystick switch value for joystick 1

int joy_2_Y_pin = A1; //connectd to analog 1 for joystick X value for joystick 1
int joy_2_X_pin = A4; //connectd to analog 2 for joystick Y value for joystick 1
int joy_2_switch_pin = 4; //variable connectd to digital 2 for joystick switch value for joystick 1


int joy1_buttonState = 0; //Variable
int joy2_buttonState = 0; //Variable

int val_1_X; //variable to read from joystick 1 x-analogpin
int val_1_Y; //variable to read from joystick 1 y-analogpin
int val_2_X; //variable to read from joystick 2 x-analogpin
int val_2_Y; //variable to read from joystick 2 y-analogpin

int defaultposition = 90; //set the base value for servos

int servo_1_X = 0; //variable to set the position for the servo from joystick 1 X value
int servo_1_Y = 0; //variable to set the position for the servo from joystick 1 Y value
int servo_2_X = 0; //variable to set the position for the servo from joystick 2 X value
int servo_2_Y = 0; //variable to set the position for the servo from joystick 2 Y value

bool firstservosToControl = true;
bool isGripOpen = true;

#define GRIPPER_CLOSED 85
#define GRIPPER_OPENED 20

void setup() {
  Serial.begin(9600);
  Serial.println("Initializing...");//Start of initialization
  
  /***** Begin BASE_ROT Configuration *****/
  arm.setJointCenter(BASE_ROT, 90);
  arm.setJointMin(BASE_ROT, 0);
  arm.setJointMax(BASE_ROT, 180);
  /***** End BASE_ROT Configuration *****/

  /***** Begin SHOULDER Configuration *****/
  arm.setJointCenter(SHOULDER, 90);
  arm.setJointMin(SHOULDER, 0);
  arm.setJointMax(SHOULDER, 180);
  /***** End SHOULDER Configuration *****/

  /***** Begin ELBOW Configuration *****/
  arm.setJointCenter(ELBOW, 90);
  arm.setJointMin(ELBOW, 0);
  arm.setJointMax(ELBOW, 180);
  /***** End ELBOW Configuration *****/

  /***** Begin WRIST Configuration *****/
  arm.setJointCenter(WRIST, 90);
  arm.setJointMin(WRIST, 0);
  arm.setJointMax(WRIST, 180);
  /***** End WRIST Configuration *****/

  /***** Begin WRIST_ROT Configuration *****/
  arm.setJointCenter(WRIST_ROT, 90);
  arm.setJointMin(WRIST_ROT, 0);
  arm.setJointMax(WRIST_ROT, 180);
  /***** End WRIST_ROT Configuration *****/

  /***** Begin GRIPPER Configuration *****/
  arm.setJointCenter(GRIPPER, 40);
  arm.setJointMin(GRIPPER, 15);
  arm.setJointMax(GRIPPER, 90);
  /***** End GRIPPER Configuration *****/
  //There are two ways to start the arm:
  //1. Start to default position.
  arm.begin(true);// Start to default center position

  Serial.println("Ready");
  pinMode(joy_1_switch_pin, INPUT_PULLUP);
  pinMode(joy_2_switch_pin, INPUT_PULLUP);
  pinMode(led, OUTPUT);
  pinMode(pauseLed, OUTPUT);

}

void loop() {
  joy1_buttonState = digitalRead(joy_1_switch_pin);
  joy2_buttonState = digitalRead(joy_2_switch_pin);
  val_1_X = analogRead(joy_1_X_pin); //reads X value from joystick 1
  val_1_Y = analogRead(joy_1_Y_pin); //reads Y value from joystick 1
  val_2_X = analogRead(joy_2_X_pin); //reads X value from joystick 2
  val_2_Y = analogRead(joy_2_Y_pin); //reads Y value from joystick 2 
  Serial.print("Joystick1 - X: ");
  Serial.print(val_1_X);
  Serial.print(" Y: ");
  Serial.print(val_1_Y);
  Serial.print("  Joystick2 - X: ");
  Serial.print(val_2_X);
  Serial.print(" Y: ");
  Serial.println(val_2_Y);
  changeServoToControl(joy1_buttonState); //method to change servos to control

  openAndCloseGrip(joy2_buttonState);

  setpositionforservo(val_1_X, findServoNr(firstservosToControl, true));
  //delay(10);
  setpositionforservo(val_1_Y, findServoNr(firstservosToControl, false));
  //delay(10);
  setpositionforservo(val_2_X, 0); //Set position for base 
  //delay(10);
  setpositionforservo(val_2_Y, 1); //Set position for shoulder 
  //Serial.println(joy2_buttonState);

  char c = Serial.read();
  if (c != -1) { //if c is not empty 
    if (c == 'c') { //if c (input) is equal 'c'
      Serial.println("Center");
      arm.setAllAbsolute(arm.getCenter(0), arm.getCenter(1),
                         arm.getCenter(2), arm.getCenter(3),
                         arm.getCenter(4), arm.getCenter(5));
      arm.safeDelay(3000);
    }
    else if (c == 'p') { // if c equal 'p' then pause 
      bool stay = true;
      digitalWrite(pauseLed, HIGH);
      while (stay) {
        char new_c = Serial.read();
        if (new_c == 'q') {
          stay = false;
          digitalWrite(pauseLed, LOW);
        }
      }
    }
  }

}
int findServoNr(bool _firstservosToControl, bool xPosition) {
  if (_firstservosToControl) {
    if (xPosition) {
      return 2;
    }
    return 3;
  }
  else {
    if (xPosition) {
      return 4;
    }
    return 5;
  }
}
void setpositionforservo(int value, int servoNr)
{

  if (value > 900)
  {
    moveServo(servoNr, 1);
    Serial.print("ServoNr: ");
    Serial.println(servoNr);
    //Missing currentposition
  }
  else if (value < 300)
  {
    moveServo(servoNr, -1);
    Serial.print("ServoNr: ");
    Serial.println(servoNr);
  }
  //delay(15);

}
void changeServoToControl(int buttonstate)
{
  if (buttonstate == 0) {
    if (firstservosToControl) {
      firstservosToControl = false;
      turnLightsOn();
    }
    else {
      firstservosToControl = true;
      turnLightsOff();
    }
  }

}

void openAndCloseGrip(int button)
{
  if (button == 0) {
    if (isGripOpen) {
      isGripOpen = false;
      closeGripper();
    }
    else {
      isGripOpen = true;
      openGripper();
    }
  }

}
void moveServo(int joint, int value) {
  arm.setOneRelative(joint, value); //Validate the value and set value to target position
  arm.update(); //move servors to target position
  //arm.safeDelay(100);

}
void turnLightsOn() {
  digitalWrite(led, HIGH);
}
void turnLightsOff() {
  digitalWrite(led, LOW);
}
void openGripper() {
  //Set the gripper position to open
  arm.setOneAbsolute(5, GRIPPER_OPENED);
  arm.safeDelay(3000);

}

void closeGripper() {
  //Set the gripper position to closed
  arm.setOneAbsolute(5, GRIPPER_CLOSED);
  arm.safeDelay(3000);
}
