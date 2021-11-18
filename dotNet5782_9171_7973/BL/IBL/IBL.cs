using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL:IBLCustomer, IBLBaseStation, IBLDrone, IBLParcel
    {
        T GetById<T>(int requestedId) where T : IDAL.DO.IIdentifiable;
        IEnumerable<T> GetList<T>() where T : IDAL.DO.IIdentifiable;  
    }
}
