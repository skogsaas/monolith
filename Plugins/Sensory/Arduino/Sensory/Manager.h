#pragma once

#include "Time.h"
#include "Messaging.h"
#include "DeviceBase.h"

class Manager
{
  public:
    Manager();

    void run();

  private:
    Time m_time;
    Messaging m_messaging;

    DeviceBase* m_devices[64];
    
    void handle(Message* msg);
};

