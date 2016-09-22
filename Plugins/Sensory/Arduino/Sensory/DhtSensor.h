#pragma once

#include "DeviceBase.h"

#include "Time.h"
#include "Messaging.h"

#include "lib_dht.h"

class DhtSensor : public DeviceBase
{
  public:
    DhtSensor(unsigned char sensorId, Time& t, Messaging& m, int pin);
    void run() override;

    bool pull(DeviceMessage* msg) override;
    bool push(DeviceMessage* msg) override;

  private:
    unsigned long m_lastread;

    Time& m_time;
    Messaging& m_messaging;

    int m_pin;
    DHT m_dht;

    float m_temperature; // DataId 1
    float m_humidity;    // DataId 2

    void send(unsigned char dataId, float data);
};

