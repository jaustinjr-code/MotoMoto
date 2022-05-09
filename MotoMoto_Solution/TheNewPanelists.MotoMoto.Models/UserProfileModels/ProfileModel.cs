using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class ProfileModel
    {
        public int? userId { get; set; }
        public string? username { get; set; }
        public bool status { get; set; }
        public bool eventAccount { get; set; }
        public string? profileImagePath { get; set; }
        public string? profileDescription { get; set; }
        public string? newProfileUsername { get; set; }
        public IEnumerable<UpvotedPostsModel>? upVotedPosts { get; set; }
        public IEnumerable<UserPostModel>? userPosts { get; set; }

        public string? systemResponse { get; set; }
        
        public ProfileModel GetResponse(ResponseModel.response _responseAction)
        {
            if (systemResponse != null)
            {
                return this;
            }
            if (systemResponse == "success" || systemResponse == null)
            {
                systemResponse = _responseAction.ToString();
                return this;
            }
            return this;
        }
    }
}