﻿using Microsoft.AspNetCore.Http;
using System;

namespace HIMS.Model.Opd
{
    public class OpdAppointmentParams
    {
        public RegistrationSave RegistrationSave { get; set; }
        public RegistrationSavewithPhoto RegistrationSavewithPhoto { get; set; }
        public VisitSave VisitSave { get; set; }
        public Appointmentcancle Appointmentcancle { get; set; }
        public TokenNumberWithDoctorWiseSave TokenNumberWithDoctorWiseSave { get; set; }
        public RegistrationUpdate RegistrationUpdate { get; set; }
        public UpdateVitalInformation UpdateVitalInformation { get; set; }
    }


    public class VisitSave
    {
        public long VisitID { get; set; }
        public long RegId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }
        public long UnitId { get; set; }
        public long PatientTypeId { get; set; }
        public long ConsultantDocId { get; set; }
        public long RefDocId { get; set; }
        public long TariffId { get; set; }
        public long CompanyId { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public long ClassId { get; set; }
        public long DepartmentId { get; set; }
        public long PatientOldNew { get; set; }
        public int FirstFollowupVisit { get; set; }
        public long AppPurposeId { get; set; }
        public DateTime FollowupDate { get; set; }
        public int CrossConsulFlag { get; set; }
        public long PhoneAppId { get; set; }
    }

    public class RegistrationSave
    {
        public long RegID { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RegTime { get; set; }
        public long PrefixId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public long GenderID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }

        public long AddedBy { get; set; }
        //public long UpdatedBy { get; set; }

        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long MaritalStatusId { get; set; }

        public int ReligionId { get; set; }
        public bool IsCharity { get; set; }
        //  public string RegPrefix { get; set; }
        public long AreaId { get; set; }
        //  public long VillageId { get; set; }
        public bool IsSeniorCitizen { get; set; }

        //public long TalukaId { get; set; }
        // public double PatientWeight { get; set; }
        public string AadharCardNo { get; set; }
        public string PanCardNo { get; set; }
        // public String ImgFile { get; set; }
        public string Photo { get; set; }

    }


    public class RegistrationSavewithPhoto
    {
        public long RegID { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RegTime { get; set; }
        public long PrefixId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public long GenderID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }

        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }

        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long MaritalStatusId { get; set; }

        public int ReligionId { get; set; }
        public bool IsCharity { get; set; }
        //  public string RegPrefix { get; set; }
        public long AreaId { get; set; }
        //  public long VillageId { get; set; }
        public bool IsSeniorCitizen { get; set; }

        //public long TalukaId { get; set; }
        // public double PatientWeight { get; set; }
        public string Aadharcardno { get; set; }
        public string Pancardno { get; set; }
        public IFormFile ImgFile { get; set; }
        public string Photo { get; set; }

    }
    public class TokenNumberWithDoctorWiseSave
    {
        public long PatVisitID { get; set; }
    }

    public class RegistrationUpdate
    {
        public long RegId { get; set; }
        public long PrefixId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public long GenderID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }

        // public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }

        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long MaritalStatusId { get; set; }

        // public int ReligionId { get; set; }
        public bool IsCharity { get; set; }
        //  public string RegPrefix { get; set; }
        // public long AreaId { get; set; }
        //  public long VillageId { get; set; }
        //  public bool IsSeniorCitizen { get; set; }

        //public long TalukaId { get; set; }
        // public double PatientWeight { get; set; }
        public string AadharCardNo { get; set; }
        public string PanCardNo { get; set; }
        // public String ImgFile { get; set; }
        public string Photo { get; set; }

    }

    public class Appointmentcancle{

        public long VisitId { get; set; }
    }

    public class UpdateVitalInformation
    {
        public long VisitId { get; set; }
        public string Height { get; set; }
        public string PWeight { get; set; }
        public string BMI { get; set; }
        public string BSL { get; set; }
        public string SpO2 { get; set; }
        public string Temp { get; set; }
        public string Pulse { get; set; }
        public string BP { get; set; }
    }

}