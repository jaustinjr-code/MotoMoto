using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto
{
    public class ResponseModel
    {
        public enum response
        {
            success,
            managerInvalidInteger,
            managerInvalidString,
            managerInvalidBoolean,
            managerInvalidObject,
            managerObjectFailOnRetrieval,
            serviceObjectCreationFailure,
            serviceObjectFailOnRetrievalFromDataAccess,
            dataAccessFailedObjectOutOfRange,
            dataAccessFailedObjectNonExistent,
        }
    }
}
