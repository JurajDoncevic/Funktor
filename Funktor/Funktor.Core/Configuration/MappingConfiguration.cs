using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Funktor.Core.Configuration
{
    public class MappingConfiguration
    {
        private string _sourceFilePath;
        private string _destinationFilePath;

        public List<MappingItem> ItemMappings { get; set; } = new List<MappingItem>();
        [JsonIgnore]
        public string SourceFilePath { get => _sourceFilePath; set => _sourceFilePath = value; }
        [JsonIgnore]
        public string DestinationFilePath { get => _destinationFilePath; set => _destinationFilePath = value; }
    }
}
