using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto
{
    public class ModifyCarBuildModel
    {
        public string? partNumber { get; set; }   //Number of the part
        public string? type { get; set; }   //Whether the part is OEM or Aftermarket

        public ModifyCarBuildModel()
        {
            partNumber = null;
            type = null;
        }

        public ModifyCarBuildModel(string partNumber, string type)
        {
            PartNumber = partNumber;
            Type = type;
        }

        public string? PartNumber
        {
            get
            {
                if (PartNumber is not null)
                {
                    if (PartNumber.Trim().Length == 0)
                    {
                        return null;
                    }
                    return PartNumber.ToLower().Trim();
                }
                return PartNumber;
            }
            set
            {
                PartNumber = value;
            }
        }

        public string? Type
        {
            get
            {
                if (Type is not null)
                {
                    if (Type.Trim().Length == 0)
                    {
                        return null;
                    }
                    return Type.ToLower().Trim();
                }
                return Type;
            }
            set
            {
                Type = value;
            }
        }
    }
}
