using System.Globalization;

namespace Algorithm.Model
{
    public partial class CommonModel
    {
        public static double[,] ReadDataFromFile(string filePath)
        {
            CultureInfo customCulture = new("en-US");
            customCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            CultureInfo.CurrentCulture = customCulture;
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Split(',').Length;
            double[,] data = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                string[] values = lines[i].Split(',');
                for (int j = 0; j < cols; j++)
                {
                    if (double.TryParse(values[j], NumberStyles.Any, CultureInfo.CurrentCulture, out double value))
                    {
                        data[i, j] = value;
                    }
                    else
                    {
                        // Xử lý lỗi nếu dữ liệu không phải là số
                        Console.WriteLine($"Lỗi ở dòng {i + 1}, cột {j + 1}");
                    }
                }
            }
            return data;
        }
    }
}