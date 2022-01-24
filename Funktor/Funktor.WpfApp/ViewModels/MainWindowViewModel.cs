using Funktor.WpfApp.Commands;
using Funktor.WpfApp.Models;
using Funktor.WpfApp.Utils;
using Funktor.WpfApp.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Funktor.WpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand CreateConfigurationCommand => new RelayCommand(_ => CreateConfiguration());
        public ICommand OpenConfigurationCommand => new RelayCommand(async _ => await OpenConfigurationFile());
        public ICommand SaveConfigurationCommand => new RelayCommand(async _ => await SaveConfigurationFile());
        public ICommand CloseApplicationCommand => new RelayCommand(_ => CloseApplication());

        public MappingPage CurrentPage { get; set; }


        /// <summary>
        /// Creates a new empty configuration page
        /// </summary>
        private void CreateConfiguration()
        {
            if (CurrentPage != null)
            {
                (MessageBox.Show("Do you want to save the current configuration?\nAll unsaved changes will be lost!", "Save configuration?", MessageBoxButton.YesNoCancel)
                    switch
                {
                    MessageBoxResult.Yes => (Action)(async () => { await SaveConfigurationFile(); CreateCleanConfiguration(); }), 
                    MessageBoxResult.No => (Action)(() => { CreateCleanConfiguration(); })
                }).Invoke();
            }
            else
            {
                CreateCleanConfiguration();
            }
        }

        /// <summary>
        /// Sets to an empty configuration display
        /// </summary>
        private void CreateCleanConfiguration()
        {
            CurrentPage = null;
            CurrentPage = new MappingPage();
        }

        /// <summary>
        /// Used to open a configuration file. Sets the CurrentPage property if a configuration is properly loaded.
        /// </summary>
        private async Task OpenConfigurationFile()
        {
            string filePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON configuration | *.json";
            if (openFileDialog.ShowDialog() ?? false)
            {
                filePath = openFileDialog.FileName;

                CreateCleanConfiguration();

                CurrentPage.MappingPageViewModel.MappingConfiguration =
                    (await Core.Configuration.ConfigurationManagement.LoadMappingConfiguration(filePath) switch
                    {
                        { IsSuccess: true } dr => (Func<MappingConfiguration>)(() => MapperSingleton.GetMapperInstance().Map<MappingConfiguration>(dr.Data)),
                        { IsSuccess: false } dr => () => { MessageBox.Show(dr.ErrorMessage, "Error reading configuration"); return new MappingConfiguration(); }
                    }).Invoke();

            }
        }

        /// <summary>
        /// Saves the current configuration to a file
        /// </summary>
        private async Task SaveConfigurationFile()
        {
            string filePath = null;
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "JSON configuration | *.json";
            if (openFileDialog.ShowDialog() ?? false)
            {
                filePath = openFileDialog.FileName;
                var configuration = MapperSingleton.GetMapperInstance().Map<Core.Configuration.MappingConfiguration>(CurrentPage.MappingPageViewModel.MappingConfiguration);

                (await Core.Configuration.ConfigurationManagement.SaveMappingConfiguration(filePath, configuration) switch
                {
                    { IsSuccess: true } => (Action) (() => MessageBox.Show("Configuration saved!")),
                    var dr => () => MessageBox.Show(dr.ErrorMessage, "Error occured during saving!")
                }).Invoke();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        private void CloseApplication()
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }
    }
}
