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

        public List<CarTypeModel> FetchCarType()
        {
            return _carBuildDAO.GetCarType();
        }

        public bool SaveModifiedCarBuild(ModifyCarBuildModel modifiedCar)
        {
            var modifyCarBuildModel = new ModifyCarBuildModel()
            {
                partNumber = modifiedCar!.partNumber,
                type = modifiedCar!.type
            };
            return _carBuildDAO.InsertNewDataStoreOEMAndAfterMarketPartsEntity(modifyCarBuildModel);   //WHAT DO I PUT CAUSE I DON'T HAVE AN ENTITY FOR MODIFY CAR BUILD BECAUSE CAR MODIFICATIONS JUST REFERENCE TO A PART ID
        }

        public List<ModifyCarBuildModel> FetchModifiedCarBuild()
        {
            return _carBuildDAO.GetModifiedCarBuild();
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
