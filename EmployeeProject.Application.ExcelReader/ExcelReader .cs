using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;

namespace EmployeeProject.Application.ExcelReader
{
    public class ExcelReader : IExcelReader
    {
        public DataTable Read(string fileName)
        {
            DataTable dtSource = new DataTable();
            if (fileName == string.Empty)
                return dtSource;


            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                // init
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                SharedStringTable sst = sstpart.SharedStringTable;

                Worksheet sheet = worksheetPart.Worksheet;

                var cells = sheet.Descendants<Cell>();
                var rows = sheet.Descendants<Row>();

                //process
                dtSource = CreateDataTable(rows.First(), sst);

                int rowCount = 0;
                foreach (Row row in rows)
                {
                    if (rowCount > 0)
                    {
                        int count = 0;
                        DataRow dtRow = dtSource.NewRow();
                        foreach (Cell c in row.Elements<Cell>())
                        {
                            if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                            {
                                int ssid = int.Parse(c.CellValue.Text);
                                string str = sst.ChildElements[ssid].InnerText;
                                dtRow[count] = str;
                            }
                            else if (c.CellValue != null)
                            {
                                dtRow[count] = c.CellValue.Text;
                            }
                            count++;
                        }

                        dtSource.Rows.Add(dtRow);
                    }
                    rowCount++;
                }
            }
            return dtSource;
        }

        private DataTable CreateDataTable(Row row, SharedStringTable sst)
        {
            var dtSource = new DataTable();

            foreach (Cell cell in row.Elements<Cell>())
            {
                if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                {
                    int ssid = int.Parse(cell.CellValue.Text);
                    string str = sst.ChildElements[ssid].InnerText;
                    dtSource.Columns.Add(str);
                }
                else if (cell.CellValue != null)
                {
                    dtSource.Columns.Add(cell.CellValue.Text);
                }
            }

            return dtSource;
        }
    }
}
