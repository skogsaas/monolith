#pragma once

class Time
{
  public:
    Time();
  
    void tick();

    unsigned long microseconds();
    unsigned long milliseconds();
    unsigned long seconds();
    unsigned long minutes();
    unsigned long hours();
    unsigned long days();

    unsigned long totalSeconds();
    unsigned long totalMinutes();
    unsigned long totalHours();

  private:
    unsigned long m_currentmicro;
  
    unsigned long m_microseconds;
    unsigned long m_milliseconds;
    unsigned long m_seconds;
    unsigned long m_minutes;
    unsigned long m_hours;
    unsigned long m_days;
    
    unsigned long current(); // Microseconds since overflow.
};

