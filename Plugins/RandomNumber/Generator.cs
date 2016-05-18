using System.Threading.Tasks;
using System.Threading;
using System;

namespace Monolith.Plugins.RandomNumber
{
    public class Generator : IPlugin
    {
        private Task task;
        private Signals.Signal<int> signal;

        private Framework.Channel channel;

        private Random random;

        public Generator()
        {
            this.random = new Random();
            this.channel = Framework.Manager.Instance.create("Signals");

            this.signal = new Signals.Signal<int>("Random");
            this.signal.State = this.random.Next(0, 100);
            this.channel.publish(this.signal);

            this.task = Monolith.Utilities.PeriodicTask.StartPeriodicTask(trigger, 5000, new CancellationToken());
        }

        public void initialize()
        {

        }

        private void trigger()
        {
            Monolith.Logging.Logger.Trace("Test");
            this.signal.State = this.random.Next(0, 100);
        }
    }
}
