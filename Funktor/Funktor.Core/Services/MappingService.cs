using FunctionalExtensions.Base.Results;
using static FunctionalExtensions.Base.Results.ResultExtensions;
using Funktor.Core.Configuration;
using Funktor.Core.FileManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionalExtensions.Base;

namespace Funktor.Core.Services
{
    public class MappingService
    {
        public static Result Map(ExcelFileManager sourceManager, ExcelFileManager destinationManager, MappingConfiguration mappingConfiguration) => 
                sourceManager.OpenFile()
                    .Bind(_ => destinationManager.OpenFile())
                    .Bind(_ => mappingConfiguration.ItemMappings
                                    .Map(mi => sourceManager.GetFieldValue(mi.SourceWorksheet, mi.SourceField)
                                                            .Bind(_ => destinationManager.SetFieldValue(mi.DestinationWorksheet, mi.DestinationField, _.Data)))
                                    .Aggregate((_1, _2) => _1.IsSuccess ? _2 : _1)
                                    )
                    .Bind(_ => sourceManager.SaveFile())
                    .Bind(_ => destinationManager.SaveFile())
                    .Bind(_ => sourceManager.CloseFile())
                    .Bind(_ => destinationManager.CloseFile());
    }
}
