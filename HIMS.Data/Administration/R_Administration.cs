using HIMS.Data.CustomerPayment;
using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Model.Inventory;
using HIMS.Common.Utility;
using HIMS.Model.Administration;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;

namespace HIMS.Data.Administration
{
    public  class R_Administration : GenericRepository, I_Administration
    {

        public R_Administration(IUnitofWork unitofWork) : base(unitofWork)
        {

        }




        public bool InsertPackageDetails(PackageDetailParam PackageDetailParams)
        {

            // delete previous data from  table
            var ServiceId = PackageDetailParams.Delete_PackageDetails.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_PackageDetails", ServiceId);

            // add  table
            foreach (var a in PackageDetailParams.insert_PackageDetails)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_PackageDetails", disc1);
            }
          

            _unitofWork.SaveChanges();

            return true;
        }

        public bool InsertCompanyServiceAssignMaster(CompanyServiceAssignMaster CompanyServiceAssignMaster)
        {

            // delete previous data from  table
            var CompanyId = CompanyServiceAssignMaster.Delete_CompantServiceDetails.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_CompantServiceDetails", CompanyId);

            // add  table
            foreach (var a in CompanyServiceAssignMaster.insert_CompanyServiceAssignMaster)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_M_CompanyServiceAssignMaster_1", disc1);
            }


            _unitofWork.SaveChanges();

            return true;
        }

        public bool CancleTExpenseParam(TExpenseParam TExpenseParam)
        {
            var disc1 = TExpenseParam.Update_T_Expenses_IsCancel.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_T_Expenses_IsCancel", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool SaveTExpenseParam(TExpenseParam TExpenseParam)
        {
            // throw new NotImplementedException();
            var disc = TExpenseParam.insert_T_Expense.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_Expense_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool UpdateBillcancellation(AdministrationParam administrationParam)
        {
            if (administrationParam.BillCancellationParam.OP_IP_type == 0)
            {
                var disc3 = administrationParam.BillCancellationParam.ToDictionary();
                disc3.Remove("OP_IP_type");
                ExecNonQueryProcWithOutSaveChanges("OP_BILL_CANCELLATION", disc3);
            }
            else
            {
                var disc3 = administrationParam.BillCancellationParam.ToDictionary();
                disc3.Remove("OP_IP_type");
                ExecNonQueryProcWithOutSaveChanges("IP_BILL_CANCELLATION", disc3);
            }

            _unitofWork.SaveChanges();
            return true;
        }

        public bool InsertDoctorShareMaster(DoctorShareParam doctorShareParam)
        {
             var disc3 = doctorShareParam.InsertDoctorShareMasterParam.ToDictionary();
             disc3.Remove("DoctorShareId");
             ExecNonQueryProcWithOutSaveChanges("Insert_DoctorShareMaster_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool UpdateDoctorShareMaster(DoctorShareParam doctorShareParam)
        {
            var disc3 = doctorShareParam.UpdateDoctorShareMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_DoctorShareMaster_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool DoctorShareProcess(DoctorShareProcessParam doctorShareProcessParam)
        {
            var disc3 = doctorShareProcessParam.ProcessDoctorShareParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("OP_DoctorSharePerCalculation_1", disc3);
            ExecNonQueryProcWithOutSaveChanges("IP_DoctorSharePerCalculation_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool IPDischargeCancel(IPDischargeCancelParam iPDischargeCancelParam)
        {
            var disc3 = iPDischargeCancelParam.IP_DischargeCancelParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("IP_DISCHARGE_CANCELLATION", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
       

    }
}


    

