using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreCarBuilds
    {
        public int? CarBuildID { get; set; }    //Auto-incremented ID for each car build
        public int? CarID { get; set; }         //Auto-incremented ID pulled from CarTypes table
        public string? Username { get; set; }   //The user associated with the car build
    }
}
