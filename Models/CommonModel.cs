namespace Algorithm.Model
{
    public partial class CommonModel
    {
        /// <summary>
        /// Hàm in lỗi và nơi bắt được lỗi (class + method)
        /// </summary>
        /// <param name="ex"></param>
        protected static void PrintError(Exception ex)
        {
            if(ex.TargetSite == null)
            {
                Console.Write($"EXCEPTION IN UNKNOWN METHOD, ERROR: {ex.Message}");
            }
            else
            {
                Console.Write($"EXCEPTION IN CLASS: [{ex.TargetSite.DeclaringType}], METHOD: [{ex.TargetSite.Name}], ERROR: {ex.Message}");
            }
        }
        /// <summary>
        /// Hàm in mảng 1 chiều áp dụng cho các kiểu dữ liệu cơ bản
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="TData"></typeparam>
        public void PrintArray<TData>(TData[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + "\t");
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Hàm in mảng 2 chiều áp dụng cho các kiểu dữ liệu cơ bản
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="TData"></typeparam>
        public void PrintArray<TData>(TData[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Hàm xây dựng ma trận chuyển vị áp dụng cho các kiểu dữ liệu cơ bản
        /// </summary>
        /// <param name="inputMatrix"></param>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public static TData[,] Transpose<TData>(TData[,] inputMatrix)
        {
            int rows = inputMatrix.GetLength(0);
            int cols = inputMatrix.GetLength(1);
            TData[,] result = new TData[cols, rows];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    result[j, i] = inputMatrix[i, j];
                }
            }
            return result;
        }
        /// <summary>
        /// Hàm lấy dữ liệu 1 hàng trong mảng 2 chiều
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="rowNumber"></param>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public static TData[] GetRow<TData>(TData[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }
        /// <summary>
        /// Hàm lấy dữ liệu 1 cột trong mảng 2 chiều
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="columnNumber"></param>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public static TData[] GetCollumn<TData>(TData[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }
        /// <summary>
        /// Hàm nhận vào 1 ma trận vuông, đọc phần tam giác trên,
        /// chuyển nó thành 1 ma trận đối xứng
        /// </summary>
        /// <param name="matrix"></param>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public static void MakeSymmetric<TData>(TData[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    // Lấy giá trị từ tam giác trên và gán cho tam giác dưới
                    matrix[j, i] = matrix[i, j];
                }
            }
        }
    }
}