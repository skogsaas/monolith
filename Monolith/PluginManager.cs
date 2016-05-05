using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Monolith
{
    public class PluginManager
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
                Assembly assembly = Assembly.LoadFile(dll);
                Type[] types = assembly.GetExportedTypes();

                foreach(Type type in types)
                {
                    if(type != typeof(IPlugin) && typeof(IPlugin).IsAssignableFrom(type))
                    {
                        IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

                        if(plugin != null)
                        {
                            initialize(plugin);
                        }
                    }
                }
            }
        }

        private void initialize(IPlugin plugin)
        {
            this.plugins.Add(plugin);
        }
    }
}
