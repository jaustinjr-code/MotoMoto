using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    // IEventDetailsEntity implements IPostEntity and stores details regarding events
    public interface IEventDetailsEntity : IPostEntity
    {
        int? eventID { get; }
        string? eventLocation { get; }
        string? eventTime { get; }
        string? eventDate { get; }
        string? registeredUsers { get; }
    }
}
