using System.Collections.Generic;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;


namespace Algorithm.Model.Schema
{
    public partial class AHP : CommonModel
    {
        public AHP(int n)
        {
            length = n;
            Data = new double[n, n];
            NormalizedData = new double[n, n];
            WeightSet = new double[n];
            isWeightSetValid = false;
        }
        public AHP(int n, int m)
        {
            length = n;
            choose = m;
            Data = new double[n, n];
            DataChoice = new double[m,m];
            NormalizedData = new double[n, n];
            WeightSet = new double[n];
            isWeightSetValid = false;
        }
        /// <summary>
        /// Ma trận 2 chiều chứa n tiêu chí so sánh
        /// </summary>
        public double[,] Data;
        /// <summary>
        /// Ma trận 2 chiều chứa n tiêu chí so sánh
        /// </summary>
        public double[,]? DataChoice;
        /// <summary>
        /// Ma trận sau chuẩn hoá
        /// </summary>
        public double[,] NormalizedData;
        /// <summary>
        /// Bộ trọng số
        /// </summary>
        public double[] WeightSet;
        public double LamdaMax;
        public double CI;
        public double CR;
        public int length;
        public int choose;
        /// <summary>
        /// Biến thể hiện bộ trọng số có được chấp nhận không
        /// </summary>
        public bool isWeightSetValid;
        /// <summary>
        /// Hàm đọc dữ liệu từ 1 file csv và gán các giá trị cho mảng data trong đối tượng của class AHP
        /// </summary>
        /// <param name="csvFilePath"></param>
        public static void LoadDataFromCSV(string csvFilePath, int length, double[,] array)
        {
            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture){Delimiter = ","}))
                {
                    for (int row = 0; row < length; row++)
                    {
                        csv.Read();
                        for (int col = 0; col < length; col++)
                        {
                            array[row, col] = csv.GetField<double>(col);
                        }
                    }
                }
                // Console.WriteLine("-----------------------------------");
                // Console.WriteLine("Ma trận tiêu chí");
                // printArray(array);
            }
            catch (Exception ex)
            {
                printError(ex);
                throw;
            }
        }
        /// <summary>
        /// Hàm lấy dữ liệu từ các file csv và gán cho các mảng AHP cụ thể dựa trên filePath được truyền vào
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="array"></param>
        public static void LoadDataFromCSV(List<string> filePaths, int length, AHP[] arrays)
        {
            try
            {
                foreach(string path in filePaths)
                {
                    foreach(AHP array in arrays)
                    {
                        LoadDataFromCSV(path, length, array.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                printError(ex);
                throw;
            }
        }
        public void LoadDataChoiceFromCSV(string csvFilePath, int length)
        {
            try
            {
                if (DataChoice == null)
                {
                    DataChoice = new double[length, length];
                }
                if (isWeightSetValid){
                    using (var reader = new StreamReader(csvFilePath))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture){Delimiter = ","}))
                    {
                        for (int row = 0; row < length; row++)
                        {
                            csv.Read();
                            for (int col = 0; col < length; col++)
                            {
                                DataChoice[row, col] = csv.GetField<double>(col);
                            }
                        }
                    }
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Ma trận lựa chọn");
                    printArray(DataChoice);
                }
            }
            catch (Exception ex)
            {
                printError(ex);
                throw;
            }
        }
        // Phương pháp chuẩn hoá ma trận
        /// <summary>
        /// Hàm chuẩn hoá ma trận data bằng cách chia từng phần tử trong cột cho tổng của cột
        /// </summary>
        public void NormalizeData()
        {
            try
            {
                for (int col = 0; col < length; col++)
                {
                    // Tính tổng của cột hiện tại
                    double columnSum = 0;
                    for (int row = 0; row < length; row++)
                    {
                        columnSum += Data[row, col];
                    }
                    // Chuẩn hoá cột bằng cách chia từng phần tử trong cột cho tổng của cột
                    for (int row = 0; row < length; row++)
                    {
                        NormalizedData[row, col] = Math.Round(Data[row, col] / columnSum, 2);
                    }
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Ma trận sau chuẩn hoá:");
                printArray(NormalizedData);
            }
            catch (Exception ex)
            {
                printError(ex);
                throw;
            }
        }
        // Xác định tỉ số nhất quán
        /// <summary>
        /// Hàm tính bộ trọng số
        /// </summary>
        public void CalWeightSet()
        {
            try
            {
                for (int row = 0; row < length; row++)
                {
                    double columnSum = 0;
                    for (int col = 0; col < length; col++)
                    {
                        columnSum += NormalizedData[row, col];
                    }
                    WeightSet[row] = columnSum;
                }
                // Chuẩn hoá bộ trọng số để tổng của chúng bằng 1
                double totalWeight = WeightSet.Sum();
                for (int i = 0; i < length; i++)
                {
                    WeightSet[i] = Math.Round(WeightSet[i] / totalWeight, 2);
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Bộ trọng số:");
                printArray(WeightSet);
            }
            catch (Exception ex)
            {
                printError(ex);
                throw;
            }
        }
        /// <summary>
        /// Hàm xác định tỉ số nhất quán
        /// </summary>
        public void CalConsistencyRatio()
        {
            // Vector Tổng trọng số
            double[] WeightVector = MultiplyArray(Data, WeightSet);
            for(int i = 0; i < WeightVector.Length; i++)
            {
                WeightVector[i] = Math.Round(WeightVector[i], 2);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Vector trọng số:");
            printArray(WeightVector);
            // Vector nhất quán
            double[] CR_Vector = new double[length];
            for(int i = 0; i < length; i++)
            {
                CR_Vector[i] = Math.Round(WeightVector[i]/WeightSet[i], 2);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Vector nhất quán");
            printArray(CR_Vector);
            Console.WriteLine("-----------------------------------");
            // Xác định LamdaMax
            LamdaMax = Math.Round(CR_Vector.Average(), 2);
            Console.WriteLine($"LamdaMax: {LamdaMax}");
            // Xác định CI
            CI = Math.Round((LamdaMax - length)/(length - 1), 2);
            Console.WriteLine($"CI: {CI}");
            // Xác định CR
            CR = Math.Round((CI/RI_Dictionary[length]), 2);
            Console.WriteLine($"RI: {RI_Dictionary[length]}");
            Console.WriteLine($"CR: {CR}");
            // Bộ trọng số có được chấp nhận?
            if(CR < ValidBound)
            {
                isWeightSetValid = true;
            }
            Console.WriteLine($@"Bộ trọng số {(isWeightSetValid ? "được chấp nhận" : "không được chấp nhận")}");
        }
        /// <summary>
        /// Hàm tính tổng trọng số các phương án
        /// </summary>
        public void CalSumWeightSet()
        {
            // Nếu bộ trọng số không hợp lệ thì không tính tổng trọng số
            if (isWeightSetValid)
            {
                // Khởi tạo mảng chứa tổng trọng số của các phương án
                double[] sumWeightSet = new double[choose];
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Tổng trọng số cho từng lựa chọn");
                // Tính tổng trọng số của mỗi phương án
                for (int i = 0; i < choose; i++)
                {
                    for (int j = 0; j < choose; j++)
                    {
                        sumWeightSet[i] = Math.Round(sumWeightSet[i] + DataChoice[i, j] * WeightSet[j], 2);
                    }
                    Console.WriteLine($"Lựa chọn {i + 1} = {sumWeightSet[i]}");
                }
            }
        }
    }
}