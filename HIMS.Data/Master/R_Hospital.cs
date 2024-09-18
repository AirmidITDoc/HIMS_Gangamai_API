using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Users;
using HIMS.Model.Opd.OP;
using HIMS.Model.Master;
using System.Linq;
using HIMS.Model.Master.PersonalDetails;

namespace HIMS.Data.Master
{
    public class R_Hospital : GenericRepository, I_Hospital
    {
        public R_Hospital(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public HospitalMaster GetHospitalById(long Id)
        {
            if (Id == 0) return new HospitalMaster();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", Id);
            return GetList<HospitalMaster>("SELECT * FROM HospitalMaster WHERE HospitalId=@Id", para).FirstOrDefault();
        }
        public HospitalStoreMaster GetHospitalStoreById(long Id)
        {
            if (Id == 0) return new HospitalStoreMaster();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", Id);
            return GetList<HospitalStoreMaster>("SELECT StoreId,PrintStoreName,StoreAddress,HospitalMobileNo,HospitalEmailId,PrintStoreUnitName,DL_NO,GSTIN,Header FROM M_StoreMaster WHERE StoreId=@Id", para).FirstOrDefault();
        }

        public bool Save(HospitalMasterParam HospitalMasterParam)
        {
            // throw new NotImplementedException();
            var disc = HospitalMasterParam.HospitalMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("m_insert_HospitalMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(HospitalMasterParam HospitalMasterParam)
        {
            var disc1 = HospitalMasterParam.HospitalMasterUpdate.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("m_update_HospitalMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
