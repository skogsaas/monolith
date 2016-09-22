#pragma once

#include "DeviceBase.h"

#include "Time.h"
#include "Messaging.h"

#define DEBOUNCE_TIME 10

class DigitalSensor : public DeviceBase
{
  public:
    DigitalSensor(unsigned char sensorId, Time& t, Messaging& m, int pin);
    void run() override;

    bool pull(DeviceMessage* msg) override;
    bool push(DeviceMessage* msg) override;

  private:
    unsigned long m_debounce;

    Time& m_time;
    Messaging& m_messaging;

    int m_pin;

    bool m_value;

    void send(unsigned char dataId, bool data);
};

