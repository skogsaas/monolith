#pragma once

#include <UIPEthernet.h>

enum MessageTypes : unsigned char
{
  Push = 1,
  Pull = 2
};

struct Message
{
  unsigned char channelId;
};

struct DeviceMessage : Message
{
  MessageTypes type;
  unsigned char dataId;
};

template<typename T>
struct DeviceDataMessage : DeviceMessage
{
  unsigned char dataSize;
  T data;
  
  DeviceDataMessage()
    : dataSize(sizeof(T))
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
  
  ~Packet()
  {
    if(size != 0)
    {
      free(data);
    }
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

