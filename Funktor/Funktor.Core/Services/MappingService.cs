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
            AsResult(() =>
                mappingConfiguration.ItemMappings
                    .Map(mi => sourceManager.GetFieldValue(mi.SourceWorksheet, mi.SourceField)
                                    .Bind(_ => destinationManager.SetFieldValue(mi.DestinationWorksheet, mi.DestinationField, _.Data))
                        )
                    .All(_ => _.IsSuccess)
                );
    }
}
