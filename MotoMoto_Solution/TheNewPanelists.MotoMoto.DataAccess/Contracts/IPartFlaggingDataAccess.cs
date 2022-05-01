using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface IPartFlaggingDataAccess
    {
        /// <summary>
        /// Updates the PartFlag table to reflect the incoming Flag.
        /// If the flag exists, increment the flag count, else create a new row for the flag.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to construct or update a row in the PartFlag database table</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the new flag</returns>
        public Task<bool> CreateOrIncrementFlag(FlagModel flag);


        /// <summary>
        /// Updates the part flag table to reflect the decrementing of the flag.
        /// If the flag count is greater than 1, decrement the flag count, if the flag count is 1, remove the flag,
        /// if the flag count is 0 do nothing.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to find the flag to decrement</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the flag decrement</returns>
        public  Task<bool> DecrementOrRemove(FlagModel flag);


        /// <summary>
        /// Removes all counts of a flag from the part flagging database if it exists
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to find the flag to decrement</param>
        ///
        /// <returns>True if the flag was found and removed, false otherwise</returns>
        public Task<bool> DeleteFlag(FlagModel flag);

        /// <summary>
        /// Retrieves the count of number of the number of times a particular flag has been cited.
        /// If the flag does not exist in the database, the number of times cited is zero.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to query a row from the database table PartFlag</param>
        ///
        /// <returns>Count of times a particular flag has been cited.</returns>
        public Task<int> GetFlagCount(FlagModel flag);
    }
}
