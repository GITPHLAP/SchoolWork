#include <Arduino_FreeRTOS.h>

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

int servo_0_value = 0;
int servo_1_value = 0;
int servo_2_value = 0;
int servo_3_value = 0;
int servo_4_value = 0;
int servo_5_value = 0;

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
  //Serial.print("Joystick1 - X: ");
  //Serial.print(val_1_X);
  //Serial.print(" Y: ");
  //Serial.print(val_1_Y);
  //Serial.print("  Joystick2 - X: ");
  //Serial.print(val_2_X);
  //Serial.print(" Y: ");
  //Serial.println(val_2_Y);
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
      servo_0_value = arm.getCenter(0);
      servo_1_value = arm.getCenter(1);
      servo_2_value = arm.getCenter(2);
      servo_3_value = arm.getCenter(3);
      servo_4_value = arm.getCenter(4);
      servo_5_value = arm.getCenter(5);

      arm.setAllAbsolute(servo_0_value, servo_1_value,
                         servo_2_value, servo_3_value,
                         servo_4_value, servo_5_value);
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
    else if (c == 's') {
      arm.setDelta(1, 1);
      arm.setDelta(2, 1);
      arm.setDelta(3, 1);
      arm.setDelta(4, 1);
      arm.setDelta(5, 1);
      arm.setAllAbsolute(66, 149, 120, 170, 90, 40);
      arm.safeDelay(3000);
      arm.setOneAbsolute(5, 88);
      arm.safeDelay(3000);
      Serial.println("Ready");

    }
    else if (c == 'd') {
      arm.setDelta(1, 3);
      arm.setDelta(2, 3);
      arm.setDelta(3, 3);
      arm.setDelta(4, 3);
      arm.setDelta(5, 1);
      arm.setAllAbsolute(66, 58, 90, 70, 90, 40);
      //Below is an example of running some other code while waiting for the arm to move.
      //Exit the while loop after 3000ms has passed
      unsigned long endTime = 0;
      endTime = millis() + 2000;
      while (millis() < endTime) {
        //Run some code
        Serial.print("Working while waiting for movement... ");
        Serial.print("millis: ");
        Serial.print(millis());
        Serial.print(" ");
        Serial.println(endTime);

        //Update the arm
        arm.update();
        arm.setOneAbsolute(5, 40);
        //Delay a short time to prevent the updates from happening too close together.
        delay(1);
      }

    }
    else if (c == 'f') {
      arm.setDelta(1, 6);
      arm.setDelta(2, 6);
      arm.setDelta(3, 6);
      arm.setDelta(4, 6);
      arm.setDelta(5, 1);
      arm.setAllAbsolute(66, 58, 90, 70, 90, 40);
      unsigned long endTime = millis() + 2000;
      while (millis() < endTime) {
        arm.update();
        arm.setOneAbsolute(5, 20);
        delay(9);
      }
    }
    else if (c == 'g') {
      arm.setAllAbsolute(66, 149, 120, 170, 90, 40);
      delay(10);
      arm.update();
      //arm.safeDelay(300, 20);
      //arm.setOneAbsolute(5, 85);
      //arm.safeDelay(300);
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
  int _servovalue = 0;
  switch (joint) {
    case 0:
      servo_0_value += value;
      _servovalue = servo_0_value;
      break;
    case 1:
      servo_1_value += value;
      _servovalue = servo_1_value;
      break;
    case 2:
      servo_2_value += value;
      _servovalue = servo_2_value;
      break;
    case 3:
      servo_3_value += value;
      _servovalue = servo_3_value;
      break;
    case 4:
      servo_4_value += value;
      _servovalue = servo_4_value;
      break;
    case 5:
      servo_5_value += value;
      _servovalue = servo_5_value;
      break;
  }

  Serial.print("Servo: ");
  Serial.print(joint);
  Serial.print(" Value: ");
  Serial.println(_servovalue);
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
