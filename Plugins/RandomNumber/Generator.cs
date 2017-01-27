using System.Threading.Tasks;
using System.Threading;
using System;
using Skogsaas.Legion;

namespace Skogsaas.Monolith.Plugins.RandomNumber
{
    public class Generator : PluginBase
    {
        private Task task;
        private Random random;

        private Channel channel;
        private IDummy dummy;

        public Generator()
            : base("RandomNumber")
        {
            this.random = new Random();

            this.channel = Manager.Create(Data.Constants.Channel);
            this.dummy = this.channel.CreateType<IDummy>("DUMMY");
            this.channel.Publish(this.dummy);

            this.task = Monolith.Utilities.PeriodicTask.StartPeriodicTask(trigger, 1000, new CancellationToken());
        }

        public override void initialize()
        {
            base.initialize();
        }

        private void trigger()
        {
            double value = (double)this.random.Next(0, 100);

            Monolith.Logging.Logger.Trace("Random number: " + value);
            this.dummy.Value = value;
        }
    }
}
