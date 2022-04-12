using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreCarBuilds
    {
        public int? carBuildID { get; set; }    //Auto-incremented ID for each car build
        public int? carID { get; set; }         //Auto-incremented ID pulled from CarTypes table
        public string? username { get; set; }   //The user associated with the car build
    }
}
