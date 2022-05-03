// using Xunit;
// using TheNewPanelists.MotoMoto.BusinessLayer;
// using TheNewPanelists.MotoMoto.Models;
// using TheNewPanelists.MotoMoto.DataAccess;

// namespace TheNewPanelists.MotoMoto.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the part flagging business layer
//     /// </summary>
//     public class NotificationSystemBusinessUnitTest
//     {
//         /// <summary>
//         /// Business logic for part notification system functionality
//         /// </summary>
//         private readonly NotificationSystemManager _notificationSystemManager;

//         /// <summary>
//         /// Data access for notification system functionality
//         /// </summary>
//         private readonly NotificationSystemDataAccess _notificationSystemDataAccerss;



//         /// <summary>
//         /// Default Constructor. Initializes part flagging business layer entity
//         /// </summary>
//         public NotificationSystemBusinessUnitTest()
//         {
//             _notificationSystemManager = new NotificationSystemManager();
//             _notificationSystemDataAccerss = new NotificationSystemDataAccess();
//         }

//         /// <summary>
//         /// Creates flag entity with all primary key attributes
//         /// Test is successful if all attributes are lower case strings when retrieved.
//         /// </summary>  
//         [Fact]
//         public void FetchRegisteredEvents()
//         {
//             // string eventTime = "11:00 AM";
//             // string eventDate = "2022-04-25";
//             // string eventStreetAddress = "1250 Bellflower Blvd";
//             // string eventCity = "Anaheim";
//             // string eventState = "OR";
//             // string eventCountry = "Canada";
//             // string eventZipCode = "12345";
            
//             string username = "user118";
//             NotificationSystemInAppModel inAppNoti = _notificationSystemManager.RetrieveRegisteredEvents(user118);

//             bool result = newFlag.PartNumber == "1" &&
//                           newFlag.CarMake == "toyota" &&
//                           newFlag.CarModel == "corolla" &&
//                           newFlag.CarYear == "1999";

//             Assert.True(result);
//         }

//         /// <summary>
//         /// Creates flag entity with all primary key attributes being empty strings
//         /// Test is successful if all attributes are null when retrieved because empty strings are invalid.
//         /// </summary> 
//         [Fact]
//         public void CreateFlagWithIncompleteData()
//         {
//             string partNumber = "";
//             string carMake = "";
//             string carModel = "";
//             string carYear = "";
            
//             FlagModel newFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);

//             bool result = newFlag.PartNumber is null &&
//                           newFlag.CarMake is null &&
//                           newFlag.CarModel is null &&
//                           newFlag.CarYear is null;

//             Assert.True(result);
//         }

//         /// <summary>
//         /// Creates flag entity with all primary key attributes, but with whitespace on either side.
//         /// Test is successful if all attributes have extra whitespace removed when retrieved.
//         /// </summary> 
//         [Fact]
//         public void CreateFlagWithExtraWhitespace()
//         {
//             string partNumber = "      1        ";
//             string carMake = "         Toyota      ";
//             string carModel = "     Corolla     ";
//             string carYear = "     1999     ";

            
//             FlagModel newFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);

//             bool result = newFlag.PartNumber == "1" &&
//                           newFlag.CarMake == "toyota" &&
//                           newFlag.CarModel == "corolla" &&
//                           newFlag.CarYear == "1999";

//             Assert.True(result);
//         }

//         /// <summary>
//         /// Creates flag entity with all primary key attributes being multiple strings with only whitespace.
//         /// Test is successful if all attributes are null when retrieved because there is no actual data.
//         /// </summary> 
//         [Fact]
//         public void CreateFlagWithOnlyWhitespace()
//         {
//             string partNumber = "        ";
//             string carMake = "          ";
//             string carModel = "          ";
//             string carYear = "          ";
            
//             FlagModel newFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);

//             bool result = newFlag.PartNumber is null &&
//                           newFlag.CarMake is null &&
//                           newFlag.CarModel is null &&
//                           newFlag.CarYear is null;

//             Assert.True(result);
//         }

//         /// <summary>
//         /// Checks if valid flag is computed to be valid.
//         /// Test is successful if the flag validation is true.
//         /// </summary> 
//         [Fact]
//         public void ValidateFlagWithValidData()
//         {
//             string partNumber = "1";
//             string carMake = "Toyota";
//             string carModel = "Corolla";
//             string carYear = "1999";
            
//             FlagModel newFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);
//             bool result = __partFlaggingBusinessLayer.IsValidFlag(newFlag);
//             Assert.True(result);
//         }

//         /// <summary>
//         /// Checks if flag with null data is computed to be invalid.
//         /// Test is successful if the flag validation is false.
//         /// </summary> 
//         [Fact]
//         public void ValidateFlagWithNullData()
//         {
//             string partNumber = "";
//             string carMake = "";
//             string carModel = "";
//             string carYear = "";

