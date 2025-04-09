using HIMS.Model.Pharmacy;

namespace HIMS.Data.Pharmacy
{
    public interface I_GRN
    {

        public string InsertGRNDirectNEW(GRNParamsNEW grnParams);

        public bool UpdateGRNNEW(GRNParamsNEW grnParams);

        public string InsertGRNDirect(GRNParams grnParams);

        public bool UpdateGRN(GRNParams grnParams);

        public string InsertGRNPurchase(GRNParams grnParams);

        public bool UpdateGRNPurchase(GRNParams grnParams);

        public bool VerifyGRN(GRNParams grnParams);

        string ViewGRNReport(int GRNID, string htmlFilePath, string HeaderName);
    }
}
