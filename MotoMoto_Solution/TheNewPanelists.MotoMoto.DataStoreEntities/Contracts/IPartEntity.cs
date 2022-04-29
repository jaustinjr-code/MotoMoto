using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public interface IPartEntity
    {
        int partID { get; set; }
        string? partName { get; set; }
        string? rating { get; set; }
        int ratingCount { get; set; }
        string? productURL { get; set; }
        double currentPrice { get; set; }

        public void ShrinkPartName()
        {
            switch(partName!.Length)
            {
                case > 50:
                    partName = partName!.Substring(0, 50);
                    break;
                case > 40:
                    partName = partName!.Substring(0, 40);
                    break;
                case > 30:
                    partName = partName!.Substring(0, 30);
                    break;
                case > 20:
                    partName = partName!.Substring(0, 20);
                    break;
                case > 10:
                    partName = partName!.Substring(0, 10);
                    break;
                default:
                    break;
            }
        }
    }
}
