using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class CarBuildManager
    {
        private readonly CarBuildService _carBuildService;

        public CarBuildManager(CarBuildService carBuildService)
        {
            _carBuildService = carBuildService;
        }

        public bool SaveCarBuildManager(CarTypeModel savedCarBuild)
        {
            return true;
        }

        public bool ModifyCarBuildManager(ModifyCarBuildModel modifiedCarBuild)
        {
            return true;
        }
    }
}
