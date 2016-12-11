using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            commandsNamesList?.Clear();
            autoCompleteDataList?.Clear();

            PopulateStandardCommands(commandsNamesList, autoCompleteDataList);

            XmlDocument doc = new XmlDocument();

            doc.Load(filePath);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                autoCompleteDataList.Add(new AutoCompleteData
                {
                    Name = node.Attributes["Name"].InnerText,
                    Type = node.Attributes["Type"].InnerText,
                    Command = node.Attributes["Command"].InnerText,
                    Arguments = node.Attributes["Arguments"].InnerText
                });

                commandsNamesList.Add(node.Attributes["Name"].InnerText);
            }
        }

        private static void PopulateStandardCommands(List<string> commandsNamesList, List<AutoCompleteData> autoCompleteDataList)
        {
            List<AutoCompleteData> standardCommandsList = new List<AutoCompleteData>
            {
                new AutoCompleteData
                {
                    Name = "exit",
                    Arguments = "",
                    Command = "exit",
                    Type = "Standard"
                }
            };

            foreach (AutoCompleteData standardCommandData in standardCommandsList)
            {
                commandsNamesList.Add(standardCommandData.Name);
                autoCompleteDataList.Add(standardCommandData);
            }
        }
    }
}
