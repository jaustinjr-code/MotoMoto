﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface IDataAccess
    {
        public bool EstablishMariaDBConnection();
    }
}
