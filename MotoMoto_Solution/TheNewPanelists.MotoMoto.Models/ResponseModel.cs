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
            invalidStringParameter,
            invalidIntegerParameter,
            invalidBooleanParameter,
            invalidObjectParameter,
            managerInvalidInteger,
            managerInvalidString,
            managerInvalidBoolean,
            managerInvalidObject,
            managerInvalidProfileDescriptionRetrieval,
            managerInvalidProfileUsernameRetrieval,
            managerObjectFailOnRetrieval,
            serviceObjectCreationFailure,
            serviceObjectFailOnRetrievalFromDataAccess,
            dataAccessFailedObjectOutOfRange,
            dataAccessFailedObjectNonExistent,
            nullObjectReferenceAchieved,
        }
    }
}
