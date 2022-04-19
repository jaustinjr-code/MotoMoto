using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto
{
    public class CarTypeModel
    {
        public string? make { get; set; }       //Car make
        public string? model { get; set; }      //Car model
        public DateOnly? year { get; set; }     //Car year
        public string? country { get; set; }    //Car country of origin
    }
}
