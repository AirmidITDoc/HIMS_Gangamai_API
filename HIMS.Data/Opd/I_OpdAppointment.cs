using HIMS.Model.Opd;
using System.Collections.Generic;

namespace HIMS.Data.Opd
{
    public interface I_OpdAppointment
    {
        //List<dynamic> GetBrowseOPDBill(BrowseOPDBillParams browseOPDBillParams);
        public string Save(OpdAppointmentParams opdAppointmentParams);

        public string SavewithPhoto(OpdAppointmentParams opdAppointmentParams);
        public bool Update(OpdAppointmentParams opdAppointmentParams);


        string ViewpatientAppointmentReceipt(int VisitId, string htmlFilePath, string htmlHeaderFilePath);
        string ViewOppatientAppointmentdetailsReceipt(int VisitId, string htmlFilePath, string htmlHeaderFilePath);
    }
}