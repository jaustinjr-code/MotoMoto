using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IPartFlaggingService
    {
         /// <summary>
        /// Calls the data access layer's function to create or increment the given flag.
        /// </summary>
        ///
        /// <param name="flag">The flag to be created or incremented in the partflagging database</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the new flag</returns>
        public bool CallFlagCreation(FlagModel flag);

        /// <summary>
        /// Calls the data access layer's function to retrieve the flag count for a given flag.
        /// </summary>
        ///
        /// <param name="flag">The flag to have it's count retrieved from the part flagging database.</param>
        ///
        /// <returns>Integer value of the count of the argument part flag.</returns>
        public int CallGetFlagCount(FlagModel flag);

        /// <summary>
        /// Calls the data access layer's function to decrement or remove the given flag.
        /// </summary>
        ///
        /// <param name="flag">The flag to be decremented or removed in the partflagging database</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the decremented flag</returns>
        public bool CallDecrementFlagCount(FlagModel flag);

    }
}
