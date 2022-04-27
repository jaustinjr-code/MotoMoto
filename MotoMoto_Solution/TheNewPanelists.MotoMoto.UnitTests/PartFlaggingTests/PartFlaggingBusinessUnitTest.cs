using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartFlaggingBusinessUnitTest
    {
        [Fact]
        public void CreateFlagWithCompleteData()
        {
            string partNumber = "1";
            string carMake = "Toyota";
            string carModel = "Corolla";
            string carYear = "1999";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);

            bool result = newFlag.PartNumber == "1" &&
                          newFlag.CarMake == "toyota" &&
                          newFlag.CarModel == "corolla" &&
                          newFlag.CarYear == "1999";

            Assert.True(result);
        }

        [Fact]
        public void CreateFlagWithIncompleteData()
        {
            string partNumber = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);

            bool result = newFlag.PartNumber is null &&
                          newFlag.CarMake is null &&
                          newFlag.CarModel is null &&
                          newFlag.CarYear is null;

            Assert.True(result);
        }

        [Fact]
        public void CreateFlagWithExtraWhitespace()
        {
            string partNumber = "      1        ";
            string carMake = "         Toyota      ";
            string carModel = "     Corolla     ";
            string carYear = "     1999     ";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);

            bool result = newFlag.PartNumber == "1" &&
                          newFlag.CarMake == "toyota" &&
                          newFlag.CarModel == "corolla" &&
                          newFlag.CarYear == "1999";

            Assert.True(result);
        }

        [Fact]
        public void CreateFlagWithOnlyWhitespace()
        {
            string partNumber = "        ";
            string carMake = "          ";
            string carModel = "          ";
            string carYear = "          ";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);

            bool result = newFlag.PartNumber is null &&
                          newFlag.CarMake is null &&
                          newFlag.CarModel is null &&
                          newFlag.CarYear is null;

            Assert.True(result);
        }

        [Fact]
        public void ValidateFlagWithValidData()
        {
            string partNumber = "1";
            string carMake = "Toyota";
            string carModel = "Corolla";
            string carYear = "1999";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);
            bool result = partFlaggingBusinessLayer.isValidFlag(newFlag);
            Assert.True(result);
        }

        [Fact]
        public void ValidateFlagWithNullData()
        {
            string partNumber = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);

            bool result = partFlaggingBusinessLayer.isValidFlag(newFlag);
            Assert.False(result);
        }

        public void ValidateFlagWithInvalidDate()
        {
            string partNumber = "1";
            string carMake = "Toyota";
            string carModel = "Corolla";
            string carYear = "not a date";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            
            FlagModel newFlag = partFlaggingBusinessLayer.createFlagModel(partNumber, carMake, carModel, carYear);
            bool result = partFlaggingBusinessLayer.isValidFlag(newFlag);
            Assert.False(result);
        }

        [Fact]
        public void HandleValidFlagCreation()
        {
            string partNumberParameter = "1";
            string carMakeParameter = "Nissan";
            string carModelParameter = "Altima";
            string carYearParameter = "2005";

            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            Assert.True(partFlaggingBusinessLayer.handleFlagCreation(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter)); 
        }

        public void HandleInvalidFlagCreation()
        {
            string partNumberParameter = "";
            string carMakeParameter = "";
            string carModelParameter = "";
            string carYearParameter = "";
            
            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            Assert.True(partFlaggingBusinessLayer.handleFlagCreation(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter)); 
        }

        [Theory]
        [InlineData("1", "Nissan", "Altima", "2005")]
        [InlineData("1", "TestMake", "TestModel", "1955")]
        public void HandleValidFlagCountRetrieval(string partNumberParameter, string carMakeParameter, string carModelParameter, string carYearParameter)
        {
            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            Assert.NotNull(partFlaggingBusinessLayer.HandleGetFlagCompatibility(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter));
        }

        [Fact]
        public void HandleInvalidFlagCountRetrieval()
        {
            string partNumberParameter = "";
            string carMakeParameter = "";
            string carModelParameter = "";
            string carYearParameter = "";
            
            PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
            Assert.Null(partFlaggingBusinessLayer.HandleGetFlagCompatibility(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter)); 
        }
    }
}