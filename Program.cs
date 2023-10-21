using System;
using System.Text;
using Algorithm.Model;
using Algorithm.Model.Schema;
using Algorithm.Template.AHP;
using Algorithm.Template.UserBased;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            // AHPFilePath ahpFilePath = AHPFilePath.instance;
            // string dataFilepath = ahpFilePath.DATA_WINDOWS_FILEPATH;
            // string choose = ahpFilePath.CHOICE_WINDOWS_FILEPATH;
            // // Program:
            // Console.WriteLine("Chương trình tính AHP");
            // // Console.WriteLine("Bạn có bao nhiêu tiêu chí?");
            // // n = Convert.ToInt32(Console.ReadLine());
            // // Console.WriteLine("Bạn có bao nhiêu sự lựa chọn?");
            // // m = Convert.ToInt32(Console.ReadLine());
            // // Đọc dữ liệu từ file
            // // ahpArray.LoadDataFromCSV(data, n);
            // double[,] arrayDouble = CommonModel.ReadDataFromFile(choose);
            // int m = arrayDouble.GetLength(0);
            // int n = arrayDouble.GetLength(1);
            // // tính toán
            // AHP ahpArray = new AHP(m, n);
            // AHP.LoadDataFromCSV(dataFilepath, ahpArray.length, ahpArray.Data);
            // Console.WriteLine("-----------------------------------");
            // Console.WriteLine("Ma trận tiêu chí");
            // ahpArray.PrintArray(ahpArray.Data);
            // // Bắt đầu chuẩn hoá
            // ahpArray.NormalizeData();
            // // Xác định bộ trọng số
            // ahpArray.CalWeightSet();
            // // Xác định tỷ số nhất quán
            // ahpArray.CalConsistencyRatio();
            // //Đọc file lựa chọn
            // ahpArray.LoadDataChoiceFromCSV(choose, arrayDouble.GetLength(1));
            // ahpArray.CalSumWeightSet();
            UserBased_FilePath userBased_FilePath = UserBased_FilePath.Instance;
            string dataFilepath = userBased_FilePath.DATA_WINDOWS_FILEPATH;
            UserBased ubData = new(dataFilepath);
            ItemBased ibData = new(dataFilepath);
            double ubPredictedValue = ubData.PredictedRating(3, 0);
            double ibPredictedValue = ibData.PredictedRating(0, 3);
            Console.WriteLine($"Giá trị dự đoán là: {Math.Round((ubPredictedValue + ibPredictedValue)/2, 2)}");
        }
    }
}