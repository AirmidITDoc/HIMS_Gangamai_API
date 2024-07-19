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
        public HospitalMaster GetHospitalStoreById(long Id)
        {
            if (Id == 0) return new HospitalMaster();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", Id);
            return GetList<HospitalMaster>("SELECT StoreId HospitalId,PrintStoreName HospitalName,StoreAddress HospitalAddress,HospitalMobileNo Phone FROM M_StoreMaster WHERE StoreId=@Id", para).FirstOrDefault();
        }
    }
}
