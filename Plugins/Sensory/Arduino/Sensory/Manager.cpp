#include "Manager.h"

#include "DhtSensor.h"

void(*reset) (void) = 0;

Manager::Manager()
  : m_time()
  , m_messaging()
{
  memset(m_devices, nullptr, sizeof(m_devices));
  
  DeviceBase* dht = new DhtSensor(m_messaging, m_time, 3);
  DeviceBase* dht2 = new DhtSensor(m_messaging, m_time, 4);

  m_devices[1] = dht;
  m_devices[2] = dht2;
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
        if(m_devices[msg->channelId] != nullptr)
        {
          m_devices[msg->channelId]->handle(msg);
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

void Manager::handle(Message* msg)
{
  Serial.println("Handle message sent to channelId = 0.");
}
