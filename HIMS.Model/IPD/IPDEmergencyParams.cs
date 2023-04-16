using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public  class IPDEmergencyParams
    {
        public IPDEmergencyRegInsert IPDEmergencyRegInsert { get; set; }
        public IPDEmergencyAdv IPDEmergencyAdv { get; set; }

    }

    public class IPDEmergencyAdv
    {
        public int BedId { get; set; }
    }

    public class IPDEmergencyRegInsert
    {
        public int RegId { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RegTime { get; set; }
        public int PrefixId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public int GenderID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public int AddedBy { get; set; }
        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int MaritalStatusId { get; set; }
        public Boolean IsCharity { get; set; }
        public int ReligionId { get; set; }
        public int AreaId { get; set; }
        public int VillageId { get; set; }
        public int TalukaId { get; set; }
        public float PatientWeight { get; set; }
        public string AreaName { get; set; }
        public string AadharCardNo { get; set; }
        public string PanCardNo { get; set; }
    }
}

