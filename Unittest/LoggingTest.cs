using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monolith.Framework;
using Monolith.Logging;

namespace Unittest
{
    [TestClass]
    public class LoggingTest
    {
        [TestMethod]
        public void Log()
        {
            int events = 0;

            Channel channel = Manager.Instance.create("Logging");
            channel.subscribe(
                typeof(LogEvent), 
                (Channel c, IEvent evt) => 
                {
                    LogEvent l = (LogEvent)evt;
                    Assert.AreEqual("test", l.Message);
                    events++;
                });

            Monolith.Logging.Logger.Trace("test");

            Assert.AreEqual(1, events);
        }
    }
}
