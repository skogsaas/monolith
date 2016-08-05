using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monolith.Framework;

namespace Unittest
{
    [TestClass]
    public class FrameworkTest
    {
        [TestMethod]
        public void CreateChannel()
        {
            Monolith.Framework.Channel one = Monolith.Framework.Manager.Instance.create("CHANNEL");
            Monolith.Framework.Channel two = Monolith.Framework.Manager.Instance.create("CHANNEL");

            Assert.AreSame(one, two);
        }

        [TestMethod]
        public void PublishEvent()
        {
            Channel ch = Monolith.Framework.Manager.Instance.create("CHANNEL");

            int events = 0;

            ch.subscribePublish(typeof(IEvent), (Channel c, IEvent e) => { events++; });

            ch.publish(new TestEvent());

            Assert.AreEqual(1, events);
        }

        [TestMethod]
        public void PublishObject()
        {
            Channel ch = Monolith.Framework.Manager.Instance.create("CHANNEL");

            int objects = 0;
            TestObject obj = new TestObject("OBJECT");

            ch.subscribePublish(typeof(IObject), (Channel c, IObject o) => { objects++; Assert.AreSame(obj, o); });

            ch.publish(obj);

            Assert.AreEqual(1, objects);
        }
    }
}
