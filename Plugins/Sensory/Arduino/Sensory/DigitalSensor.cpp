#include "DigitalSensor.h"

DigitalSensor::DigitalSensor(unsigned char s, Time& t, Messaging& m, int pin)
  : DeviceBase(s)
  , m_debounce(0)
  , m_time(t)
  , m_messaging(m)
  , m_pin(pin)
  , m_value(false)
{
  pinMode(m_pin, INPUT);
}

void DigitalSensor::run()
{    
  int value = digitalRead(m_pin);

  if(value != m_value)
  {
    if(m_debounce > 0)
    {
      if(m_debounce < millis())
      {
        m_value = value;
        m_debounce = 0;
        send(1, m_value);
      }
    }
    else
    {
      m_debounce = millis() + DEBOUNCE_TIME;
    }
  }
}

bool DigitalSensor::pull(DeviceMessage* msg)
{
  DeviceDataMessage<bool>* ddm = reinterpret_cast<DeviceDataMessage<bool>*>(msg);
  
  if(msg->dataId == 1)
  {
    return true;
  }
  else
  {
    return false;
  }
}

bool DigitalSensor::push(DeviceMessage* msg)
{
  return false;
}

void DigitalSensor::send(unsigned char dataId, bool data)
{
  DeviceDataMessage<bool> msg;
  msg.channelId = m_sensorId;
  msg.type = MessageTypes::Push;
  msg.dataId = dataId;
  msg.dataSize = sizeof(bool);
  msg.data = data;
  
  Packet packet;
  packet.size = sizeof(DeviceDataMessage<bool>);
  packet.data = (unsigned char*)&msg;

  m_messaging.write(packet);
}

