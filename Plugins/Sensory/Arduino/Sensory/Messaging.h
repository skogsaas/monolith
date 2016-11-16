#pragma once

#include <UIPEthernet.h>

enum MessageTypes : unsigned char
{
  Subscribe = 1,
  Update = 2,
  Heartbeat = 3
};

struct Message
{
  unsigned short deviceId;
  MessageTypes type;
};

struct DeviceMessage : Message
{
  unsigned short dataId;
  DataTypes dataType;
  char dataSize;
  char* dataPtr;
};

class IPAddress;

class Messaging
{
  public:
    Messaging(unsigned short deviceId);
    
    void publish(Slot* slot);

  private:
    unsigned char m_mac[6];

    IPAddress m_localIp;
    int m_localPort;
    
    IPAddress m_remoteIp;
    int m_remotePort;
  
    EthernetUDP m_udp;
};

