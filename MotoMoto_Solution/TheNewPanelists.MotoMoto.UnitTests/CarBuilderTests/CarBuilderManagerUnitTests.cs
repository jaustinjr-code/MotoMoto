using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.UnitTests.CarBuilderTests
{
    public class CarBuilderManagerUnitTests
    {

        // Below are for CarTypeModel

        [Fact]
        public void CreateCarWithCompleteData()
        {
            string make = "Toyota";
            string model = "GR86";
            string year = "2022";

            CarBuildManager manager = new CarBuildManager();

            CarTypeModel newCar = manager.CreateCarTypeModel(make, model, year);

            bool result = newCar.Make == "toyota" &&
                          newCar.Model == "gr86" &&
                          newCar.Year == "2022";

            Assert.True(result);
        }

        [Fact]
        public void CreateCarWithIncompleteData()
        {
            string make = "";
            string model = "";
            string year = "";

            CarBuildManager manager = new CarBuildManager();

            CarTypeModel newCar = manager.CreateCarTypeModel(make, model, year);

            bool result = newCar.Make is null &&
                          newCar.Model is null &&
                          newCar.Year is null;

            Assert.True(result);
        }

        [Fact]
        public void CreateCarWithExtraWhitespace()
        {
            string make = "         Toyota      ";
            string model = "     GR86     ";
            string year = "     2022     ";

            CarBuildManager manager = new CarBuildManager();

            CarTypeModel newCar = manager.CreateCarTypeModel(make, model, year);

            bool result = newCar.Make == "toyota" &&
                          newCar.Model == "gr86" &&
                          newCar.Year == "2022";

            Assert.True(result);
        }

        [Fact]
        public void CreateCarWithOnlyWhitespace()
        {
            string make = "          ";
            string model = "          ";
            string year = "          ";

            CarBuildManager manager = new CarBuildManager();

            CarTypeModel newCar = manager.CreateCarTypeModel(make, model, year);

            bool result = newCar.Make is null &&
                          newCar.Model is null &&
                          newCar.Year is null;

            Assert.True(result);
        }

        // Below are for ModifyCarBuildModel

        [Fact]
        public void ModifyCarWithCompleteData()
        {
            string partNumber = "D1195";
            string type = "Aftermarket";

            CarBuildManager manager = new CarBuildManager();

            ModifyCarBuildModel newCar = manager.CreateModifyCarBuildModel(partNumber, type);

            bool result = newCar.PartNumber == "D1195" &&
                          newCar.Type == "Aftermarket";

            Assert.True(result);
        }

        [Fact]
        public void ModifyCarWithIncompleteData()
        {
            string partNumber = "";
            string type = "";

            CarBuildManager manager = new CarBuildManager();

            ModifyCarBuildModel newCar = manager.CreateModifyCarBuildModel(partNumber, type);

            bool result = newCar.PartNumber is null &&
                          newCar.Type is null;

            Assert.True(result);
        }

        [Fact]
        public void ModifyCarWithExtraWhitespace()
        {
            string partNumber = "     D1195     ";
            string type = "     Aftermarket     ";

            CarBuildManager manager = new CarBuildManager();

            ModifyCarBuildModel newCar = manager.CreateModifyCarBuildModel(partNumber, type);

            bool result = newCar.PartNumber == "D1195" &&
                          newCar.Type == "Aftermarket";

            Assert.True(result);
        }

        [Fact]
        public void ModifyCarWithOnlyWhitespace()
        {
            string partNumber = "          ";
            string type = "          ";

            CarBuildManager manager = new CarBuildManager();

            ModifyCarBuildModel newCar = manager.CreateModifyCarBuildModel(partNumber, type);

            bool result = newCar.PartNumber is null &&
                          newCar.Type is null;

            Assert.True(result);
        }
    }
}
