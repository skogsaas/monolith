#include "Time.h"

#if ARDUINO >= 100
#include "Arduino.h"
#else
#include "WProgram.h"
#endif

const unsigned long ULONGMAX = 4294967295;

Time::Time()
  : m_currentmicro(0)
  , m_microseconds(0)
  , m_milliseconds(0)
  , m_seconds(0)
  , m_minutes(0)
  , m_hours(0)
  , m_days(0)
{
  
}

void Time::tick()
{
  // Trigger a overflow check.
  unsigned long c = current();
}

unsigned long Time::microseconds()
{
  return (m_microseconds + current()) % 1000000;
}

unsigned long Time::milliseconds()
{
  return (m_milliseconds + current() / 1000) % 1000;
}

unsigned long Time::seconds()
{
  return (m_seconds + current() / 1000000) % 60;
}

unsigned long Time::minutes()
{
  return (m_minutes + (current() / 1000000) / 60) % 60;
}

unsigned long Time::hours()
{
  return (m_hours + (current() / 1000000) / 60*60) % 24;
}

unsigned long Time::days()
{
  return (m_days + (current() / 1000000) / 60*60*24) % 24;
}

unsigned long Time::totalSeconds()
{
  return totalMinutes() * 60 + seconds();
}

unsigned long Time::totalMinutes()
{
  return totalHours() * 60 + minutes();
}

unsigned long Time::totalHours()
{
  return m_days * 24 + hours();
}

unsigned long Time::current()
{
  unsigned long m = micros();

  if(m < m_currentmicro) // Check if overflow has happened
  {    
    m_milliseconds += (m_microseconds + (ULONGMAX % 1000000)) / 1000;
    m_seconds += (m_milliseconds / 1000); 
    m_minutes += (m_seconds / 60);
    m_hours += (m_minutes / 60);
    m_days += (m_hours / 24);

    m_milliseconds = m_milliseconds % 1000;
    m_seconds = m_seconds % 60;
    m_minutes = m_minutes % 60;
    m_hours = m_hours % 24;
    
    m_microseconds = (m_microseconds + (ULONGMAX % 1000000)) % 1000000;
  }

  m_currentmicro = m;
  
  return m_currentmicro;
}

