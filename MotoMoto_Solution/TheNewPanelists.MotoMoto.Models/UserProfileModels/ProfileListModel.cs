using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.Models
{
    public class ProfileListModel
    {
        public IEnumerable<ProfileModel>? profiles { get; set; }

        public string systemResponse = string.Empty;
        public ProfileListModel GetResponse(ResponseModel.response _responseAction)
        {
            systemResponse = _responseAction.ToString();
            return this;
        }
    }
}
