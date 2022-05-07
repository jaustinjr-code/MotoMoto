using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder;
using TheNewPanelists.MotoMoto.Models.CarbuilderModels;

namespace TheNewPanelists.MotoMoto.UnitTests.CarBuilderTests
{
    public class CarBuilderManagerUnitTests
    {

        [Fact]
        public void TestSaveCarType()
        {
            CarBuildManager _buildManager = new CarBuildManager();
            CarTypeModel carTypeModel = new CarTypeModel();

            carTypeModel.make = "Toyota";
            carTypeModel.model = "Corolla";
            carTypeModel.year = "2000";

            bool result = _buildManager.TestSaveCarTypeManager(carTypeModel);
            Assert.True(result);
        }

        //[Fact]
        //public void TestSaveCarModifications()
        //{
        //    CarBuildManager _buildManager = new CarBuildManager();
        //    ModifyCarBuildModel carModel = new ModifyCarBuildModel();

        //    carModel.partNumber = "MDKR3948";
        //    carModel.type = "OEM";

        //    bool result = _buildManager.TestSaveCarModificationsManager(carModel);
        //    Assert.True(result);
        //}

        [Fact]
        public void TestUpdateCar()
        {
            CarBuildManager _buildManager = new CarBuildManager();
            UpdateCarModel carModel = new UpdateCarModel();

            carModel.carID = "1";
            carModel.partID = "1";
            carModel.username = "user1";
               
            bool result = _buildManager.TestUpdateCarManager(carModel);
            Assert.True(result);
        }
    }
}
