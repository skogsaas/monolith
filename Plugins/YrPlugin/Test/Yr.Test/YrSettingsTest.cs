using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yr; 

namespace Yr.Test
{
    [TestClass]
    public class YrSettingsTest : Yr 
    {
        [TestMethod]
        public void TestYrSettings()
        {
            Assert.IsNotNull(Yr.Settings); 
        }
    }
}
