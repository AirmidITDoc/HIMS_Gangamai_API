using HIMS.Data.Master.PersonalDetails;
using HIMS.Model.Master.PersonalDetails;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalDetailsController : Controller
    {
       /* public IActionResult Index()
        {
            return View();
        }*/

        public readonly I_PrefixMaster _PrefixMaster ;
        public readonly I_GenderMaster _GenderMaster;
        public readonly I_RelationshipMaster _RelationshipMaster;
        public readonly I_PatientTypeMaster _PatientTypeMaster;
        public readonly I_MaritalStatusMaster _MaritalStatusMaster;
        public readonly I_ReligionMaster _ReligionMaster;
        public readonly I_CountryMaster _CountryMaster;
        public readonly I_StateMaster _StateMaster;
        public readonly I_TalukaMaster _TalukaMaster;
        public readonly I_VillageMaster _VillageMaster;
        public readonly I_AreaMaster _AreaMaster;
        public readonly I_CityMaster _CityMaster;
        // public readonly I_GenderMaster _GenderMaster;

        public PersonalDetailsController(I_PrefixMaster prefixMaster, I_GenderMaster genderMaster,
            I_RelationshipMaster relationshipMaster,I_PatientTypeMaster patientTypeMaster
            ,I_MaritalStatusMaster maritalStatusMaster,I_ReligionMaster religionMaster,
            I_CountryMaster countryMaster,I_StateMaster stateMaster,I_TalukaMaster talukaMaster,
            I_VillageMaster villageMaster,I_AreaMaster areaMaster,I_CityMaster cityMaster
            )
        {
            this._PrefixMaster = prefixMaster;
            this._GenderMaster = genderMaster;
            this._RelationshipMaster = relationshipMaster;
            this._PatientTypeMaster = patientTypeMaster;
            this._MaritalStatusMaster = maritalStatusMaster;
            this._ReligionMaster = religionMaster;
            this._CountryMaster = countryMaster;
            this._StateMaster = stateMaster;
            this._TalukaMaster = talukaMaster;
            this._VillageMaster = villageMaster;
            this._AreaMaster = areaMaster;
            this._CityMaster = cityMaster;
        }


        //Prefix Save and Update
        [HttpPost("PrefixSave")]
        public IActionResult PrefixSave(PrefixMasterParams PrefixMasterParams)
        {
            var ServiceSave = _PrefixMaster.Save(PrefixMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("PrefixUpdate")]
        public IActionResult PrefixUpdate(PrefixMasterParams PrefixMasterParams)
        {
            var ServiceSave = _PrefixMaster.Update(PrefixMasterParams);
            return Ok(ServiceSave);
        }
        //Gender Save and Update
        [HttpPost("GenderSave")]
        public IActionResult GenderSave(GenderMasterParams GenderMasterParams)
        {

            var ServiceSave = _GenderMaster.Save(GenderMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("GenderUpdate")]
        public IActionResult GenderMasterUpdate(GenderMasterParams GenderMasterParams)
        {
            I_GenderMaster _GenderMaster1 = _GenderMaster;
            var ServiceSave = _GenderMaster1.Update(GenderMasterParams);
            return Ok(ServiceSave);
        }
        //PatientType Save and Update

        [HttpPost("PatientTypeSave")]
        public IActionResult PatientTypeSave(PatientTypeMasterParams PatientTypeMasterParams)
        {

            var ServiceSave = _PatientTypeMaster.Save(PatientTypeMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("PatientTypeUpdate")]
        public IActionResult PatientTypeMasterUpdate(PatientTypeMasterParams PatientTypeMasterParams)
        {
            
            var ServiceSave = _PatientTypeMaster.Update(PatientTypeMasterParams);
            return Ok(ServiceSave);
        }
        //Relationship Save and update
        [HttpPost("RelationshipSave")]
        public IActionResult RelationshipSave(RelationshipMasterParams RelationshipMasterParams)
        {

            var ServiceSave = _RelationshipMaster.Save(RelationshipMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("RelationshipUpdate")]
        public IActionResult RelationshipUpdate(RelationshipMasterParams RelationshipMasterParams)
        {
            
            var ServiceSave = _RelationshipMaster.Update(RelationshipMasterParams);
            return Ok(ServiceSave);
        }
        //MaritalStatus Save and update
        [HttpPost("MaritalStatusSave")]
        public IActionResult MaritalStatusSave(MaritalStatusMasterParams MaritalStatusMasterParams)
        {

            var ServiceSave = _MaritalStatusMaster.Save(MaritalStatusMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("MaritalStatusUpdate")]
        public IActionResult MaritalStatusUpdate(MaritalStatusMasterParams MaritalStatusMasterParams)
        {

            var ServiceSave = _MaritalStatusMaster.Update(MaritalStatusMasterParams);
            return Ok(ServiceSave);
        }
        //Religion Save and update
        [HttpPost("ReligionMasterSave")]
        public IActionResult ReligionMasterSave(ReligionMasterParams ReligionMasterParams)
        {

            var ServiceSave = _ReligionMaster.Save(ReligionMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("ReligionMasterUpdate")]
        public IActionResult ReligionMasterUpdate(ReligionMasterParams ReligionMasterParams)
        {

            var ServiceSave = _ReligionMaster.Update(ReligionMasterParams);
            return Ok(ServiceSave);
        }
        //Country Save and update
        [HttpPost("CountrySave")]
        public IActionResult CountrySave(CountryMasterParams CountryMasterParams)
        {

            var ServiceSave = _CountryMaster.Save(CountryMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CountryUpdate")]
        public IActionResult CountryUpdate(CountryMasterParams CountryMasterParams)
        {

            var ServiceSave = _CountryMaster.Update(CountryMasterParams);
            return Ok(ServiceSave);
        }
        //State Save and update
        [HttpPost("StateSave")]
        public IActionResult StateSave(StateMasterParams StateMasterParams)
        {

            var ServiceSave = _StateMaster.Save(StateMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("StateUpdate")]
        public IActionResult StateUpdate(StateMasterParams StateMasterParams)
        {

            var ServiceSave = _StateMaster.Update(StateMasterParams);
            return Ok(ServiceSave);
        }
        //City Save and update
        [HttpPost("CitySave")]
        public IActionResult CitySave(CityMasterParams CityMasterParams)
        {

            var ServiceSave = _CityMaster.Save(CityMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CityUpdate")]
        public IActionResult CityUpdate(CityMasterParams CityMasterParams)
        {

            var ServiceSave = _CityMaster.Update(CityMasterParams);
            return Ok(ServiceSave);
        }
        //Taluka Save and update
        [HttpPost("TalukaSave")]
        public IActionResult TalukaSave(TalukaMasterParams TalukaMasterParams)
        {

            var ServiceSave = _TalukaMaster.Save(TalukaMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("TalukaUpdate")]
        public IActionResult TalukaUpdate(TalukaMasterParams TalukaMasterParams)
        {

            var ServiceSave = _TalukaMaster.Update(TalukaMasterParams);
            return Ok(ServiceSave);
        }
        //Village Save and update
        [HttpPost("VillageSave")]
        public IActionResult VillageSave(VillageMasterParams VillageMasterParams)
        {

            var ServiceSave = _VillageMaster.Save(VillageMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("VillageUpdate")]
        public IActionResult VillageUpdate(VillageMasterParams VillageMasterParams)
        {

            var ServiceSave = _VillageMaster.Update(VillageMasterParams);
            return Ok(ServiceSave);
        }
        //AreaMaster Save and update
        [HttpPost("AreaSave")]
        public IActionResult AreaSave(AreaMasterParams AreaMasterParams)
        {

            var ServiceSave = _AreaMaster.Save(AreaMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("AreaUpdate")]
        public IActionResult AreaUpdate(AreaMasterParams AreaMasterParams)
        {

            var ServiceSave = _AreaMaster.Update(AreaMasterParams);
            return Ok(ServiceSave);
        }
    }
}
