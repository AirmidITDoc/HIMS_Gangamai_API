using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Numerics;
namespace HIMS.Model.Master.DoctorMaster
{
    public class DoctorMaster
    {
        public long DoctorId { get; set; }
        public long PrefixID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int GenderId { get; set; }
        public string Education { get; set; }
        public bool IsConsultant { get; set; }
        public bool IsRefDoc { get; set; }
        public bool IsActive { get; set; }
        public int DoctorTypeId { get; set; }
        public string PassportNo { get; set; }
        public string ESINO { get; set; }
        public string RegNo { get; set; }
        //public DateTime RegDate { get; set; }
        public string MahRegNo { get; set; }
        //public DateTime MahRegDate { get; set; }
        public int Addedby { get; set; }
        public int UpdatedBy { get; set; }
       // public string RefDocHospitalName { get; set; }
        public bool IsInHouseDoctor { get; set; }
        public bool IsOnCallDoctor { get; set; }
        public string PanCardNo { get; set; }
        public string AadharCardNo { get; set; }
        public string Signature { get; set; }
        public List<DoctorDepartmentDet> Departments { get; set; }
    }
    public class DoctorMasterParams
    {
        public InsertDoctorMaster InsertDoctorMaster { get; set; }
        public UpdateDoctorMaster UpdateDoctorMaster { get; set; }
        public DeleteAssignDoctorToDepartment DeleteAssignDoctorToDepartment { get; set; }
        public List<DoctorDepartmentDet> AssignDoctorDepartmentDet { get; set; }
    }
    public class InsertDoctorMaster
    {
        public long PrefixID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int GenderID { get; set; }
        public string Education { get; set; }
        public Boolean IsConsultant { get; set; }
        public Boolean IsRefDoc { get; set; }
        public Boolean IsActive { get; set; }
        public long DoctorTypeId { get; set; }
        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public string PassportNo { get; set; }
        public string ESINO { get; set; }
        public string PanCardNo { get; set; }
        public string AadharCardNo { get; set; }
        public string RegNo { get; set; }

        //public DateTime RegDate { get; set; }
        public string MahRegNo { get; set; }
        //public DateTime MahRegDate { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }
        public Boolean IsInHouseDoctor { get; set; }
        public Boolean IsOnCallDoctor { get; set; }
        public long DoctorId { get; set; }
        public string Signature { get; set; }
    }

    public class UpdateDoctorMaster
    {
        public long DoctorId { get; set; }
        public long PrefixID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int GenderID { get; set; }
        public string Education { get; set; }
        public Boolean IsConsultant { get; set; }
        public Boolean IsRefDoc { get; set; }
        public Boolean IsActive { get; set; }
        public long DoctorTypeId { get; set; }
        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public string PassportNo { get; set; }
        public string ESINO { get; set; }
        public string RegNo { get; set; }
        public string PanCardNo { get; set; }
        public string AadharCardNo { get; set; }

        //public DateTime RegDate { get; set; }
        public string MahRegNo { get; set; }
        //public DateTime MahRegDate { get; set; }
        public long UpdatedBy { get; set; }
        public Boolean IsInHouseDoctor { get; set; }
        public Boolean IsOnCallDoctor { get; set; }

    }
    public class DoctorDepartmentDet
    {

        public long DoctorId { get; set; }
        public long DepartmentId { get; set; }
    }

    public class DeleteAssignDoctorToDepartment
    {
        public long DoctorId { get; set; }
    }


}

