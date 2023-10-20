using System.Globalization;
<<<<<<< HEAD
using CsvHelper;
using CsvHelper.Configuration;
=======
>>>>>>> 8c1095e2873a1f08cef7fb3ee222d64124d638db

namespace Algorithm.Model
{
    public partial class CommonModel
    {
        public static double[,] ReadDataFromFile(string filePath)
        {
            CultureInfo customCulture = new CultureInfo("en-US");
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

        public static void PrintArray(double[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{array[i, j]}       ");
                }
                Console.WriteLine();
            }
        }
    }
}