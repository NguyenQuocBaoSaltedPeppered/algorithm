namespace Algorithm.Model.Schema
{
    public class UserBased
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
        public double SimCosin {get; set;}
        /// <summary>
        /// SinPerson của ma trận
        /// </summary>
        /// <value></value>
        public double SimPerson {get; set;}
    }
}