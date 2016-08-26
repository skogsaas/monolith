#include "Messaging.h"

Messaging::Messaging()
  : m_mac({0x00,0x01,0x02,0x03,0x04,0x05})
  , m_localIp(IPAddress(192, 168, 1, 115))
  , m_localPort(5000)
  , m_remoteIp(IPAddress(192, 168, 1, 110))
  , m_remotePort(5000)
{
  Ethernet.begin(m_mac, m_localIp);

  m_udp.begin(m_localPort);
}

bool Messaging::read(Packet& packet)
{
  int size = m_udp.parsePacket();
  
  if(size > 0)
  {
    packet.data = (unsigned char*)malloc(size);
    
    int offset = 0;
    
    while(m_udp.available() > 0 && offset != size)
    {
      offset += m_udp.read(&packet.data[offset], size - offset);
    }

    m_udp.flush();

    packet.size = size;

    return true;
  }

  return 0;
}

bool Messaging::write(Packet& packet)
{
  m_udp.beginPacket(m_remoteIp, m_remotePort);
  
  int written = m_udp.write(packet.data, packet.size);

  return m_udp.endPacket() != 0; // This will return 0 on fail.
}

