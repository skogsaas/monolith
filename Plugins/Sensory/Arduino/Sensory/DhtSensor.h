#pragma once

#include "DeviceBase.h"

#include "Time.h"
#include "Messaging.h"

#include "lib_dht.h"

class DhtSensor : public DeviceBase
{
  public:
    DhtSensor(Messaging& m, Time& t, int pin);
    void run() override;

    void handle(Message* msg) override;

  private:
    unsigned long m_lastread;

    Messaging& m_messaging;
    Time& m_time;

    int m_pin;
    DHT m_dht;

    double m_temperature; // DataId 1
    double m_humidity;    // DataId 2
};

