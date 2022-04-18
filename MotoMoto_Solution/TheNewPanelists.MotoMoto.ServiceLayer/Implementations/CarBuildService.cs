using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class CarBuildService 
    {
        private readonly CarBuildDataAccess _carBuildDAO;

        public CarBuildService()
        {
            _carBuildDAO = new CarBuildDataAccess();
        }

        public bool SaveCarBuild(DataStoreCarType carType)
        {
            var dataStoreCarType = new DataStoreCarType()
            {
                carID = carType!.carID, //Remove because auto-increment
                make = carType!.make,
                model = carType!.model,
                year = carType!.year,
                country = carType!.country
            };
            return _carBuildDAO.InsertNewDataStoreCarTypeEntity(dataStoreCarType);
        }
        /*
        public bool SaveCarBuild(CarTypeModel carType)
        {
            var dataStoreCarType = new CarTypeModel()
            {
                make = carType!.make,
                model = carType!.model,
                year = carType!.year,
                country = carType!.country
            };
            return _carBuildDAO.CarTypeEntity(dataStoreCarType);
        }

        public bool ModifyCarBuild(DataStoreCarModifications modifiedCar)
        {
            var dataStoreCarModifications = new DataStoreCarModifications()
            {
                _carModificationID = modifiedCar!._carModificationID,
                _carBuildID = modifiedCar!._carBuildID,
                _partID = modifiedCar!._partID
            };
            return _carBuildDAO.InsertNewDataStoreCarModificationsEntity(dataStoreCarModifications);
        }

        public bool ModifyCarBuild(ModifyCarBuildModel modifiedCar)
        {
            var dataStoreCarModifications = new ModifyCarBuildModel()
            {
                partName = modifiedCar!.partName,
                type = modifiedCar!.type
            };
            return _carBuildDAO.ModifyCarBuildEntity(dataStoreCarModifications);
        }
        */
    }
}
