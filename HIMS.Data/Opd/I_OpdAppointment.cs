using HIMS.Model.Opd;
using System.Collections.Generic;
using System.Data;

namespace HIMS.Data.Opd
{
    public interface I_OpdAppointment
    {
        //List<dynamic> GetBrowseOPDBill(BrowseOPDBillParams browseOPDBillParams);
        public string Save(OpdAppointmentParams opdAppointmentParams);
        public string SavewithPhoto(OpdAppointmentParams opdAppointmentParams);
        public bool AppointmentCancle(OpdAppointmentParams opdAppointmentParams);
        public string Update(OpdAppointmentParams opdAppointmentParams);
        string ViewpatientAppointmentReceipt(int VisitId, string htmlFilePath, string htmlHeaderFilePath);
        string ViewOppatientAppointmentdetailsReceipt(int VisitId, string htmlFilePath, string htmlHeaderFilePath);
        DataTable GetDataForReport(int VisitId);
        string ViewAppointmentTemplate(DataTable Bills, string htmlFilePath, string HeaderName);
        bool UpdateVitalInformation(OpdAppointmentParams opdAppointmentParams);
    }
}