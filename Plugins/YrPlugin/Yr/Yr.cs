using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yr.Network; 

namespace Yr
{
    public class Yr
    {

        private static Settings settings = null;

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

        public static Settings Settings
        {
            get
            {
                if(settings == null)
                {
                    Yr network = new Yr();
                }

                return settings; 
            }
            set
            {
                settings = value; 
            }
        }

        public Yr(Settings network) 
        {
            if (network == null)
                throw new ArgumentNullException("The network object cannot be null");

            Yr.Settings = network; 
        }

        public Yr()
        {
            if(!File.Exists(this.settingsFilePath + Path.DirectorySeparatorChar + this.settingsFileName))
            {
                this.createFile(); 
            }
            Yr.Settings = this.loadFile(); 
        }

        public Yr(bool overrideFile = false)
        {
            this.createFile(overrideFile);

            Yr.Settings = this.loadFile(); 
        }

        public void createFile(bool overrideFile = false)
        {
            string filePath = this.settingsFilePath + Path.DirectorySeparatorChar + this.settingsFileName;

            if (overrideFile == false && File.Exists(filePath))
                throw new IOException("File allready exists");

            if (settings == null)
                settings = new Settings(); 

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

        public Settings loadFile()
        {
            string filePath = this.settingsFilePath + Path.DirectorySeparatorChar + this.settingsFileName;

            if(File.Exists(filePath))
            {
                FileStream file = File.Open(filePath, FileMode.Open);

                StreamReader stream = new StreamReader(file);

                JsonSerializer serializer = new JsonSerializer();

                Settings settings = (Settings)serializer.Deserialize(stream, typeof(Settings));

                file.Close(); 

                return settings; 
            }

            return null; 
        }
    }
}
