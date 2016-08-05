using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO; 
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

        [TestMethod]
        public void TestSettingsPathEndsWith()
        {
            Assert.IsTrue(Yr.FilePath.EndsWith(Path.DirectorySeparatorChar.ToString())); 
        }

    }
}
