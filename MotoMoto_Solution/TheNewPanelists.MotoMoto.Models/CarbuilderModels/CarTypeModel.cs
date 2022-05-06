using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto
{
    public class CarTypeModel
    {
        public string? make { get; set; }       //Car make
        public string? model { get; set; }      //Car model
        public string? year { get; set; }       //Car year
        public string? carID { get; set; }      //


        //public CarTypeModel()
        //{
        //    make = null;
        //    model = null;
        //    year = null;
        //}

        //public CarTypeModel(string make, string model, string year)
        //{
        //    Make = make;
        //    Model = model;
        //    Year = year;
        //}

        //public string? Make
        //{
        //    get
        //    {
        //        if (Make is not null)
        //        {
        //            if (Make.Trim().Length == 0)
        //            {
        //                return null;
        //            }
        //            return Make.ToLower().Trim();
        //        }
        //        return Make;
        //    }
        //    set
        //    {
        //        Make = value;
        //    }
        //}

        //public string? Model
        //{
        //    get
        //    {
        //        if (Model is not null)
        //        {
        //            if (Model.Trim().Length == 0)
        //            {
        //                return null;
        //            }
        //            return Model.ToLower().Trim();
        //        }
        //        return Model;
        //    }
        //    set
        //    {
        //        Model = value;
        //    }
        //}

        //public string? Year
        //{
        //    get
        //    {
        //        if (Year is not null)
        //        {
        //            if (Year.Trim().Length == 0)
        //            {
        //                return null;
        //            }
        //            return Year.ToLower().Trim();
        //        }
        //        return Year;
        //    }
        //    set
        //    {
        //        Year = value;
        //    }
        //}
    }
}
