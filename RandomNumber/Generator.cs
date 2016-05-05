using System.Threading.Tasks;
using System.Threading;

namespace Monolith.Plugins.RandomNumber
{
    public class Generator : IPlugin
    {
        private Task task;

        public Generator()
        {
            this.task = Monolith.Utilities.PeriodicTask.StartPeriodicTask(trigger, 5000, new CancellationToken());
        }

        private void trigger()
        {
            Monolith.Logging.Logger.Trace("Test");
        }
    }
}
