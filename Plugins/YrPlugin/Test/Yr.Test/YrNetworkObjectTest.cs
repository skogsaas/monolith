using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yr.Test.Objects;

namespace Yr.Test
{
    [TestClass]
    public class YrNetworkObjectTest
    {
        SomeClass some;

        string xmlContent = "<to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body>";

        [TestInitialize]
        public void TestInitalize()
        {
            some = new SomeClass(); 
        }

        [TestMethod]
        public void TestXml()
        {
            SomeClass another = some.createObjectFromXml(xmlContent);

            Assert.IsNotNull(another); 
        }

        [TestMethod]
        public void TestEmptyXml()
        {
            SomeClass another = some.createObjectFromXml("");


        }
    }
}
