using HIMS.Model.Administration;
using HIMS.Model.Inventory;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Radiology;
using System;
using System.Collections.Generic;
using System.Data;
using HIMS.Model.CustomerInformation;

namespace HIMS.Data.Administration
{
    public  interface I_Administration
    {
       
        public bool UpdateBillcancellation(AdministrationParam administrationParam);
        public bool InsertDoctorShareMaster(DoctorShareParam doctorShareParam);
        public bool UpdateDoctorShareMaster(DoctorShareParam doctorShareParam);
        public bool DoctorShareProcess(DoctorShareProcessParam doctorShareProcessParam);
        public bool IPDischargeCancel(IPDischargeCancelParam iPDischargeCancelParam);

        public bool InsertPackageDetails(PackageDetailParam PackageDetailParam);

        public bool InsertCompanyServiceAssignMaster(CompanyServiceAssignMaster CompanyServiceAssignMaster);
        public bool SaveMExpensesHeadMaster(MExpensesHeadMasterParam MExpensesHeadMasterParam);
        public bool UpdateMExpensesHeadMaster(MExpensesHeadMasterParam MExpensesHeadMasterParam);
        public bool SaveTExpenseParam(TExpenseParam TExpenseParam);

        public bool CancleTExpenseParam(TExpenseParam TExpenseParam);

        public bool SaveNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam);

        public bool UpdateNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam);
        public bool SaveUptDocMerge(UptDocMergeParam UptDocMergeParam);

        public bool SaveNursingOrygenVentilator(NursingOrygenVentilatorParam NursingOrygenVentilatorParam);
        public bool UpdateNursingOrygenVentilator(NursingOrygenVentilatorParam NursingOrygenVentilatorParam);

        public bool SaveNursingVitals(NursingVitalsParam NursingVitalsParam);
        public bool UpdateNursingVitals(NursingVitalsParam NursingVitalsParam);


        public bool SaveNursingSugarLevel(NursingSugarLevelParam NursingSugarLevelParam);
        public bool UpdateNursingSugarLevel(NursingSugarLevelParam NursingSugarLevelParam);


        public bool SaveDischargeInitiate(DischargeInitiateParam DischargeInitiateParam);

        //public bool UpdateDischargeInitiate(DischargeInitiateParam DischargeInitiateParam);

        public bool SavePatientFeedBack(Parameter PatientFeedbackParameter);
        public bool UpdateDischargeInitiateApproval(DischargeInitiateApprovalParam DischargeInitiateApprovalParam);
      
        public bool SaveNursingWeight(NursingWeightParam NursingWeightParam);

        public bool UpdateNursingWeight(NursingWeightParam NursingWeightParam);
        public bool SaveTDoctorsNotes(TDoctorsNotesParam TDoctorsNotesParam);
        public bool UpdateTDoctorsNotes(TDoctorsNotesParam TDoctorsNotesParam);
        public bool SaveTDoctorPatientHandover(TDoctorPatientHandoverParam TDoctorPatientHandoverParam);
        public bool UpdateTDoctorPatientHandover(TDoctorPatientHandoverParam TDoctorPatientHandoverParam);
    }
}
