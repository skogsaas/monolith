using Monolith.Devices;
using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.REST
{
    class DataModel
    {
        public ObservableCollection<PluginState> Plugins;
        public ObservableCollection<DeviceState> Devices;
        public ObservableCollection<ISignal> Signals;

        public DataModel()
        {
            this.Plugins = new ObservableCollection<PluginState>();
            this.Devices = new ObservableCollection<DeviceState>();
            this.Signals = new ObservableCollection<ISignal>();
        }
    }
}
