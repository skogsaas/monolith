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
        private List<IIdentifier> loaded;
        private List<IIdentifier> unloaded;

        private Channel configChannel;
        
        private JsonSerializerSettings serializerSettings;

        public ConfigurationManager()
        {
            this.loaded = new List<IIdentifier>();
            this.unloaded = new List<IIdentifier>();

            this.configChannel = Manager.Create(Constants.Channel);
            this.configChannel.RegisterType(typeof(IIdentifier));
            
            this.serializerSettings = new JsonSerializerSettings();
            this.serializerSettings.Converters.Add(new Skogsaas.Legion.Json.TypeConverter(this.configChannel));
            this.serializerSettings.Formatting = Formatting.Indented;

            this.configChannel.SubscribePublish(typeof(IIdentifier), onPublish);
            this.configChannel.SubscribeUnpublish(typeof(IIdentifier), onUnpublish);

            // Make sure the config directory exists
            Directory.CreateDirectory(Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder }));

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

                    IIdentifier config = (IIdentifier)JsonConvert.DeserializeObject(data, this.configChannel.FindType(typeof(IIdentifier)), this.serializerSettings);

                    Type type = this.configChannel.FindType(config.Typename);

                    if (type != null)
                    {
                        IIdentifier configLoaded = load(type, config.Plugin, config.Id);
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
            foreach(IIdentifier i in this.unloaded)
            {
                if(i.Typename == type.FullName)
                {
                    load(type, i.Plugin, i.Id);
                }
            }
        }

        private IIdentifier load(Type type, string plugin, string IIdentifier)
        {
            string data = File.ReadAllText(makeFilePath(plugin, IIdentifier));

            IIdentifier config = (IIdentifier)this.configChannel.CreateType(type.FullName, IIdentifier);

            JsonConvert.PopulateObject(data, config, this.serializerSettings);

            return config;
        }

        private void store(IIdentifier config)
        {
            string data = JsonConvert.SerializeObject(config, this.serializerSettings);

            ensureDirectoryExists(makeDirectoryPath(config));

            File.WriteAllText(makeFilePath(config), data);
        }

        private void delete(IIdentifier config)
        {
            string path = makeFilePath(config);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void onPublish(Channel channel, IObject obj)
        {
            if(typeof(IIdentifier).IsAssignableFrom(obj.GetType()))
            {
                IIdentifier config = (IIdentifier)obj;

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
            if (typeof(IIdentifier).IsAssignableFrom(obj.GetType()))
            {
                IIdentifier config = (IIdentifier)obj;

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

            if (obj != null && typeof(IIdentifier).IsAssignableFrom(obj.GetType()))
            {
                IIdentifier config = (IIdentifier)obj;

                store(config);
            }
        }

        private static string makeDirectoryPath(string plugin)
        {
            return Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder, plugin });
        }

        private static string makeDirectoryPath(IIdentifier config)
        {
            return makeDirectoryPath(config.Plugin);
        }

        private static string makeFilePath(string plugin, string IIdentifier)
        {
            return Path.Combine(new string[] { Directory.GetCurrentDirectory(), Constants.ConfigurationFolder, plugin, IIdentifier + ".cfg" });
        }

        private static string makeFilePath(IIdentifier config)
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
