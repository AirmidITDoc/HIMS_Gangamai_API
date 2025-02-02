﻿using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_Mrdmedicalcertificate : GenericRepository, I_Mrdmedicalcertificate
    {
        public R_Mrdmedicalcertificate(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool InsertMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam)
        {
            // throw new NotImplementedException();
            var disc = MrdMedicolegalCertificateparam.InsertMrdMedicolegalCertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_MedicolegalCertificate_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
        public bool UpdateMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam)
        {
            // throw new NotImplementedException();
            var disc = MrdMedicolegalCertificateparam.UpdateMrdMedicolegalCertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_MedicolegalCertificate_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Insert(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            //  throw new NotImplementedException();

            var dic = Mrdmedicalcertificateparam.InsertMrdmedicalcertificate.ToDictionary();
            dic.Remove("MLCId");
            ExecNonQueryProcWithOutSaveChanges("insert_MLCInfo_1", dic);


            _unitofWork.SaveChanges();
            return true;
        }

     
        public bool Update(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            // throw new NotImplementedException();

            var disc1 = Mrdmedicalcertificateparam.UpdateMrdmedicalcertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_MLCInfo_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool SaveMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MICDCodingMasterParam.SaveMICDCodingMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ICDCodingMaster_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam)
        {

            var disc3 = MICDCodingMasterParam.UpdateMICDCodingMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_ICDCodingMaster_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool SaveMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MICDCdeheadMasterParam.SaveMICDCdeheadMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ICDCdeheadMaster_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam)
        {

            var disc3 = MICDCdeheadMasterParam.UpdateMICDCdeheadMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_ICDCdeheadMaster_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }


    }
}
