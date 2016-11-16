#pragma once

enum DataTypes : unsigned char
{
  Int8 = 1,
  UInt8 = 2,
  Int16 = 3,
  UInt16 = 4,
  Int32 = 5,
  UInt32 = 6,
  Float = 7,
  Double32 = 8,
  Double64 = 9,
  Array = 10
};

class IProperty
{
  public:
    virtual const unsigned short id() = 0;
    virtual const DataTypes type() = 0;
    virtual const int size() = 0;
    virtual const unsigned char* ptr() = 0;
};

template<typename T>
class Property : public IProperty
{
  public:
    Property(const unsigned short dataId, const T& data, DataTypes type)
      : m_id(dataId)
      , m_data(data)
      , m_type(type)
    {
    }

    Property(const unsigned short dataId, const char& data)
    : Property(dataId, data, DataTypes::Int8)
    {
    }

    Property(const unsigned short dataId, const unsigned char& data)
    : Property(dataId, data, DataTypes::UInt8)
    {
    }

    Property(const unsigned short dataId, const short& data)
    : Property(dataId, data, DataTypes::Int16)
    {
    }

    Property(const unsigned short dataId, const unsigned short& data)
    : Property(dataId, data, DataTypes::UInt16)
    {
    }

    Property(const unsigned short dataId, const int& data)
    : Property(dataId, data, DataTypes::Int32)
    {
    }

    Property(const unsigned short dataId, const unsigned int& data)
    : Property(dataId, data, DataTypes::UInt32)
    {
    }

    Property(const unsigned short dataId, const float& data)
    : Property(dataId, data, DataTypes::Float)
    {
    }

    Property(const unsigned short dataId, const double& data)
    : Property(dataId, data, DataTypes::Double32)
    {
    }

    const unsigned short id() override
    {
      return m_id;
    }

    const DataTypes type() override
    {
      return m_type;
    }

    const int size() override
    {
      return sizeof(T);
    }

    const unsigned char* ptr() override
    {
      return (unsigned char*)&m_data;
    }
    
    void set(const T& data)
    {
      m_data = data;
    }

    const T& get()
    {
      return m_data;
    }

  private:
    const unsigned short m_id;
    T m_data;
    DataTypes m_type;
}