//             FlagModel newFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);

//             bool result = __partFlaggingBusinessLayer.IsValidFlag(newFlag);
//             Assert.False(result);
//         }

//         /// <summary>
//         /// Checks if flag with invalid data is computed to be invalid.
//         /// Test is successful if the flag validation is false.
//         /// </summary> 
//         public void ValidateFlagWithInvalidDate()
//         {
//             string partNumber = "1";
//             string carMake = "Toyota";
//             string carModel = "Corolla";
//             string carYear = "not a date";
            
//             FlagModel newFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);
//             bool result = __partFlaggingBusinessLayer.IsValidFlag(newFlag);
//             Assert.False(result);
//         }

//         /// <summary>
//         /// Uses the business layer to call flag creation for a valid flag.
//         /// Test is successful if the creation function returns true because it has received a true
//         /// result from the service layer.
//         /// </summary> 
//         [Fact]
//         public void HandleValidFlagCreation()
//         {
//             string partNumberParameter = "1";
//             string carMakeParameter = "Nissan";
//             string carModelParameter = "Altima";
//             string carYearParameter = "2005";

//             Assert.True(__partFlaggingBusinessLayer.HandleFlagCreation(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter)); 
//         }

//         /// <summary>
//         /// Uses the business layer to call flag creation for an invalid flag.
//         /// Test is successful if the creation function returns false because it returns false
//         /// after flag validation.
//         /// </summary>
//         public void HandleInvalidFlagCreation()
//         {
//             string partNumberParameter = "";
//             string carMakeParameter = "";
//             string carModelParameter = "";
//             string carYearParameter = "";
            
//             Assert.True(__partFlaggingBusinessLayer.HandleFlagCreation(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter)); 
//         }

//         /// <summary>
//         /// Uses the business layer to check flag compatibility.
//         /// Test passes for both true and false results, because both would dictate
//         /// a successful retrieval of flag count from the database. Only null values 
//         /// fail the test, because null indicates a failed operation.
//         /// </summary>
//         [Theory]
//         [InlineData("1", "Nissan", "Altima", "2005")]
//         [InlineData("1", "TestMake", "TestModel", "1955")]
//         public void HandleValidFlagCountRetrieval(string partNumberParameter, string carMakeParameter, string carModelParameter, string carYearParameter)
//         {
//             Assert.NotNull(__partFlaggingBusinessLayer.HandleGetFlagCompatibility(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter));
//         }

//         /// <summary>
//         /// Uses the business layer to check flag compatibility for a flag with empty attributes.
//         /// Test is successful if the operation returns null because the operation never goes through
//         /// after validating the flag.
//         /// </summary>
//         [Fact]
//         public void HandleInvalidFlagCountRetrieval()
//         {
//             string partNumberParameter = "";
//             string carMakeParameter = "";
//             string carModelParameter = "";
//             string carYearParameter = "";
            
//             Assert.Null(__partFlaggingBusinessLayer.HandleGetFlagCompatibility(partNumberParameter, carMakeParameter, carModelParameter, carYearParameter)); 
//         }

//         /// <summary>
//         /// Tests decrementing existing flags of count equal to and greater than one.
//         /// Test is successful if the function returns true meaning it has received a true result
//         /// from the service layer.
//         /// </summary>
//         [Theory]
//         [InlineData(1)]
//         [InlineData(2)]
//         public async void HandleValidFlagCountDecrement(int count)
//         {
//             const string testName = "HandleValidFlagCountDecrement";

//             string partNumber = testName;
//             string carMake = testName;
//             string carModel = testName;
//             string carYear = "2022";

//             FlagModel testFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);
            
//             await __partFlaggingDataAccess.DeleteFlag(testFlag);
//             for (int countIt = 0; countIt < count; ++countIt)
//             {
//                 await __partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
//             }

//             Assert.True(__partFlaggingBusinessLayer.HandleFlagCountDecrement(partNumber, carMake, carModel, carYear));
//         }

//         /// <summary>
//         /// Tests decrementing a nonexisting flag.
//         /// Test is successful if the function returns false meaning it has not decremented
//         /// any flags because the flag does not exist.
//         /// </summary>
//         [Fact]
//         public async void HandleInvalidFlagCountDecrement()
//         {
//             const string testName = "HandleInvalidFlagCountDecrement";

//             string partNumber = testName;
//             string carMake = testName;
//             string carModel = testName;
//             string carYear = "2022";

//             FlagModel testFlag = __partFlaggingBusinessLayer.CreateFlagModel(partNumber, carMake, carModel, carYear);
            
//             await __partFlaggingDataAccess.DeleteFlag(testFlag);

//             Assert.False(__partFlaggingBusinessLayer.HandleFlagCountDecrement(partNumber, carMake, carModel, carYear));
//         }
//     }
// }