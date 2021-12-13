namespace TheNewPanelists.DataAccessLayer.Logging
{
    interface IDataAccess
    {
        bool EstablishMariaDBConnection();
        string SqlGenerator();
    }
}