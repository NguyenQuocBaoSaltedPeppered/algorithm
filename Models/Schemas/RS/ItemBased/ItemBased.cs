namespace Algorithm.Model.Schema
{
    public class ItemBased : UserBased
    {
        public ItemBased(string csvFilepath) : base(csvFilepath)
        {
            double[,] dataReader = ReadDataFromFile(csvFilepath);
            RawData = Transpose(dataReader);
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
        }
        // public PredictedRating()
    }
}