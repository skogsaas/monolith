using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yr.Network;

namespace Yr.Test
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
                BasePath = @"htt://api.met.no/"
            };

            init = new NetworkInit(settings);
        }


        [TestMethod]
        public void TestNetworkInitNotNull()
        {
            Assert.IsNotNull(init);
        }
    }
}
