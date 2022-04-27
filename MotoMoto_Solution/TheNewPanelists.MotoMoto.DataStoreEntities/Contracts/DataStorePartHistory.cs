using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStorePartHistory : IPartPriceHistory
    {
        public int productId { get; set; }
        public DateTime? dateTime { get; set; }
        public double productPrice { get; set; }

        public DataStorePartHistory(int _productId, DateTime _dateTime, double _productPrice) 
        { 
            productId = _productId;
            dateTime = _dateTime;
            productPrice = _productPrice;
        }
    }
}
