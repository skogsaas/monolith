using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Obelisk.Models
{
    public class Model
    {
        public ObservableCollection<Plugin> Plugins { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<ISignal> Signals { get; set; }
        public ObservableCollection<Binding> Bindings { get; set; }

        public Model()
        {
            this.Plugins = new ObservableCollection<Plugin>();
            this.Devices = new ObservableCollection<Device>();
            this.Signals = new ObservableCollection<ISignal>();
            this.Bindings = new ObservableCollection<Binding>();
        }
    }
}
