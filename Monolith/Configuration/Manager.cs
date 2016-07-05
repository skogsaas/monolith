using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public class Manager
    {
        #region Static interface

        private static Manager instance = null;
        private static Manager Instance
        {
            get {
                if (instance == null)
                    instance = new Manager();

                return instance;
            }
        }

        internal static bool Store(ConfigurationBase config)
        {
            return Manager.Instance.storeImpl(config);
        }

        internal static void Load(ConfigurationBase config)
        {
            Manager.Instance.loadImpl(config);
        }

        internal static void Register(ConfigurationBase config)
        {
            Manager.Instance.registerImpl(config);
        }

        #endregion

        private Dictionary<string, ConfigurationBase> configurations;

        private Manager()
        {
            this.configurations = new Dictionary<string, ConfigurationBase>();
        }

        private void loadImpl(ConfigurationBase config)
        {
            try
            {
                string data = File.ReadAllText(config.Filename);

                JsonConvert.PopulateObject(data, config);

                Logging.Logger.Trace("Managed to load the configuration file <" + config.Filename + ">");

                config.NotifyChanged();
            }
            catch(FileNotFoundException fex)
            {
                Logging.Logger.Error("Could not find the configuration file <" + config.Filename + ">");
            }
            catch(Exception ex)
            {
                Logging.Logger.Error("Could not load the configuration from the file <" + config.Filename + "> - " + ex.Message);
            }
        }

        private bool storeImpl(ConfigurationBase config)
        {
            try
            {
                string data = JsonConvert.SerializeObject(config);

                File.WriteAllText(config.Filename, data);

                Logging.Logger.Trace("Managed to store the configuration file <" + config.Filename + ">");

                return true;
            }
            catch(Exception ex)
            {
                Logging.Logger.Error("Could not store the configuration file <" + config.Filename + "> - " + ex.Message);
            }

            return false;
        }

        private void registerImpl(ConfigurationBase config)
        {
            this.configurations[config.Filename] = config;
        }
    }
}
