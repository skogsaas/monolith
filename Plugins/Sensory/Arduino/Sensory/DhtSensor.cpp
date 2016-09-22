#include "DhtSensor.h"

DhtSensor::DhtSensor(unsigned char s, Time& t, Messaging& m, int pin)
  : DeviceBase(s)
  , m_lastread(0)
  , m_time(t)
  , m_messaging(m)
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

    float temperature = m_dht.readTemperature();
    float humidity = m_dht.readHumidity();

    if(m_temperature != temperature)
    {
      send(1, temperature);
    }

    if(m_humidity != humidity)
    {
      send(2, humidity);
    }
    
    m_temperature = temperature;
    m_humidity = humidity;
  }
}

bool DhtSensor::pull(DeviceMessage* msg)
{
  DeviceDataMessage<float>* ddm = reinterpret_cast<DeviceDataMessage<float>*>(msg);
  
  switch(msg->dataId)
  {
    case 1: // Temperature
      {
        ddm->dataSize = sizeof(float);
        ddm->data = m_temperature;

        return true;
      }
    break;

    case 2: // Humidity
      {          
        
        ddm->dataSize = sizeof(float);
        ddm->data = m_humidity;

        return true;
      }
    break;

    default:
      return false;
  }
}

bool DhtSensor::push(DeviceMessage* msg)
{
  return false;
}

void DhtSensor::send(unsigned char dataId, float data)
{
  DeviceDataMessage<long> msg;
  msg.channelId = m_sensorId;
  msg.type = MessageTypes::Push;
  msg.dataId = dataId;
  msg.dataSize = sizeof(long);
  msg.data = (long)(data * 1000.0);
  
  Packet packet;
  packet.size = sizeof(DeviceDataMessage<long>);
  packet.data = (unsigned char*)&msg;

  m_messaging.write(packet);
}

