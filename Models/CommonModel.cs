using System;

namespace Algorithm.Model
{
    public partial class CommonModel
    {
        /// <summary>
        /// Hàm in lỗi và nơi bắt được lỗi (class + method)
        /// </summary>
        /// <param name="ex"></param>
        protected static void printError(Exception ex)
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
        public void printArray<TData>(TData[] array)
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
        public void printArray<TData>(TData[,] array)
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
    }
}