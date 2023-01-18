#include "position.h"

Position::Position(int id, int start_x, int start_y, int end_x, int end_y) {
  Id = id;
  Start_x = start_x;
  Start_y = start_y;
  End_x = end_x;
  End_y = end_y;
}
Position::Position() {
  Start_x = -1;
  Start_y = -1;
  End_x = -1;
  End_y = -1;
}