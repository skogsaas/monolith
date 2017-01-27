using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Utilities
{
    public class PeriodicTask
    {
        public static Task StartPeriodicTask(Action action, int intervalInMilliseconds, CancellationToken cancelToken)
        {
            Action wrapperAction = () =>
            {
                if (cancelToken.IsCancellationRequested) { return; }

                action();
            };

            Action mainAction = async () =>
            {
                TaskCreationOptions attachedToParent = TaskCreationOptions.AttachedToParent;

                if (cancelToken.IsCancellationRequested) { return; }

                while (true)
                {
                    if (cancelToken.IsCancellationRequested) { break; }

                    await Task.Factory.StartNew(wrapperAction, cancelToken, attachedToParent, TaskScheduler.Current);

                    if (cancelToken.IsCancellationRequested || intervalInMilliseconds == Timeout.Infinite)
                    {
                        break;
                    }

                    await Task.Delay(intervalInMilliseconds);
                }
            };

            return Task.Factory.StartNew(mainAction, cancelToken);
        }
    }
}
