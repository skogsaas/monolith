using Monolith.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Monolith
{
    internal class PluginManager
    {
        private List<IPlugin> plugins = new List<IPlugin>();

        public PluginManager()
        {
            load();
        }

        private void load()
        {
            string path = Directory.GetCurrentDirectory();
            string[] dlls = Directory.GetFiles(path, "*.dll");

            foreach(string dll in dlls)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(dll);
                    Type[] types = assembly.GetExportedTypes();

                    foreach (Type type in types)
                    {
                        if (type != typeof(IPlugin) && typeof(IPlugin).IsAssignableFrom(type))
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

                            if (plugin != null)
                            {
                                initialize(plugin);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Logging.Logger.Error("Error while tying to load <" + Path.GetFileName(dll) + "> " + ex.ToString());
                }
            }
        }

        private void initialize(IPlugin plugin)
        {
            this.plugins.Add(plugin);
            plugin.initialize();
        }
    }
}
