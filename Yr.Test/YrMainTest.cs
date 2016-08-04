using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yr; 

namespace Yr.Test
{

    [TestClass]
    public class YrMainTest
    {
        public Yr yr; 

        [TestInitialize]
        public void TestInitialize()
        {
             yr = new Yr(); 
        }
    
        [TestMethod]
        public void TestCreateSettings()
        {
            Assert.IsNotNull(Yr.Settings); 
        }

    }
}
