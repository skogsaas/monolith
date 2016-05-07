using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{

    public class Object<T> : IObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private T _value;

        public T Value
        {
            get { return this._value; }
            set
            {
                this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("Value"));
                this._value = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public string Identifier { get; private set; }

        public Object(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
