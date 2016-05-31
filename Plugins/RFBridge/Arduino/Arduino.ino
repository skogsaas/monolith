#include <Arduino.h>

#include <NexaCtrl.h>
#include <UIPEthernet.h>

#define htonl(x) ( ((x)<<24 & 0xFF000000UL) | \
                   ((x)<< 8 & 0x00FF0000UL) | \
                   ((x)>> 8 & 0x0000FF00UL) | \  
                   ((x)>>24 & 0x000000FFUL) )
#define ntohl(x) htonl(x)

const int TX_PIN = 2;
const int RX_PIN = 13;

EthernetUDP g_udp;
NexaCtrl g_nexa(TX_PIN, RX_PIN);

void handleNexaDeviceOnOff(char* payload, int size);
void handleNexaDeviceDim(char* payload, int size);
void handleNexaGroupOnOff(char* payload, int size);

void setup() {
  Serial.begin(9600);

  uint8_t mac[6] = { 0x00,0x01,0x02,0x03,0x04,0x05 };
  IPAddress ip(10, 0, 0, 10);

  Ethernet.begin(mac, ip);
  int success = g_udp.begin(5000);

  Serial.print("initialize: ");
  Serial.println(success ? "success" : "failed");
}

void loop() {
  int packetSize = g_udp.parsePacket();

  if (g_udp.available())
  {
    Serial.print("Received ");
    Serial.print(packetSize);
    Serial.print(" bytes from ");

    IPAddress remote = g_udp.remoteIP();
    for (int i = 0; i < 4; i++)
    {
      Serial.print(remote[i], DEC);
      if (i < 3)
      {
        Serial.print(".");
      }
    }

    Serial.print(":");
    Serial.println(g_udp.remotePort());

    char packetBuffer[128];
    
    if(g_udp.read(packetBuffer, 128) > 0)
    {
      switch(packetBuffer[0])
      {
        case 1:
          handleNexaDeviceOnOff(&packetBuffer[1], packetSize - 1);
        break;

        case 2:
          handleNexaDeviceDim(&packetBuffer[1], packetSize - 1);
        break;

        case 3:
          handleNexaGroupOnOff(&packetBuffer[1], packetSize - 1);
        break;

        default:
        break;
      }
    }
  }
}

void handleNexaDeviceOnOff(char* payload, int size)
{
  Serial.println("Handling Nexa device on/off");

  // 4 byte group id
  // 1 byte device id
  // 1 byte on/off

  unsigned long groupId = htonl(*((unsigned long*)(&payload[0])));
  byte deviceId = payload[4];
  bool on = payload[5] > 0;

  Serial.print("Group <");
  Serial.print(groupId);
  Serial.print("> device <");
  Serial.print(deviceId);
  Serial.print("> on <");
  Serial.print(on);
  Serial.println(">");

  for(int i = 0; i < 3; i++)
  {
    if(on)
    {
      g_nexa.DeviceOn(groupId, deviceId);
    }
    else
    {
      g_nexa.DeviceOff(groupId, deviceId);
    }
  }
}

void handleNexaDeviceDim(char* payload, int size)
{
  Serial.println("Handling Nexa device dim");

  // 4 byte group id
  // 1 byte device id
  // 1 byte dim
}

void handleNexaGroupOnOff(char* payload, int size)
{
  Serial.println("Handling Nexa group on/off");

  // 4 byte group id
  // 1 byte on/off
}

