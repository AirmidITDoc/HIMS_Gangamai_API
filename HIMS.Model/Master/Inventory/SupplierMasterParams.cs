using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{ 
    public class SupplierMasterParams
    {
        public InsertSupplierMaster InsertSupplierMaster { get; set; }
        public UpdateSupplierMaster UpdateSupplierMaster { get; set; }
        public List<InsertAssignSupplierToStore> InsertAssignSupplierToStore { get; set; }
        public DeleteAssignSupplierToStore DeleteAssignSupplierToStore { get; set; }

    }
    public class InsertSupplierMaster
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string CreditPeriod { get; set; }   
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int ModeOfPayment { get; set; }
        public int TermOfPayment { get; set; }
        public int TaxNature { get; set; }
        public int CurrencyId { get; set; }
        public int Octroi { get; set; }
        public int Freight { get; set; }
        public bool IsDeleted { get; set; }
        public int AddedBy { get; set; }
        public string GSTNo { get; set; }
        public string PanNo { get; set; }
    }
    public class UpdateSupplierMaster
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string CreditPeriod { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int ModeOfPayment { get; set; }
        public int TermOfPayment { get; set; }
        public int TaxNature { get; set; }
        public int CurrencyId { get; set; }
        public int Octroi { get; set; }
        public int Freight { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
        public string GSTNo { get; set; }
        public string PanNo { get; set; }
    }
    public class DeleteAssignSupplierToStore
    {
        public long SupplierId { get; set; }
    }
    public class InsertAssignSupplierToStore
    {
        public long StoreId { get; set; }
        public long SupplierId { get; set; }
    }
}
