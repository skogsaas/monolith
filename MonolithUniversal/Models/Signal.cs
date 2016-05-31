using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Models
{
    public class Signal<T> : ISignal, INotifyPropertyChanged
    {
        public string Identifier { get; private set; }

        private T state;
        public T State
        {
            get { return this.state; }
            set { this.state = value; notify(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notify([CallerMemberName] string info = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public Signal(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
