using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_Admission
    {
        public String AdmissionNewInsert(AdmissionParams AdmissionParams);

        public string AdmissionRegistredInsert(AdmissionParams AdmissionParams);


        public bool AdmissionUpdate(AdmissionParams AdmissionParams);

        //public bool BedUpdate(AdmissionParams AdmissionParams);


        string AdmissionListCurrent(int DoctorId, int WardId,int CompanyId, string htmlFilePath, string HeaderName);

        string AdmissionListCurrentHospitaldetail(int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        string AdmissionListCurrentPharmacydetail(int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        string ViewAdmissionPaper( int AdmissionId, string htmlFilePath, string HeaderName);
    }
}
