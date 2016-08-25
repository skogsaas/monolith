#pragma once

class Message;

class DeviceBase
{
  public:
    virtual void run() = 0;
    virtual void handle(Message* msg) = 0;
};

