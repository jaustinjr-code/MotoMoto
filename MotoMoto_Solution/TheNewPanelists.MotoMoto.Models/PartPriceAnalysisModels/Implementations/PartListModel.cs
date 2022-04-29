using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class PartListModel
    {
        public int categoryId { get; set; }
        public IEnumerable<IPartEntity>? partList { get; set; }
        public readonly string[] categories = new string[] { "Alternator", "Brake Pads", "Brake Rotor", "Cylinder Head", "Engine Block",
                                       "Exhaust Manifold", "Muffler", "Oil Filter", "Radiator", "Spark Plug", 
                                       "Timing Belt", "Timing Chain", "Turbo", "Water Pump" };
        public string? categorySelect { get; set; }
            
        public bool returnValueNoRealCategory = true;

        public PartListModel InvalidRetrunValueForNoTrueCategory()
        { 
            returnValueNoRealCategory = false;
            return this;
        }
    }
}
