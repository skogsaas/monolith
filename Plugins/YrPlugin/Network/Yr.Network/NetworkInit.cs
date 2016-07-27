using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yr.Network
{
    public class NetworkInit
    {

        private static NetworkSettings settings = null;

        public string settingsFileName = "settings.json";

        private string settingsFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public string SettingsFilePath
        {
            get
            {
                return this.settingsFilePath; 
            }
            set
            {
                if (!Directory.Exists(value))
                    throw new DirectoryNotFoundException("Could not found the specified directory");
                this.settingsFilePath = value; 
            }
        }

        internal static NetworkSettings Settings
        {
            get
            {
                if(settings == null)
                {
                    NetworkInit network = new NetworkInit();
                }
                
                return settings; 
            }
            set
            {
                settings = value; 
            }
        }

        public NetworkInit(NetworkSettings network) 
        {
            if (network == null)
                throw new ArgumentNullException("The network object cannot be null");

            NetworkInit.Settings = network; 
        }

        public NetworkInit()
        {
            if(!File.Exists(this.settingsFilePath + Path.DirectorySeparatorChar + this.settingsFileName))
            {
                this.createFile(); 
            }
            this.loadFile(); 
        }

        public NetworkInit(bool overrideFile = false)
        {
            this.createFile(overrideFile);

            this.loadFile(); 
        }

        public void createFile(bool overrideFile = false)
        {
            string filePath = this.settingsFilePath + Path.DirectorySeparatorChar + this.settingsFileName;

            if (overrideFile == false && File.Exists(filePath))
                throw new IOException("File allready exists");

            if (settings == null)
                settings = new NetworkSettings(); 

            FileStream file = File.Open(filePath, FileMode.Create);


            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented; 

            using(StreamWriter stream = new StreamWriter(file))
            using(JsonWriter writer = new JsonTextWriter(stream))
            {
                serializer.Serialize(writer, settings); 
            }

            file.Close(); 
        }

        public NetworkSettings loadFile()
        {
            string filePath = this.settingsFilePath + Path.DirectorySeparatorChar + this.settingsFileName;

            if(File.Exists(filePath))
            {
                FileStream file = File.Open(filePath, FileMode.Open);

                StreamReader stream = new StreamReader(file);

                JsonSerializer serializer = new JsonSerializer();

                NetworkSettings settings = (NetworkSettings)serializer.Deserialize(stream, typeof(NetworkSettings));

                file.Close(); 

                return settings; 
            }

            return null; 
        }
       
    }
}
