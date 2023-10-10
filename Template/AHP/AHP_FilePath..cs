namespace Algorithm.Template.AHP
{
    public class AHPFilePath
    {
        private static AHPFilePath _instance;
        #region windowsPath
        public string DATA_WINDOWS_FILEPATH { get; private set; }
        public string CHOICE_WINDOWS_FILEPATH { get; private set; }
        #endregion
        #region macPath
        public string DATA_MAC_FILEPATH { get; private set; }
        public string CHOICE_MAC_FILEPATH { get; private set; }
        #endregion
        private AHPFilePath()
        {
            DATA_WINDOWS_FILEPATH = @".\Template\AHP\AHP_data.csv";
            CHOICE_WINDOWS_FILEPATH = @".\Template\AHP\AHP_choice.csv";
            DATA_MAC_FILEPATH = @"./Template/AHP/AHP_data.csv";
            CHOICE_MAC_FILEPATH = @"./Template/AHP/AHP_choice.csv";
        }
        public static AHPFilePath instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AHPFilePath();
                }
                return _instance;
            }
        }
    }
}