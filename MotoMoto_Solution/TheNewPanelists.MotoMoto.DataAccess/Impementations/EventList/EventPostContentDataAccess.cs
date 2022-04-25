using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    internal class EventPostContentDataAccess : IContentDataAccess
    {
        MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "server=localhost;user=dev_moto;database=dev_EventList;port=3306;password=motomoto;";//write config so this only appears once

        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public IEnumerable<IPostEntity>? FetchAllPosts(IFeedModel feedInput)
        {
            throw new NotImplementedException();
        }

        public IFeedEntity? GetPost(IFeedModel postInput)
        {
            throw new NotImplementedException();
        }

        public string SqlGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
