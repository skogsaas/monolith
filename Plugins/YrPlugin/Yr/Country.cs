using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Yr.Network;
using System.Xml.Linq;


namespace Yr
{
   
    public class Country : NetworkRequest
    {
        protected readonly string locationUrl = @"http://fil.nrk.no/yr/viktigestader/noreg.txt";

        public List<string[]> getList()
        {
            List<string[]> list = new List<string[]>(); 

            string content = preformRequest(locationUrl).Result;

            foreach (string value in content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                list.Add(value.Split('\t')); 
            }

            return list; 
        }


        public XElement getXml(List<string[]> values, string rootElementName = "Locations", string subElementName = "Place")
        {
            XElement root = new XElement(rootElementName);

            List<string[]> list = getList();

            //Remove at 0 position. 
            string[] info = list[0];

            list.RemoveAt(0); 

            foreach(string[] value in values)
            {
                XElement element = new XElement(subElementName);

                for(int x = 0; x < value.Count(); x++)
                {
                    if (x < info.Length && x < value.Length && !String.IsNullOrEmpty(info[x]) && !String.IsNullOrEmpty(value[x]))
                    {
                        try
                        {
                            XElement valueElement = new XElement(info[x]);
                            valueElement.Value = value[x];

                            element.Add(valueElement); 
                        }
                        catch(Exception e)
                        {

                        }
                    }
                }
                root.Add(element); 
            }

            return root; 
        }

        public void writeFile(XElement element)
        {
            FileStream stream = File.Open(Yr.FilePath + Yr.Settings.locationFileName, FileMode.OpenOrCreate);
            
            element.Save(stream);

            stream.Close(); 
        }


        public XElement loadFile()
        {
            if (File.Exists(Yr.FilePath + Yr.Settings.locationFileName))
            {
                FileStream stream = File.Open(Yr.FilePath + Yr.Settings.locationFileName, FileMode.Open);

                return XElement.Load(stream);

                stream.Close(); 
            }
            return null; 
        }
    }
}
