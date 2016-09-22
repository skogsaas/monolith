#include "Manager.h"

#include "DhtSensor.h"

void(*reset) (void) = 0;

Manager::Manager()
  : m_time()
  , m_messaging()
{
  memset(m_devices, 0, sizeof(m_devices));
  
  DeviceBase* dht = new DhtSensor(1, m_time, m_messaging, 3);

  m_devices[1] = dht;
}

void Manager::run()
{
  m_time.tick();

  {
    Packet packet;
    
    if(m_messaging.read(packet) && packet.size >= 2)
    {
      Message* msg = reinterpret_cast<Message*>(packet.data);
      
      if(msg->channelId == 0)
      {
        
      }
      else if(msg->channelId > 0 && msg->channelId < 64)
      {
        DeviceMessage* dmsg = reinterpret_cast<DeviceMessage*>(msg);
        
        if(m_devices[msg->channelId] != nullptr)
        {
          if(dmsg->type == MessageTypes::Pull)
          {
            bool success = m_devices[msg->channelId]->pull(dmsg);
          }
          else if(dmsg->type == MessageTypes::Push)
          {
            bool success = m_devices[msg->channelId]->push(dmsg);
          }
        }
      }
    }
  }

  for(unsigned char i = 1; i < 64; i++)
  {
    if(m_devices[i] != nullptr)
    {
      m_devices[i]->run();
    }
  }
}
