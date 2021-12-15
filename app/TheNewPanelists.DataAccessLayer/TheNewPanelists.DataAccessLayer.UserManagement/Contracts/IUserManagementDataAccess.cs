namespace TheNewPanelists.DataAccessLayer
{
    interface IDataAccess
    {
        bool EstablishMariaDBConnection();
        string SqlGenerator();
    }
}
