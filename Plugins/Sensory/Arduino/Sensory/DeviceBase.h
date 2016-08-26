#pragma once

class Message;

class DeviceBase
{
  public:
    virtual void run() = 0;
    
    virtual bool pull(DeviceMessage* msg) = 0;
    virtual bool push(DeviceMessage* msg) = 0;
};

