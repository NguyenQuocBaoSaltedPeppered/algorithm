using System;
using Algorithm.Model.Schema.AHP;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            const string csvFilePath = @".\Template\AHP\AHP_data.csv";
            Console.WriteLine("Hello!");
            // Program:
            Console.WriteLine("This is AHP calculation");
            Console.WriteLine("How many criteria do you want?");
            n = Convert.ToInt32(Console.ReadLine());
            AHP ahpArray = new AHP(n);
            // Đọc dữ liệu từ file
            ahpArray.LoadDataFromCSV(csvFilePath);
            ahpArray.printArray(ahpArray.Data);
            // Bắt đầu chuẩn hoá
            ahpArray.NormalizeData();
            // Xác định bộ trọng số
            ahpArray.CalWeightSet();
            // Xác định tỷ số nhất quán
            ahpArray.CalConsistencyRatio();
        }
    }
}