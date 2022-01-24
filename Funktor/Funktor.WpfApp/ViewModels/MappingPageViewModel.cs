using FunctionalExtensions.Base.Results;
using Funktor.Core.FileManagers;
using Funktor.Core.Services;
using Funktor.WpfApp.Commands;
using Funktor.WpfApp.Models;
using Funktor.WpfApp.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Funktor.WpfApp.ViewModels
{
    public class MappingPageViewModel : BaseViewModel
    {
        public ICommand SetSourceFilePathCommand => new RelayCommand(_ => SetExcelFilePath(fileName => MappingConfiguration.SourceFilePath  = fileName));
        public ICommand SetDestinationFilePathCommand => new RelayCommand(_ => SetExcelFilePath(fileName => MappingConfiguration.DestinationFilePath = fileName));
        public ICommand LoadExcelFilesCommand => new RelayCommand(_ => LoadExcelFiles(), _ => !string.IsNullOrEmpty(MappingConfiguration?.SourceFilePath?.Trim()) && !string.IsNullOrEmpty(MappingConfiguration?.DestinationFilePath?.Trim()));
        public ICommand MapExcelDataCommand => new RelayCommand(async _ => await MapExcelData(), _ => _sourceFileManager != null && _destinationFileManager != null);

        public MappingConfiguration MappingConfiguration { get; set; } = new MappingConfiguration();

        private ExcelFileManager _sourceFileManager { get; set; }
        private ExcelFileManager _destinationFileManager { get; set; }

        public ObservableCollection<string> SourceWorksheets { get; set; }
        public ObservableCollection<string> DestinationWorksheets { get; set; }

        public Visibility ProgressBarVisibility { get; set; } = Visibility.Collapsed;

        private void SetExcelFilePath(Action<string> filePathToLabel)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel OpenXML|*.xlsx|Excel binary|*.xls|All file types|*.*";
            if(openFileDialog.ShowDialog() ?? false)
            {
                filePathToLabel(openFileDialog.FileName);
            }
        }

        private void LoadExcelFiles()
        {
            _sourceFileManager = ExcelFileManager.Create(MappingConfiguration.SourceFilePath);
            _destinationFileManager = ExcelFileManager.Create(MappingConfiguration.DestinationFilePath);
        }

        private async Task MapExcelData()
        {
            var mappingConfig = MapperSingleton.GetMapperInstance().Map<Core.Configuration.MappingConfiguration>(MappingConfiguration);


            ProgressBarVisibility = Visibility.Visible;
            var result =
                await Task.Run(() => MappingService.Map(_sourceFileManager, _destinationFileManager, mappingConfig));
            ProgressBarVisibility = Visibility.Collapsed;

            var messageBoxResult =
                result switch
                {
                    { IsSuccess: true } r => MessageBox.Show("Data successfully mapped", "Data mapped!", MessageBoxButton.OK, MessageBoxImage.Information),
                    { IsSuccess: false } r => MessageBox.Show(r.ErrorMessage, "Error occured during mapping!", MessageBoxButton.OK, MessageBoxImage.Error)
                };
        }
    }
}
