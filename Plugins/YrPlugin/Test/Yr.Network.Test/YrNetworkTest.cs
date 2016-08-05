using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yr.Network;
using System.IO;

namespace Yr.Network.Test
{
    [TestClass]
    public class YrNetworkTest
    {
        static NetworkInit init;

        [TestInitialize]
        public void TestInitalizeObject()
        {
            NetworkSettings settings = new NetworkSettings
            {
                BasePath = @"http://api.met.no/"
            };

            init = new NetworkInit(settings);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInitalizeObjectException()
        {
            NetworkSettings settings = new NetworkSettings
            {
                BasePath = @"htt://api.met.no/"
            };
        }

        [TestMethod]
        public void TestOverrideDirectory()
        {
            string path = @"C:\Users\JohnMartin\Documents\GitHub\monolith";
            init.SettingsFilePath = path;

            Assert.AreEqual(path, init.SettingsFilePath); 
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void TestOverrideDirectoryException()
        {
            init.SettingsFilePath = "SomePath"; 
        }

        [TestMethod]
        public void TestNetworkInitNotNull()
        {
            Assert.IsNotNull(init);
        }

        [TestMethod]
        public void TestNetworkSettings()
        {
            init.createFile(true);
 
            Assert.IsNotNull(init.loadFile()); 
        }
    }
}
