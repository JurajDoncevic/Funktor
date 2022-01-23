using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.WpfApp.Models
{
    public class MappingConfiguration : INotifyPropertyChanged
    {
        [NonSerialized]
        private string _sourceFilePath;
        [NonSerialized]
        private string _destinationFilePath;

        public List<MappingItem> MappingItems { get; set; } = new List<MappingItem>();
        public string SourceFilePath { get => _sourceFilePath; set => _sourceFilePath = value; }
        public string DestinationFilePath { get => _destinationFilePath; set => _destinationFilePath = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
