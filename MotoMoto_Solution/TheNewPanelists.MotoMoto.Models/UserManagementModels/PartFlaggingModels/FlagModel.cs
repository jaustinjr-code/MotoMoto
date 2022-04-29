using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class FlagModel
    {

        private string? partNumber;
        private string? carMake;

        private string? carModel;

        private string? carYear; 

        public FlagModel()
        {
            partNumber = null;
            carMake = null;
            carModel = null; 
            carYear = null;
        }

        public FlagModel(string partNumber, string carMake, string carModel, string carYear) 
        {
            PartNumber = partNumber;
            CarMake = carMake;
            CarModel = carModel; 
            CarYear = carYear;
        }
    
        public string? PartNumber 
        {
            get 
            {
                if (partNumber is not null)
                {
                    if (partNumber.Trim().Length == 0) 
                    {
                        return null;
                    }
                    return partNumber.ToLower().Trim();
                }
                return partNumber;
                
            }
            set
            {
                partNumber = value;
            }
        }

        public string? CarMake 
        {
            get 
            {
                if (carMake is not null)
                {
                    if (carMake.Trim().Length == 0) 
                    {
                        return null;
                    }
                    return carMake.ToLower().Trim();
                }
                return carMake;
            }
            set
            {
                carMake = value;
            }
        }

        public string? CarModel 
        {
            get 
            {
                if (carModel is not null)
                {
                    if (carModel.Trim().Length == 0) 
                    {
                        return null;
                    }
                    return carModel.ToLower().Trim();
                }
                return carModel;
            }
            set
            {
                carModel = value;
            }
        }

        public string? CarYear 
        {
            get 
            {
                if (carYear is not null)
                {
                    if (carYear.Trim().Length == 0) 
                    {
                        return null;
                    }
                    return carYear.ToLower().Trim();
                }
                return carYear;
            }
            set
            {
                carYear = value;
            }
        }
    }
}
