#include "Manager.h"

Manager* g_manager = nullptr;

void setup()
{
  g_manager = new Manager();
}

void loop()
{  
  g_manager->run();
}
