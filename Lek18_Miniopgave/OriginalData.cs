using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lek18_Miniopgave
{
    internal class OriginalData
    {
        /*
        public string city {  get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string country { get; set; }
        public double temperature { get; set; }
        public int humidity { get; set; }
        public int pressure { get; set; }
        public int windSpeed { get; set; }
        public string windDrection { get; set; }
        public string clouds { get; set; }
        public int visibility { get; set; }
        public DateTime sunset {  get; set; }
        public DateTime sunrise { get; set; }
        */
        public XmlDocument Document { get; set; }
        public OriginalData(string city)
        {
            XmlNode xmlNode = GetXml(city);
            Document = new XmlDocument();
            convertData(xmlNode);
        }

        // Return the XML result of the URL.
        private XmlNode GetXml(string city)
        {
            // Create a web client.
            using (WebClient client = new WebClient())
            {
                // Get the response string from the URL.
                string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city +"&units=metric&mode=xml&appid=b3ca43f581f12abee3280c970b54dddb";
                string xml = client.DownloadString(url);

                // Load the response into an XML document.
                XmlDocument xml_document = new XmlDocument();
                xml_document.LoadXml(xml);
                return xml_document.SelectSingleNode("/current");
            }
        }

        private void convertData(XmlNode originalNode)
        {
            XmlElement root = Document.CreateElement("root");
            Document.AppendChild(root);

            XmlNode cityNode = originalNode.SelectSingleNode("//city");
            XmlNode timezone = cityNode.SelectSingleNode("//timezone");
            cityNode.RemoveChild(timezone);
            XmlNode copiedCityNode = Document.ImportNode(cityNode, true);
            root.AppendChild(copiedCityNode);

            XmlNode tempNode = originalNode.SelectSingleNode("//temperature");
            XmlNode copiedTempNode = Document.ImportNode(tempNode, true);
            root.AppendChild(copiedTempNode);

            XmlNode humidNode = originalNode.SelectSingleNode("//humidity");
            XmlNode copiedHumidNode = Document.ImportNode(humidNode, true);
            root.AppendChild(copiedHumidNode);

            // Håndter pressure node
            XmlNode pressureNode = originalNode.SelectSingleNode("//pressure");
            XmlNode copiedPressureNode = Document.ImportNode(pressureNode, true);
            root.AppendChild(copiedPressureNode);

            // Håndter wind node
            XmlNode windNode = originalNode.SelectSingleNode("//wind");
            XmlNode copiedWindNode = Document.ImportNode(windNode, true);
            root.AppendChild(copiedWindNode);

            // Håndter clouds node
            XmlNode cloudsNode = originalNode.SelectSingleNode("//clouds");
            XmlNode copiedCloudsNode = Document.ImportNode(cloudsNode, true);
            root.AppendChild(copiedCloudsNode);

            // Håndter visibility node
            XmlNode visiNode = originalNode.SelectSingleNode("//visibility");
            XmlNode copiedVisiNode = Document.ImportNode(visiNode, true);
            root.AppendChild(copiedVisiNode);
        }
    }
}