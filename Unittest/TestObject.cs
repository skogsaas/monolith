using Monolith.Framework;

namespace Unittest
{
    public class TestObject : IObject
    {
        public string Identifier { get; set; }

        public TestObject(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
