namespace Algorithm.Model.Schema
{
    public partial class AHP
    {
        Dictionary<int, double> RI_Dictionary = new Dictionary<int, double>
        {
            {1, 0},
            {2, 0},
            {3, 0.52},
            {4, 0.89},
            {5, 1.11},
            {6, 1.25},
            {7, 1.35},
            {8, 1.40},
            {9, 1.45},
            {10, 1.49},
            {11, 1.52},
            {12, 1.54},
            {13, 1.56},
            {14, 1.58},
            {15, 1.59},
        };
        /// <summary>
        /// Giới hạn cho CR
        /// </summary>
        private const double ValidBound = 0.1;
    }
}