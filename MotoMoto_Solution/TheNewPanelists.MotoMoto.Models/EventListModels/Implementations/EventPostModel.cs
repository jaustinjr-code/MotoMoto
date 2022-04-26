using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class EventPostModel : IPostModel
    {
        public int postID { get; }
        public string? postTitle { get; set; }
        public string? contentType { get; set; }
        public string? postUser { get; set; }
        public string? postDescription { get; set; }
        public string? postLocation { get; set; }
        public string? postDate { get; set; }
        public IEnumerable<string>? registeredUsers { get; set; }
        public DateTime submitUTC { get; set; }
        public IEnumerable<byte[]>? imageList { get; set; }

        public EventPostModel(int id)
        {
            postID = id;
        }

        public EventPostModel(int id, string title, string type, string username, string description, string location, string date, string[] users, DateTime utc)
        {
            postID = id;
            postTitle = title;
            contentType = type;
            postUser = username;
            postDescription = description;
            postLocation = location;
            postDate = date;
            registeredUsers = users;
            submitUTC = utc;
        }
    }
}
