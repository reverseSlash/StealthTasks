using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Xml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using StealthTasks.Model;

namespace StealthTasks.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Visibility _windowVisibility;
        public Visibility WindowVisibility
        {
            get { return _windowVisibility; }
            set { _windowVisibility = value; RaisePropertyChanged(); }
        }

        private string _selectedCommandText;
        public string SelectedCommandText
        {
            get { return _selectedCommandText; }
            set { _selectedCommandText = value; RaisePropertyChanged(); }
        }

        private List<string> _commandsNamesList;
        public List<string> CommandsNamesList
        {
            get { return _commandsNamesList; }
            set { _commandsNamesList = value; }
        }

        private List<AutoCompleteData> _autoCompleteDataList;
        public List<AutoCompleteData> AutoCompleteDataList
        {
            get { return _autoCompleteDataList; }
            set { _autoCompleteDataList = value; }
        }

        public ICommand EnterPressedCommand { get; set; }
        public ICommand EscapePressedCommand { get; set; }
        public ICommand SpecialKeyComboPressedCommand { get; set; }
        public ICommand RefreshComboPressedCommand { get; set; }

        public MainViewModel()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Icon.ico");
            ni.Visible = true;

            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    WindowVisibility = Visibility.Visible;
                };

            _selectedCommandText = null;
            _commandsNamesList = new List<string>();
            _autoCompleteDataList = new List<AutoCompleteData>();

            EnterPressedCommand = new RelayCommand(ExecuteEnterPressedCommand);
            EscapePressedCommand = new RelayCommand(ExecuteEscapePressedCommand);
            SpecialKeyComboPressedCommand = new RelayCommand(ExecuteSpecialKeyComboPressedCommand);
            RefreshComboPressedCommand = new RelayCommand(ExecuteRefreshComboPressedCommand);
            
            Services.XmlDataService.GetAutoCompleteData("AutoCompleteData.xml", _commandsNamesList, _autoCompleteDataList);
        }

        private void ExecuteRefreshComboPressedCommand()
        {
            Services.XmlDataService.GetAutoCompleteData("AutoCompleteData.xml", _commandsNamesList, _autoCompleteDataList);
        }

        private void ExecuteSpecialKeyComboPressedCommand()
        {
            WindowVisibility = Visibility.Visible;
        }

        private void ExecuteEscapePressedCommand()
        {
            SelectedCommandText = "";
            WindowVisibility = Visibility.Hidden;
        }

        private void ExecuteEnterPressedCommand()
        {
            foreach (AutoCompleteData autoCompleteData in _autoCompleteDataList)
            {
                if (autoCompleteData.Name.Equals(_selectedCommandText))
                {
                    autoCompleteData.ExecuteCommand();

                    SelectedCommandText = "";
                    WindowVisibility = Visibility.Hidden;
                }
            }
        }
    }
}