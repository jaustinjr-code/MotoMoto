using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    internal class DataStoreOEMAndAfterMarketParts
    {
        public int partID { get; set; }         //Auto-incremented ID for each part in the table
        public string? partName { get; set; }   //Name of the part
        public string? type { get; set; }       //Whether the part is OEM or AfterMarket
    }
}
