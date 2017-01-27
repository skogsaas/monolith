using Newtonsoft.Json;
using Skogsaas.Legion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Skogsaas.Monolith.Configuration
{
    public class ConfigurationManager
    {
        private List<Identifier> loaded;
        private List<Identifier> unloaded;

        private Channel configChannel;
        
        private JsonSerializerSettings serializerSettings;

        public ConfigurationManager()
        {
            this.loaded = new List<Identifier>();
            this.unloaded = new List<Identifier>();

            this.configChannel = Manager.Create(Constants.Channel);
            this.configChannel.RegisterType(typeof(Identifier));
            
            this.serializerSettings = new JsonSerializerSettings();
            this.serializerSettings.Converters.Add(new Skogsaas.Legion.Json.TypeConverter(this.configChannel));
            this.serializerSettings.Formatting = Formatting.Indented;

            this.configChannel.SubscribePublish(typeof(Identifier), onPublish);
            this.configChannel.SubscribeUnpublish(typeof(Identifier), onUnpublish);

            scan();

            this.configChannel.OnTypeRegistered += onTypeRegistered;
        }

        private void scan()
        {
            foreach (string plugin in Directory.GetDirectories(Path.Combine(new string[]{Directory.GetCurrentDirectory(), Constants.ConfigurationFolder})))
            {
                foreach(string filepath in Directory.GetFiles(plugin))
                {
                    string data = File.ReadAllText(filepath);

                    Identifier config = (Identifier)JsonConvert.DeserializeObject(data, this.configChannel.FindType(typeof(Identifier)), this.serializerSettings);

                    Type type = this.configChannel.FindType(config.Type);

                    if (type != null)
                    {
                        Identifier configLoaded = load(type, config.Plugin, config.Id);
                        this.loaded.Add(configLoaded);

                        configLoaded.PropertyChanged += onChange;

                        this.configChannel.Publish(configLoaded);
                    }
                    else
                    {
                        this.unloaded.Add(config);
                    }
                }
            }
        }

        private void onTypeRegistered(Type type, Type generated)
        {
            foreach(Identifier i in this.unloaded)
            {
                if(i.Type == type.FullName)
                {
                    load(type, i.Plugin, i.Id);
                }
            }
        }

        private Identifier load(Type type, string plugin, string identifier)
        {
            string data = File.ReadAllText(makeFilePath(plugin, identifier));

            Identifier config = (Identifier)this.configChannel.CreateType(type.FullName, identifier);

            JsonConvert.PopulateObject(data, config, this.serializerSettings);

            return config;
        }

        private void store(Identifier config)
        {
            string data = JsonConvert.SerializeObject(config, this.serializerSettings);

            ensureDirectoryExists(makeDirectoryPath(config));

            File.WriteAllText(makeFilePath(config), data);
        }

        private void delete(Identifier config)
        {
            string path = makeFilePath(config);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void onPublish(Channel channel, IObject obj)
        {
            if(typeof(Identifier).IsAssignableFrom(obj.GetType()))
            {
                Identifier config = (Identifier)obj;

                if (!this.loaded.Contains(config))
                {
                    this.loaded.Add(config);

                    config.PropertyChanged += onChange;

                    store(config);
                }
            }
        }

        private void onUnpublish(Channel channel, IObject obj)
        {
            if (typeof(Identifier).IsAssignableFrom(obj.GetType()))
            {
                Identifier config = (Identifier)obj;

                if (this.loaded.Contains(config))
                {
                    config.PropertyChanged -= onChange;

                    delete(config);
                }
            }
        }

        private void onChange(object caller, PropertyChangedEventArgs args)
        {
            IObject obj = caller as IObject;

            if (obj != null && typeof(Identifier).IsAssignableFrom(obj.GetType()))
            {
                Identifier config = (Identifier)obj;

                store(config);
            }
        }

        private static string makeDirectoryPath(string plugin)
        {
            return Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder, plugin });
        }

        private static string makeDirectoryPath(Identifier config)
        {
            return makeDirectoryPath(config.Plugin);
        }

        private static string makeFilePath(string plugin, string identifier)
        {
            return Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder, plugin, identifier + ".cfg" });
        }

        private static string makeFilePath(Identifier config)
        {
            return makeFilePath(config.Plugin, config.Id);
        }

        private static void ensureDirectoryExists(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
