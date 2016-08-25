#pragma once

#include <UIPEthernet.h>

enum MessageTypes : unsigned char
{
  Read = 1,
  Write = 2
};

struct Message
{
  MessageTypes type;
  unsigned char deviceId;
};

template<typename T>
struct DeviceMessage : Message
{
  unsigned char dataId;
  unsigned char dataLength;
  T data;

  DeviceMessage()
    : dataLength(sizeof(T))
  {
  }
};

struct Packet
{
  int size;
  unsigned char* data;

  Packet()
    : size(0)
  {
  }
};

class IPAddress;

class Messaging
{
  public:
    Messaging();
  
    bool read(Packet& packet);
    bool write(Packet& packet);

  private:
    unsigned char m_mac[6];

    IPAddress m_localIp;
    int m_localPort;
    
    IPAddress m_remoteIp;
    int m_remotePort;
  
    EthernetUDP m_udp;
};

