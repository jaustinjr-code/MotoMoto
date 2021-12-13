namespace app.TheNewPanelists.DataAccessLayer
{
    interface IDataAccess
    {
        private string operation;
        private static bool EstablishMariaDBConnection();
        private abstract string SqlGenerator();
        // NOTE: Jacob gets an error for access modifiers
    }
}