using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Plugins
{
    internal class PluginManager
    {
        private List<Type> types;
        private List<IPlugin> plugins;
        private List<IPlugin> initialized;

        public PluginManager()
        {
            this.types = new List<Type>();
            this.plugins = new List<IPlugin>();
            this.initialized = new List<IPlugin>();

            load();
            create();
            initialize();
        }

        private void load()
        {
            string path = Directory.GetCurrentDirectory();
            string[] dlls = Directory.GetFiles(path, "*.dll");

            foreach(string dll in dlls)
            {
                Assembly assembly = Assembly.LoadFile(dll);
                Type[] types = assembly.GetExportedTypes();

                foreach (Type type in types)
                {
                    if (type != typeof(IPlugin) && type != typeof(PluginBase) && typeof(IPlugin).IsAssignableFrom(type))
                    {
                        this.types.Add(type);
                    }
                }
                
            }
        }

        private void create()
        {
            foreach(Type type in this.types)
            {
                try
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

                    this.plugins.Add(plugin);
                }
                catch (Exception ex)
                {
                    Logging.Logger.Error("Error while tying to create <" + type.FullName + "> " + ex.ToString());
                }
            }
        }

        private void initialize()
        {
            foreach (IPlugin plugin in this.plugins)
            {
                try
                {
                    plugin.initialize();

                    this.initialized.Add(plugin);
                }
                catch (Exception ex)
                {
                    Logging.Logger.Error("Error while tying to initialize <" + plugin + "> " + ex.ToString());
                }
            }
        }
    }
}
