using Blazor.Pages;
using System.Data;
using OfficeOpenXml;


public class ExcelService
{
    public DataTable ReadExcelFile(string filePath)
    {
        // Создаем экземпляр пакета ExcelPackage, передавая путь к файлу
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            // Получаем первый лист в файле
            var worksheet = package.Workbook.Worksheets[0];

            // Создаем новую таблицу данных
            DataTable dt = new DataTable();

            // Читаем заголовки столбцов (первую строку)
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                dt.Columns.Add(firstRowCell.Text);
            }

            // Читаем данные, начиная со второй строки
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var newRow = dt.NewRow();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    newRow[col - 1] = worksheet.Cells[row, col].Text;
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }

    }
}
