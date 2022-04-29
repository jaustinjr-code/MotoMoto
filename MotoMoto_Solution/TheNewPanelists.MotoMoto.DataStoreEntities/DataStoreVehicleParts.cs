using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreVehicleParts : IPartEntity
    {
        public int partID { get; set; }
        public string? partName { get; set; }
        public string? rating { get; set; }
        public int ratingCount { get; set; }
        public string? productURL { get; set; }
        public double currentPrice { get; set; }

        public DataStoreVehicleParts() { }
        public DataStoreVehicleParts(int _partID, string _partName, string _rating, int _ratingCount, string _proURL, double _currentPrice)
        {
            partID = _partID;
            partName = _partName;
            rating = _rating;
            ratingCount = _ratingCount;
            productURL = _proURL;
            currentPrice = _currentPrice;
        }
    }
}
