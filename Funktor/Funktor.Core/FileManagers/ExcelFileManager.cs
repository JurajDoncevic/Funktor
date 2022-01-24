using FunctionalExtensions.Base.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.Core.FileManagers
{
    public abstract class ExcelFileManager
    {
        protected readonly string _filePath;

        internal ExcelFileManager(string filePath)
        {
            _filePath = filePath;
        }

        public abstract Result OpenFile();
        public abstract Result CloseFile();
        public abstract Result SaveFile();

        public abstract List<string> GetWorksheetNames();
        public abstract DataResult<string> GetFieldValue(string worksheetName, string fieldName);
        public abstract DataResult<string> GetFieldValue(string fieldName);
        public abstract Result SetFieldValue(string fieldName, string value);
        public abstract Result SetFieldValue(string worksheetName, string fieldName, string value);


        public static ExcelFileManager Create(string filePath) =>
            filePath switch
            {
                var fp when fp.ToLower().EndsWith(".xls") => new BinaryExcelFileManager(filePath),
                var fp when fp.ToLower().EndsWith(".xlsx") => new OpenExcelFileManager(filePath),
                _ => throw new Exception("File format not supported!")
            };
    }
}
