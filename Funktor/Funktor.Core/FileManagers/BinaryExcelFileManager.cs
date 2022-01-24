using FunctionalExtensions.Base.Results;
using static FunctionalExtensions.Base.Results.ResultExtensions;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.Core.FileManagers
{
    public class BinaryExcelFileManager : ExcelFileManager
    {
        private Workbook _workbook;

        internal BinaryExcelFileManager(string filePath) : base(filePath)
        {
        }

        public override Result CloseFile() =>
            AsResult(() =>
                {
                    _workbook.Dispose();
                    return true;
                });

        public override DataResult<string> GetFieldValue(string worksheetName, string fieldName) =>
            AsDataResult(() =>
                _workbook.Worksheets[worksheetName].Range[fieldName].Text
                );

        public override DataResult<string> GetFieldValue(string fieldName) =>
            AsDataResult(() =>
                _workbook.ActiveSheet.Range[fieldName].Text
                );

        public override List<string> GetWorksheetNames() =>
            _workbook == null
                ? new List<string>()
                : _workbook.Worksheets.Select(_ => _.Name).ToList();

        public override Result OpenFile() => 
            AsResult(() =>
                {
                    _workbook = new Workbook();
                    _workbook.LoadFromFile(_filePath);
                    return true;
                });

        public override Result SaveFile() =>
            AsResult(() =>
                {
                    _workbook.Save();
                    return true;
                });

        public override Result SetFieldValue(string fieldName, string value) =>
            AsResult(() =>
                {
                    _workbook.ActiveSheet.Range[fieldName].Text = value;
                    return true;
                });

        public override Result SetFieldValue(string worksheetName, string fieldName, string value) =>
            AsResult(() =>
            {
                _workbook.Worksheets[worksheetName].Range[fieldName].Text = value;
                return true;
            });
    }
}
