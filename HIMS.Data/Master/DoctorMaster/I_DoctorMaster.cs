using HIMS.Model.Master;
using HIMS.Model.Master.DoctorMaster;

namespace HIMS.Data.Master.DoctorMaster
{
    public interface I_DoctorMaster
    {
        bool Save(HIMS.Model.Master.DoctorMaster.DoctorMaster obj);
        bool Update(HIMS.Model.Master.DoctorMaster.DoctorMaster doctorMasterParams);
        
    }
}
