namespace Algorithm.Model.Schema
{
    public class UserBased : CommonModel
    {
        /// <summary>
        /// Mãng dữ liệu đầu vào
        /// </summary>
        /// <value></value>
        public double[,] RawData {get; set;}
        /// <summary>
        /// SinCosin của ma trận
        /// </summary>
        /// <value></value>
        public double[,] SimCosin {get; set;}
        /// <summary>
        /// SinPerson của ma trận
        /// </summary>
        /// <value></value>
        public double[,] SimPearson {get; set;}
        public UserBased(string csvFilepath)
        {
            RawData = ReadDataFromFile(csvFilepath);
            Console.WriteLine("RawData");
            PrintArray(RawData);
            int n = RawData.GetLength(0);
            SimCosin = new double[n, n];
            SimPearson = new double[n, n];
            for(int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if(i == j)
                    {
                        SimCosin[i, j] = 1;
                        SimPearson[i, j] = 1;
                    }
                    SimCosin[i, j] = SimCosinCalculation(i, j);
                    SimPearson[i, j] = SimPearsonCalculator(i, j);
                }
            }
            MakeSymmetric(SimCosin);
            MakeSymmetric(SimPearson);
            // Console.WriteLine("SimCosin");
            PrintArray(SimCosin);
            // Console.WriteLine("SimPearson");
            PrintArray(SimPearson);
        }
        /// <summary>
        /// Hàm tính mảng SimCosin
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        protected double SimCosinCalculation(int n, int m)
        {
            double[] vectorA = GetRow(RawData, n);
            double[] vectorB = GetRow(RawData, m);
            // Tích vô hướng 2 vector
            double dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
            // Độ dài 2 vector
            double magnitudeA = Math.Sqrt(vectorA.Select(a => a*a).Sum());
            double magnitudeB = Math.Sqrt(vectorB.Select(a => a*a).Sum());
            // Tránh trường hợp chia cho 0
            if(magnitudeA == 0 || magnitudeB == 0)
            {
                return 0.0;
            }
            return Math.Round(dotProduct/(magnitudeA * magnitudeB), 2);
        }
        /// <summary>
        /// Hàm tính mảng SimPearson
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        protected double SimPearsonCalculator(int n, int m)
        {
            double[] vectorA = GetRow(RawData, n);
            double[] vectorB = GetRow(RawData, m);
            double sumA = vectorA.Sum();
            double sumB = vectorB.Sum();
            double sumSquareA = vectorA.Select(a => a * a).Sum();
            double sumSquareB = vectorB.Select(b => b * b).Sum();
            double sumProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
            int lengthA = vectorA.Length;
            double numerator = lengthA * sumProduct - sumA * sumB;
            double denominatorA = Math.Sqrt(lengthA * sumSquareA - sumA * sumA);
            double denominatorB = Math.Sqrt(lengthA * sumSquareB - sumB * sumB);
            if (denominatorA == 0 || denominatorB == 0)
            {
                return 0.0; // Handle the case of division by zero
            }
            return Math.Round(numerator/(denominatorA * denominatorB), 2);
        }
        public double PredictedRating(int userIndex, int itemIndex)
        {
            int numUsers = RawData.GetLength(0);
            if (userIndex < 0 || userIndex >= numUsers || itemIndex < 0 || itemIndex >= RawData.GetLength(1))
            {
                // Console.WriteLine("Chỉ số người dùng hoặc mục tiêu đánh giá không hợp lệ.");
                return 0.0;
            }
            if (RawData[userIndex, itemIndex] != 0)
            {
                // Console.WriteLine($"Người dùng đã đánh giá từ trước: {RawData[userIndex, itemIndex]}");
                return RawData[userIndex, itemIndex];
            }
            else
            {
                double numerator = 0.0;
                double denominator = 0.0;
                for(int otherUserIndex = 0; otherUserIndex < numUsers; otherUserIndex++)
                {
                    if(userIndex != otherUserIndex && RawData[otherUserIndex, itemIndex] != 0)
                    {
                        numerator += RawData[otherUserIndex, itemIndex] * SimCosin[userIndex, otherUserIndex];
                        denominator += Math.Abs(SimCosin[userIndex, otherUserIndex]);
                    }
                }
                if(denominator > 0)
                {
                    // Console.WriteLine($"Người {userIndex} có thể sẽ dự đoán item {itemIndex} có giá trị là: {Math.Round(numerator/denominator)}");
                    return Math.Round(numerator/denominator);
                }
                else
                {
                    // Nếu không có người dùng tương tự nào hoặc mẫu số bằng 0, trả về giá trị mặc định.
                    // Console.WriteLine($"Người {userIndex} có thể sẽ dự đoán item {itemIndex} có giá trị là: 0.0");
                    return 0.0;
                }
            }
        }
    }
}