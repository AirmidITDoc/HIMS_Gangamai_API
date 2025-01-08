using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class MOTSurgeryCategoryMasterParam
    {
       public SaveMOTSurgeryCategoryMasterParam SaveMOTSurgeryCategoryMasterParam { get; set; }
       public UpdateMOTSurgeryCategoryMasterParam UpdateMOTSurgeryCategoryMasterParam { get; set; }
       public CancelMOTSurgeryCategoryMasterParam CancelMOTSurgeryCategoryMasterParam { get; set; }
    }
    public class SaveMOTSurgeryCategoryMasterParam
    {
        public string SurGeryCategoryName { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long surgeryCategoryId { get; set; }

    }
    public class UpdateMOTSurgeryCategoryMasterParam
    {
        public long SurgeryCategoryId { get; set; }
        public string SurGeryCategoryName { get; set;}
        public long IsActive { get; set; }
        public long ModifiedBy { get; set; }
    }
    public class CancelMOTSurgeryCategoryMasterParam
    {
        public long SurgeryCategoryId { get; set; }
        public long IsCancelled { get; set; }
        public string IsCancelledBy { get; set; }


    }
}
