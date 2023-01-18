#ifndef identifiedBoss_h
#define identifiedBoss_h
#include <Arduino.h>
class IdentifiedBoss {
  private:
  public:
    IdentifiedBoss(int id, int direction, int camId);
    int Id;
    int Direction;
    int CamId;
};
#endif