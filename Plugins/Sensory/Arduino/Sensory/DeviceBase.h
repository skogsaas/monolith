#pragma once

class DeviceMessage;

class DeviceBase
{
  public:
    DeviceBase(unsigned char s);
    virtual ~DeviceBase();
    
    virtual void run() = 0;
    
    virtual bool pull(DeviceMessage* msg) = 0;
    virtual bool push(DeviceMessage* msg) = 0;

  protected:
    unsigned char m_sensorId;
};

