using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
  public  interface I_Openingbalance
    {
        public bool Insert(Openingbalance Openingbalance);

        public bool Update(Openingbalance Openingbalance);
    }
}
