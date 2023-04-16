using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HIMS.Common.Extensions;

namespace HIMS.Data.Opd
{
    public class R_Dashboard
    {
        //private readonly IUnitofWork _unitofWork;
        //private readonly SqlCommand command;
        //public R_Dashboard(IUnitofWork unitofWork) : base(unitofWork)
        //{
        //    //transaction and connection
        //    _unitofWork = unitofWork;
        //    command = _unitofWork.CreateCommand();
        //}
        //public List<dynamic> GetDashboard(Dashboard dashboard)
        //{
        //      //SqlCommand command; 
        //    command.CommandText = "Dashboard";
        //    //command.Parameters.AddWithValue("@F_Name", browseOPDBillParams.F_Name);
        //    //command.Parameters.AddWithValue("@L_Name", browseOPDBillParams.L_Name);
        //    //command.Parameters.AddWithValue("@VisitDate", dashboard.widget4.VisitDate);
        //    //command.Parameters.AddWithValue("@To_Dt", browseOPDBillParams.To_Dt);
        //    //command.Parameters.AddWithValue("@Reg_No", browseOPDBillParams.Reg_No);
        //    //command.Parameters.AddWithValue("@PBillNo", browseOPDBillParams.PBillNo);

        //    var dataSet = new DataSet();
        //    (new SqlDataAdapter(command)).Fill(dataSet);
        //    command.Parameters.Clear();

        //    return dataSet.Tables[0].ToDynamic();
        //}
    }
}
