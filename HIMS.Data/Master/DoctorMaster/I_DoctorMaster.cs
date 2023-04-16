using HIMS.Model.Master;
using HIMS.Model.Master.DoctorMaster;

namespace HIMS.Data.Master.DoctorMaster
{
    public interface I_DoctorMaster
    {
        bool Save(Model.Master.DoctorMaster.DoctorMasterParams doctorMasterParams);
        bool Update(Model.Master.DoctorMaster.DoctorMasterParams doctorMasterParams);
        
    }
}
