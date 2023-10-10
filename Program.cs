using System;
using Algorithm.Model.Schema.AHP;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            const string data = @"./Template/AHP/AHP_data.csv";
            const string choose = @"./Template/AHP/AHP_choose.csv";
            // Program:
            Console.WriteLine("Chương trình tính AHP");
            Console.WriteLine("Bạn có bao nhiêu tiêu chí?");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bạn có bao nhiêu sự lựa chọn?");
            m = Convert.ToInt32(Console.ReadLine());
            AHP ahpArray = new AHP(n, m);
            // Đọc dữ liệu từ file
            ahpArray.LoadDataFromCSV(data, n);
            // Bắt đầu chuẩn hoá
            ahpArray.NormalizeData();
            // Xác định bộ trọng số
            ahpArray.CalWeightSet();
            // Xác định tỷ số nhất quán
            ahpArray.CalConsistencyRatio();
            //Đọc file lựa chọn
            ahpArray.LoadDataChooseFromCSV(choose, m);
            ahpArray.CalSumWeightSet();
        }
    }
}