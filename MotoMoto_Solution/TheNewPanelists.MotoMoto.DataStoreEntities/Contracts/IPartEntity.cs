﻿using System;
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
                default:
                    break;
            }
        }
    }
}
