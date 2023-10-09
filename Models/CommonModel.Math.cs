namespace Algorithm.Model
{
    public partial class CommonModel
    {
        public static TData[] MultiplyArray<TData>(TData[,] Matrix_2D, TData[] Vector) where TData : notnull
        {
            int rows = Matrix_2D.GetLength(0);
            int cols = Matrix_2D.GetLength(1);
            if(cols != Vector.Length)
            {
                printError(new ArgumentException("The number of columns in the matrix must be equal to the length of the vector."));
            }
            TData[] result = new TData[rows];
            for(int i = 0; i < rows; i++)
            {
                TData sum = default(TData);
                for(int j = 0; j < cols; j++)
                {
                    sum = Add(sum, Multiply(Matrix_2D[i, j], Vector[j]));
                }
                result[i] = sum;
            }
            return result;
        }
        protected static TData Multiply<TData>(TData a, TData b)
        {
            dynamic da = a;
            dynamic db = b;
            return da * db;
        }

        protected static TData Add<TData>(TData a, TData b)
        {
            dynamic da = a;
            dynamic db = b;
            return da + db;
        }
    }
}