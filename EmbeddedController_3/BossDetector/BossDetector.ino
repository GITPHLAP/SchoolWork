#include "HUSKYLENS.h"
#include "position.h"
#include "identifiedBoss.h"
// WIFI library
#include <SPI.h>
#include <WiFi101.h>

HUSKYLENS   huskylens;
//HUSKYLENS green line >> SDA; blue line >> SCL
void printResult(HUSKYLENSResult result);

#define BOSS_ARRAY_SIZE 10
#define CAM_ID 1
// WIFI Setup
#define SECRET_SSID "AntiBoss"

char ssid[] = SECRET_SSID;
int status = WL_IDLE_STATUS;

Position positions[BOSS_ARRAY_SIZE];
IPAddress server(192,168,69,103);
WiFiClient client;

void setup() {
    Serial.begin(115200);
    Wire.begin();

    // attempt to connect to Wifi network:
    while (status != WL_CONNECTED) {
      Serial.print("Attempting to connect to network: ");
      Serial.println(ssid);
      status = WiFi.begin(ssid);

      // wait 10 seconds for connection:
      delay(10000);
    }
    // you're connected now, so print out the data:
    Serial.println("You're connected to the network");
    printWifiData();

    if (client.connect(server,8080)) {      
      Serial.println("Connected to socket");
    } else {
      Serial.println("Could not connect to socket");
    }


    while (!huskylens.begin(Wire))
    {
        Serial.println(F("Begin failed!"));
        Serial.println(F("1.Please recheck the \"Protocol Type\" in HUSKYLENS (General Settings>>Protocol Type>>I2C)"));
        Serial.println(F("2.Please recheck the connection."));
        delay(100);
    }

    // Set huskylens algorithm
    huskylens.writeAlgorithm(ALGORITHM_FACE_RECOGNITION);

    // Custom names for learned faces
    // while (!huskylens.setCustomName("Henrik",1))  // bool setCustomName(String name,uint8_t id)
    // {
    //   Serial.println(F("ID1 customname failed!")); 
    //   delay(100);
    // }
    // while (!huskylens.setCustomName("Henrik",2))  // bool setCustomName(String name,uint8_t id)
    // {
    //   Serial.println(F("ID1 customname failed!")); 
    //   delay(100);
    // }
}

void loop() {
    delay(1000);  
    bool readedIds[BOSS_ARRAY_SIZE]{0};
    if (!huskylens.request()) Serial.println(F("Fail to request data from HUSKYLENS, recheck the connection!"));
    else if(!huskylens.isLearned()) Serial.println(F("Nothing learned, press learn button on HUSKYLENS to learn one!"));
    // else if(!huskylens.available()) Serial.println(F("No block or arrow appears on the screen!"));
    else {
        // Serial.println(F("###########"));
        while (huskylens.available())
        {
            HUSKYLENSResult result = huskylens.read();

            if (result.ID != 0){
              readedIds[result.ID] = true;
              setBossPosition(result.ID, result.xCenter, result.yCenter);
              printBoss(result.ID);
            }
        }    

    }

    for (int i=0; i<BOSS_ARRAY_SIZE; i++) {
      if (!readedIds[i]) { // If id is not readed then send object and reset it... If exists
        if (positions[i].End_x != -1 && positions[i].End_y != -1){
          // Send data 
          int direction = getDirections(positions[i].Start_x, positions[i].Start_y, positions[i].End_x, positions[i].End_y);
          IdentifiedBoss identBoss = IdentifiedBoss(positions[i].Id, direction, CAM_ID);
          if (client.connected()){
            char* identifiedBoss = reinterpret_cast <char*> (&identBoss);
            client.write(identifiedBoss,sizeof(IdentifiedBoss)); 
            printIdentBoss(identBoss);
          }
        }
        // Reset object 
        positions[i] = Position();
      }
    }
}

void printResult(HUSKYLENSResult result){
    if (result.command == COMMAND_RETURN_BLOCK){
        Serial.println(String()+F("Block:xCenter=")+result.xCenter+F(",yCenter=")+result.yCenter+F(",width=")+result.width+F(",height=")+result.height+F(",ID=")+result.ID);
    }
    else if (result.command == COMMAND_RETURN_ARROW){
        Serial.println(String()+F("Arrow:xOrigin=")+result.xOrigin+F(",yOrigin=")+result.yOrigin+F(",xTarget=")+result.xTarget+F(",yTarget=")+result.yTarget+F(",ID=")+result.ID);
    }
    else{
        Serial.println("Object unknown!");
    }
}

void printWifiData() {
  Serial.println("Board Information:");
  // print your board's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  Serial.println();
  Serial.println("Network Information:");
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.println(rssi);

}

int getDirections(int x_start, int y_start, int x_end, int y_end) {
  // Top left is (0,0)
  // Top right is (320,0)
  // Bottom left is (0,240)
  // Bottom right is (320,240)

  // Create vector from coordinates 
  int direction = 0;
  if (x_end > x_start){
    direction = 90;
    if (y_end > y_start){
      direction = direction + 45;
    } else {
      direction = direction - 45;
    }
  } else {
    direction = 270;
    if (y_end > y_start){
      direction = direction - 45;
    } else {
      direction = direction + 45;
    }
  }

  return direction;
}

void setBossPosition(int id, int x, int y) {
  positions[id].Id = id;
  
  if (positions[id].Start_x != -1 && positions[id].Start_y != -1) {
    positions[id].End_x = x;
    positions[id].End_y = y;
  } else {
    positions[id].Start_x = x;
    positions[id].Start_y = y;
  }

}

void printBoss(int id) {
  Serial.println("Boss");
  Serial.println(positions[id].Id);
  Serial.println(positions[id].Start_x);
  Serial.println(positions[id].Start_y);
  Serial.println(positions[id].End_x);
  Serial.println(positions[id].End_y);
}

void printIdentBoss(IdentifiedBoss boss) {
  Serial.println(" ident Boss");
  Serial.println(boss.Id);
  Serial.println(boss.Direction);
  Serial.println(boss.CamId);
}


