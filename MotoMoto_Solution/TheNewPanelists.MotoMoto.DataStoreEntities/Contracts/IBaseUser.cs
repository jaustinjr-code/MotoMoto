using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public abstract class IBaseUser
    {
        public virtual int userId { get; set; }
    }
}
