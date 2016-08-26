#include "DhtSensor.h"

DhtSensor::DhtSensor(Time& t, int pin)
  : m_lastread(0)
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
  DeviceMessage* dMsg = reinterpret_cast<DeviceMessage*>(msg);
  
  if(msg->type == MessageTypes::Pull)
  {
    switch(msg->dataId)
    {
      case 1: // Temperature
        {
          Packet packet;
          packet.size = sizeof(DeviceDataMessage<double>);
          packet.data = (unsigned char*)malloc(packet.size);
          
          DeviceDataMessage<double>* ddm = reinterpret_cast<DeviceDataMessage<double>*>(packet.data);
          
          ddm->channelId
        }
      break;

      case 2: // Temperature
      break;

      default:
      break;
    }
    */
  }
  else // Push
  {
    // Do nothing, as this device don't support writing.
  }
}

