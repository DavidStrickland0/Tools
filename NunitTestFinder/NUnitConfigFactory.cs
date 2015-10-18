using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NunitTestFinder
{
    class NunitConfigFactory
    {
        public static XmlDocument CreateConfig(IEnumerable<string> files)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode projectNode = doc.CreateElement("NUnitProject");
            doc.AppendChild(projectNode);

            XmlNode settingNode = doc.CreateElement("Settings");
            projectNode.AppendChild(settingNode);
            XmlAttribute activeconfig = doc.CreateAttribute("activeconfig");
            activeconfig.Value = "Debug";

            settingNode.Attributes.Append(activeconfig);
            

            XmlNode configNode = doc.CreateElement("Config");

            XmlAttribute nameAttribute = doc.CreateAttribute("name");
            nameAttribute.Value = "Debug";

            XmlAttribute binPathTypeAttribute = doc.CreateAttribute("binpathtype");
            binPathTypeAttribute.Value = "Auto";

            configNode.Attributes.Append(nameAttribute);
            configNode.Attributes.Append(binPathTypeAttribute);

            foreach(string fileName in files)
            {
                XmlNode assemblyNode = doc.CreateElement("assembly");
                XmlAttribute pathAttribute = doc.CreateAttribute("path");
                pathAttribute.Value = fileName;
                assemblyNode.Attributes.Append(pathAttribute);
                configNode.AppendChild(assemblyNode);
            }
            projectNode.AppendChild(configNode);

            XmlNode configReleaseNode = doc.CreateElement("Config");

            XmlAttribute nameReleaseAttribute = doc.CreateAttribute("name");
            nameReleaseAttribute.Value = "Release";

            XmlAttribute binPathTypeReleaseAttribute = doc.CreateAttribute("binpathtype");
            binPathTypeReleaseAttribute.Value = "Auto";

            configReleaseNode.Attributes.Append(nameReleaseAttribute);
            configReleaseNode.Attributes.Append(binPathTypeReleaseAttribute);
            projectNode.AppendChild(configReleaseNode);

            return doc;
        }
    }
}
