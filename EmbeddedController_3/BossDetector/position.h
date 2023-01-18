#ifndef Position_h
#define Position_h
#include <Arduino.h>
class Position {
  private:
  public:
    Position(int id, int start_x, int start_y, int end_x, int end_y);
    Position();
    int Id;
    int Start_x;
    int Start_y;
    int End_x;
    int End_y;
};
#endif