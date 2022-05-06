using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.Models.CarbuilderModels;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class CarBuildManager
    {
        private readonly CarBuildService _carBuildService;

        public CarBuildManager(CarBuildService carBuildService)
        {
            _carBuildService = carBuildService;
        }

        public bool SaveCarTypeManager(CarTypeModel savedCarBuild)
        {
            if (Regex.Match(savedCarBuild.year, "^[0-9]{4}").Success)  // Regex checks that the user input is 4 integers
            {
                int input = Int32.Parse(savedCarBuild.year);            // Parse the user input string into integers called input
                if (input < 1950 || input > 2022)                       // Make sure the integer is equal to or between 1950 & 2022
                {
                    return false;                                       // If out of range, return false
                }
            } 
            else 
            {
                return false;                                           // If user input is anything that is not four integers, return false
            }

            if (savedCarBuild.make!.Length == 0 || savedCarBuild.make!.Length > 20)     // Make sure user input is not null and is less than 20 characters
            {
                return false;
            }

            if (savedCarBuild.model!.Length == 0 || savedCarBuild.model!.Length > 20)   // Make sure user input is not null and is less than 20 characters
            {
                return false;
            }

            return _carBuildService.SaveCarType(savedCarBuild);         // Save information to CarTypeModel
        }

        public bool SaveCarModificationsManager(ModifyCarBuildModel modifiedCarBuild)
        {
            if (modifiedCarBuild.partNumber!.Length == 0 || modifiedCarBuild.partNumber!.Length > 30)   // Make sure user input is not null and is less than 30 characters
            {
                return false;
            }
            if (modifiedCarBuild.type != "OEM" || modifiedCarBuild.type != "Aftermarket")           // If user input string is not 'OEM' or 'Aftermarket', return false
            {
                return false;
            }
            return _carBuildService.SaveModifiedCarBuild(modifiedCarBuild);
        }

        public bool UpdateCarManager(UpdateCarModel updateCar)
        {
            //if (modifiedCarBuild.partNumber!.Length == 0 || modifiedCarBuild.partNumber!.Length > 30)   // Make sure user input is not null and is less than 30 characters
            //{
            //    return false;
            //}
            //if (modifiedCarBuild.type != "OEM" || modifiedCarBuild.type != "Aftermarket")           // If user input string is not 'OEM' or 'Aftermarket', return false
            //{
            //    return false;
            //}
            return _carBuildService.UpdateCarBuild(updateCar);
        }

        public IList<CarTypeModel> RetrieveAllCarTypes()
        {
            return _carBuildService.FetchCarType();
        }

        public IList<UserCarBuildModel> RetrieveAllModifiedCarBuilds(string username)
        {
            return _carBuildService.FetchModifiedCarBuild(username);
        }

        //public CarTypeModel CreateCarTypeModel(string make, string model, string year)
        //{
        //    return new CarTypeModel(make, model, year);
        //}

        //public ModifyCarBuildModel CreateModifyCarBuildModel(string partNumber, string type)
        //{
        //    return new ModifyCarBuildModel(partNumber, type);
        //}
    }
}
