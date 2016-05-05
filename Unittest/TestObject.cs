using Monolith.Framework;

namespace Unittest
{
    public class TestObject : IObject
    {
        public string Name { get; set; }

        public TestObject(string name)
        {
            this.Name = name;
        }
    }
}
