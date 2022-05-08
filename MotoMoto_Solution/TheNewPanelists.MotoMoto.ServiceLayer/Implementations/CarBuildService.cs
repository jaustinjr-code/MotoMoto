using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models.CarbuilderModels;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class CarBuildService
    {
        private readonly CarBuildDataAccess _carBuildDAO;

        public CarBuildService(CarBuildDataAccess carBuildDataAccess)
        {
            _carBuildDAO = new CarBuildDataAccess();
        }

        public bool SaveCarType(CarTypeModel carType)
        {
            var carTypeModel = new CarTypeModel()
            {
                //carID = carType!.carID, //Remove because auto-increment
                make = carType!.make,
                model = carType!.model,
                year = carType!.year,
            };
            return _carBuildDAO.InsertNewDataStoreCarTypeEntity(carTypeModel);
        }

        public List<CarTypeModel> FetchCarType()    //Add VALUE: 
        {
            return _carBuildDAO.GetCarType();
        }
        public List<ModifyCarBuildModel> FetchCarParts()
        {
            return _carBuildDAO.GetCarParts();
        }
        public bool SaveModifiedCarBuild(ModifyCarBuildModel modifiedCar)
        {
            var modifyCarBuildModel = new ModifyCarBuildModel()
            {
                partNumber = modifiedCar!.partNumber,
                type = modifiedCar!.type
            };
            return _carBuildDAO.InsertNewDataStoreOEMAndAfterMarketPartsEntity(modifyCarBuildModel);
        }

        public List<UserCarBuildModel> FetchModifiedCarBuild(string username)
        {
            return _carBuildDAO.GetModifiedCarBuild(username);
        }

        public bool UpdateCarBuild(UpdateCarModel updatedCar)
        {
            var updateCarModel = new UpdateCarModel()
            {
                carID = updatedCar!.carID,
                partID = updatedCar!.partID,
                username = updatedCar!.username
            };
            return _carBuildDAO.InsertNewDataStoreCarBuildsEntity(updateCarModel);   
        }

        //public bool SaveCarBuilds(DataStoreCarBuilds carBuilds)
        //{
        //    var dataStoreCarBuilds = new DataStoreCarBuilds()
        //    {
        //        carBuildID = carBuilds.carID,
        //        carID = carBuilds?.carID,
        //        username = carBuilds?.username
        //    };
        //    return _carBuildDAO.InsertNewDataStoreCarBuildsEntity(dataStoreCarBuilds);
        //}

        //public bool SaveCarModifications(DataStoreCarModifications carModifications) 
        //{
        //    var dataStoreCarModifications = new DataStoreCarModifications()
        //    {
        //        carModificationID = carModifications.carModificationID,
        //        carBuildID = carModifications.carBuildID,
        //        partID = carModifications.partID
        //    };
        //    return _carBuildDAO.InsertNewDataStoreCarModificationsEntity(dataStoreCarModifications);
        //}

        //public bool SaveOEMAndAfterMarketParts(DataStoreOEMAndAfterMarketParts carParts)
        //{
        //    var dataStoreOEMAndAfterMarketParts = new DataStoreOEMAndAfterMarketParts()
        //    {
        //        partID = carParts.partID,
        //        partName = carParts.partName,
        //        type = carParts.type
        //    };
        //    return _carBuildDAO.InsertNewDataStoreOEMAndAfterMarketPartsEntity(dataStoreOEMAndAfterMarketParts);
        //}
    }
}
