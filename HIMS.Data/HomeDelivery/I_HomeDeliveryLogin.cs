using HIMS.Model.HomeDelivery;

namespace HIMS.Data.HomeDelivery
{
    public interface I_HomeDeliveryLogin
    {
        public string HomeDeliveryLoginInsert(HomeDeliveryLoginParams homeDeliveryLoginParams);
        public bool HomeDeliveryProfileUpdate(HomeDeliveryLoginParams homeDeliveryLoginParams);
    }
}
