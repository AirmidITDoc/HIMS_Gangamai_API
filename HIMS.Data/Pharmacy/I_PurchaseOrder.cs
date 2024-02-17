using HIMS.Model.Pharmacy;

namespace HIMS.Data.Pharmacy
{
    public interface I_PurchaseOrder
    {
        public string InsertPurchaseOrder(PurchaseParams purchaseParams);
        public bool UpdatePurchaseOrder(PurchaseParams purchaseParams);
        public bool VerifyPurchaseOrder(PurchaseParams purchaseParams);

        string ViewPurchaseorder(int PurchaseID, string htmlFilePath, string HeaderName);
    }
}
