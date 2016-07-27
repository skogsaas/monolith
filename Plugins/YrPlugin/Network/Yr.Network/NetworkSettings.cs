using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yr.Network
{
    public class NetworkSettings
    {

        private string basePath;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]

        public string BasePath
        {
            get
            {
                return this.basePath; 
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("basePath cannot be null");

                if (this.isUrl(value))
                    this.basePath = value;
                else
                    throw new ArgumentException("The url is not a path, try starting with HTTP or HTTPS"); 
            }
        }

        [JsonIgnore]
        private string fileType = "xml";

        public bool SanitizeHtml = false; 

        public string FileType
        {
            get
            {
                return this.fileType;
            }
            set
            {
                switch (value.ToLower())
                {
                    case "xml":
                        this.fileType = value.ToLower();
                        break; 
                    case "json":
                        this.fileType = value.ToLower();
                        break;
                    default:
                        throw new ArgumentException("Not a valid options, available file formats : json, xml");
                }
            }
        }

        /// <summary>
        /// Checks if a path is an url. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        private bool isUrl(string path)
        {
            Uri uriResult;
            return Uri.TryCreate(path, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
