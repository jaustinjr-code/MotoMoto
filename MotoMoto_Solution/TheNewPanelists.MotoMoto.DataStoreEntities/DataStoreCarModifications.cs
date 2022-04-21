using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreCarModifications
    {
        public int carModificationID { get; set; }  //Auto-incremented ID for each car build
        public int carBuildID { get; set; }         //Auto-incremented ID pulled from CarBuilds table
        public int partID { get; set; }             //Auto-incremented ID pulled from OEMAndAfterMarketParts table

    }
}
