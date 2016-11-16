#pragma once

#include "Property.h"
#include "Messaging.h"

Registry::Registry(Messaging& messaging)
  : m_next(0)
  , m_messaging(messaging)
{ 
}

Registry::~Registry()
{
}

IProperty* Registry::get(unsigned short id)
{
  return nullptr;
}

void Registry::notify(IProperty* property)
{
  // TODO push to messaging system.
}
