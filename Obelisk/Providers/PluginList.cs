using System;
using System.Collections.Generic;

namespace Obelisk.Providers
{
    public class PluginList : GenericList
    {
        private List<Plugin> plugins;

        public PluginList(Models.Model m)
            : base(m, "plugins")
        {
            this.plugins = new List<Plugin>();
        }

        protected override void create(string identifier, string type)
        {
            this.plugins.Add(new Plugin(this.model, identifier));
        }
    }
}
