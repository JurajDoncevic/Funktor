using FunctionalExtensions.Base.Results;
using static FunctionalExtensions.Base.Results.ResultExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Funktor.Core.Configuration
{
    public static class ConfigurationManagement
    {
        public static async Task<Result> SaveMappingConfiguration(string filepath, MappingConfiguration mappingConfiguration) =>
            await AsResult(async () => 
                {
                    await System.IO.File.WriteAllTextAsync(
                        filepath,
                        JsonSerializer.Serialize(mappingConfiguration)
                        );
                    return true;
                }
                );

        public static async Task<DataResult<MappingConfiguration>> LoadMappingConfiguration(string filePath) =>
            await AsDataResult<MappingConfiguration>(async () =>
                    JsonSerializer.Deserialize<MappingConfiguration>(await System.IO.File.ReadAllTextAsync(filePath))
                );
    }
}
