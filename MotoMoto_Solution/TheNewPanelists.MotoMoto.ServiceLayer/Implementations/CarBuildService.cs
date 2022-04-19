using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class CarBuildService 
    {
        private readonly CarBuildDataAccess _carBuildDAO;

        public CarBuildService()
        {
            _carBuildDAO = new CarBuildDataAccess();
        }

        public bool SaveCarType(DataStoreCarType carType)
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

        public bool SaveCarBuilds(DataStoreCarBuilds carBuilds)
        {
            var dataStoreCarBuilds = new DataStoreCarBuilds()
            {
                carBuildID = carBuilds.carID,
                carID = carBuilds?.carID,
                username = carBuilds?.username
            };
            return _carBuildDAO.InsertNewDataStoreCarBuildsEntity(dataStoreCarBuilds);
        }

        public bool SaveCarModifications(DataStoreCarModifications carModifications)
        {
            var dataStoreCarModifications = new DataStoreCarModifications()
            {
                carModificationID = carModifications.carModificationID,
                carBuildID = carModifications.carBuildID,
                partID = carModifications.partID
            };
            return _carBuildDAO.InsertNewDataStoreCarModificationsEntity(dataStoreCarModifications);
        }

        public bool SaveOEMAndAfterMarketParts(DataStoreOEMAndAfterMarketParts carParts)
        {
            var dataStoreOEMAndAfterMarketParts = new DataStoreOEMAndAfterMarketParts()
            {
                partID = carParts.partID,
                partName = carParts.partName,
                type = carParts.type
            };
            return _carBuildDAO.InsertNewDataStoreOEMAndAfterMarketPartsEntity(dataStoreOEMAndAfterMarketParts);
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
