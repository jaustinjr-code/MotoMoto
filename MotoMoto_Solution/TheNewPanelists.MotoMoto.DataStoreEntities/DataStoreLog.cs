﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreLog 
    {
        public int? LogId { get; set; }
        public string? LevelName { get; set; }
        public string? CategoryName { get; set; }
        public string? _dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        public string? UserId { get; set; }
        public string? Description { get; set; }
    }
}
