using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;


namespace Algorithm.Model.Schema.AHP
{
    public partial class AHP : CommonModel
    {
        /// <summary>
        /// Ma trận 2 chiều chứa n tiêu chí so sánh
        /// </summary>
        public double[,] Data;
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
        /// <summary>
        /// Biến thể hiện bộ trọng số có được chấp nhận không
        /// </summary>
        public bool isWeightSetValid;
        public AHP(int n)
        {
            length = n;
            Data = new double[n, n];
            NormalizedData = new double[n, n];
            WeightSet = new double[n];
            isWeightSetValid = false;
        }
        /// <summary>
        /// Hàm đọc dữ liệu từ 1 file csv và gán các giá trị cho mảng data trong đối tượng của class AHP
        /// </summary>
        /// <param name="csvFilePath"></param>
        public void LoadDataFromCSV(string csvFilePath)
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
                            Data[row, col] = csv.GetField<double>(col);
                        }
                    }
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Input Matrix");
                printArray(Data);
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
                Console.WriteLine("Matrix after normalize:");
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
                Console.WriteLine("Matrix Weight Set:");
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
            Console.WriteLine("Matrix Weight Vector:");
            printArray(WeightVector);
            // Vector nhất quán
            double[] CR_Vector = new double[length];
            for(int i = 0; i < length; i++)
            {
                CR_Vector[i] = Math.Round(WeightVector[i]/WeightSet[i], 2);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Matrix CR Vector");
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
            Console.WriteLine($@"Weight Set is {(isWeightSetValid ? "acceptable" : "unacceptable")}");
        }
    }
}