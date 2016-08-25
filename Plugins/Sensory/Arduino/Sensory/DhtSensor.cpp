#include "DhtSensor.h"

DhtSensor::DhtSensor(Messaging& m, Time& t, int pin)
  : m_lastread(0)
  , m_messaging(m)
  , m_time(t)
  , m_dht(pin, DHT22)
  , m_pin(pin)
  , m_temperature(0.0)
  , m_humidity(0.0)
{
  
}

void DhtSensor::run()
{    
  if((m_lastread + 5) <= m_time.totalSeconds())
  {
    m_lastread = m_time.totalSeconds();
  }
}

void DhtSensor::handle(Message* msg)
{
  if(msg->type == MessageTypes::Read)
  {
    /*
    switch(msg->dataId)
    {
      case 1:
        
      break;

      case 2:
      break;

      default:
      break;
    }
    */
  }
  else // Write
  {
    // Do nothing, as this device don't support writing.
  }
}

