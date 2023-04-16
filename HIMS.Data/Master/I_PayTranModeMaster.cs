using HIMS.Model;
using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data
{
  public interface I_PayTranModeMaster
    {
        bool Update(PayTranModeMasterParams PayTranModeMasterParamsHome);
        bool Save(PayTranModeMasterParams PayTranModeMasterParamsHome);
    }
}
