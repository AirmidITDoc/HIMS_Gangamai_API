using HIMS.Model.Pharmacy;

namespace HIMS.Data.Pharmacy
{
    public interface I_MaterialAcceptance
    {
        public bool UpdateMaterialAcceptance(MaterialAcceptParams materialAcceptParams);

        public bool UpdateStockToMainStock(MaterialAcceptParams materialAcceptParams);
        string ViewMaterialReceivedfrDept(int IssueId, string htmlFilePath, string HeaderName);
    }
}
