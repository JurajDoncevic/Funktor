using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.WpfApp.Models
{
    public class MappingItem : INotifyPropertyChanged
    {
        private string _sourceWorksheet;
        private string _sourceField;
        private string _destinationWorksheet;
        private string _destinationField;

        public string SourceWorksheet { get => _sourceWorksheet; set => _sourceWorksheet = value; }
        public string SourceField { get => _sourceField; set => _sourceField = value; }
        public string DestinationWorksheet { get => _destinationWorksheet; set => _destinationWorksheet = value; }
        public string DestinationField { get => _destinationField; set => _destinationField = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
