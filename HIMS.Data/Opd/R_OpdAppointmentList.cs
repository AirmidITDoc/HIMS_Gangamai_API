using HIMS.Common.Extensions;
using HIMS.Model.Opd;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Opd
{
    public class R_OpdAppointmentList: I_OpdAppointmentList
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_OpdAppointmentList(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> GetAppointmentList(AppointmentList appointmentlist)
        {
            command.CommandText = "RetrieveVisitDetailsList";

            command.Parameters.AddWithValue("@F_Name", appointmentlist.F_Name);
            command.Parameters.AddWithValue("@L_Name", appointmentlist.L_Name);
            command.Parameters.AddWithValue("@Reg_No", appointmentlist.Reg_No);
            command.Parameters.AddWithValue("@Doctor_Id", appointmentlist.Doctor_Id);
            command.Parameters.AddWithValue("@From_Dt", appointmentlist.From_Dt);
            command.Parameters.AddWithValue("@To_Dt", appointmentlist.To_Dt);
            command.Parameters.AddWithValue("@IsMark", appointmentlist.IsMark);
            command.Parameters.AddWithValue("@Department", appointmentlist.Department);

            var dataSet = new DataSet();
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();

        }

    }
}
