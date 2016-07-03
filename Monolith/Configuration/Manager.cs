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
        #region Instance

        private static Manager instance = null;
        public static Manager Instance
        {
            get {
                if (instance == null)
                    instance = new Manager();

                return instance;
            }
        }

        #endregion

        private Manager()
        {

        }

        public T load<T>(string filename)  where T : class, IConfiguration, new()
        {
            Type type = typeof(T);

            try
            {
                string data = File.ReadAllText(filename);

                T config = JsonConvert.DeserializeObject<T>(data);

                Logging.Logger.Trace("Managed to load the configuration file <" + filename + ">");

                return config;
            }
            catch(FileNotFoundException fex)
            {
                Logging.Logger.Error("Could not find the configuration file <" + filename + ">");
            }
            catch(Exception ex)
            {
                Logging.Logger.Error("Could not load the configuration from the file <" + filename + "> - " + ex.Message);
            }

            return null;
        }

        public bool store<T>(T config, string filename) where T : class, IConfiguration, new()
        {
            Type type = typeof(T);

            try
            {
                string data = JsonConvert.SerializeObject(config);

                File.WriteAllText(type.Name + ".cfg", data);

                Logging.Logger.Trace("Managed to store the configuration file <" + filename + ">");

                return true;
            }
            catch(Exception ex)
            {
                Logging.Logger.Error("Could not store the configuration file <" + filename + "> - " + ex.Message);
            }

            return false;
        }
    }
}
