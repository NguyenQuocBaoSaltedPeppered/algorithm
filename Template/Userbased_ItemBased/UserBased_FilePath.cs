namespace Algorithm.Template.UserBased
{
    public class UserBased_FilePath
    {
        private static UserBased_FilePath _instance = null!;
        #region windowsPath
        public string DATA_WINDOWS_FILEPATH { get; private set; }
        #endregion
        #region macPath
        public string DATA_MAC_FILEPATH { get; private set; }
        #endregion
        private UserBased_FilePath()
        {
            DATA_WINDOWS_FILEPATH = @".\Template\UserBased_ItemBased\UserBased.csv";
            DATA_MAC_FILEPATH = @"./Template/UserBased_ItemBased/UserBased.csv";
        }
        public static UserBased_FilePath Instance
        {
            get
            {
                _instance ??= new UserBased_FilePath();
                return _instance;
            }
        }
    }
}