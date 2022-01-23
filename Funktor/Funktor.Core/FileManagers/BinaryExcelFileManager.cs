using FunctionalExtensions.Base.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.Core.FileManagers
{
    public class BinaryExcelFileManager : ExcelFileManager
    {
        internal BinaryExcelFileManager(string filePath) : base(filePath)
        {
        }

        public override DataResult<string> GetFieldValue(string worksheetName, string fieldName)
        {
            throw new NotImplementedException();
        }

        public override DataResult<string> GetFieldValue(string fieldName)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetWorksheetNames()
        {
            throw new NotImplementedException();
        }

        public override void OpenFile()
        {
            throw new NotImplementedException();
        }

        public override void SaveFile()
        {
            throw new NotImplementedException();
        }

        public override Result SetFieldValue(string fieldName, string value)
        {
            throw new NotImplementedException();
        }

        public override Result SetFieldValue(string worksheetName, string fieldName, string value)
        {
            throw new NotImplementedException();
        }
    }
}
