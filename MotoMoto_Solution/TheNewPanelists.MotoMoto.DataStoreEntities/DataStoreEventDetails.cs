using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreEventDetails : IEventDetailsEntity
    {
        public string type { get; } = "event";
        // Columns that come from the EventDetails Model
        public int? eventID { get; }
        public string? eventLocation { get; }
        public string? eventTime { get; }
        public string? eventDate { get; }
        public string? registeredUsers { get; }

        // Columns that come from the PostEntity Model
        public int postId { get; }
        public string postTitle { get => throw new NotImplementedException(); } // Did not include in SQL table for EventDetails
        public DataStoreUserProfile postUser { get => throw new NotImplementedException(); } // Did not include in SQL table for EventDetails
        public string postUsername { get => throw new NotImplementedException(); }
        public string? postDescription { get; }

        // Did not include in SQL table for EventDetails
        public IEnumerable<byte[]>? imageList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
