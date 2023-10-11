using System;
using Algorithm.Model.Schema;
using Algorithm.Template.AHP;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            AHPFilePath ahpFilePath = AHPFilePath.instance;
            string dataFilepath = ahpFilePath.DATA_WINDOWS_FILEPATH;
            string choose = ahpFilePath.CHOICE_WINDOWS_FILEPATH;
            // Program:
            Console.WriteLine("Chương trình tính AHP");
            Console.WriteLine("Bạn có bao nhiêu tiêu chí?");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bạn có bao nhiêu sự lựa chọn?");
            m = Convert.ToInt32(Console.ReadLine());
            AHP ahpArray = new AHP(n, m);
            // Đọc dữ liệu từ file
            // ahpArray.LoadDataFromCSV(data, n);
            AHP.LoadDataFromCSV(dataFilepath, ahpArray.length, ahpArray.Data);
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Ma trận tiêu chí");
            ahpArray.printArray(ahpArray.Data);
            // Bắt đầu chuẩn hoá
            ahpArray.NormalizeData();
            // Xác định bộ trọng số
            ahpArray.CalWeightSet();
            // Xác định tỷ số nhất quán
            ahpArray.CalConsistencyRatio();
            //Đọc file lựa chọn
            ahpArray.LoadDataChoiceFromCSV(choose, m);
            ahpArray.CalSumWeightSet();
        }
    }
}