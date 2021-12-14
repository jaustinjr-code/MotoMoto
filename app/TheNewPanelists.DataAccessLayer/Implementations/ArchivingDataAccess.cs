using MySql.Data.MySqlClient;

namespace TheNewPanelists.DataAccessLayer
{
    class ArchivingDataAccess : IDataAccess
    {
        private string operation;
        public ArchivingDataAccess()
        {

        }

        bool EstablishMariaDBConnection()
        {
            return false;
        }

        string SqlGenerator()
        {
            return "";
        }
    }
}