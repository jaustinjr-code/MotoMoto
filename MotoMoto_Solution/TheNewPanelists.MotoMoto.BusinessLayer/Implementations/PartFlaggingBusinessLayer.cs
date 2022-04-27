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
        
        public PartFlaggingBusinessLayer()
        {
            
        }

        public PartFlaggingBusinessLayer(string filepath)
        {
            
        }

        /// <summary>
        /// Checks that all parameters are not null, and calls the flag creation method in
        /// the service layer to create a new part flag.
        /// </summary>
        ///
        /// <param name="partNumber">The number associated with the incompatible part</param>
        /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
        /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
        /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the new flag</returns>
        public bool handleFlagCreation(string partNumber, string carMake, string carModel, string carYear) 
        {
            bool result = false;
            FlagModel flag = createFlagModel(partNumber, carMake, carModel, carYear);
            if (isValidFlag(flag))
            {
                PartFlaggingService partFlaggingService = new PartFlaggingService();
                result = partFlaggingService.callFlagCreation(flag);
            }
            return result;
        }

        public FlagModel createFlagModel(string partNumber, string carMake, string carModel, string carYear)
        {
            return new FlagModel(partNumber, carMake, carModel, carYear);
        }

        public bool isValidFlag(FlagModel flag)
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

        public bool? HandleGetFlagCompatibility(string partNumber, string carMake, string carModel, string carYear)
        {
            const int FLAG_COMPATIBILITY_THRESHOLD = 100;
            FlagModel flag = createFlagModel(partNumber, carMake, carModel, carYear);
            if (isValidFlag(flag))
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
    }
}
