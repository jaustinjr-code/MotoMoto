using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public interface IPartPriceHistory
    {
        int productId { get; set; }
        DateTime? dateTime { get; set; }
        double productPrice { get; set; }
    }
}
