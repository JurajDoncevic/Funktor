using Funktor.Core.FileManagers;
using Funktor.WpfApp.Commands;
using Funktor.WpfApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Funktor.WpfApp.ViewModels
{
    public class MappingPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SetSourceFilePathCommand => new RelayCommand(_ => SetExcelFilePath(fileName => MappingConfiguration.SourceFilePath  = fileName));
        public ICommand SetDestinationFilePathCommand => new RelayCommand(_ => SetExcelFilePath(fileName => MappingConfiguration.DestinationFilePath = fileName));
        public ICommand LoadExcelFilesCommand => new RelayCommand(_ => LoadExcelFiles(), _ => !string.IsNullOrEmpty(MappingConfiguration?.SourceFilePath?.Trim()) && !string.IsNullOrEmpty(MappingConfiguration?.DestinationFilePath?.Trim()));

        public MappingConfiguration MappingConfiguration { get; set; } = new MappingConfiguration();

        private ExcelFileManager _sourceFileManager { get; set; }
        private ExcelFileManager _destinationFileManager { get; set; }

        public ObservableCollection<string> SourceWorksheets { get; set; }
        public ObservableCollection<string> DestinationWorksheets { get; set; }

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
    }
}
