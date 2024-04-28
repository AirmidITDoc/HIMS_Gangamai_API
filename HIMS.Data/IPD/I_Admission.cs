using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_Admission
    {
        public String Insert(AdmissionParams AdmissionParams);

        public string Update(AdmissionParams AdmissionParams);

        //public bool BedUpdate(AdmissionParams AdmissionParams);
        

        string AdmissionListCurrent(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        string AdmissionListCurrentHospitaldetail(int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        string AdmissionListCurrentPharmacydetail(int DoctorId, int WardId, string htmlFilePath, string HeaderName);

        string ViewAdmissionPaper( int AdmissionId, string htmlFilePath, string HeaderName);
    }
}
