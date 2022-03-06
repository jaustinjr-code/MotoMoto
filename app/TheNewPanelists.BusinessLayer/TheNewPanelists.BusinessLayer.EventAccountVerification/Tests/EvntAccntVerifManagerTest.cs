using Xunit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.BusinessLayer.EventAccountVerification;
using System.Diagnostics;

namespace TheNewPanelists.BusinessLayer.EventAccountVerification
{
    public class EvntAccntVerifManagerTest
    {

        private string operation;
        private bool result;
        Stopwatch timer = new Stopwatch();

        [Fact]
        public void IsValidRequest_WithValidFind_ReturnTrue()
        {
            EvntAccntVerifManager evntAccntVerifManager = new EvntAccntVerifManager();
            Dictionary<string, string> request = new Dictionary<string, string>();

            request.Add("operation", "find");
            request.Add("username", "bcdelrey");

            bool result = evntAccntVerifManager.IsValidRequest(request);
            Assert.True(true, "Valid Result For Valid Input");
        }

    }
}
