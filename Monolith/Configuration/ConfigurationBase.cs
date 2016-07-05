using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public class ConfigurationBase
    {
        public delegate void ConfigurationChangedHandler(ConfigurationBase configuration);

        public event ConfigurationChangedHandler ConfigurationChanged;

        public string Filename { get; private set; }

        public ConfigurationBase(string filename)
        {
            this.Filename = filename;

            Manager.Register(this);
        }

        public void Store()
        {
            Manager.Store(this);
        }

        public void Load()
        {
            Manager.Load(this);
        }

        internal void NotifyChanged()
        {
            this.ConfigurationChanged?.Invoke(this);
        }
    }
}
