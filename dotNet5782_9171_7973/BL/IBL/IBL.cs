﻿using System;
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
        object GetById(Type type, int requestedId);
        IEnumerable GetList(Type type);  
    }
}
