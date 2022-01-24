using FunctionalExtensions.Base.Results;
using static FunctionalExtensions.Base.Results.ResultExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.Core.FileManagers
{
    public class OpenExcelFileManager : ExcelFileManager
    {
        private ExcelPackage _excelPackage;

        internal OpenExcelFileManager(string filePath) : base(filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public override Result CloseFile() =>
            AsResult(() =>
                {
                    _excelPackage.Dispose();
                    return true;
                });

        public override DataResult<string> GetFieldValue(string worksheetName, string fieldName) =>
            AsDataResult(() =>
                    _excelPackage.Workbook.Worksheets[worksheetName].Cells[fieldName].Text
                );

        public override DataResult<string> GetFieldValue(string fieldName) =>
            AsDataResult(() =>
                    _excelPackage.Workbook.Worksheets.First().Cells[fieldName].Text
                );

        public override List<string> GetWorksheetNames() =>
            _excelPackage?.Workbook?.Worksheets?.Select(_ => _.Name).ToList();

        public override Result OpenFile() =>
            AsResult(() =>
                {
                    _excelPackage?.Dispose(); // if there is an already open package
                    _excelPackage = new ExcelPackage(_filePath);
                    return true;
                });

        public override Result SaveFile() =>
            AsResult(() => 
                {
                    _excelPackage.Save();
                    _excelPackage = new ExcelPackage(_filePath); // keep the file open after saving
                    return true;
                });

        public override Result SetFieldValue(string fieldName, string value) =>
            AsResult(() => 
                {
                    _excelPackage.Workbook.Worksheets.First().Cells[fieldName].Value = value;
                    return true;
                });

        public override Result SetFieldValue(string worksheetName, string fieldName, string value) =>
            AsResult(() =>
            {
                _excelPackage.Workbook.Worksheets[worksheetName].Cells[fieldName].Value = value;
                return true;
            });
    }
}
