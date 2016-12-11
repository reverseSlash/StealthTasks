using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StealthTasks.Model
{
    public class AutoCompleteData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
        public string Arguments { get; set; }

        public void ExecuteCommand()
        {
            switch (Type)
            {
                case "Process":
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = Command;
                    startInfo.Arguments = Arguments;

                    Process.Start(startInfo);
                    break;

                case "Standard":
                    switch (Command)
                    {
                        case "exit":
                            Application.Current.Shutdown();
                            break;
                    }
                    break;
            }
        }
    }
}
