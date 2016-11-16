#include "Registry.h"

#include "Property.h"

class Messaging;

class Registry
{
  public:   
    Registry(Messaging& messaging);
    ~Registry();

    template<typename T>
    void set(unsigned short id, T& value)
    {
      IProperty* property = nullptr;
      
      for(int i = 0; i < m_next; i++)
      {
        if(m_properties[i]->id() == id)
        {
          property = m_properties[i];
          break;
        }
      }

      if(property != nullptr) // Update existing property
      {
        Property<T>* p = dynamic_cast<Property<T>>(property);
        p->set(value);
      }
      else // Create new property
      {
        Property<T>* p = new Property<T>(id, value);
        property = p;
      }

      notify(property);
    }
    
    IProperty* get(unsigned short id);

  private:
    unsigned short m_next;
    Messaging& m_messaging;

    IProperty* m_properties[64];

    void notify(IProperty* property);
}

