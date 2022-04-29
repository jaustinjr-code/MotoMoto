using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class PartFlaggingBusinessLayer
    {

        /// <summary>
        /// Encapsulates flag information into entity, checks that flag entity is valid, and passes entity to
        /// service layer to handle creating the flag in the database.
        /// </summary>
        ///
        /// <param name="partNum">The number associated with the incompatible part</param>
        /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
        /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
        /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the new flag</returns>
        public bool HandleFlagCreation(string partNum, string carMake, string carModel, string carYear) 
        {
            bool result = false;
            FlagModel flag = CreateFlagModel(partNum, carMake, carModel, carYear);
            if (IsValidFlag(flag))
            {
                PartFlaggingService partFlaggingService = new PartFlaggingService();
                result = partFlaggingService.CallFlagCreation(flag);
            }
            return result;
        }

        /// <summary>
        /// Uses part flagging information to generate an entity which encapsulates that part flagging information.
        /// </summary>
        ///
        /// <param name="partNum">The number associated with the incompatible part</param>
        /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
        /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
        /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
        ///
        /// <returns>FlagModel entity which represents a single part flag</returns>
        public FlagModel CreateFlagModel(string partNum, string carMake, string carModel, string carYear)
        {
            return new FlagModel(partNum, carMake, carModel, carYear);
        }

        /// <summary>
        /// Checks that a flag entity contains information that is valid for entry into the part flagging database.
        /// </summary>
        ///
        /// <param name="flag">Flag entity containing information related to a single part flag</param>
        ///
        /// <returns>FlagModel entity which represents a single part flag</returns>
        public bool IsValidFlag(FlagModel flag)
        {
            bool result = false;
            bool nullValidation =   flag.PartNumber is not null && 
                                    flag.CarMake is not null &&
                                    flag.CarModel is not null &&
                                    flag.CarYear is not null;

            bool yearValidation = false;
            if (nullValidation)
            {
                int year;
                if (Int32.TryParse(flag.CarYear, out year))
                {
                    if (year > 1800 && year < DateTime.Now.Year + 5)
                    {
                        yearValidation = true;
                    }
                };
            }

            result = nullValidation && yearValidation;
            return result;
        }

        /// <summary>
        /// Uses part flagging information to generate an entity which encapsulates that part flagging information,
        /// validates that flag is valid to exist in the part flagging database, retrieves the count of times that flag
        /// has been generated, and determines whether or not the flag is considered incompatible with a car.
        /// </summary>
        ///
        /// <param name="partNum">The number associated with the incompatible part</param>
        /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
        /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
        /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
        ///
        /// <returns>
        /// True if the number of part flags makes the part and vehicle combination incompatible, false if the
        /// number of part flags makes the part and vehicle combination compatible, null if the operation failed.
        /// </returns>
        public bool? HandleGetFlagCompatibility(string partNum, string carMake, string carModel, string carYear)
        {
            const int FLAG_COMPATIBILITY_THRESHOLD = 100;
            FlagModel flag = CreateFlagModel(partNum, carMake, carModel, carYear);
            if (IsValidFlag(flag))
            {
                PartFlaggingService partFlaggingService = new PartFlaggingService();
                int flagCount = partFlaggingService.CallGetFlagCount(flag);
                if (flagCount >= FLAG_COMPATIBILITY_THRESHOLD)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Encapsulates flag information into entity, checks that flag entity is valid, and if valid 
        /// decrements the count of a flag or removes it from the database if the count becomes 0
        /// after decrement.
        /// </summary>
        ///
        /// <param name="partNum">The number associated with the incompatible part</param>
        /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
        /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
        /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
        ///
        /// <returns>True if decrement operation is successful, false otherwise</returns>
        public bool HandleFlagCountDecrement(string partNum, string carMake, string carModel, string carYear)
        {
            bool result = false;
            FlagModel flag = CreateFlagModel(partNum, carMake, carModel, carYear);
            if (IsValidFlag(flag))
            {
                PartFlaggingService partFlaggingService = new PartFlaggingService();
                result = partFlaggingService.CallDecrementFlagCount(flag);
            }
            return result;
        }
    }
}
