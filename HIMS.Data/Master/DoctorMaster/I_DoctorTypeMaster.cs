
using HIMS.Model.Master;
using HIMS.Model.Master.DoctorMaster;
//using HIMS.Model.Master.DoctorTypeMaster
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DoctorMaster
{
    public interface I_DoctorTypeMaster
    {
        bool Save(DoctorTypeMasterParams doctorTypeMasterParams );
        bool Update(DoctorTypeMasterParams doctorTypeMasterParams );
        
    }
}
