#pragma once

#include "DeviceBase.h"

#include "Time.h"
#include "Messaging.h"

#include "lib_dht.h"

class DhtSensor : public DeviceBase
{
  public:
    DhtSensor(Time& t, int pin);
    void run() override;

    bool pull(DeviceMessage* msg) override;
    bool push(DeviceMessage* msg) override;

  private:
    unsigned long m_lastread;

    Time& m_time;

    int m_pin;
    DHT m_dht;

    double m_temperature; // DataId 1
    double m_humidity;    // DataId 2
};

