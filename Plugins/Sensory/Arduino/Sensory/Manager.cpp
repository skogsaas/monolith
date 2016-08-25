#include "Manager.h"

#include "DhtSensor.h"

Manager::Manager()
  : m_time()
  , m_messaging()
{
  DeviceBase* dht = new DhtSensor(m_messaging, m_time, 3);
  DeviceBase* dht2 = new DhtSensor(m_messaging, m_time, 3);

  m_devices[1] = dht;
  m_devices[2] = dht2;
}

void Manager::run()
{
  m_time.tick();

  
}

