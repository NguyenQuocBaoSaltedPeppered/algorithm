using System;
using Algorithm.Model.Schema;

namespace Algorithm.Model.Schema.FuzzyAHP
{
    public class FuzzyAHP
    {
        /// <summary>
        /// Hàm dựng cho class FuzzyAHP
        /// </summary>
        /// <param name="n">Số lượng các đánh giá</param>
        /// <param name="criteria">Số tiêu chí đánh giá</param>
        public FuzzyAHP(int n, int criteria)
        {
            p = n;
            RawData = new AHP[n];
            for(int i = 0; i < n; i++)
            {
                RawData[i] = new AHP(criteria);
            }
            FuzzyArray = new FuzzyElement[criteria, criteria];
        }
        /// <summary>
        /// Số lượng đánh giá
        /// </summary>
        /// <value></value>
        public int p { get; set; }
        /// <summary>
        /// Các ma trận đánh giá tương ứng
        /// </summary>
        public AHP[] RawData;
        /// <summary>
        /// Ma trận mờ
        /// </summary>
        public FuzzyElement[,] FuzzyArray;
        /// <summary>
        /// Hàm tính toán các FuzzyElem cho FuzzyArray
        /// </summary>
        public void setupFuzzyArray()
        {
        }
    }
    /// <summary>
    /// Phần tử mờ trong ma trận mờ
    /// </summary>
    public class FuzzyElement
    {
        public FuzzyElement(int min = 0, int max = 0, int average = 0)
        {
            MinElem = min;
            MaxElem = max;
            AverageElem = average;
        }
        /// <summary>
        /// Giá trị min
        /// </summary>
        /// <value></value>
        public int MinElem {get; set;}
        /// <summary>
        /// Giá trị max
        /// </summary>
        /// <value></value>
        public int MaxElem {get; set;}
        /// <summary>
        /// Giá trị trung bình
        /// </summary>
        /// <value></value>
        public int AverageElem {get; set;}
    }
}