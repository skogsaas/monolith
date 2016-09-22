using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public class ConfigurationManager
    {
        private Dictionary<string, Type> types;

        private List<ConfigurationBase> loaded;
        private List<ConfigurationIdentifier> unloaded;

        private Framework.Channel configChannel;
        private Framework.Channel coreChannel;

        private ConfigurationManagerProxy state;

        public ConfigurationManager()
        {
            this.types = new Dictionary<string, Type>();
            this.loaded = new List<ConfigurationBase>();
            this.unloaded = new List<ConfigurationIdentifier>();

            this.configChannel = Framework.Manager.Instance.create(Constants.Channel);
            this.coreChannel = Framework.Manager.Instance.create(Core.Constants.Channel);

            this.configChannel.subscribePublish(typeof(ConfigurationBase), onPublish);
            this.configChannel.subscribeUnpublish(typeof(ConfigurationBase), onUnpublish);

            this.state = new ConfigurationManagerProxy();
            this.state.Register.SetHandler(onRegister);
            this.coreChannel.publish(this.state);

            scan();
        }

        private void scan()
        {
            foreach (string plugin in Directory.GetDirectories(Path.Combine(new string[]{Directory.GetCurrentDirectory(), Constants.ConfigurationFolder})))
            {
                foreach(string filepath in Directory.GetFiles(plugin))
                {
                    string data = File.ReadAllText(filepath);

                    ConfigurationIdentifier config = JsonConvert.DeserializeObject<ConfigurationIdentifier>(data);

                    if(this.types.ContainsKey(config.Type))
                    {
                        ConfigurationBase configLoaded = load(config.Type, config.Plugin, config.Identifier);
                        this.loaded.Add(configLoaded);

                        configLoaded.ObjectChanged += onChange;

                        this.configChannel.publish(configLoaded);
                    }
                    else
                    {
                        this.unloaded.Add(config);
                    }
                }
            }
        }

        private ConfigurationBase load(string type, string plugin, string identifier)
        {
            string data = File.ReadAllText(MakeFilePath(plugin, identifier));

            //ConfigurationBase config = (ConfigurationBase)Activator.CreateInstance(this.types[type], identifier);

            //JsonConvert.PopulateObject(data, config);
            ConfigurationBase config = (ConfigurationBase)JsonConvert.DeserializeObject(data, this.types[type], new Framework.Serialization.ObjectBaseSerializer());

            return config;
        }

        private void store(ConfigurationBase config)
        {
            string data = JsonConvert.SerializeObject(config, Formatting.Indented,
                new Framework.Serialization.ObjectBaseSerializer(), 
                new Framework.Serialization.AttributeBaseSerializer(), 
                new Framework.Serialization.AttributeCollectionBaseSerializer());

            EnsureDirectory(MakeDirectoryPath(config));

            File.WriteAllText(MakeFilePath(config), data);
        }

        private void delete(ConfigurationBase config)
        {
            string path = MakeFilePath(config);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private bool onRegister(Type type)
        {
            this.types.Add(type.Name, type);
            this.state.Types.Add(type.Name, new Framework.AttributeBase<string>(this.state.Types, type.Name));

            foreach(ConfigurationIdentifier configUnloaded in this.unloaded)
            {
                if(configUnloaded.Type == type.Name)
                {
                    ConfigurationBase configLoaded = load(configUnloaded.Type, configUnloaded.Plugin, configUnloaded.Identifier);

                    this.unloaded.Remove(configUnloaded);
                    this.loaded.Add(configLoaded);

                    configLoaded.ObjectChanged += onChange;

                    this.configChannel.publish(configLoaded);

                    break;
                }
            }

            return true;
        }

        private void onPublish(Framework.Channel channel, Framework.IObject obj)
        {
            if(typeof(ConfigurationBase).IsAssignableFrom(obj.GetType()))
            {
                ConfigurationBase config = (ConfigurationBase)obj;

                if (!this.loaded.Contains(config))
                {
                    this.loaded.Add(config);

                    config.ObjectChanged += onChange;

                    store(config);
                }
            }
        }

        private void onUnpublish(Framework.Channel channel, Framework.IObject obj)
        {
            if (typeof(ConfigurationBase).IsAssignableFrom(obj.GetType()))
            {
                ConfigurationBase config = (ConfigurationBase)obj;

                if (this.loaded.Contains(config))
                {
                    config.ObjectChanged -= onChange;

                    delete(config);
                }
            }
        }

        private void onChange(Framework.IObject obj)
        {
            if (typeof(ConfigurationBase).IsAssignableFrom(obj.GetType()))
            {
                ConfigurationBase config = (ConfigurationBase)obj;

                store(config);
            }
        }

        private static string MakeDirectoryPath(string plugin)
        {
            return Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder, plugin });
        }

        private static string MakeDirectoryPath(ConfigurationBase config)
        {
            return MakeDirectoryPath(config.Plugin);
        }

        private static string MakeFilePath(string plugin, string identifier)
        {
            return Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder, plugin, identifier + ".cfg" });
        }

        private static string MakeFilePath(ConfigurationBase config)
        {
            return MakeFilePath(config.Plugin, config.Identifier);
        }

        private static void EnsureDirectory(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
