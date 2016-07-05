using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obelisk.Models
{
    public class Plugin
    {
        public enum States
        {
            Uninitialized = 0,
            Initialized,
            Offline,
            Online
        }

        public string Identifier { get; set; }
        public string Type { get; set; }
        public States State { get; set; }
        

        public Plugin()
        {
            this.Identifier = "Plugin Name";
            this.Type = "Plugin Type";
            this.State = States.Uninitialized;
            
        }
    }
}
