namespace app.TheNewPanelists.DataAccessLayer.Logging
{
    interface IDataAccess
    {
        private string operation;
        public bool LogAccess(string[] log);
        private bool EstablishMariaDBConnection();
        // private InsertLog(string logSql);
        private string sqlGenerator();
    }
}