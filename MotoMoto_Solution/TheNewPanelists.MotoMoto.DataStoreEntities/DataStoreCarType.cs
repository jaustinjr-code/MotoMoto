using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreCarType
    {
        public int carID { get; set; }          //Auto-incremented ID for each car type
        public string? make { get; set; }       //Car make
        public string? model { get; set; }      //Car model
        public DateTime? year { get; set; }     //Car year
        public string? country { get; set; }    //Car country of origin
    }
}
