using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.Core.Configuration
{
    public class MappingItem
    {
        private string _sourceWorksheet;
        private string _sourceField;
        private string _destinationoWorksheet;
        private string _destinationField;

        public string SourceWorksheet { get => _sourceWorksheet; set => _sourceWorksheet = value; }
        public string SourceField { get => _sourceField; set => _sourceField = value; }
        public string DestinationWorksheet { get => _destinationoWorksheet; set => _destinationoWorksheet = value; }
        public string DestinationField { get => _destinationField; set => _destinationField = value; }
    }
}
