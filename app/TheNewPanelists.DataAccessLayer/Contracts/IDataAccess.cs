namespace app.TheNewPanelists.DataAccessLayer.Logging
{
    interface IDataAccess
    {
        private string operation;
        // public bool LogAccess(string[] log);
        private static bool EstablishMariaDBConnection();
        private abstract string SqlGenerator();
        // NOTE: Jacob gets an error for access modifiers
        public bool LogRequestAsync();
    }
}