using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using StealthTasks.Model;

namespace StealthTasks.Services
{
    public static class XmlDataService
    {
        public static void GetAutoCompleteData(string filePath, List<string> commandsNamesList, List<AutoCompleteData> autoCompleteDataList)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(filePath);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                autoCompleteDataList.Add(new AutoCompleteData
                {
                    Name = node.Attributes["Name"].InnerText,
                    Command = node.Attributes["Command"].InnerText,
                    Arguments = node.Attributes["Arguments"].InnerText
                });

                commandsNamesList.Add(node.Attributes["Name"].InnerText);
            }
        }
    }
}
