using HIMS.Data.Master.Inventory;
using HIMS.Model.Master.Inventory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS;
using HIMS.Data.Inventory;
using HIMS.Model.Inventory;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        public readonly I_CurrencyMaster _CurrencyMasterRep;
        public readonly I_ItemCategoryMaster _ItemCategoryMasterRep;
        public readonly I_ItemClassMaster _ItemClassMasterRep;
        public readonly I_ItemGenericMaster _ItemGenericMasterRep;
        public readonly I_ItemTypeMaster _ItemTypeMasterRep;
        public readonly I_ManufactureMaster _ManufactureMasterRep;
        public readonly I_UnitofMeasurementMaster _UnitofMeasurementMasterRep;
        public readonly I_StoreMaster _StoreMasterRep;

        public readonly I_ItemMaster _ItemMaster;
        public readonly I_SupplierMaster _SupplierMasterRep;
        public readonly I_TaxMaster _TaxMasterRep;
        public readonly I_ModeofPaymentMaster _ModeofPaymentMasterRep;
        public readonly I_TermsofPaymentMaster _TermsofPaymentMasterRep;
        public readonly I_StoreMaster _StoreMaster;
        public readonly I_SupplierMaster _SupplierMaster;
        public readonly I_OpeningTransaction _OpeningTransaction;
        public readonly I_Constants _Constants;
        public readonly I_InvOpeningBalance _InvOpeningBalance;
        //public readonly I_SMS_Config _SMS_Config;
        public InventoryController(I_CurrencyMaster currencyMaster,
                                   I_ItemCategoryMaster itemCategoryMaster,
                                   I_ItemClassMaster itemClassMaster,
                                   I_ItemGenericMaster itemGenericMaster,
                                   I_ItemTypeMaster itemTypeMaster,
                                   I_ManufactureMaster manufactureMaster,
                                   I_UnitofMeasurementMaster unitofMeasurementMaster,
                                   I_StoreMaster storeMaster,
                                   I_ItemMaster itemMaster,
                                   I_SupplierMaster supplierMaster,
                                   I_TaxMaster taxMaster,
                                   I_ModeofPaymentMaster mopMaster,
                                   I_TermsofPaymentMaster topMaster, I_IssueTrackingInfo issueTrackingInfo,
                                   I_OpeningTransaction openingTransaction, I_InvOpeningBalance invOpeningBalance,
                                   I_Constants constants)
        //I_SMS_Config sMS_Config)
        {
            this._CurrencyMasterRep = currencyMaster;
            this._ItemCategoryMasterRep = itemCategoryMaster;
            this._ItemClassMasterRep = itemClassMaster;
            this._ItemGenericMasterRep = itemGenericMaster;
            this._ItemTypeMasterRep = itemTypeMaster;
            this._ManufactureMasterRep = manufactureMaster;
            this._UnitofMeasurementMasterRep = unitofMeasurementMaster;
            this._StoreMaster = storeMaster;
            this._ItemMaster = itemMaster;
            this._SupplierMasterRep = supplierMaster;
            this._TaxMasterRep = taxMaster;
            this._ModeofPaymentMasterRep = mopMaster;
            this._TermsofPaymentMasterRep = topMaster;
            this._SupplierMaster = supplierMaster;
            this._OpeningTransaction = openingTransaction;
            this._Constants = constants;
            this._InvOpeningBalance = invOpeningBalance;
            //this._SMS_Config = sMS_Config;
        }

        [HttpPost("CurrencySave")]
        public IActionResult CurrencySave(CurrencyMasterParams currencyMasterParams)
        {
            var CurrencySave = _CurrencyMasterRep.Insert(currencyMasterParams);
            return Ok(CurrencySave);

        }

        [HttpPost("CurrencyUpdate")]
        public IActionResult CurrencyUpdate(CurrencyMasterParams currencyMasterParams)
        {
            var CurrencyUpdate = _CurrencyMasterRep.Update(currencyMasterParams);
            return Ok(CurrencyUpdate);
        }


        [HttpPost("ItemCategorySave")]
        public IActionResult ItemCategorySave(ItemCategoryMasterParams itemCategoryMasterParams)
        {
            var ItemCategorySave = _ItemCategoryMasterRep.Save(itemCategoryMasterParams);
            return Ok(ItemCategorySave);

        }

        [HttpPost("ItemCategoryUpdate")]
        public IActionResult ItemCategoryUpdate(ItemCategoryMasterParams itemCategoryMasterParams)
        {
            var ItemCategoryUpdate = _ItemCategoryMasterRep.Update(itemCategoryMasterParams);
            return Ok(ItemCategoryUpdate);

        }
        //-------------------------------------------------
        [HttpPost("ItemClassSave")]
        public IActionResult ItemClassSave(ItemClassMasterParams itemClassMasterParams)
        {
            var ItemClassSave = _ItemClassMasterRep.Insert(itemClassMasterParams);
            return Ok(ItemClassSave);

        }

        [HttpPost("ItemClassUpdate")]
        public IActionResult ItemClassUpdate(ItemClassMasterParams itemClassMasterParams)
        {
            var ItemClassUpdate = _ItemClassMasterRep.Update(itemClassMasterParams);
            return Ok(ItemClassUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("ItemGenericSave")]
        public IActionResult ItemGenericSave(ItemGenericMasterParams itemGenericMasterParams)
        {
            var ItemGenericSave = _ItemGenericMasterRep.Insert(itemGenericMasterParams);
            return Ok(ItemGenericSave);

        }

        [HttpPost("ItemGenericUpdate")]
        public IActionResult ItemGenericUpdate(ItemGenericMasterParams itemGenericMasterParams)
        {
            var ItemGenericUpdate = _ItemGenericMasterRep.Update(itemGenericMasterParams);
            return Ok(ItemGenericUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("ItemTypeSave")]
        public IActionResult ItemTypeSave(ItemTypeMasterParams itemTypeMasterParams)
        {
            var ItemTypeSave = _ItemTypeMasterRep.Save(itemTypeMasterParams);
            return Ok(ItemTypeSave);

        }

        [HttpPost("ItemTypeUpdate")]
        public IActionResult ItemTypeUpdate(ItemTypeMasterParams itemTypeMasterParams)
        {
            var ItemTypeUpdate = _ItemTypeMasterRep.Update(itemTypeMasterParams);
            return Ok(ItemTypeUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("ManufactureSave")]
        public IActionResult ManufactureSave(ManufactureMasterParams manufMasterParams)
        {
            var ManufactureSave = _ManufactureMasterRep.Insert(manufMasterParams);
            return Ok(ManufactureSave);

        }

        [HttpPost("ManufactureUpdate")]
        public IActionResult ManufactureUpdate(ManufactureMasterParams manufMasterParams)
        {
            var ManufactureUpdate = _ManufactureMasterRep.Update(manufMasterParams);
            return Ok(ManufactureUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("UnitofMeasurementSave")]
        public IActionResult UnitofMeasurementSave(UnitofMeasurementMasterParams uomMasterParams)
        {
            var UOMSave = _UnitofMeasurementMasterRep.Insert(uomMasterParams);
            return Ok(UOMSave);

        }

        [HttpPost("UnitofMeasurementUpdate")]
        public IActionResult UnitofMeasurementUpdate(UnitofMeasurementMasterParams uomMasterParams)
        {
            var UOMUpdate = _UnitofMeasurementMasterRep.Update(uomMasterParams);
            return Ok(UOMUpdate);

        }
        //-------------------------------------------------------
        //[HttpPost("StoreSave")]
        //public IActionResult StoreSave(StoreMasterParams storeMasterParams)
        //{
        //    var StoreSave = _StoreMaster.Insert(storeMasterParams);
        //    return Ok(StoreSave);

        //}

        //[HttpPost("StoreUpdate")]
        //public IActionResult StoreUpdate(StoreMasterParams storeMasterParams)
        //{
        //    var StoreUpdate = _StoreMaster.Update(storeMasterParams);
        //    return Ok(StoreUpdate);

        //}
        //-------------------------------------------------------
        [HttpPost("ItemMasterSave")]
        public IActionResult ItemMasterSave(ItemMasterParams ItemMasterParams)
        {
            var ItemSave = _ItemMaster.Insert(ItemMasterParams);
            return Ok(ItemSave);

        }

        [HttpPost("ItemMasterUpdate")]
        public IActionResult ItemMasterUpdate(ItemMasterParams ItemMasterParams)
        {
            var ItemUpdate = _ItemMaster.Update(ItemMasterParams);
            return Ok(ItemUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("SupplierSave")]
        public IActionResult SupplierSave(SupplierMasterParams SupplierMasterParams)
        {
            var SupplierSave = _SupplierMaster.Insert(SupplierMasterParams);
            return Ok(SupplierSave);

        }

        [HttpPost("SupplierUpdate")]
        public IActionResult SupplierUpdate(SupplierMasterParams SupplierMasterParams)
        {
            var SupplierUpdate = _SupplierMaster.Update(SupplierMasterParams);
            return Ok(SupplierUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("TaxSave")]
        public IActionResult TaxSave(TaxMasterParams taxMasterParams)
        {
            var TaxSave = _TaxMasterRep.Insert(taxMasterParams);
            return Ok(TaxSave);

        }

        [HttpPost("TaxUpdate")]
        public IActionResult TaxUpdate(TaxMasterParams taxMasterParams)
        {
            var TaxUpdate = _TaxMasterRep.Update(taxMasterParams);
            return Ok(TaxUpdate);

        }
        //-------------------------------------------------------
        [HttpPost("ModeofPaymentSave")]
        public IActionResult ModeofPaymentSave(ModeofPaymentMasterParams mopMasterParams)
        {
            var MODSave = _ModeofPaymentMasterRep.Insert(mopMasterParams);
            return Ok(MODSave);

        }

        [HttpPost("ModeofPaymentUpdate")]
        public IActionResult ModeofPaymentUpdate(ModeofPaymentMasterParams mopMasterParams)
        {
            var MODUpdate = _ModeofPaymentMasterRep.Update(mopMasterParams);
            return Ok(MODUpdate);

        }
        //-------------------------------------------------------   
        [HttpPost("TermsofPaymentSave")]
        public IActionResult TermsofPaymentSave(TermsofPaymentMasterParams topMasterParams)
        {
            var TODSave = _TermsofPaymentMasterRep.Insert(topMasterParams);
            return Ok(TODSave);

        }

        [HttpPost("TermsofPaymentUpdate")]
        public IActionResult TermsofPaymentUpdate(TermsofPaymentMasterParams topMasterParams)
        {
            var TODUpdate = _TermsofPaymentMasterRep.Update(topMasterParams);
            return Ok(TODUpdate);

        }
        [HttpPost("StoreMasterSave")]
        public IActionResult StoreMasterSave(StoreMasterParams storeMasterParams)
        {
            var TODSave = _StoreMaster.Insert(storeMasterParams);
            return Ok(TODSave);

        }
        [HttpPost("StoreMasterUpdate")]
        public IActionResult StoreMasterUpdate(StoreMasterParams storeMasterParams)
        {
            var TODUpdate = _StoreMaster.Update(storeMasterParams);
            return Ok(TODUpdate);

        }
        [HttpPost("OpeningTransactionSave")]
        public IActionResult OpeningTransactionSave(OpeningTransactionParam OpeningTransactionParam)
        {
            var TODSave = _OpeningTransaction.Insert(OpeningTransactionParam);
            return Ok(TODSave);

        }
        [HttpPost("Constantssave")]
        public IActionResult Constantssave(ConstantsParam ConstantsParam)
        {
            var TODSave = _Constants.InsertConstantsParam(ConstantsParam);
            return Ok(TODSave);

        }
        [HttpPost("ConstantsUpdate")]
        public IActionResult ConstantsUpdate(ConstantsParam ConstantsParam)
        {
            var TODUpdate = _Constants.UpdateConstantsParam(ConstantsParam);
            return Ok(TODUpdate);

        }


        [HttpPost("Invopeningbalance")]
        public IActionResult Invopeningbalance(OpeningBalanceParam OpeningBalanceParam)
        {
            var TODUpdate = _InvOpeningBalance.OpeningBalanceParamInsert(OpeningBalanceParam);
            return Ok(TODUpdate);

        }
        ////[HttpPost("SMS_Configsave")]
        ////public IActionResult SMS_Configsave(SMS_ConfigParam SMS_ConfigParam)
        ////{
        ////    var TODSave = _SMS_Config.InsertSMSConfig(SMS_ConfigParam);
        ////    return Ok(TODSave);

        ////}
        ////[HttpPost("SMS_ConfigUpdate")]
        ////public IActionResult SMS_ConfigUpdate(SMS_ConfigParam SMS_ConfigParam)  
        ////{
        ////    var TODUpdate = _SMS_Config.UpdateSMSConfigParam(SMS_ConfigParam);
        ////    return Ok(TODUpdate);

        ////}
    }


}


