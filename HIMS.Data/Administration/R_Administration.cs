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
using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Radiology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using HIMS.Model.Pharmacy;


namespace HIMS.Data.Administration
{
    public  class R_Administration : GenericRepository, I_Administration
    {

        public R_Administration(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewVoucharPrint(int ExpId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@ExpId", ExpId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptExpVoucharPrint", para);


           
            int length = 0;
            length = Bills.Rows.Count;
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;


            html = html.Replace("{{PersonName}}", Bills.GetColValue("PersonName"));
            html = html.Replace("{{ExpAmount}}", Bills.GetColValue("ExpAmount"));
            html = html.Replace("{{ExpensesType}}", Bills.GetColValue("ExpensesType"));
            html = html.Replace("{{VoucharNo}}", Bills.GetColValue("VoucharNo"));
            html = html.Replace("{{Narration}}", Bills.GetColValue("Narration"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
       

            html = html.Replace("{{ExpDate}}", Bills.GetColValue("ExpTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
           

          


      
            html = html.Replace("{{Items}}", items.ToString());







            return html;
        }

        public string ViewExpensesReport(DateTime FromDate, DateTime ToDate, int ExpHeadId, int ExpType, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ExpHeadId", ExpHeadId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ExpType", ExpType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("Retrieve_T_Expenses", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_TotalAmount = 0, T_TotalAmt = 0, PatientName = 0, AdmittedDoctorName = 0, AdmissionDate = 0, BillNo = 0, CompBillDate = 0, IPDNo = 0, RoomName = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["PersonName"].ConvertToString();
                    items.Append("<tr style=\"font-size:22px;border: 1px;color:black;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["PersonName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:24px;font-weight:bold;\">Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(T_TotalAmt.ToString()).Append("</td></tr>");

                    T_TotalAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PersonName"].ConvertToString()).Append("</td></tr>");

                }

                T_TotalAmt = T_TotalAmt;
                //T_Count = T_Count + 1;
                previousLabel = dr["PersonName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["ExpDate"].ConvertToDateString("dd/MM/yyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PersonName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align left;\">").Append(dr["ExpAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["ExpensesType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["VoucharNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Narration"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(T_TotalAmt.ToString()).Append("</td></tr>");


                }
                T_TotalAmt += dr["ExpAmount"].ConvertToDouble();
                T_TotalAmount += dr["ExpAmount"].ConvertToDouble();
                //BillNo += dr["BillNo"].ConvertToDouble();
                //CompBillDate += dr["CompBillDate"].ConvertToDouble();
                //IPDNo += dr["IPDNo"].ConvertToDouble();
                //RoomName += dr["RoomName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
         

            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
           
            return html;

        }

        public bool MClassMasterInsert(ClassMasterPara ClassMasterPara)
        {
            // throw new NotImplementedException();
            var disc = ClassMasterPara.ClassMasterParamsInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_M_ClassMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool MClassMasterUpdate(ClassMasterPara ClassMasterPara)
        {
            // throw new NotImplementedException();
            var disc = ClassMasterPara.ClassMasterParamsUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_M_ClassMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool InsertPhysiotherapy(PhysiotherapyParam PhysiotherapyParam)
        {
            // throw new NotImplementedException();
            var disc = PhysiotherapyParam.InsertPhysiotherapyHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_PhysioScheduleHeader", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool UpdatePhysiotherapy(PhysiotherapyParam PhysiotherapyParam)
        {
            // throw new NotImplementedException();
            var disc = PhysiotherapyParam.UpdatePhysiotherapyDetails.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_T_PhysioScheduleDetail", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveNursingWeight(NursingWeightParam NursingWeightParam)
        {
            // throw new NotImplementedException();
            var disc = NursingWeightParam.SaveNursingWeight.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_NursingWeight_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool InsertGSTReCalculProcess(GSTReCalculProcessParam GSTReCalculProcessParam)
        {
            // throw new NotImplementedException();
            var disc = GSTReCalculProcessParam.InsertGSTReCalculProcessParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_RecalcGST", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateNursingWeight(NursingWeightParam NursingWeightParam)
        {
            // throw new NotImplementedException();
            var disc = NursingWeightParam.UpdateNursingWeight.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_NursingWeight_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SavePatientFeedBack(Parameter PatientFeedbackParameter)
        {
            foreach (var a in PatientFeedbackParameter.PatientFeedbackParams)
            {
                var disc1 = a.ToDictionary();
                disc1.Remove("PatientFeedbackId");
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_PatientFeedback_1", disc1);
            }

            _unitofWork.SaveChanges();
            return (true);

        }
        public bool SaveNursingOrygenVentilator(NursingOrygenVentilatorParam NursingOrygenVentilatorParam)
        {
            // throw new NotImplementedException();
            var disc = NursingOrygenVentilatorParam.SaveNursingOrygenVentilatorParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_NursingOrygenVentilator_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateNursingOrygenVentilator(NursingOrygenVentilatorParam NursingOrygenVentilatorParam)
        {
            // throw new NotImplementedException();
            var disc = NursingOrygenVentilatorParam.UpdateNursingOrygenVentilatorParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_NursingOrygenVentilator_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }


        public bool SaveNursingVitals(NursingVitalsParam NursingVitalsParam)
        {
            // throw new NotImplementedException();
            var disc = NursingVitalsParam.SaveNursingVitalsParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_NursingVitals_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateNursingVitals(NursingVitalsParam NursingVitalsParam)
        {
            // throw new NotImplementedException();
            var disc = NursingVitalsParam.UpdateSaveNursingVitalsParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_NursingVitals_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool SaveNursingSugarLevel(NursingSugarLevelParam NursingSugarLevelParam)
        {
            // throw new NotImplementedException();
            var disc = NursingSugarLevelParam.SaveNursingSugarLevelParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_NursingSugarLevel_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateNursingSugarLevel(NursingSugarLevelParam NursingSugarLevelParam)
        {
            // throw new NotImplementedException();
            var disc = NursingSugarLevelParam.UpdateNursingSugarLevelParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_NursingSugarLevel_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveDischargeInitiate(DischargeInitiateParam DischargeInitiateParam)
        {
            // throw new NotImplementedException();
            //var disc = DischargeInitiateParam.SaveDischargeInitiateParam.ToDictionary();
            //ExecNonQueryProcWithOutSaveChanges("m_insert_initiateDischarge_1", disc);
            //commit transaction
            foreach (var a in DischargeInitiateParam.SaveDischargeInitiateParam)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_initiateDischarge_1", disc1);
            }

            var disc5 = DischargeInitiateParam.UpdateDischargeInitiateParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_initiateDisc_1", disc5);

            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateDischargeInitiate(DischargeInitiateParam DischargeInitiateParam)
        {
            // throw new NotImplementedException();
            var disc = DischargeInitiateParam.UpdateDischargeInitiateParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_initiateDisc_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool UpdateDischargeInitiateApproval(DischargeInitiateApprovalParam DischargeInitiateApprovalParam)
        {
            // throw new NotImplementedException();
            var disc = DischargeInitiateApprovalParam.UpdateDischargeInitiateApprovalParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_initiateDischargeAprov_2", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

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
        public bool SaveMExpensesHeadMaster(MExpensesHeadMasterParam MExpensesHeadMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MExpensesHeadMasterParam.SaveMExpensesHeadMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ExpensesHeadMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMExpensesHeadMaster(MExpensesHeadMasterParam MExpensesHeadMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MExpensesHeadMasterParam.UpdateMExpensesHeadMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_M_ExpensesHeadMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool SaveNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam)
        {
            // throw new NotImplementedException();
            var disc = SaveNursingPainAssessmentParam.SaveNursingPainAssessment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_NursingPainAssessment_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
      

        public bool UpdateNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam)
        {
            // throw new NotImplementedException();
            var disc = SaveNursingPainAssessmentParam.SaveNursingPainAssessment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_NursingPainAssessment_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveUptDocMerge(UptDocMergeParam UptDocMergeParam)
        {


          
            // add table
            foreach (var a in UptDocMergeParam.UptdateDocMergeParams)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Upt_DocMerge_1", disc1);
            }
          

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
        public bool SaveTDoctorsNotes(TDoctorsNotesParam TDoctorsNotesParam)
        {
            // throw new NotImplementedException();
            var disc = TDoctorsNotesParam.SaveTDoctorsNotesParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_T_Doctors_Notes", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTDoctorsNotes(TDoctorsNotesParam TDoctorsNotesParam)
        {

            var disc3 = TDoctorsNotesParam.UpdateTDoctorsNotesParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Doctors_Notes", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool SaveTDoctorPatientHandover(TDoctorPatientHandoverParam TDoctorPatientHandoverParam)
        {
            // throw new NotImplementedException();
            var disc = TDoctorPatientHandoverParam.SaveTDoctorPatientHandoverParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_T_Doctor_PatientHandover", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTDoctorPatientHandover(TDoctorPatientHandoverParam TDoctorPatientHandoverParam)
        {

            var disc3 = TDoctorPatientHandoverParam.UpdateTDoctorPatientHandoverParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Doctor_PatientHandover", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

    }
}


    

